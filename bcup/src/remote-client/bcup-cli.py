#! /usr/bin/env python3
import requests
import json
import argparse
import os
from datetime import datetime
import sys
from tqdm import tqdm
import time
import urllib.parse
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
        full_url = url_with_params('services/api/startBackup', params)
        print(f"Url: {args.server}{full_url}")
        data = response.json()
    except Exception as e:
            print(f"El inicio del backup no fue exitoso.")
            print(f"Error: \n{e}")
            print(f"Respuesta: \n{response}")
            sys.exit()

    #print('id: ' + data["id"])
    #sys.exit()

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
            full_url = url_with_params('services/api/stepBackup', params)
            print(f"Url: {args.server}{full_url}")
            print(f"Error: \n{e}")
            print(f"Respuesta: \n{response}")
            sys.exit()

def url_with_params(url, parametros):
    # Codificar cada parámetro
    parametros_encoded = {k: urllib.parse.quote(str(v)) for k, v in parametros.items()}
    # Crear la cadena de parámetros
    parametros_string = "&".join(f"{k}={v}" for k, v in parametros_encoded.items())
    # Añadir los parámetros a la URL
    if "?" in url:
        return f"{url}&{parametros_string}"
    else:
        return f"{url}?{parametros_string}"

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
        response = requests.head(f"{args.server}services/api/stepFiles", params=params, verify=False)
        type = response.headers.get('Content-Type')
        disposition = response.headers.get('Content-Disposition')

        if response.status_code != 200:
            print(f"El backup no fue exitoso. Falló al obtenerse archivos, con código {response.status_code}.")
            print(f"Respuesta: \n{response}")
            sys.exit()

        if not disposition:
            print(f"El backup no fue exitoso. Falló al obtenerse una respuesta sin nombre de archivo cliente.")
            print(f"Respuesta: \n{response}")
            sys.exit()

        filename = disposition.split('filename=')[1].strip('"')
        filepath = os.path.join(OUTPUT_FOLDER, filename)

        if os.path.exists(filepath) and os.path.getsize(filepath) > 0:
            # ya lo tiene
            progress_bar.update(os.path.getsize(filepath));
        else:
            # lo trae
            response = requests.get(f"{args.server}services/api/stepFiles", params=params, verify=False)
            if type == 'application/json' and not filename.endswith('.json'):
                data = response.json()
                if data.get('Status') == 'COMPLETE':
                    break
            else:
                os.makedirs(os.path.dirname(filepath), exist_ok=True)

                with open(filepath, 'wb') as f:
                    f.write(response.content)

                progress_bar.update(len(response.content))

    progress_bar.close()

def getParams():
    parser = argparse.ArgumentParser(prog=f"python {sys.argv[0]}", description='Creates a remote incremental backup of Poblaciones.')
    parser.add_argument('--server', default=None, help='Base URL of the Poblaciones server (e.g. https://server.poblaciones.org/).')
    parser.add_argument('--key', default=None, help='Security key to authenticate.')
    parser.add_argument('--timestamp', default=None, help='Date since data should be stored.')
    parser.add_argument('--timestamp_folder', default=None, help='Folder to recursively look for timestamp.txt files.')
    parser.add_argument('--resume_session', default=None, help='Session to resume.')
    parser.add_argument('--sleep', default=None, type=int, help='Seconds to sleep between requests (optional).')

    args = parser.parse_args()
    return args

def get_latest_timestamp(folder):
    lastest_timestamp = None
    for raiz, dirs, archivos in os.walk(folder):
        for archivo in archivos:
            if archivo == "timestamp.txt":
                ruta_completa = os.path.join(raiz, archivo)
                with open(ruta_completa, 'r') as f:
                    contenido = f.read().strip()
                    fecha = datetime.strptime(contenido, "%Y%m%d_%H%M%S")
                    if lastest_timestamp is None or fecha > lastest_timestamp:
                        lastest_timestamp = fecha
    if lastest_timestamp:
        return lastest_timestamp.strftime("%Y-%m-%d")
    else:
        return ''

def main():
    args = getParams()
    if not args.server:
        sys.exit("Debe indicarse el parámetro --server.")
    if not args.key:
        sys.exit("Debe indicarse el parámetro --key.")
    if not args.server.endswith('/'):
        args.server += '/'
    if args.timestamp and args.timestamp_folder:
        print(f"timestamp y timestamp_folder no deben indicarse al mismo tiempo.")
        sys.exit()

    timestamp = ''
    if args.timestamp:
        timestamp = args.timestamp
    elif args.timestamp_folder:
        timestamp = get_latest_timestamp(args.timestamp_folder)

    if timestamp and args.resume_session:
        print(f"Al continuarse una sesión no debe indicarse un timestamp.")
        sys.exit()

    start = time.time()
    fecha_hora_actual = datetime.now().strftime("%Y-%m-%d %H:%M:%S")
    print(f"Iniciando [{fecha_hora_actual}]")

    if timestamp:
        print(f"Resguardando datos desde: {timestamp}")
    elif args.timestamp_folder:
        print(f"Haciendo backup completo porque no se encontró una fecha de última copia en {args.timestamp_folder}")

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

    print(f"Finalizando [{fecha_hora_actual}]")
    print(f"Tiempo total: {round(time.time() - start, 2)} secs. ---")

if __name__ == "__main__":
    main()