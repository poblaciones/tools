import os
import subprocess
import shutil
import sys
import json
import glob
import zipfile
from settings import Settings
from tqdm import tqdm


class Restore:

    def __init__(self):
        self.settings = Settings()

    def run_mysql(self, file):
        command = f"\"{self.settings.mysql}\" --defaults-file={Settings.CONFIG_FILE} --user={self.settings.db_user} --host={self.settings.db_host} " \
                f"--port={self.settings.db_port} {self.settings.db_name} < \"{file}\""

        proc = subprocess.run(command, shell=True)
        if proc.returncode != 0:
            print(f"El comando mysqldump.exe falló con el código de retorno: {proc.returncode}")
            print(f"\nComando:\n{command}")
            sys.exit("No se pudo completar con éxito la restauración.")

    def restore_database_structure(self):
        print('Restaurando estructura...')

        structure = Settings.join_path(self.settings.input_path, "database_tables.sql")
        if os.path.isfile(structure):
            self.run_mysql(structure)
        else:
            self.restore_structure_files()

        routines = Settings.join_path(self.settings.input_path, "routines.sql")
        if os.path.exists(routines):
            print("Funciones...")
            self.run_mysql(routines)

        triggers = Settings.join_path(self.settings.input_path, "triggers.sql")
        if os.path.exists(triggers):
            print("Triggers...")
            self.run_mysql(triggers)

    def restore_structure_files(self):
        files = sorted(glob.glob(Settings.join_path(self.settings.estructura_path, "*.sql")))
        with tqdm(total=len(files), ncols=70) as progress_bar:
            for file in files:
                self.run_mysql(file)
                progress_bar.update()

    def restore_data(self):
        print('Restaurando datos...')

        with open(Settings.join_path(self.settings.input_path, "tables.json"), 'r',  encoding='utf-8') as file:
            sizes = json.load(file)

        total = 0
        for table in sizes:
            file = Settings.join_path(self.settings.datos_path, table + '_#0001.sql')
            if os.path.isfile(file):
                total += sizes[table]['rows']

        with tqdm(total=total, ncols=70) as progress_bar:
            for table in sizes:
                self.restore_table_data(table, sizes[table]['rows'], sizes[table]['step'], progress_bar)

    def restore_table_data(self, table, size, step, progress_bar):
        files = sorted(glob.glob(Settings.join_path(self.settings.datos_path, f"{table}_#*.sql")))
        offset = 0
        for file in files:
            self.run_mysql(file)
            progress_bar.update(min(step, size - offset))
            offset += step

    def create_backup_path(self):
        if os.path.isdir(self.settings.input_path):
            shutil.rmtree(self.settings.input_path)

        print(f"Creando {self.settings.input_path} en {os.getcwd()}")
        os.makedirs(self.settings.input_path, exist_ok=True)

    def remove_backup_path(self):
        print(f"Eliminando {self.settings.input_path}")
        shutil.rmtree(self.settings.input_path)

    def unzip_backup(self):
        print('Descomprimiendo...')
        size = 0

        with zipfile.ZipFile(self.settings.input, 'r') as zipf:
            for file in zipf.namelist():
                size += zipf.getinfo(file).file_size

            with tqdm(total=size, unit='B', ncols=70, unit_scale=True) as pbar:
                for file in zipf.namelist():
                    extract_path = Settings.join_path(self.settings.input_path, file)
                    with zipf.open(file, 'r') as zf, open(extract_path, 'wb') as outfile:
                        block_size = 10 * 1024 * 1024  # Tamaño del bloque para leer y escribir
                        while True:
                            data = zf.read(block_size)
                            if not data:
                                break
                            outfile.write(data)
                            pbar.update(len(data))

    def get_unzip(self):
        if not self.settings.input:
            return False
        if self.settings.input and not os.path.isdir(self.settings.input_path):
            return True

        while True:
            res = input("Ya existe la carpeta de archivos expandidos. ¿Desea reutilizarla e intentar ejecutarlos? (S/n/cancel)?: ").upper()
            if res == "S":
                return False
            if res == "N":
                return True
            if res == "C" or res == "CANCEL":
                sys.exit()
            else:  # default S
                return False

    def main(self):
        self.settings.parse_config_file()
        self.settings.parse_command_line_restore()

        print("-------------------------------")
        print("DATABASE: " + self.settings.db_name)
        print("USER: " + self.settings.db_user)
        print("HOST: " + self.settings.db_host)
        print("-------------------------------")

        unzip = self.get_unzip()

        if unzip:
            self.create_backup_path()
            self.unzip_backup()

        if not self.settings.skip_structure:
            self.restore_database_structure()

        if not self.settings.skip_data:
            self.restore_data()

        while True:
            res = input("¿Desea eliminar la carpeta temporal? s/N: ").upper()
            if res == "S":
                self.remove_backup_path()
            else:  # default N
                break


if __name__ == '__main__':
    restore = Restore()
    restore.main()
