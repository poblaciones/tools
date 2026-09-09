import sys
from backup import Backup
from restore import Restore
from push import Push
from settings import Settings
import time
from datetime import datetime

def main():
    # Verificar versión de Python
    if sys.version_info < (3, 6):
        print("Error: Este script requiere Python 3.6 o superior.")
        print(f"Versión actual: Python {sys.version_info.major}.{sys.version_info.minor}.{sys.version_info.micro}")
        print("Por favor, actualiza Python o ejecuta con: python3 script.py")
        sys.exit(1)

    start = time.time()
    fecha_hora_actual = datetime.now().strftime("%Y-%m-%d %H:%M:%S")
    print(f"Iniciando [{fecha_hora_actual}]")
    show_usage = False
    if len(sys.argv) < 2 or sys.argv[1] == '-h' or sys.argv[1] == '--help':
        usage()
        show_usage = True
    elif sys.argv[1] == "backup":
        Backup().main()
    elif sys.argv[1] == "restore":
        Restore().main()
    elif sys.argv[1] == "push":
        Push().main()
    else:
        usage()
        show_usage = True

    if not show_usage:
        print(f"Finalizando [{fecha_hora_actual}]")
        print(f"Tiempo total: {round(time.time() - start, 2)} secs. ---")


def usage():
    print(f"Usage: python {sys.argv[0]} [backup|restore|push] [params...]")
    print("\n\n")
    settings = Settings()
    settings.parse_command_line_backup(True)
    print("\n\n")
    settings.parse_command_line_restore(True)
    print("\n\n")
    settings.parse_command_line_push(True)


if __name__ == '__main__':
    main()
