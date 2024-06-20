import json
import os
import sys
import shutil
import time
from datetime import date, timedelta
from glob import glob
from settings import Settings
from tqdm import tqdm


class Push:

    def __init__(self):
        self.settings = Settings()

    def push_routines(self, source):
        routines = Settings.join_path(source, "routines.zip")
        if os.path.exists(routines):
            shutil.move(routines, Settings.join_path(self.settings.dest, "routines.zip"))

    def push_tables(self, source):
        print('Pusheando tablas...')
        full_source = Settings.join_path(source, self.settings.tables_path)
        if not os.path.isdir(full_source):
            return

        full_dest = Settings.join_path(self.settings.dest, self.settings.tables_path)
        if not os.path.isdir(full_dest):
            os.makedirs(full_dest, exist_ok=True)

        # Mueve los archivos
        files = sorted(glob(Settings.join_path(full_source, "*.zip")))
        with tqdm(total=len(files), ncols=70) as progress_bar:
            for file in files:
                shutil.move(file, Settings.join_path(full_dest, os.path.basename(file)))
                progress_bar.update()

    def push_json(self, source):
        print('Pusheando metadata...')
        source_json = Settings.join_path(source, "tables.json")
        if not os.path.exists(source_json):
            return

        dest_json = Settings.join_path(self.settings.dest, "tables.json")
        if not os.path.exists(dest_json):
            shutil.move(source_json, dest_json)
            return

        with open(source_json) as file:
            source_dict = json.load(file)
        with open(dest_json) as file:
            dest_dict = json.load(file)

        merged = dict(sorted({**dest_dict, **source_dict}.items()))
        with open(dest_json, "w", encoding='utf-8') as outfile:
            json.dump(merged, outfile, indent=2)

    def push_date_file(self, source):
        print('Moviendo archivo de fecha...')
        source_files = glob(Settings.join_path(source, "*.txt"))
        if not source_files:
            return

        files = glob(Settings.join_path(self.settings.dest, "*.txt"))
        for file in files:
            os.remove(file)
        for file in source_files:
            shutil.move(file, Settings.join_path(self.settings.dest, os.path.basename(file)))

    def get_temp_path(self, name, ext):
        path = f"tmp_{name.replace(ext, "")}"
        if os.path.isdir(path):
            shutil.rmtree(path)
        os.makedirs(path, exist_ok=True)

    def get_push_paths(self):
        cut_date = date.today() - timedelta(days=int(self.settings.days))
        cut_date = str(cut_date).replace("-", "") + "_000000"
        ret = []
        dirs = sorted(glob(Settings.join_path(self.settings.source_path, "*")))
        for d in dirs:
            if os.path.isdir(d):
                part = d.split("-")[-1]
                if part <= cut_date:
                    ret.append(d)
        return ret

    def push(self, paths):
        for path in paths:
            print(f"Pusheando {path}")
            self.push_routines(path)
            self.push_tables(path)
            self.push_json(path)
            self.push_date_file(path)
            shutil.rmtree(path)

    def main(self):
        self.settings.parse_command_line_push()

        start = time.time()

        if self.settings.source:
            self.push([self.settings.source])
        elif self.settings.source_path:
            self.push(self.get_push_paths())

        print("--- Tiempo total: %s seg. ---" % round(time.time() - start, 2))
