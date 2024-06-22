import json
import os
import pymysql
import re
import shutil
import subprocess
import sys
import time
import zipfile
from glob import glob
from settings import Settings
from tqdm import tqdm


class Backup:

    def __init__(self):
        self.settings = Settings()

    def run_mysqldump(self, options, output_file, table_list=''):
        ini = f"--password={self.settings.db_pass}"
        if self.settings.has_ini:
            ini = f"--defaults-file={Settings.CONFIG_FILE}"

        command = f"\"{self.settings.mysqldump}\" {ini} --user={self.settings.db_user} --host={self.settings.db_host} --port={self.settings.db_port} " \
            f"{options} {self.settings.db_name} {table_list}"

        with open(output_file, 'w', encoding='utf-8') as f:
            proc = subprocess.run(command, shell=True, stdout=f)

        if proc.returncode != 0:
            print(f"mysqldump failed with return code: {proc.returncode}")
            print(f"\nCommand:\n{command}")
            sys.exit("Backup operation failed.")

    def remove_trigger_definer(self, file_path):
        if not os.path.isfile(file_path):
            return

        with open(file_path, 'r', encoding='utf-8') as file:
            content = file.read()

        new_content = re.sub(r'/\*![0-9]+ DEFINER=`[^`]+`@`[^`]+`\*/', '', content)

        with open(file_path, 'w', encoding='utf-8') as file:
            file.write(new_content)

    def remove_function_definer(self, file_path):
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

    def dump_routines(self):
        name = Settings.join_path(self.settings.output_path, "routines")
        if self.settings.resume and not os.path.exists(name + ".sql") and os.path.exists(name + ".zip"):
            return
        # start = time.time()
        self.print('Exporting routines...')
        self.run_mysqldump("--no-data --no-create-db --routines --skip-triggers --no-create-info ", name + ".sql")
        self.remove_function_definer(name + ".sql")
        self.zip_table("routines", self.settings.output_path)
        # self.print("--- %s sec. ---" % round(time.time() - start, 2))

    def dump_tables(self):
        tables = self.get_tables()
        total_row_count, sizes = self.get_total_row_count(tables)

        json_file = Settings.join_path(self.settings.output_path, "tables.json")

        if self.settings.resume and os.path.exists(json_file):
            with open(json_file, 'r',  encoding='utf-8') as file:
                # mergea los jsons
                sizes = dict(sorted({**json.load(file), **sizes}.items()))

        with open(json_file, "w", encoding='utf-8') as outfile:
            json.dump(sizes, outfile, indent=2)

        with tqdm(total=total_row_count, ncols=70, disable=self.settings.quiet) as progress_bar:
            for table in tables:
                self.dump_table_structure(table)
                
                if not self.dump_table_data(table, sizes[table], progress_bar):
                    self.print(f'Partial: {progress_bar.n} of {progress_bar.total}') 
                    return False
                
                self.zip_table(table, self.settings.tables_path)
                if self.settings.step_by_step:
                    self.print(f'Partial: {progress_bar.n} of {progress_bar.total}') 
                    return False
        return True

    def dump_table_data(self, table, sizes, progress_bar):
        for offset in range(0, sizes['rows'], sizes['step']):
            file = self.resolve_filename(table, (offset // sizes['step']) + 1)
            if not os.path.exists(file):
                tmp_file = self.resolve_tmp_filename()
                self.run_mysqldump(f"--no-create-info --hex-blob --skip-triggers --where=\"1 LIMIT {sizes['step']} OFFSET {offset}\"", tmp_file, table)
                os.rename(tmp_file, file)
                print('Saving... ' + file)
                if self.settings.step_by_step:
                   return False 
            progress_bar.update(min(sizes['step'], sizes['rows'] - offset))
        print('Completed ' + table)
        return True
    
    def dump_table_structure(self, table):
        file = Settings.join_path(self.settings.tables_path, f"{table}.sql")
        if not os.path.exists(file):
            tmp_file = self.resolve_tmp_filename()
            self.run_mysqldump("--no-data --no-create-db", tmp_file, table)
            self.remove_trigger_definer(tmp_file)
            os.rename(tmp_file, file)
    
    def resolve_tmp_filename(self):
        return Settings.join_path(self.settings.tables_path, '#tmp#.sql')

    def resolve_filename(self, table_name, step):
        number = str(step).zfill(4)
        return Settings.join_path(self.settings.tables_path, f'{table_name}_#{number}.sql')

    def get_connection(self):
        return pymysql.connect(user=self.settings.db_user, password=self.settings.db_pass, host=self.settings.db_host,
                               port=self.settings.db_port, database=self.settings.db_name)

    def get_tables(self):
        """ Obtiene todas las tablas que hay que backupear filtrando por fecha y filtros
            de exclusión e inclusión.  Si es resume, quita las tablas que ya están creadas.
        """
        cnx = self.get_connection()
        cursor = cnx.cursor()
        sql = "SELECT table_name FROM information_schema.TABLES " \
            "WHERE table_type = 'BASE TABLE' AND table_schema = %s AND (create_time > %s OR update_time > %s) " \
            "ORDER BY table_name"
        cursor.execute(sql, (self.settings.db_name, self.settings.from_date, self.settings.from_date))
        tables = cursor.fetchall()
        cnx.close()
        ret = []
        for table in tables:
            if self.filter_tables(table[0]):
                ret.append(table[0])

        if self.settings.resume:
            tmp_file = self.resolve_tmp_filename()
            if os.path.exists(tmp_file):
                os.remove(tmp_file)
            ret = self.filter_existing_tables(ret)

        return ret

    def filter_existing_tables(self, tables):
        # Si hay sqls borra el zip si existe y pone en 0 esos sqls para que se recreen.
        sqls = sorted(glob(Settings.join_path(self.settings.tables_path, "*.sql")))
        if sqls:
            #for file in sqls:
            #    if "_#" in file:
            #        os.remove(file)  # borra las que son numeradas
            #    else:
            #        open(file, 'w').close()  # trunca la de estructura
            zip_file = sqls[0].replace(".sql", ".zip")
            if os.path.exists(zip_file):
                os.remove(zip_file)

        # Remueve las tablas que ya fueron creadas en zips.
        files = sorted(glob(Settings.join_path(self.settings.tables_path, "*.zip")))
        for file in files:
            table = os.path.basename(file.replace(".zip", ""))
            if table in tables:
                tables.remove(table)
        return tables

    def create_backup_path(self):
        if os.path.isdir(self.settings.output_path):
            shutil.rmtree(self.settings.output_path)
        os.makedirs(self.settings.tables_path, exist_ok=True)

    def zip_table(self, table, path, level=1):
        to_zip = sorted(glob(Settings.join_path(path, table + "*.sql")))
        zip_file = Settings.join_path(path, table + ".zip")
        # No soportado en python 3.6
        # with zipfile.ZipFile(zip_file, 'w', zipfile.ZIP_DEFLATED, compresslevel=level) as zipf:
        with zipfile.ZipFile(zip_file, 'w', zipfile.ZIP_DEFLATED) as zipf:
            for file in to_zip:
                zipf.write(file, os.path.basename(file))
        for file in to_zip:
            os.remove(file)

    def zip_full_path(self, path, zip_file, level=1):
        if os.path.isfile(zip_file):
            os.remove(zip_file)
        to_zip = []
        total = 0
        # No soportado en python 3.6
        # with zipfile.ZipFile(zip_file, 'w', zipfile.ZIP_DEFLATED, compresslevel=level) as zipf:
        with zipfile.ZipFile(zip_file, 'w', zipfile.ZIP_STORED) as zipf:
            for root, dirs, files in os.walk(path):
                for file in files:
                    src = Settings.join_path(root, file)
                    size = os.path.getsize(src)
                    total += size
                    to_zip.append({'file': src, 'size': size})

            self.print(f'Compressing {len(files)} files...')

            with tqdm(total=total, unit='B', ncols=70, unit_scale=True, disable=self.settings.quiet) as progress_bar:
                for file in to_zip:
                    zipf.write(file['file'], os.path.relpath(file['file'], path))
                    progress_bar.update(file['size'])

    def match_wildcard(self, pattern, text):
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
                exclussion = exclussion[1:]  # Elimina el primer carácter "!"
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
        self.print('Calculating rows...')
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
            text = f"{int(total_row_count / 1000000)} millon"
        elif total_row_count > 2000:
            text = f"{int(total_row_count / 1000)} thousand"

        self.print(f"Ready to backup ~{text} rows")

        return total_row_count, sizes

    def create_timestamp_file(self):
        file = Settings.join_path(self.settings.output_path, "timestamp.txt")
        with open(file, 'w', encoding='utf-8') as file:
            file.write(self.settings.date)

    def create_unfinished_file(self):
        file = Settings.join_path(self.settings.output_path, "unfinished")
        open(file, 'w').close()

    def delete_unfinished_file(self):
        file = Settings.join_path(self.settings.output_path, "unfinished")
        os.remove(file)

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
            self.print('Resuming...')
        else:
            self.create_backup_path()
            self.create_unfinished_file()
            self.create_timestamp_file()

        if not self.settings.skip_routines:
            self.dump_routines()

        if not self.dump_tables():
            self.print("---- STEP COMPLETED. ")
            return

        self.delete_unfinished_file()

        if self.settings.zip:
            self.zip_full_path(self.settings.output_path, self.settings.output_path + ".zip", 0)
            shutil.rmtree(self.settings.output_path)

        self.print("--- Total time: %s sec. ---" % round(time.time() - start, 2))
        self.print("--- Backup completed.")
