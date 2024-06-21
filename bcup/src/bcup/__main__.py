import sys
from backup import Backup
from restore import Restore
from push import Push
from settings import Settings


def main():
    if len(sys.argv) < 2 or sys.argv[1] == '-h' or sys.argv[1] == '--help':
        usage()
    elif sys.argv[1] == "backup":
        Backup().main()
    elif sys.argv[1] == "restore":
        Restore().main()
    elif sys.argv[1] == "push":
        Push().main()
    else:
        usage()


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
