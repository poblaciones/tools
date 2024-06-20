import sys
from backup import Backup
from restore import Restore
from push import Push


def main():
    if len(sys.argv) < 2:
        sys.exit(f"Uso python {sys.argv[0]} [backup|restore|push] [params...]")
    if sys.argv[1] == "backup":
        Backup().main()
    elif sys.argv[1] == "restore":
        Restore().main()
    elif sys.argv[1] == "push":
        Push().main()


if __name__ == '__main__':
    main()
