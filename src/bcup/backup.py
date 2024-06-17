import os
import sys
import time
import glob
import pymysql
import subprocess
import shutil
import zipfile
import re
import json
from tqdm import tqdm
from settings import Settings
from pathlib import Path

# Variables globales
settings = Settings()


def run_mysqldump(options, output_file, table_list=''):
    command = f"\"{settings.mysqldump}\" --defaults-file={settings.CONFIG_FILE} --user={settings.db_user} --host={settings.db_host} " \
            f"--port={settings.db_port} {options} {settings.db_name} {table_list}"

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


def print_quiet(text):
    if not settings.quiet:
        print(text)


def dump_database_structure():
    print_quiet('Exportando tablas y triggers...')
    tables = get_tables(settings.estructura_path)
    with tqdm(total=len(tables), ncols=70, disable=settings.quiet) as progress_bar:
        for table in tables:
            file = Settings.join_path(settings.estructura_path, f"{table}.sql")
            run_mysqldump("--no-data --no-create-db", file, table)
            remove_trigger_definer(file)
            progress_bar.update()

    if not settings.skip_routines:
        # start = time.time()
        print_quiet('Exportando funciones...')
        run_mysqldump("--no-data --no-create-db --routines --skip-triggers --no-create-info ", Settings.join_path(settings.output_path, "routines.sql"))
        remove_function_definer(Settings.join_path(settings.output_path, "routines.sql"))
        # print_quiet("--- %s seg. ---" % round(time.time() - start, 2))


def dump_data():
    tables = get_tables(settings.datos_path)
    total_row_count, sizes = get_total_row_count(tables)

    with open(Settings.join_path(settings.output_path, "tables.json"), "w", encoding='utf-8') as outfile:
        json.dump(sizes, outfile, indent=2)

    with tqdm(total=total_row_count, ncols=70, disable=settings.quiet) as progress_bar:
        for table in tables:
            dump_table_data(table, sizes[table], progress_bar)


def dump_table_data(table, sizes, progress_bar):
    for offset in range(0, sizes['rows'], sizes['step']):
        file = resolve_filename(table, (offset // sizes['step']) + 1)
        run_mysqldump(f"--no-create-info --hex-blob --skip-triggers --where=\"1 LIMIT {sizes['step']} OFFSET {offset}\"", file, table)
        progress_bar.update(min(sizes['step'], sizes['rows'] - offset))


def resolve_filename(table_name, step):
    number = str(step).zfill(4)
    return Settings.join_path(settings.datos_path, f'{table_name}_#{number}.sql')


def get_connection():
    return pymysql.connect(user=settings.db_user, password=settings.db_pass, host=settings.db_host, port=settings.db_port, database=settings.db_name)


def get_tables(resume_path):
    """ Obtiene todas las tablas que hay que backupear. Filtrando por fecha y filtros de exclusión e inclusión.
        Si es resume quita las tablas que ya existen salvo la última que la vuelve a regenerar por si el backup
        anterior se cortó por la mitad.  """
    cnx = get_connection()
    cursor = cnx.cursor()
    sql = "SELECT table_name FROM information_schema.TABLES WHERE table_type = 'BASE TABLE' AND table_schema = %s AND (create_time > %s OR update_time > %s)"
    cursor.execute(sql, (settings.db_name, settings.from_date, settings.from_date))
    tables = cursor.fetchall()
    cnx.close()
    ret = []
    for table in tables:
        if filter_tables(table[0]):
            ret.append(table[0])

    if settings.resume:
        # obtiene los archivos de estructura existentes ordenados por fecha
        files = sorted(Path(resume_path).iterdir(), key=os.path.getmtime)
        if files:
            # quita el último modificado de los archivos existentes (para que lo regenere) y lo mueve
            # arriba de todo en la lista de tablas para que sea el primero y continúe desde ahí
            last = files.pop()
            clean_file(str(last))
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


def create_backup_path():
    if os.path.isdir(settings.output_path):
        remove_backup_path()
    os.makedirs(settings.datos_path, exist_ok=True)
    os.makedirs(settings.estructura_path, exist_ok=True)


def remove_backup_path():
    shutil.rmtree(settings.output_path)


def zip_backup_path():
    if os.path.isfile(settings.output):
        os.remove(settings.output)
    filesToZip = []
    tt = 0
    with zipfile.ZipFile(settings.output, 'w', zipfile.ZIP_DEFLATED, compresslevel=1) as zipf:
        for root, dirs, files in os.walk(settings.output_path):
            for file in files:
                src = Settings.join_path(root, file)
                s = os.path.getsize(src)
                tt += s
                filesToZip.append({'file': src, 'size': s})
        print_quiet(f'Comprimiendo {len(files)} archivos...')

        with tqdm(total=tt, unit='B', ncols=70, unit_scale=True, disable=settings.quiet) as progress_bar:
            for file in filesToZip:
                zipf.write(file['file'], os.path.relpath(file['file'], settings.output_path))
                progress_bar.update(file['size'])


def match_wildcard(pattern, text):
    if pattern.lower() == text.lower():
        return True
    regexp = re.compile(pattern.replace('*', '.*'), re.IGNORECASE)
    if regexp.fullmatch(text):
        return True
    return False


def match_exclude(table):
    if table in settings.exclude_tables:
        return True
    for exclussion in settings.exclude_tables:
        if match_wildcard(exclussion, table):
            return True
    return False


def match_include(table):
    if table in settings.include_tables:
        return True
    # TODO: Sacar signo de admiración ! o no?
    for exclussion in settings.include_tables:
        if not exclussion.startswith('!'):
            if match_wildcard(exclussion, table):
                return True
    in_reversed = 0
    filters = 0
    for exclussion in settings.include_tables:
        if exclussion.startswith('!'):
            filters += 1
            exclussion = exclussion[1:]  # Eliminamos el primer carácter "!"
            if match_wildcard(exclussion, table):
                in_reversed += 1
    if filters > 0 and in_reversed == filters:
        return True
    return False


def filter_tables(table):
    if match_exclude(table):
        return False
    elif settings.include_tables:
        return match_include(table)

    return True


def get_total_row_count(tables):
    print_quiet('Calculando filas...')
    sizes = {}
    cnx = get_connection()
    cursor = cnx.cursor()

    total_row_count = 0
    for table in tables:
        stmt = f"SELECT COUNT(*), (SELECT DATA_LENGTH FROM information_schema.TABLES WHERE table_schema = %s AND table_name = %s) FROM `{table}`"
        cursor.execute(stmt, (settings.db_name, table))
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

    print_quiet(f"Listo para resguardar ~{text} filas")

    return total_row_count, sizes


def main():
    settings.parse_config_file()
    settings.parse_command_line()

    print_quiet("-------------------------------")
    print_quiet(f"DATABASE: {settings.db_name}")
    print_quiet(f"USER: {settings.db_user}")
    print_quiet(f"HOST: {settings.db_host}")
    print_quiet(f"OUTPUT: {settings.output}")
    print_quiet("-------------------------------")

    start = time.time()
    if settings.resume:
        print_quiet('Continuando...')
    else:
        create_backup_path()

    if not settings.skip_structure:
        dump_database_structure()

    if not settings.skip_data:
        dump_data()

    zip_backup_path()
    remove_backup_path()
    print_quiet("--- Tiempo total: %s seg. ---" % round(time.time() - start, 2))


if __name__ == '__main__':
    main()
