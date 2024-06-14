# ver .096

import os
import subprocess
import shutil
import sys
import argparse
import json
import zipfile
import configparser

from tqdm import tqdm

# Parámetros
args = {}
args['MYSQL_HOST'] = "127.0.0.1"
args['MYSQL_PORT'] = 3306
args['MYSQL_DB'] = ""

if sys.platform.startswith('win'):
    MYSQL_PATH = '"C:/Program Files/MySQL/MySQL Server 5.7/bin/mysql.exe"'
else:
    MYSQL_PATH = 'mysql'
    
CONFIG_FILE_PATH = 'db_settings.ini'
BACKUP_FOLDER = "tmp_restore"
config = configparser.ConfigParser()
config.read(CONFIG_FILE_PATH)
args['MYSQL_USER'] = config['client']['user']
args['MYSQL_PASSWORD'] = config['client']['password']
args['MYSQL_FILE'] = ''

if 'extra' in config and 'database' in config['extra']:
    args['DATABASE'] = config['extra']['database']
else:
    args['DATABASE'] = None

if 'extra' in config and 'host' in config['extra']:
    args['HOST'] = config['extra']['host']
else:
    args['HOST'] = None

def run_mysql(args, command):
    full_command = f"{MYSQL_PATH} --defaults-file={CONFIG_FILE_PATH} --user={args['MYSQL_USER']} --host={args['MYSQL_HOST']} " \
                   f"--port={args['MYSQL_PORT']} {args['MYSQL_DB']} --execute=\"{command}\""

    completed_process = subprocess.run(full_command, shell=True)
    if completed_process.returncode != 0:
        print(f"El comando mysqldump.exe falló con el código de retorno: {completed_process.returncode}")
        print('\nComando:\n' + full_command);
        raise Exception("No se pudo completar con éxito la restauración.") 

def restore_database_structure(args):
    print('Restaurando estructura...')
    run_mysql(args, "source " + os.path.join(BACKUP_FOLDER, "database_tables.sql"))
    run_mysql(args, "source " + os.path.join(BACKUP_FOLDER, "routines.sql"))
    run_mysql(args, "source " + os.path.join(BACKUP_FOLDER, "triggers.sql"))
    

def restore_table_data(args, table, progress_bar):
    table_name = table['table']
    tablesize = table['rows']
    files = os.listdir(os.path.join(BACKUP_FOLDER, 'datos'))
    offset = 0
    for file in files:
        if file.startswith(f'{table_name}_#'):
            file_path = os.path.join(BACKUP_FOLDER, 'datos', file)
            command = f"source {file_path}"
            
            run_mysql(args, command)
            progress_bar.update(min(table['step'], tablesize - offset))   
            offset += table['step']


def create_backup_folder():
    if os.path.isdir(BACKUP_FOLDER):
        shutil.rmtree(BACKUP_FOLDER)
    
    directory = os.getcwd()
    print('Creando ' + BACKUP_FOLDER + ' en ' + directory)
    os.makedirs(BACKUP_FOLDER, exist_ok=True)
    os.makedirs(BACKUP_FOLDER + '/datos', exist_ok=True)


def remove_backup_folder():
    print('Eliminando ' + BACKUP_FOLDER)
    shutil.rmtree(BACKUP_FOLDER)

def unzip_backup_folder(args):
    print('Descomprimiendo...')
    total_size = 0

    with zipfile.ZipFile(args['MYSQL_FILE'], 'r') as zipf:
        file_count = len(zipf.namelist())
        for file in zipf.namelist():
            total_size += zipf.getinfo(file).file_size

        with tqdm(total=total_size, unit='B', ncols=70, unit_scale=True) as pbar:
            for file in zipf.namelist():
                extract_path = os.path.join(BACKUP_FOLDER, file)
                with zipf.open(file, 'r') as zf, open(extract_path, 'wb') as outfile:
                    block_size = 10 * 1024 * 1024  # Tamaño del bloque para leer y escribir
                    while True:
                        data = zf.read(block_size)
                        if not data:
                            break
                        outfile.write(data)
                        pbar.update(len(data))
                
def restore_data(args):
    print('Restaurando datos...')
  
    with open(BACKUP_FOLDER + "/tables.json", 'r',  encoding='utf-8') as openfile:
       table_sizes = json.load(openfile)
       
    total_row_count = 0
    for item in table_sizes:
        file_path = os.path.join(BACKUP_FOLDER, 'datos', item['table'] + '_#0001.sql')
        if os.path.isfile(file_path):
            total_row_count += item['rows']

    # total_row_count = sum(item['rows'] for item in table_sizes)
    with tqdm(total=total_row_count, ncols=70) as progress_bar:
        for table in table_sizes:
            restore_table_data(args, table, progress_bar)

def main(args):
    global BACKUP_FOLDER
    # Parse command line arguments
    parser = argparse.ArgumentParser(description='Python script with arguments')
    parser.add_argument('--port', default=None, help='The port for the database.')
    parser.add_argument('--user', default=None, help='The user for the database.')
    parser.add_argument('--host', default=None, help='The host for the database. Defaults to 127.0.0.1')
    parser.add_argument('--password', default=None, help='The password for the database. If ommited, values from db_settings.ini are used.')
    parser.add_argument('--database', default=None, help='The name of the database.')
    parser.add_argument('--input', default=None, help='The dump file to be restored (zipped)')
    parser.add_argument('--folder', default=None, help='The dump folder to be restored (expanded)')
    parser.add_argument('--data', default=None, help='Indicates structure is already created)')
    command_args = parser.parse_args()

    if not command_args.database or (not command_args.input and not command_args.folder):
        stop = False
        if not command_args.database and not args['DATABASE']:
            print("No database name provided. Please specify a database name using the '--database' argument.")
            stop = True
        if not command_args.input and not command_args.folder:
            print("No input file or folder was provided. Please specify an input file or folder using the '--input' or '--folder' arguments.")
            stop = True
        if stop:
            return
        
    # Assign values from arguments to variables
    if not command_args.host:
        if (args['HOST']):
          args['MYSQL_HOST'] = args['HOST']
    else:
        args['MYSQL_HOST'] = command_args.host

    if not command_args.database:
        print(1)
        args['MYSQL_DB'] = args['DATABASE']
    else:
        print(2)
        args['MYSQL_DB'] = command_args.database

    args['MYSQL_FILE'] = command_args.input
   
    args['FOLDER'] = command_args.folder
    args['DATA_ONLY'] = command_args.data
    
    user = command_args.user
    password = command_args.password
    if user != None:
        args['MYSQL_USER'] = user
    if command_args.port != None:
        args['MYSQL_PORT'] = command_args.port
    if password != None:
        args['MYSQL_PASSWORD'] = password
 
    unzipFiles = "N"
    if not args['FOLDER']:
      if os.path.isdir(BACKUP_FOLDER) and os.path.isfile(os.path.join(BACKUP_FOLDER, "database_tables.sql")):
       while True:
           unzipFiles = input("Ya existe la carpeta de archivos expandidos. ¿Desea reutilizarla e intentar ejecutarlos? (S/N/Cancel)?: ")                 
           if unzipFiles.upper() == "S" or unzipFiles.upper() == "N":
               break
           if unzipFiles.upper() == "C" or unzipFiles.upper() == "Cancel":
               sys.exit()
    if args['FOLDER']:
      BACKUP_FOLDER = args['FOLDER']
      if not os.path.isdir(BACKUP_FOLDER):
        print("The folder '" + BACKUP_FOLDER + "' does not exist.")
        return
    
    # EMPIEZA A EJECUTAR
    print("-------------------------------")
    print("DATABASE: " + args['MYSQL_DB'])
    print("USER: " + args['MYSQL_USER'])
    print("HOST: " + args['MYSQL_HOST'])
    print("-------------------------------")
    
    if unzipFiles.upper() == "N" and not args['FOLDER']:
       create_backup_folder()
       unzip_backup_folder(args)
    
    if not args['DATA_ONLY']:
      restore_database_structure(args)
  
    restore_data(args)

    if not args['FOLDER']:
      if unzipFiles.upper() == "N":
        remove_backup_folder()
      else:      
        # está reutilizando
        while True:
           removeFiles = input("¿Desea eliminar la carpeta temporal? (S/N/Cancel)?: ")  
           if removeFiles.upper() == "S":
               remove_backup_folder()               
           if removeFiles.upper() == "S" or removeFiles.upper() == "N":
               break
           if removeFiles.upper() == "C" or removeFiles.upper() == "Cancel":
               sys.exit()
 

if __name__ == '__main__':
    main(args)
    print('Listo')
