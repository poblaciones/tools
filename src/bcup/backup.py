import os
import sys
import time
import pymysql
import subprocess
import shutil
import zipfile
import re
import json
from tqdm import tqdm
from settings import Settings

# Lectura de configuración de MySQL desde el archivo ini
settings = Settings()
settings.parse_config_file()
filtered_tables = []


def run_mysqldump(options, output_file, table_list=''):
    command = f"{settings.mysqldump} --defaults-file={settings.CONFIG_FILE_PATH} --user={settings.db_user} --host={settings.db_host} " \
              f"--port={settings.db_port} {options} {settings.db_name} {table_list}"

    with open(output_file, 'w', encoding='utf-8') as f:
        completed_process = subprocess.run(command, shell=True, stdout=f)

    if completed_process.returncode != 0:
        print(f"El comando mysqldump.exe falló con el código de retorno: {completed_process.returncode}")
        print(f"\nComando:\n{command}")
        raise Exception("No se pudo completar con éxito el backup.")


def remove_trigger_definer(file_path):
    if not os.path.isfile(file_path):
        return

    with open(file_path, 'r', encoding='utf-8') as file:
        content = file.read()

    # Utilizamos una expresión regular para buscar y eliminar la cadena deseada
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
                # Utilizamos una expresión regular para eliminar la cadena deseada de la línea
                new_line = re.sub(r'DEFINER=`[^`]+`@`[^`]+`', '', line)
                file.write(new_line)
            else:
                file.write(line)


def print_quiet(text):
    if not settings.quiet:
        print(text)


def dump_database_structure():
    if settings.resume:
        path = join_path(settings.output_path, 'datos')
        sql_files = [file for file in os.listdir(path) if file.endswith('.sql')]
        if len(sql_files) > 0:
            print_quiet('Omitiendo estructura...')
            return

    print_quiet('Exportando estructura...')
    ignore_tables = ''
    if settings.exclude_tables:
        excluded_tables = get_excluded_table_names()
        if len(excluded_tables) > 0:
            print_quiet(f"Excluyendo {len(excluded_tables)} tabla(s).")
        for table in excluded_tables:
            ignore_tables += f" --ignore-table={settings.db_name}.`{table}`"
    # Tablas
    print_quiet('Exportando tablas...')
    table_list = ''
    if settings.include_tables:
        included_tables = get_included_table_names()
        for table in included_tables:
            table_list += f" \"{table}\""

    # print_quiet('Exportando de a una...')
    # start = time.time()
    # # TODO: ver si exportar un archivo por tabla de esctructura o ver si agregarlo al archivo de datos o ver si exporta todas siempre
    # for table in get_tables():
    #     run_mysqldump("--no-data --no-create-db", join_path(settings.output_path, "datos", f"{table}.sql"), table)
    #     remove_trigger_definer(join_path(settings.output_path, "datos", f"{table}.sql"))
    # print_quiet("--- %s seg. ---" % round(time.time() - start, 2))

    print_quiet('Exportando todas...')
    start = time.time()
    # TODO: ver si exportar un archivo por tabla de esctructura o ver si agregarlo al archivo de datos o ver si exporta todas siempre
    run_mysqldump(f"--no-data --no-create-db --skip-triggers {ignore_tables}", join_path(settings.output_path, "database_tables.sql"), table_list)
    print_quiet("--- %s seg. ---" % round(time.time() - start, 2))

    start = time.time()
    # TODO: ídem con estructura pero para triggers
    print_quiet('Exportando triggers...')
    run_mysqldump(f"--no-data --no-create-db --triggers --no-create-info {ignore_tables}", join_path(settings.output_path, "triggers.sql"), table_list)
    remove_trigger_definer(join_path(settings.output_path, "triggers.sql"))
    print_quiet("--- %s seg. ---" % round(time.time() - start, 2))

    if not settings.skip_routines:
        start = time.time()
        print_quiet('Exportando funciones...')
        run_mysqldump("--no-data --no-create-db --routines --skip-triggers --no-create-info ", join_path(settings.output_path, "routines.sql"))
        remove_function_definer(join_path(settings.output_path, "routines.sql"))
        print_quiet("--- %s seg. ---" % round(time.time() - start, 2))


def resolve_filename(table_name, step):
    file_number = str(step).zfill(4)
    return join_path(settings.output_path, 'datos', f'{table_name}_#{file_number}.sql')


def check_table_file(table_name):
    if not settings.resume:
        return False
    if not table_name:
        return False
    current_file_start = resolve_filename(table_name, 1)
    return os.path.exists(current_file_start)


def dump_table_data(table, next_table, next_next_table, progress_bar):
    table_name = table['table']
    row_count = table['rows']
    step = table['step']

    if check_table_file(table_name) and check_table_file(next_table):
        if not check_table_file(next_next_table):
            next_file_start = resolve_filename(next_table, 1)
            os.remove(next_file_start)
        print_quiet(f"Omitiendo {table_name}...")
        progress_bar.update(row_count)
        return

    for offset in range(0, row_count, step):
        output_file = resolve_filename(table_name, (offset // step) + 1)
        condition = f'"1 LIMIT {step} OFFSET {offset}"'
        command = f"{settings.mysqldump} --defaults-file={settings.CONFIG_FILE_PATH} --hex-blob --skip-triggers --user={settings.db_user} --host={settings.db_host} " \
                  f"--port={settings.db_port} --no-create-info --where={condition} {settings.db_name} {table_name}"

        with open(output_file, 'w', encoding='utf-8') as f:
            completed_process = subprocess.run(command, shell=True, stdout=f)

        if completed_process.returncode != 0:
            print(f"El comando mysqldump.exe falló con el código de retorno: {completed_process.returncode}")
            return False

        # Actualizar la barra de progreso
        progress_bar.update(min(step, row_count - offset))


def get_connection():
    return pymysql.connect(user=settings.db_user, password=settings.db_pass, host=settings.db_host, port=settings.db_port, database=settings.db_name)


def get_tables():
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

    return ret


# # TODO: ver si se usa o borrar y exportar extructura por tabla
# def get_all_tables():
#     cnx = get_connection()
#     cursor = cnx.cursor()
#     cursor.execute("SHOW FULL TABLES WHERE table_type != 'VIEW'")
#     tables = cursor.fetchall()
#     cnx.close()
#     ret = []
#     for table in tables:
#         if filter_tables(table[0]):
#             ret.append(table[0])
#
#     return ret


def get_included_table_names():
    cnx = get_connection()
    cursor = cnx.cursor()
    cursor.execute("SHOW FULL TABLES WHERE table_type != 'VIEW'")
    tables = cursor.fetchall()
    cnx.close()
    ret = []
    for table in tables:
        if match_include(table[0]) and not match_exclude(table[0]):
            ret.append(table[0])

    return ret


def get_excluded_table_names():
    cnx = get_connection()
    cursor = cnx.cursor()
    cursor.execute("SHOW FULL TABLES WHERE table_type != 'VIEW'")
    tables = cursor.fetchall()
    cnx.close()
    ret = []
    for table in tables:
        if match_exclude(table[0]):
            ret.append(table[0])

    return ret


def create_backup_path():
    if os.path.isdir(settings.output_path):
        remove_backup_path()
    os.makedirs(join_path(settings.output_path, 'datos'), exist_ok=True)


def remove_backup_path():
    shutil.rmtree(settings.output_path)


def zip_backup_path():
    if os.path.isfile(settings.output):
        os.remove(settings.output)
    filesToZip = []
    tt = 0
    with zipfile.ZipFile(settings.output, 'w', zipfile.ZIP_DEFLATED) as zipf:
        for root, dirs, files in os.walk(settings.output_path):
            for file in files:
                src = join_path(root, file)
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
    # TODO: Sacar signo de admiración !
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
    sizes = []
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

        sizes.append({'table': table, 'rows': rows, 'bytes': table_bytes, 'step': step})

    cnx.close()

    text = total_row_count
    if total_row_count > 1000000:
        text = f'{int(total_row_count / 1000000)} millones de'
    elif total_row_count > 2000:
        text = f'{int(total_row_count / 1000)} mil'

    print_quiet(f"Listo para resguardar ~{text} filas")

    return total_row_count, sizes


def dump_data():
    tables = get_tables()
    total_row_count, sizes = get_total_row_count(tables)

    with open(settings.output_path + "/tables.json", "w", encoding='utf-8') as outfile:
        json.dump(sizes, outfile)

    with tqdm(total=total_row_count, ncols=70, disable=settings.quiet) as progress_bar:
        validTables = []
        for table in tables:
            if filter_tables(table):
                validTables.append(table)

        for index, table in enumerate(validTables):
            table_attributes = [x for x in sizes if x['table'] == table][0]
            if index+1 < len(validTables):
                next_table = validTables[index+1]
            else:
                next_table = None
            if index + 2 < len(validTables):
                next_next_table = validTables[index + 2]
            else:
                next_next_table = None
            dump_table_data(table_attributes, next_table, next_next_table, progress_bar)


def join_path(*parts):
    return "/".join(parts).replace("\\", "/").replace("//", "/")


def main():
    settings.parse_command_line()

    print("-------------------------------")
    print(f"DATABASE: {settings.db_name}")
    print(f"USER: {settings.db_user}")
    print(f"HOST: {settings.db_host}")
    print(f"OUTPUT: {settings.output}")
    print("-------------------------------")

    if settings.resume:
        print_quiet('Continuando...')
    else:
        create_backup_path()

    dump_database_structure()

    dump_data()
    zip_backup_path()
    remove_backup_path()


# que pueda correr sin progress

if __name__ == '__main__':
    main()
