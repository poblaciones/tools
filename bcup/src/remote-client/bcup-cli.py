#! /usr/bin/env python3
import requests
import json
import argparse
import os
import sys
from tqdm import tqdm
import time
import urllib3

# Desactivar advertencias de SSL inseguro
urllib3.disable_warnings(urllib3.exceptions.InsecureRequestWarning)

PARAMS = {
    "v": "1"
}

OUTPUT_FOLDER = "./partial_backups"
MERGED_FOLDER = "./target_backup"


def start_backup(args, timestamp):
    params = PARAMS
    params['d'] = timestamp
    params['s'] = args.key
    response = ''
    try:
        response = requests.get(f"{args.server}services/api/startBackup", params=params, verify=False)
        data = response.json()
    except Exception as e:
            print(f"El inicio del backup no fue exitoso.")
            print(f"Error: \n{e}")
            print(f"Respuesta: \n{response}")
            sys.exit()

    return data["id"]

def step_backup(args, session_id):
    params = {**PARAMS, "id": session_id}
    params['s'] = args.key
    response = ''
    params['f'] = '1'
    progress_bar = tqdm(total=100, desc="Backup Progress", bar_format='{l_bar}{bar}| {n_fmt}/{total_fmt}')
    try:
      while True:
        #print('services/api/stepBackup')
        response = requests.get(f"{args.server}services/api/stepBackup", params=params, verify=False)
        data = response.json()
        # print(data)
        if data["Status"] == "COMPLETE":
            progress_bar.close()
            return

        progress = int(data["currentBytes"] / data["totalBytes"] * 10000) / 100
        progress_bar.update(progress - progress_bar.n)
        params['f'] = ''
        if args.sleep and args.sleep > 0:
            time.sleep(args.sleep)
    except Exception as e:
            print(f"El backup no fue exitoso.")
            print(f"Error: \n{e}")
            print(f"Respuesta: \n{response}")
            sys.exit()

def download_files(args, session_id):
    params = {**PARAMS, "id": session_id}
    params['s'] = args.key

    # Obtener información sobre los archivos
    params["n"] = 0
    response = requests.get(f"{args.server}services/api/stepFiles", params=params, verify=False)
    file_info = response.json()
    total_size = file_info["Filessize"]
    file_count = file_info["Filecount"]

    progress_bar = tqdm(total=total_size, unit='B', unit_scale=True, desc="Downloading Files",
                        bar_format='{l_bar}{bar}| {n_fmt}/{total_fmt} [{rate_fmt}{postfix}]')

    for n in range(1, file_count + 1):
        params["n"] = n
        response = requests.get(f"{args.server}services/api/stepFiles", params=params, verify=False)

        if response.headers.get('Content-Type') == 'application/json':
            data = response.json()
            if data.get('Status') == 'COMPLETE':
                break
        else:
            filename = response.headers.get('Content-Disposition').split('filename=')[1].strip('"')
            filepath = os.path.join(OUTPUT_FOLDER, filename)
            os.makedirs(os.path.dirname(filepath), exist_ok=True)

            with open(filepath, 'wb') as f:
                f.write(response.content)

            progress_bar.update(len(response.content))

    progress_bar.close()

def getParams():
    parser = argparse.ArgumentParser(prog=f"python {sys.argv[0]}", description='Creates a remote incremental backup of Poblaciones.')
    parser.add_argument('--server', default=None, help='Base URL of the Poblaciones server (e.g. https://server.poblaciones.org/).')
    parser.add_argument('--key', default=None, help='Security key to authenticate.')
    parser.add_argument('--resume_session', default=None, help='Session to resume.')
    parser.add_argument('--sleep', default=None, type=int, help='Seconds to sleep between requests (optional).')

    args = parser.parse_args()
    return args

def get_latest_timestamp():
    return ''

def main():
    args = getParams()
    if not args.server:
        sys.exit("You must provide the --server parameter.")
    if not args.key:
        sys.exit("You must provide the --key parameter.")
    if not args.server.endswith('/'):
        args.server += '/'

    timestamp = get_latest_timestamp()
    if not args.resume_session:
        print(f"Obteniendo backup de: {args.server}")
        session_id = start_backup(args, timestamp)
        print(f"Sesión iniciada: {session_id}")
    else:
        session_id = args.resume_session
        print(f"Continuando sesión: {session_id}")
    if args.sleep and args.sleep > 0:
        print(f"Pausa entre pedidos: {args.sleep} segundos")

    step_backup(args, session_id)
    print("Backup completado")

    download_files(args, session_id)
    print("Descarga de archivos completada")

if __name__ == "__main__":
    main()