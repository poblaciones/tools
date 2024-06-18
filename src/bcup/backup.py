import glob
import json
import os
import pymysql
import re
import shutil
import subprocess
import sys
import time
import zipfile
from pathlib import Path
from settings import Settings
from tqdm import tqdm


class Backup:

    def __init__(self):
        self.settings = Settings()

    def run_mysqldump(self, options, output_file, table_list=''):
        command = f"\"{self.settings.mysqldump}\" --defaults-file={Settings.CONFIG_FILE} --user={self.settings.db_user} --host={self.settings.db_host} " \
                f"--port={self.settings.db_port} {options} {self.settings.db_name} {table_list}"

        with open(output_file, 'w', encoding='utf-8') as f:
            proc = subprocess.run(command, shell=True, stdout=f)

        if proc.returncode != 0:
            print(f"El comando mysqldump.exe falló con el código de retorno: {proc.returncode}")
            print(f"\nComando:\n{command}")
            sys.exit("No se pudo completar con éxito el backup.")

    def remove_trigger_definer(file_path):
        if not os.path.isfile(file_path):
            return

        with open(file_path, 'r', encoding='utf-8') as file:
            content = file.read()

        new_content = re.sub(r'/\*![0-9]+ DEFINER=`[^`]+`@`[^`]+`\*/', '', content)

        with open(file_path, 'w', encoding='utf-8') as file:
            file.write(new_content)

    def remove_function_definer(file_path):
        if not os.path.isfile(file_path):
            return

        with open(file_path, 'r', encoding='utf-8') as file:
            lines = file.readlines()

        with open(file_path, 'w', encoding='utf-8') as file:
            for line in lines:
                if 'CREATE DEFINER=' in line:
                    new_line = re.sub(r'DEFINER=`[^`]+`@`[^`]+`', '', line)
                    file.write(new_line)
                else:
                    file.write(line)

    def print(self, text):
        if not self.settings.quiet:
            print(text)

    def dump_database_structure(self):
        self.print('Exportando tablas y triggers...')
        tables = self.get_tables(self.settings.estructura_path)
        with tqdm(total=len(tables), ncols=70, disable=self.settings.quiet) as progress_bar:
            for table in tables:
                file = Settings.join_path(self.settings.estructura_path, f"{table}.sql")
                self.run_mysqldump("--no-data --no-create-db", file, table)
                self.remove_trigger_definer(file)
                progress_bar.update()

        if not self.settings.skip_routines:
            # start = time.time()
            self.print('Exportando funciones...')
            self.run_mysqldump("--no-data --no-create-db --routines --skip-triggers --no-create-info ", Settings.join_path(self.settings.output_path, "routines.sql"))
            self.remove_function_definer(Settings.join_path(self.settings.output_path, "routines.sql"))
            # self.print("--- %s seg. ---" % round(time.time() - start, 2))

    def dump_data(self):
        tables = self.get_tables(self.settings.datos_path)
        total_row_count, sizes = self.get_total_row_count(tables)

        with open(Settings.join_path(self.settings.output_path, "tables.json"), "w", encoding='utf-8') as outfile:
            json.dump(sizes, outfile, indent=2)

        with tqdm(total=total_row_count, ncols=70, disable=self.settings.quiet) as progress_bar:
            for table in tables:
                self.dump_table_data(table, sizes[table], progress_bar)

    def dump_table_data(self, table, sizes, progress_bar):
        for offset in range(0, sizes['rows'], sizes['step']):
            file = self.resolve_filename(table, (offset // sizes['step']) + 1)
            self.run_mysqldump(f"--no-create-info --hex-blob --skip-triggers --where=\"1 LIMIT {sizes['step']} OFFSET {offset}\"", file, table)
            progress_bar.update(min(sizes['step'], sizes['rows'] - offset))

    def resolve_filename(self, table_name, step):
        number = str(step).zfill(4)
        return Settings.join_path(self.settings.datos_path, f'{table_name}_#{number}.sql')

    def get_connection(self):
        return pymysql.connect(user=self.settings.db_user, password=self.settings.db_pass, host=self.settings.db_host,
                               port=self.settings.db_port, database=self.settings.db_name)

    def get_tables(self, resume_path):
        """ Obtiene todas las tablas que hay que backupear. Filtrando por fecha y filtros de exclusión e inclusión.
            Si es resume quita las tablas que ya existen salvo la última que la vuelve a regenerar por si el backup
            anterior se cortó por la mitad.  """
        cnx = self.get_connection()
        cursor = cnx.cursor()
        sql = """SELECT table_name FROM information_schema.TABLES
            WHERE table_type = 'BASE TABLE' AND table_schema = %s AND (create_time > %s OR update_time > %s)
            ORDER BY table_name"""
        cursor.execute(sql, (self.settings.db_name, self.settings.from_date, self.settings.from_date))
        tables = cursor.fetchall()
        cnx.close()
        ret = []
        for table in tables:
            if self.filter_tables(table[0]):
                ret.append(table[0])

        if self.settings.resume:
            # obtiene los archivos de estructura existentes ordenados por fecha
            files = sorted(Path(resume_path).iterdir())
            if files:
                # quita el último modificado de los archivos existentes (para que lo regenere) y lo mueve
                # arriba de todo en la lista de tablas para que sea el primero y continúe desde ahí
                last = files.pop()
                self.clean_file(str(last))
                tables.remove(last.stem)  # stem es nombre de archivo solo sin extensión ni directorios
                tables.insert(0, last.stem)
                for file in files:
                    tables.remove(file.stem)

        return ret

    def clean_file(file):
        """ Vacía el archivo sin borrarlo para que el resume pise el archivo, si lo
            borrara y vuelve a fallar se van perdiendo archivos en cada resume, si el
            archivo es de datos (nombre con _#000x) vacía todos los de la serie
        """
        if "_#" in file:
            replaced = re.sub(r"(_#)\d+(\.sql)", r"\1*\2", file)
            files = glob.glob(replaced)
        else:
            files = [file]
        for f in files:
            open(f, 'w').close()

    def create_backup_path(self):
        if os.path.isdir(self.settings.output_path):
            self.remove_backup_path()
        os.makedirs(self.settings.datos_path, exist_ok=True)
        os.makedirs(self.settings.estructura_path, exist_ok=True)

    def remove_backup_path(self):
        shutil.rmtree(self.settings.output_path)

    # public static
    def zip_full_path(self, path, zip_file, quiet=False, level=1):
        if os.path.isfile(zip_file):
            os.remove(zip_file)
        to_zip = []
        total = 0
        with zipfile.ZipFile(zip_file, 'w', zipfile.ZIP_DEFLATED, compresslevel=level) as zipf:
            for root, dirs, files in os.walk(path):
                for file in files:
                    src = Settings.join_path(root, file)
                    size = os.path.getsize(src)
                    total += size
                    to_zip.append({'file': src, 'size': size})

            if not quiet:
                print(f'Comprimiendo {len(files)} archivos...')

            with tqdm(total=total, unit='B', ncols=70, unit_scale=True, disable=quiet) as progress_bar:
                for file in to_zip:
                    zipf.write(file['file'], os.path.relpath(file['file'], path))
                    progress_bar.update(file['size'])

    def match_wildcard(pattern, text):
        if pattern.lower() == text.lower():
            return True
        regexp = re.compile(pattern.replace('*', '.*'), re.IGNORECASE)
        if regexp.fullmatch(text):
            return True
        return False

    def match_exclude(self, table):
        if table in self.settings.exclude_tables:
            return True
        for exclussion in self.settings.exclude_tables:
            if self.match_wildcard(exclussion, table):
                return True
        return False

    def match_include(self, table):
        if table in self.settings.include_tables:
            return True
        for exclussion in self.settings.include_tables:
            if not exclussion.startswith('!'):
                if self.match_wildcard(exclussion, table):
                    return True
        in_reversed = 0
        filters = 0
        for exclussion in self.settings.include_tables:
            if exclussion.startswith('!'):
                filters += 1
                exclussion = exclussion[1:]  # Eliminamos el primer carácter "!"
                if self.match_wildcard(exclussion, table):
                    in_reversed += 1
        if filters > 0 and in_reversed == filters:
            return True
        return False

    def filter_tables(self, table):
        if self.match_exclude(table):
            return False
        elif self.settings.include_tables:
            return self.match_include(table)

        return True

    def get_total_row_count(self, tables):
        self.print('Calculando filas...')
        sizes = {}
        cnx = self.get_connection()
        cursor = cnx.cursor()

        total_row_count = 0
        for table in tables:
            stmt = f"SELECT COUNT(*), (SELECT DATA_LENGTH FROM information_schema.TABLES WHERE table_schema = %s AND table_name = %s) FROM `{table}`"
            cursor.execute(stmt, (self.settings.db_name, table))
            item = cursor.fetchone()
            rows = item[0]
            table_bytes = item[1]
            total_row_count += rows
            if rows == 0:
                step = 1
            else:
                row_bytes = table_bytes / rows
                step = int(70000000 / row_bytes)
                if step == 0:
                    step = 10

            sizes[table] = {'rows': rows, 'bytes': table_bytes, 'step': step}

        cnx.close()

        text = total_row_count
        if total_row_count > 1000000:
            text = f'{int(total_row_count / 1000000)} millones de'
        elif total_row_count > 2000:
            text = f'{int(total_row_count / 1000)} mil'

        self.print(f"Listo para resguardar ~{text} filas")

        return total_row_count, sizes

    def main(self):
        self.settings.parse_config_file()
        self.settings.parse_command_line_backup()

        self.print("-------------------------------")
        self.print(f"DATABASE: {self.settings.db_name}")
        self.print(f"USER: {self.settings.db_user}")
        self.print(f"HOST: {self.settings.db_host}")
        self.print(f"OUTPUT: {self.settings.output}")
        self.print("-------------------------------")

        start = time.time()
        if self.settings.resume:
            self.print('Continuando...')
        else:
            self.create_backup_path()

        if not self.settings.skip_structure:
            self.dump_database_structure()

        if not self.settings.skip_data:
            self.dump_data()

        self.zip_full_path(self.settings.output_path, self.settings.output, self.settings.quiet)
        self.remove_backup_path()
        self.print("--- Tiempo total: %s seg. ---" % round(time.time() - start, 2))


if __name__ == '__main__':
    backup = Backup()
    backup.main()
