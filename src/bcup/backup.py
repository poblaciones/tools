# -*- coding: utf-8 -*-
# ver .096

import os
import sys
import pymysql
import subprocess
import shutil
import zipfile
import re
import datetime
import argparse
import json
import configparser
from tqdm import tqdm

args = {}
args['MYSQL_HOST'] = "localhost"
args['MYSQL_PORT'] = 3306
args['MYSQL_DB'] = ""

# Lectura de configuración de MySQL desde el archivo ini
CONFIG_FILE_PATH = 'db_settings.ini'
config = configparser.ConfigParser()
config.read(CONFIG_FILE_PATH)
args['MYSQL_USER'] = config['client']['user']
args['MYSQL_PASSWORD'] = config['client']['password'].strip('\"')
args['MYSQL_FILE'] = ''

if 'extra' in config and 'output' in config['extra']:
    args['OUTPUT'] = config['extra']['output']
else:
    args['OUTPUT'] = None
    
if 'extra' in config and 'database' in config['extra']:
    args['DATABASE'] = config['extra']['database']
else:
    args['DATABASE'] = None

if 'extra' in config and 'host' in config['extra']:
    args['HOST'] = config['extra']['host']
else:
    args['HOST'] = None

if sys.platform.startswith('win'):
    MYSQLDUMP_PATH = '"C:/Program Files/MySQL/MySQL Server 5.7/bin/mysqldump.exe"'
else:
    MYSQLDUMP_PATH = 'mysqldump'
DUMP_OPTIONS = "--no-data --no-create-db"

# Carpetas
BACKUP_FOLDER = "tmp_backup"


def run_mysqldump(args, options, output_file, table_list = ''):
    if MYSQLDUMP_PATH != 'mysqldump' and not os.path.isfile(MYSQLDUMP_PATH.strip('\"')):
        print("El ejecutable mysqldump.exe no existe.")
        return False

    command = f"{MYSQLDUMP_PATH} --defaults-file={CONFIG_FILE_PATH} --user={args['MYSQL_USER']} --host={args['MYSQL_HOST']} " \
              f"--port={args['MYSQL_PORT']} {options} {args['MYSQL_DB']} { table_list }"

    with open(output_file, 'w', encoding='utf-8') as f:
        completed_process = subprocess.run(command, shell=True, stdout=f)

    if completed_process.returncode != 0:
        print(f"El comando mysqldump.exe falló con el código de retorno: {completed_process.returncode}")
        print('\nComando:\n' + command);
        raise Exception("No se pudo completar con éxito el backup.") 

def remove_trigger_definer_from_file(file_path):
    if not os.path.isfile(file_path):
        return
    
    with open(file_path, 'r', encoding='utf-8') as file:
        content = file.read()

    # Utilizamos una expresión regular para buscar y eliminar la cadena deseada
    new_content = re.sub(r'/\*![0-9]+ DEFINER=`[^`]+`@`[^`]+`\*/', '', content)

    with open(file_path, 'w', encoding='utf-8') as file:
        file.write(new_content)


def remove_function_definer_from_file(file_path):
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
def printQuiet(args, text):
    if not args['quiet']:
        print(text)

def dump_database_structure(args):
    if args['resume']:
       path = BACKUP_FOLDER + '/datos'
       sql_files = [file for file in os.listdir(path) if file.endswith('.sql')]
       if len(sql_files) > 0:
          printQuiet(args, 'Omitiendo estructura...')
          return
    
    printQuiet(args, 'Capturando estructura...')
    ignore_tables = ''
    if args['exclude_tables']:
        excluded_tables = get_excluded_table_names(args)
        if (len(excluded_tables) > 0):
            printQuiet(args, f"Excluyendo {{ len(excluded_tables) }} tabla(s).") 
        for table in excluded_tables:
            ignore_tables += " --ignore-table=" + args['MYSQL_DB'] + ".`" + table + "`"
    # Tablas
    printQuiet(args, 'Capturando tablas...')
    table_list = ''
    if (args['include_tables']):
        included_tables = get_included_table_names(args)
        for table in included_tables:
            table_list += " \"" + table + "\""
    run_mysqldump(args, DUMP_OPTIONS + " --skip-triggers " + ignore_tables, os.path.join(BACKUP_FOLDER, "database_tables.sql"), table_list)
    # Triggers
    printQuiet(args, 'Capturando triggers...')
    run_mysqldump(args, DUMP_OPTIONS + " --triggers --no-create-info " + ignore_tables, os.path.join(BACKUP_FOLDER, "triggers.sql"), table_list)
    # Funciones
    printQuiet(args, 'Capturando funciones...')
    run_mysqldump(args, DUMP_OPTIONS + " --routines --skip-triggers --no-create-info ", os.path.join(BACKUP_FOLDER, "routines.sql"))
 
    remove_trigger_definer_from_file(os.path.join(BACKUP_FOLDER, "triggers.sql"))
    remove_function_definer_from_file(os.path.join(BACKUP_FOLDER, "routines.sql"))
    
    
def resolve_filename(table_name, step):
   file_number = str(step).zfill(4);
   return os.path.join(BACKUP_FOLDER + '/datos', f'{table_name}_#{file_number}.sql')
        
    
def check_table_file(args, table_name):
    if not args['resume']:
        return False
    if not table_name:
        return False
    current_file_start = resolve_filename(table_name, 1);
    return os.path.exists(current_file_start)
       
def dump_table_data(args, table, next_table, next_next_table, progress_bar):
    table_name = table['table']
    row_count = table['rows']
    step = table['step']
    
    if check_table_file(args, table_name) and check_table_file(args, next_table):
      if not check_table_file(args, next_next_table):
           next_file_start = resolve_filename(next_table, 1);
           os.remove(next_file_start)
      printQuiet(args, 'Omitiendo ' + table_name + '...')
      progress_bar.update(row_count)
      return
     
    # printQuiet(args, 'Guardando ' + table_name + '...')
    
    for offset in range(0, row_count, step):
        output_file = resolve_filename(table_name, (offset // step) + 1);
        condition = f'"1 LIMIT {step} OFFSET {offset}"'
        command = f"{MYSQLDUMP_PATH} --defaults-file={CONFIG_FILE_PATH} --hex-blob --skip-triggers --user={args['MYSQL_USER']} --host={args['MYSQL_HOST']} " \
                  f"--port={args['MYSQL_PORT']} --no-create-info --where={condition} {args['MYSQL_DB']} {table_name}"
       
        with open(output_file, 'w', encoding='utf-8') as f:
            completed_process = subprocess.run(command, shell=True, stdout=f)

        if completed_process.returncode != 0:
            print(f"El comando mysqldump.exe falló con el código de retorno: {completed_process.returncode}")
            return False

        # Actualizar la barra de progreso
        progress_bar.update(min(step, row_count - offset))


def get_table_names():
    cnx = pymysql.connect(user=args['MYSQL_USER'], password=args['MYSQL_PASSWORD'],
                                  host=args['MYSQL_HOST'], database=args['MYSQL_DB'])
    cursor = cnx.cursor()
    cursor.execute("show full tables where Table_Type != 'VIEW'")
    tables = cursor.fetchall()
    cnx.close()
    ret = []
    for table in tables:
      if matches(args, table[0]):
        ret.append(table[0])
        
    return ret

def get_included_table_names(args):
    cnx = pymysql.connect(user=args['MYSQL_USER'], password=args['MYSQL_PASSWORD'],
                                  host=args['MYSQL_HOST'], database=args['MYSQL_DB'])
    cursor = cnx.cursor()
    cursor.execute("show full tables where Table_Type != 'VIEW'")
    tables = cursor.fetchall()
    cnx.close()
    ret = []
    for table in tables:
      if match_include(args, table[0]) and not match_exclude(args, table[0]):
        ret.append(table[0])
        
    return ret

def get_excluded_table_names(args):
    cnx = pymysql.connect(user=args['MYSQL_USER'], password=args['MYSQL_PASSWORD'],
                                  host=args['MYSQL_HOST'], database=args['MYSQL_DB'])
    cursor = cnx.cursor()
    cursor.execute("show full tables where Table_Type != 'VIEW'")
    tables = cursor.fetchall()
    cnx.close()
    ret = []
    for table in tables:
      if match_exclude(args, table[0]):
        ret.append(table[0])
        
    return ret

def create_backup_folder():
    if os.path.isdir(BACKUP_FOLDER):
        shutil.rmtree(BACKUP_FOLDER)
    os.makedirs(BACKUP_FOLDER, exist_ok=True)
    os.makedirs(BACKUP_FOLDER + '/datos', exist_ok=True)


def remove_backup_folder():
    shutil.rmtree(BACKUP_FOLDER)


def zip_backup_folder(args):
    if os.path.isfile(args['MYSQL_FILE']):
     os.remove(args['MYSQL_FILE'])
    filesToZip = []
    tt = 0
    with zipfile.ZipFile(args['MYSQL_FILE'], 'w', zipfile.ZIP_DEFLATED) as zipf:
        for root, dirs, files in os.walk(BACKUP_FOLDER):
            for file in files:
                src = os.path.join(root, file)
                s = os.path.getsize(src)
                tt += s
                filesToZip.append({ 'file' : src, 'size' : s })
        printQuiet(args, f'Comprimiendo { len(files) } archivos...')
         
        with tqdm(total=tt, unit='B', ncols=70, unit_scale=True, disable=args['quiet']) as progress_bar:
            for file in filesToZip:
                zipf.write(file['file'], os.path.relpath(file['file'], BACKUP_FOLDER))
                progress_bar.update(file['size'])

def match_wildcard(pattern, text):
    if pattern.lower() == text.lower():
        return True
    filtro_regexp = re.compile(pattern.replace('*', '.*'), re.IGNORECASE)
    if filtro_regexp.fullmatch(text):
          return True
    return False


def match_exclude(args, table):
    if table in args['exclude_tables']:
       return True
    for exclussion in args['exclude_tables']:
        if match_wildcard(exclussion, table):
            return True
    return False


def match_include(args, table):
    if table in args['include_tables']:
       return True
    for exclussion in args['include_tables']:
        if not exclussion.startswith('!'):
            if match_wildcard(exclussion, table):
                return True
    in_reversed = 0
    filters = 0
    for exclussion in args['include_tables']:
        if exclussion.startswith('!'):
            filters += 1
            exclussion = exclussion[1:]  # Eliminamos el primer carácter "!"
            if match_wildcard(exclussion, table):
                in_reversed += 1
    if (filters > 0 and in_reversed == filters):
        return True
    return False
 
def matches(args, table):
    if match_exclude(args, table):
       return False
    elif (len(args['include_tables']) > 0):
        return match_include(args, table)
    else:
        return True
        
def get_total_row_count(args, tables):
    printQuiet(args, 'Calculando filas...')
    sizes = []
    cnx = pymysql.connect(user=args['MYSQL_USER'], password=args['MYSQL_PASSWORD'],
                                  host=args['MYSQL_HOST'], database=args['MYSQL_DB'])
    cursor = cnx.cursor()

    total_row_count = 0
    for table in tables:
      if matches(args, table):
        stmt = f"SELECT COUNT(*), (SELECT DATA_LENGTH FROM information_schema.TABLES WHERE TABLE_SCHEMA = '{args['MYSQL_DB']}' AND TABLE_NAME = '{table}') FROM `{table}`"
        cursor.execute(stmt)
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
                
        sizes.append({ 'table': table, 'rows': rows, 'bytes': table_bytes, 'step':
                       step })

    cnx.close()

    text = f'{total_row_count}'
    if (total_row_count > 1000000):
         text = f'{ int(total_row_count / 1000000) } millones de'
    elif (total_row_count > 2000):
         text = f'{ int(total_row_count / 1000) } mil'
    
    printQuiet(args, 'Listo para resguardar ~' + text + ' filas')
    
    return total_row_count, sizes

def dump_data(args):
    tables = get_table_names()
    total_row_count, sizes = get_total_row_count(args, tables)

    with open(BACKUP_FOLDER + "/tables.json", "w", encoding='utf-8') as outfile:
        json.dump(sizes, outfile)

    with tqdm(total=total_row_count, ncols=70, disable=args['quiet']) as progress_bar:
        validTables = []
        for table in tables:
           if matches(args, table):
               validTables.append(table)
               
        for index, table in enumerate(validTables):
           table_attributes = [x for x in sizes if (x['table'] == table)][0]
           if index+1 < len(validTables):
               next_table = validTables[index+1]
           else:
               next_table = None
           if index + 2 < len(validTables):
               next_next_table = validTables[index + 2]
           else:
               next_next_table = None
           dump_table_data(args, table_attributes, next_table, next_next_table, progress_bar)


def main(args):
    # Parse command line arguments
    parser = argparse.ArgumentParser(description='Python script with arguments')
    parser.add_argument('--host', default='127.0.0.1', help='The host address (default: 127.0.0.1)')
    parser.add_argument('--port', default=None, help='The port for the database.')
    parser.add_argument('--user', default=None, help='The user for the database.')
    parser.add_argument('--quiet', default=False, help='No UI messages.')
    parser.add_argument('--resume', default=False, help='Try to resume backup.')
    parser.add_argument('--password', default=None, help='The password for the database. If ommited, values from db_settings.ini are used.')
    parser.add_argument('--database', default=None, help='The name of the database.')
    parser.add_argument('--output', default=None, help='The dump file to be created (zipped)')
    parser.add_argument('--autoname', default=None, help='Create output based on current date')
    parser.add_argument('--exclude_tables', nargs='+', default=[], help='List of tables to exclude (comma separated). Can use * as wildcard.')
    parser.add_argument('--include_tables', nargs='+', default=[], help='List of tables to include (comma separated). Can use * as wildcard and can begin with ! for negation.')
    command_args = parser.parse_args()

    if not command_args.database or (not command_args.output and not command_args.autoname):
        stop = False
        if not command_args.database and not args['DATABASE']:
            print("No database name provided. Please specify a database name using the '--database' argument.")
            stop = True
        if not command_args.output and not command_args.autoname and not args['OUTPUT']:
            print("No output file was provided. Please specify an output file using the '--output' argument.")
            stop = True
        if stop:
            return
    # Assign values from arguments to variables
    if not command_args.host:
        args['MYSQL_HOST'] = args['HOST']
    else:
        args['MYSQL_HOST'] = command_args.host

    if not command_args.database:
        args['MYSQL_DB'] = args['DATABASE']
    else:
        args['MYSQL_DB'] = command_args.database
    args['quiet'] = (command_args.quiet != False)
    
    if command_args.resume:
        args['resume'] = True
    else:
        args['resume'] = False
    
    if command_args.autoname:
        current_date_time = datetime.datetime.now()
        formatted_date_time = current_date_time.strftime('%Y%m%d_%H%M%S')
        args['MYSQL_FILE'] = 'bkdb-' + formatted_date_time + ".zip"
    else:
        if not command_args.output:
            args['MYSQL_FILE'] = args['OUTPUT']    
        else:
            args['MYSQL_FILE'] = command_args.output
            
    args['exclude_tables'] = command_args.exclude_tables
    if (args['exclude_tables']):
        args['exclude_tables'] = args['exclude_tables'][0].split(',')
    args['include_tables'] = command_args.include_tables
    if (args['include_tables']):
        args['include_tables'] = args['include_tables'][0].split(',')
    
    user = command_args.user
    password = command_args.password
    if user != None:
        args['MYSQL_USER'] = user
    if command_args.port != None:
        args['MYSQL_PORT'] = command_args.port
    if password != None:
        args['MYSQL_PASSWORD'] = password
 
    # EMPIEZA A EJECUTAR
    print("-------------------------------")
    print("DATABASE: " + args['MYSQL_DB'])
    print("USER: " + args['MYSQL_USER'])
    print("HOST: " + args['MYSQL_HOST'])
    print("-------------------------------")

    if not args['MYSQL_FILE']:
        args['MYSQL_FILE'] = args['MYSQL_DB'] + ".zip"
    if args['resume']:
        printQuiet(args, 'Continuando...')
    else:
        create_backup_folder()
    #if len(args['include_tables']) == 0:
    dump_database_structure(args)
   
    dump_data(args)
    zip_backup_folder(args)
    remove_backup_folder()


## que pueda correr sin progress
#chequear si el pedir solo una tabla anda bien
    
if __name__ == '__main__':
    main(args)