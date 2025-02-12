import json
import os
import shutil
import subprocess
import sys
import pymysql
import time
import zipfile
from settings import Settings
from tqdm import tqdm


class Restore:

    def __init__(self):
        self.settings = Settings()

    def unzip_file_to_folder(self, zipFile, targetFolder):
        size = 0
        with zipfile.ZipFile(zipFile, 'r') as zipf:
            for file in zipf.namelist():
                if file.endswith('/'):
                    continue
                size += zipf.getinfo(file).file_size
            with tqdm(total=size, unit='B', ncols=70, unit_scale=True) as progress_bar:
                for file in zipf.namelist():
                    if file.endswith('/'):
                        continue
                    extract_path = Settings.join_path(targetFolder, file)
                    with zipf.open(file, 'r') as zf, open(extract_path, 'wb') as outfile:
                        block_size = 10 * 1024 * 1024  # Tamaño del bloque para leer y escribir
                        while True:
                            data = zf.read(block_size)
                            if not data:
                                break
                            outfile.write(data)
                            progress_bar.update(len(data))

    def run_mysql_zip(self, file):
        # expande
        temp_dir = Settings.join_path(self.settings.input_path, "extract")
        if os.path.exists(temp_dir):
            shutil.rmtree(temp_dir)
        os.makedirs(temp_dir)

        self.unzip_file_to_folder(file, temp_dir)

        # with zipfile.ZipFile(file, 'r') as zip_ref:
        #    zip_ref.extractall(temp_dir)

        # procesa uno por uno
        for root, _, files in os.walk(temp_dir):
            sorted_files = sorted(files)
            for file in sorted_files:
                file_path = os.path.join(temp_dir, file)
                print('cleaning ' + file_path)
                self.clean_maria_db(file_path)
                print('running ' + file_path)
                self.run_mysql(file_path)

        # elimina temporarios
        shutil.rmtree(temp_dir)

    def clean_maria_db(self, file):
        # Leer el contenido del archivo
        with open(file, 'r', encoding='utf-8') as f:
            content = f.readlines()

        # Filtrar la primera línea si contiene el comando de sandbox
        if content and "/*!999999\\- enable the sandbox mode */" in content[0]:
            content = content[1:]
            # Escribir el contenido filtrado de vuelta al archivo
            with open(file, 'w', encoding='utf-8') as f:
                f.writelines(content)

    def run_mysql_native(self, file):
        ini = f"--password={self.settings.db_pass}"
        if self.settings.has_ini:
            ini = f"--defaults-file={Settings.CONFIG_FILE}"

        command = f"\"{self.settings.mysql}\" {ini} --user={self.settings.db_user} --host={self.settings.db_host} " \
            f"--port={self.settings.db_port} {self.settings.db_name} < \"{file}\""

        proc = subprocess.run(command, shell=True)
        if proc.returncode != 0:
            print(f"mysql command failed with exit code: {proc.returncode}")
            print(f"\nCommand:\n{command}")
            sys.exit("Restore operation failed.")

    def run_mysql(self, file):
        try:
            # Conexión a la base de datos
            connection = pymysql.connect(
                host=self.settings.db_host,
                user=self.settings.db_user,
                password=self.settings.db_pass,
                database=self.settings.db_name,
                port=self.settings.db_port,
                charset='utf8mb4',
                autocommit=True,
                cursorclass=pymysql.cursors.DictCursor
            )

            with connection.cursor() as cursor:
                # Leer el archivo SQL completo
                with open(file, 'r', encoding='utf-8') as f:
                    sql_script = f.read()

                # Manejo de cambios de delimitadores
                statements = self._split_sql_statements(sql_script)

                # Ejecutar cada sentencia
                for statement in statements:
                    statement = statement.strip()
                    if statement:  # Evitar sentencias vacías
                        cursor.execute(statement)

        except pymysql.MySQLError as e:
            print(f"Error al ejecutar el script: {e}")
            sys.exit("Restore operation failed.")

        finally:
            connection.close()

    def _split_sql_statements(self, script):
        """
        Divide el script SQL en sentencias individuales teniendo en cuenta los cambios de delimitadores.
        """
        delimiter = ';'  # Delimitador por defecto
        statements = []
        current_statement = []

        # Procesar el script línea por línea
        for line in script.splitlines():
            line = line.strip()

            if line.startswith('DELIMITER'):
                # Cambiar el delimitador
                delimiter = line.split()[1]
                continue  # Saltar a la siguiente línea

            # Añadir la línea al statement actual
            current_statement.append(line)

            # Si encontramos el delimitador, cerramos la sentencia
            if line.endswith(delimiter):
                statements.append('\n'.join(current_statement).replace(delimiter, ';'))
                current_statement = []

        # Agregar la última sentencia si no quedó vacía
        if current_statement:
            statements.append('\n'.join(current_statement))

        return statements

    def restore_routines(self):
        routines = Settings.join_path(self.settings.input_path, "routines.zip")
        if os.path.exists(routines):
            print('Restoring routines...')
            self.run_mysql_zip(routines)
            if self.settings.move_done:
                shutil.move(routines, Settings.join_path(self.settings.done_path, "routines.zip"))

    def restore_tables(self):
        print('Restoring tables...')

        with open(Settings.join_path(self.settings.input_path, "tables.json"), 'r',  encoding='utf-8') as file:
            sizes = json.load(file)

        total = 0
        for table in sizes:
            file = Settings.join_path(self.settings.tables_path, table + ".zip")
            if os.path.isfile(file):
                sizes[table]['file'] = file
                total += sizes[table]['rows']

        with tqdm(total=total, ncols=70) as progress_bar:
            for table in sizes:
                if 'file' in sizes[table]:
                    self.run_mysql_zip(sizes[table]['file'])
                    if self.settings.move_done:
                        done_path = Settings.join_path(self.settings.done_path, 'tables')
                        shutil.move(sizes[table]['file'], Settings.join_path(done_path, table + ".zip"))
                    progress_bar.update(sizes[table]['rows'])

    def create_restore_path(self):
        if os.path.isdir(self.settings.input_path):
            shutil.rmtree(self.settings.input_path)

        print(f"Creating {self.settings.input_path}")
        os.makedirs(self.settings.input_path, exist_ok=True)
        os.makedirs(self.settings.tables_path, exist_ok=True)

    def ensure_done_path_exists(self):
        done_path_tables = Settings.join_path(self.settings.done_path, 'tables')
        if not os.path.exists(done_path_tables):
            print(f"Creating {self.settings.done_path}")
            os.makedirs(done_path_tables, exist_ok=True)

    def remove_restore_path(self):
        print(f"Removing {self.settings.input_path}")
        shutil.rmtree(self.settings.input_path)

    def unzip_backup(self):
        print('Uncompressing...')
        self.unzip_file_to_folder(self.settings.input, self.settings.input_path)

    def main(self):
        self.settings.parse_config_file()
        self.settings.parse_command_line_restore()

        print("-------------------------------")
        print("DATABASE: " + self.settings.db_name)
        print("USER: " + self.settings.db_user)
        print("HOST: " + self.settings.db_host)
        print("-------------------------------")

        start = time.time()

        if self.settings.input:
            self.create_restore_path()
            self.unzip_backup()

        if self.settings.move_done:
            self.ensure_done_path_exists()

        if not self.settings.skip_routines:
            self.restore_routines()

        self.restore_tables()

        if self.settings.input:
            while True:
                res = input("¿Do you wish to delete the temp folder? Y/n: ").lower()
                if res == "n":
                    break
                else:  # default Y
                    self.remove_restore_path()
                    break

        print("--- Total time: %s sec. ---" % round(time.time() - start, 2))
