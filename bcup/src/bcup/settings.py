import argparse
import configparser
import os
import subprocess
import sys
from datetime import datetime
from glob import glob


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

        self.tables_path = "tables"
        self.done_path = "done"
        self.date = datetime.now().strftime('%Y%m%d_%H%M%S')
        self.date = datetime.now().strftime('%Y%m%d_%H%M%S')

        self.has_ini = True

        # solo por command line

        # backup y restore
        self.skip_routines = False

        # backup
        self.output = ""
        self.output_path = "tmp_backup"
        self.from_date = '2000-01-01'
        self.include_tables = []
        self.exclude_tables = []
        self.forced_tables = []
        self.resume = False
        self.step_by_step = False
        self.quiet = False
        self.zip = False

        # restore
        self.input = ""
        self.input_path = "tmp_restore"
        self.move_done = False

        # push
        self.days = -1
        self.dest = ""
        self.source = ""
        self.source_path = ""

    def parse_command_line_backup(self, show_help=False):
        parser = argparse.ArgumentParser(prog=f"python {sys.argv[0]} backup", description='Creates incremental backups of full mysql tables.')
        parser.add_argument('backup', default='backup', help=argparse.SUPPRESS)
        self.database_args(parser)
        parser.add_argument('--from_date', default=self.from_date, help='Last changed date to start backup. Format yyyy-mm-dd.')
        parser.add_argument('--quiet', action='store_true', required=False, help='Disables messages.')
        parser.add_argument('--resume', action='store_true', help='Resume the backup.')
        parser.add_argument('--step_by_step', action='store_true', help='Advances one step by execution.')
        parser.add_argument('--output', default=None, help='Name for backup (default: [database name]-[ISO date]). Bcup will create a directory or a zip file named after this setting.')
        parser.add_argument('--output_path', default=None, help=f'Location to create the output directory or zip file --output (default: {self.output_path}).')
        parser.add_argument('--include_tables', nargs='+', default=[], help='Comma separated list of tables to include. Can use * as wildcard and start with ! for negation.')
        parser.add_argument('--exclude_tables', nargs='+', default=[], help='Comma separated list of tables to exclude. Can use * as wildcard.')
        parser.add_argument('--forced_tables', nargs='+', default=[], help='Comma separated list of tables to always include when using from_date. Can use * as wildcard.')
        parser.add_argument('--zip', action='store_true', help='Adds full backup to zip file without compression and removes backup path.')

        if show_help:
            parser.print_help()
            return

        args = parser.parse_args()

        self.parse_database_args(args)

        if args.from_date:
            self.from_date = args.from_date

        self.quiet = args.quiet
        self.resume = args.resume
        self.step_by_step = args.step_by_step
        self.zip = args.zip

        if args.output_path:
            self.output_path = args.output_path

        # El directorio de salida salvo explicitado es basado en la fecha de creación del backup,
        # entonces si hace resume tiene que buscar el último directorio con el formato de base-fecha
        if args.output:
            self.output = args.output
        else:
            if not self.resume:
                self.output = f"{self.db_name}-{self.date}"
            else:
                paths = [path for path in sorted(glob(Settings.join_path(self.output_path, self.db_name + "-*"))) if os.path.isdir(path)]
                if not paths:
                    sys.exit("Path not found.")
                self.output = os.path.basename(paths[-1])

        self.output_path = Settings.join_path(self.output_path, self.output)
        self.tables_path = Settings.join_path(self.output_path, self.tables_path)

        if args.exclude_tables:
            self.exclude_tables = args.exclude_tables[0].split(',')
        if args.include_tables:
            self.include_tables = args.include_tables[0].split(',')
        if args.forced_tables:
            self.forced_tables = args.forced_tables[0].split(',')


    def parse_command_line_restore(self, show_help=False):
        parser = argparse.ArgumentParser(prog=f"python {sys.argv[0]} restore", description='Restores backups created with this script.')
        parser.add_argument('restore', default='restore', help=argparse.SUPPRESS)
        self.database_args(parser)
        parser.add_argument('--input', default=None, help='Zip file to restore.')
        parser.add_argument('--input_path', default=None, help='Path to restore.')
        parser.add_argument('--move_done', action='store_true', required=False, help='Moves restored zip files to done directory.')

        if show_help:
            parser.print_help()
            return

        args = parser.parse_args()

        self.parse_database_args(args)

        if not args.input and not args.input_path:
            sys.exit("You must provide a zip file for --input or a path for --input_path.")

        if args.input and args.input_path:
            sys.exit("You must provide a zip file for --input or a path for --input_path. Not both.")
        if args.input_path and not os.path.isdir(args.input_path):
            sys.exit(f"Path not found: {args.input_path}.")
        if args.input and not os.path.exists(args.input):
            sys.exit(f"File not found: {args.input}.")

        if args.input_path:
            self.input_path = args.input_path
        if args.input:
            self.input = args.input

        self.move_done = args.move_done
        self.tables_path = Settings.join_path(self.input_path, self.tables_path)
        self.done_path = Settings.join_path(self.input_path, self.done_path)

    def parse_command_line_push(self, show_help=False):
        parser = argparse.ArgumentParser(prog=f"python {sys.argv[0]} push", description='Pushes one or many backups to dest path.')
        parser.add_argument('push', default='push', help=argparse.SUPPRESS)
        parser.add_argument('--dest', default=None, help='Destination path.')
        parser.add_argument('--source', default=None, help='Source path.')
        parser.add_argument('--source_path', default=None, help='Source path but with many backups.')
        parser.add_argument('--days', default=self.days, help='Number of days in the past to start pushing backups (skip less than --days).')

        if show_help:
            parser.print_help()
            return

        args = parser.parse_args()

        if not args.source and not args.source_path:
            sys.exit("You must provide a backup path for --source or a path with many backups for --source_path.")

        if args.source and args.source_path:
            sys.exit("You must provide a backup path for --source or a path with many backups for --source_path. Not both.")

        if args.source and not os.path.isdir(args.source):
            sys.exit(f"Path not found: {args.source}.")

        if args.source_path and not os.path.isdir(args.source_path):
            sys.exit(f"Path not found: {args.source_path}.")

        if args.dest and not os.path.isdir(args.dest):
            sys.exit(f"Path not found: {args.dest}.")

        self.days = args.days

        if args.source:
            self.source = args.source
        if args.source_path:
            self.source_path = args.source_path
        if args.dest:
            self.dest = args.dest

    def database_args(self, parser):
        parser.add_argument('--host', default=self.db_host, help=f'Host address (default: {self.db_host})')
        parser.add_argument('--port', default=self.db_port, help=f'Database port (default: {self.db_port}).')
        parser.add_argument('--user', default=None, help='Database user.')
        parser.add_argument('--password', default=None, help=f'Database password. If omitted it should be in configuration file {Settings.CONFIG_FILE}.')
        parser.add_argument('--database', default=None, help='Database name.')
        parser.add_argument('--skip_routines', action='store_true', help='Skip routines.')

    def parse_database_args(self, args):
        if not args.database and not self.db_name:
            sys.exit("Database name missing (--database).")

        if not args.user and not self.db_user:
            sys.exit("Username missing (--user).")

        if not args.password and not self.db_pass:
            sys.exit(f"Password missing, it's recommended to use the configuration file \"{Settings.CONFIG_FILE}\""
                     ", it's possible but unsafe to pass it by parameter (--password).")

        self.skip_routines = args.skip_routines

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
        if not os.path.exists(Settings.CONFIG_FILE):
            self.myslq_binaries()
            self.has_ini = False
            return

        config = configparser.ConfigParser()
        config.read(Settings.CONFIG_FILE)
        self.db_user = config['client']['user']
        self.db_pass = config['client']['password'].strip('\"')

        path = ""
        if 'extra' in config:
            if 'database' in config['extra']:
                self.db_name = config['extra']['database']
            if 'host' in config['extra']:
                self.db_host = config['extra']['host']
            if 'port' in config['extra']:
                self.db_port = config['extra']['port']

            if 'mysql_bin_path' in config['extra']:
                path = config['extra']['mysql_bin_path']

        self.myslq_binaries(path)

    def myslq_binaries(self, path=""):
        if path:
            self.mysql_bin_path = path
        elif sys.platform.startswith("win"):
            self.mysql_bin_path = "C:/Program Files/MySQL/MySQL Server 5.7/bin"

        self.mysql = Settings.join_path(self.mysql_bin_path, "mysql")
        self.mysqldump = Settings.join_path(self.mysql_bin_path, "mysqldump")

        # Chequea que se pueda correr el ejecutable: mysqldump
        ret = subprocess.Popen(f"\"{self.mysqldump}\" --version", shell=True, stdout=subprocess.PIPE).stdout.read()
        if b"mysqldump " not in ret:
            sys.exit(f"mysqldump not found or failed. Path: \"{self.mysqldump}\"")

        # Chequea que se pueda correr el ejecutable: mysql
        ret = subprocess.Popen(f"\"{self.mysql}\" --version", shell=True, stdout=subprocess.PIPE).stdout.read()
        if b"mysql " not in ret:
            sys.exit(f"mysql not found or failed. Path: \"{self.mysql}\"")

    def join_path(*parts):
        ret = "/".join(parts).replace("\\", "/").replace("//", "/")
        if len(parts) > 0 and parts[0] == "":
            return ret[1:]
        return ret
