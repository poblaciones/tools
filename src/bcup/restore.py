import json
import os
import shutil
import subprocess
import sys
import time
import zipfile
from settings import Settings
from tqdm import tqdm


class Restore:

    def __init__(self):
        self.settings = Settings()

    def run_mysql(self, file):
        command = f"unzip -p \"{file}\" | \"{self.settings.mysql}\" --defaults-file={Settings.CONFIG_FILE} --user={self.settings.db_user} " \
                f"--host={self.settings.db_host} --port={self.settings.db_port} {self.settings.db_name}"

        proc = subprocess.run(command, shell=True)
        if proc.returncode != 0:
            print(f"El comando mysqldump.exe falló con el código de retorno: {proc.returncode}")
            print(f"\nComando:\n{command}")
            sys.exit("No se pudo completar con éxito la restauración.")

    def restore_routines(self):
        print('Restaurando funciones...')
        routines = Settings.join_path(self.settings.input_path, "routines.zip")
        if os.path.exists(routines):
            self.run_mysql(routines)

    def restore_tables(self):
        print('Restaurando tablas...')

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
                    self.run_mysql(sizes[table]['file'])
                    progress_bar.update(sizes[table]['rows'])

    def create_restore_path(self):
        if os.path.isdir(self.settings.input_path):
            shutil.rmtree(self.settings.input_path)

        print(f"Creando {self.settings.input_path}")
        os.makedirs(self.settings.input_path, exist_ok=True)

    def remove_restore_path(self):
        print(f"Eliminando {self.settings.input_path}")
        shutil.rmtree(self.settings.input_path)

    def unzip_backup(self):
        print('Descomprimiendo...')
        size = 0

        with zipfile.ZipFile(self.settings.input, 'r') as zipf:
            for file in zipf.namelist():
                size += zipf.getinfo(file).file_size

            with tqdm(total=size, unit='B', ncols=70, unit_scale=True) as progress_bar:
                for file in zipf.namelist():
                    extract_path = Settings.join_path(self.settings.input_path, file)
                    with zipf.open(file, 'r') as zf, open(extract_path, 'wb') as outfile:
                        block_size = 10 * 1024 * 1024  # Tamaño del bloque para leer y escribir
                        while True:
                            data = zf.read(block_size)
                            if not data:
                                break
                            outfile.write(data)
                            progress_bar.update(len(data))

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

        if not self.settings.skip_routines:
            self.restore_routines()

        self.restore_tables()

        if self.settings.input:
            while True:
                res = input("¿Desea eliminar la carpeta temporal? S/n: ").upper()
                if res == "N":
                    break
                else:  # default S
                    self.remove_restore_path()

        self.print("--- Tiempo total: %s seg. ---" % round(time.time() - start, 2))
