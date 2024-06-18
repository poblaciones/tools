import argparse
import configparser
import subprocess
import os
import sys
from datetime import datetime


class Settings:

    CONFIG_FILE = 'settings.ini'

    def __init__(self):
        self.db_host = "localhost"
        self.db_port = 3306
        self.db_name = ""
        self.db_pass = ""
        self.db_user = ""

        self.mysql_bin_path = ""
        self.mysql = ""
        self.mysqldump = ""

        self.datos_path = "datos"
        self.estructura_path = "estructura"
        self.output = ""

        # solo por command line
        self.input_path = "tmp_restore"
        self.merge_path = "tmp_merge"
        self.input = ""

        self.from_date = '2000-01-01'
        self.include_tables = []
        self.exclude_tables = []
        self.output_path = "tmp_backup"
        self.resume = False
        self.skip_routines = False
        self.skip_structure = False
        self.skip_data = False
        self.quiet = False
        self.autoname = False

    def parse_command_line_backup(self):
        parser = argparse.ArgumentParser(prog=f"python {sys.argv[0]}", description='Hace backup incremental de tablas mysql.')
        self.database_args(parser)
        parser.add_argument('--from_date', default=self.from_date, help='La fecha desde para comenzar el backup. Formato aaaa-dd-mm')
        parser.add_argument('--quiet', action='store_true', required=False, help='No muestra mensajes.')
        parser.add_argument('--resume', action='store_true', help='Intenta continuar el backup desde donde quedó.')
        parser.add_argument('--output', default=None, help='El archivo zip a ser creado con el backup')
        parser.add_argument('--output_path', default=None, help=f'El directorio de salida (puede usarse como sesión combinado con --resume) (default: {self.output_path}).')
        parser.add_argument('--autoname', action='store_true', help='Crear archivos en base a la fecha actual.')
        parser.add_argument('--include_tables', nargs='+', default=[], help='Lista de tablas a incluir (separadas por comas). Puede usarse * como wildcard y comenzar con ! para negación.')
        parser.add_argument('--exclude_tables', nargs='+', default=[], help='Lista de tablas a excluir (separadas por comas). Puede usarse * como wildcard.')
        args = parser.parse_args()

        self.parse_database_args(args)

        if args.from_date:
            self.from_date = args.from_date

        self.quiet = args.quiet
        self.resume = args.resume

        if args.output_path:
            self.output_path = args.output_path

        self.datos_path = Settings.join_path(self.output_path, self.datos_path)
        self.estructura_path = Settings.join_path(self.output_path, self.estructura_path)

        if args.autoname:
            name = datetime.now().strftime('%Y%m%d_%H%M%S')
            self.output = 'bkdb-' + name + ".zip"
        elif args.output:
            self.output = args.output
        elif not self.output:
            self.output = self.db_name + ".zip"

        if args.exclude_tables:
            self.exclude_tables = args.exclude_tables[0].split(',')
        if args.include_tables:
            self.include_tables = args.include_tables[0].split(',')

    def parse_command_line_restore(self):
        parser = argparse.ArgumentParser(prog=f"python {sys.argv[0]}", description='Hace restore de backups creados con este script.')
        self.database_args(parser)
        parser.add_argument('--input', default=None, help='El archivo zip para restaurar.')
        parser.add_argument('--input_path', default=None, help='El directorio a restaurar, si no se pasa --input.')
        args = parser.parse_args()

        self.parse_database_args(args)

        if not args.input and not args.input_path:
            sys.exit("Debe indicar un archivo zip para --input o un directorio para --input_path.")

        if args.input and args.input_path:
            sys.exit("Debe indicar un archivo zip para --input o un directorio para --input_path. No ambos.")
        if args.input_path and not os.path.isdir(args.input_path):
            sys.exit(f"No se encontró el directorio {args.input_path}.")
        if args.input and not os.path.isfile(args.input):
            sys.exit(f"No se encontró el archivo {args.input}.")

        if args.input_path:
            self.input_path = args.input_path
        if args.input:
            self.input = args.input

        self.datos_path = Settings.join_path(self.input_path, self.datos_path)
        self.estructura_path = Settings.join_path(self.input_path, self.estructura_path)

        if args.input:
            self.input = args.input
        elif not self.input:
            self.input = self.db_name + ".zip"

    def parse_command_line_merge(self):
        parser = argparse.ArgumentParser(prog=f"python {sys.argv[0]}", description='Hace merge de varios backups incrementales creados con este script.')
        parser.add_argument('--input', default=None, help='El archivo zip full backup sobre el que se va a mergear.')
        parser.add_argument('--source_path', default=None, help='El directorio con todos los archivos para mergear.')
        parser.add_argument('--merge_path', default=None, help='El directorio para mergear.')
        # parser.add_argument('--files', nargs='+', default=[], help='Lista de archivos a mergear (separadas por comas).')
        parser.add_argument('--zip', action='store_true', required=False, help='Comprime al final.')
        parser.add_argument('--keep_merge_path', action='store_true', required=False, help='No borra el directorio de destino al final.')
        parser.add_argument('--keep_zip_files', action='store_true', required=False, help='No borra los archivos zip que fue mergeando.')
        parser.add_argument('--join_structure', action='store_true', required=False, help='Concatena archivos separados de estructura en uno en el raiz.')

        args = parser.parse_args()

        if args.files:
            self.files = args.files[0].split(',')

        if not args.input and not args.source_path:
            sys.exit("Debe indicar un archivo zip para --input y/o un directorio con zips para --source_path.")

        if args.source_path and not os.path.isdir(args.source_path):
            sys.exit(f"No se encontró el directorio {args.source_path}.")
        if args.input and not os.path.isfile(args.input):
            sys.exit(f"No se encontró el archivo {args.input}.")

        self.join_structure = args.join_structure
        self.zip = args.zip
        self.keep_merge_path = args.keep_merge_path
        self.keep_zip_files = args.keep_zip_files
        self.source_path = args.source_path

        if args.input_path:
            self.input_path = args.input_path
        if args.merge_path:
            self.merge_path = args.merge_path

    def database_args(self, parser):
        parser.add_argument('--host', default=self.db_host, help=f'Dirección del host (default: {self.db_host})')
        parser.add_argument('--port', default=self.db_port, help=f'El puerto de la base de datos (default: {self.db_port}).')
        parser.add_argument('--user', default=None, help='El usuario de la base de datos.')
        parser.add_argument('--password', default=None, help=f'La constraseña para la base de datos. Si se omite se toma el valor de {Settings.CONFIG_FILE}.')
        parser.add_argument('--database', default=None, help='El nombre de la base de datos.')
        parser.add_argument('--skip_routines', action='store_true', help='Omite la exportación de funciones (es muy lento).')
        parser.add_argument('--skip_data', action='store_true', help='Exporta solo la estructura sin los datos.')
        parser.add_argument('--skip_structure', action='store_true', help='Exporta los datos sin la estructura.')

    def parse_database_args(self, args):
        if not args.database and not self.db_name:
            sys.exit("Falta nombre de base de datos. Especifique usando el argumento '--database'.")

        self.skip_routines = args.skip_routines
        self.skip_structure = args.skip_structure
        self.skip_data = args.skip_data

        if args.user:
            self.db_user = args.user
        if args.password:
            self.db_pass = args.password
        if args.database:
            self.db_name = args.database
        if args.host:
            self.db_host = args.host
        if args.port:
            self.db_port = args.port

    def parse_config_file(self):
        if not os.path.isfile(Settings.CONFIG_FILE):
            return

        config = configparser.ConfigParser()
        config.read(Settings.CONFIG_FILE)
        self.db_user = config['client']['user']
        self.db_pass = config['client']['password'].strip('\"')

        if 'extra' in config:
            if 'database' in config['extra']:
                self.db_name = config['extra']['database']
            if 'host' in config['extra']:
                self.db_host = config['extra']['host']
            if 'port' in config['extra']:
                self.db_port = config['extra']['port']

            if 'output' in config['extra']:
                self.output = config['extra']['output']

            if 'mysql_bin_path' in config['extra']:
                self.mysql_bin_path = config['extra']['mysql_bin_path']
            elif sys.platform.startswith('win'):
                self.mysql_bin_path = 'C:/Program Files/MySQL/MySQL Server 5.7/bin'

        self.mysql = Settings.join_path(self.mysql_bin_path, "mysql")
        self.mysqldump = Settings.join_path(self.mysql_bin_path, "mysqldump")

        # Chequea que se pueda correr el ejecutable: mysqldump
        ret = subprocess.Popen(self.mysqldump + " --version", shell=True, stdout=subprocess.PIPE).stdout.read()
        if b"mysqldump " not in ret:
            sys.exit("El ejecutable mysqldump no se encontró o falló.")

        # Chequea que se pueda correr el ejecutable: mysql
        ret = subprocess.Popen(self.mysql + " --version", shell=True, stdout=subprocess.PIPE).stdout.read()
        if b"mysql " not in ret:
            sys.exit("El ejecutable mysql no se encontró o falló.")

    def join_path(*parts):
        return "/".join(parts).replace("\\", "/").replace("//", "/")
