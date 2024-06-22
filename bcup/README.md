# bcup
Herramienta para realizar backup (opción backup) incremental de bases de datos MySQL. También restaura (opción restore) los backups y los consolida (opción push)

Realiza un backup incremental con granularidad de tablas (guarda las tablas con cambios), tiene opción para continuar backups interrumpidos.

## Escenario

## Sintaxis

Se puede correr desde src/bcup como:

```python bcup```

(equivalente a ```python bcup --help```)

para ayuda de cada función

```
python bcup backup --help
python bcup restore --help
python bcup push --help
```

También se puede hacer un zip con los archivos, y ejecutar:

```python bcup.zip```

Se puede compilar usando pyinstaller (```pip install pyinstaller```)

```pyinstaller --onefile bcup/__main__.py```

## Requisitos

Python 3.6, pymysql y tqdm 
(```pip install pymysql tqdm```)

## Funcionamiento
Tanto backup como restore requieren conectarse a la base de datos, es recomendable crear un archivo de configuración settings.ini con las credenciales de usuarios, también se puede agregar el nombre de la base de datos y el directorio de los archivos binarios de mysql si estos no estuvieran en el PATH o en la ubicación default. ver [settings.ini.example](https://github.com/poblaciones/tools/blob/master/bcup/src/settings.ini.example)

De todos modos, todos los parámetros pueden pasarse por línea de comandos, este instructivo supone la existencia del archivo de configuración.

### Backup
El backup crea un directorio default ```tmp_backup``` que es modificable con la opción ```--output_path``` en donde creará un directorio con el nombre de la base de datos y la fecha de creación de ese backup (a menos que se indique otro nombre para ese directorio con la opción ```--output```), en ese directorio guardará un archivo zip con las funciones de sql (pueden omitirse con ```--skip_routines```) y en un subdirectorio ```tables``` un por tabla con dos o más sqls, uno con la estructura y triggers de la tabla y otro con los datos, si la tabla es muy grande la partirá en partes separadas en archivos sql. También habrá un archivo timestamp.txt que indica el momento en el que se creó el backup, tambien un archivo json con metadata e información de las tablas backupeadas. También un archivo ```unfinished``` que se elimina al terminar el backup.

El backup puede crearse de toda la base o pueden filtrarse tablas con ```--include_tables``` y ```--exclude_tables``` ambos comandos soportan ```*``` como wildcard, también pueden filtrarse las tablas que han sido modificadas desde una fecha con ```--from_date```.

Si el backup se interrumpe puede continuarse con ```--resume```, es importante llamar al script con los mismos parámetros cuando se intenta continuar un backup. 

Cuando finaliza opcionalmente puede comprimirse todo en un zip con ```--zip```.

### Restore
La restauración a la base debe hacerse con backups creados con este script para funcionar correctamente. Realiza un restore de las tablas (y funciones) backupeadas tomando un archivo zip con el backup ```--input``` o tomando un directorio de backup ```--input_path```

### Push
La consolidación de backups funciona juntando o pisando un backup más viejo con uno o varios más nuevos, renovando lo que el nuevo trae sin perder lo que el viejo tenía y el nuevo no actualizó.

Funciona con el directorio de un backup de destino ```--dest``` y, un directorio de origen ```--source```, o un directorio que contiene varios backups ```--source_path``` que cronológicamente serán consolidados con el destino. Si se incluye el parámetro ```--days``` sólo consolidará backups que tengan más días que los indicados. Por ejemplo, si un directorio tubiera diez backups hechos uno por día y se indica ```---days 4``` el script sólo consolidará los scripts creados hace cuatro o más días dejando entonces los cuatro más nuevos (suponiendo que el backup del día haya sido realizado también) y consolidando los otros seis.

## Limitaciones

No distribuye la eliminación de tablas, solo novedades y cambios.

## Contacto

Desarrollado por Pablo De Grande y Rodrigo Queipo.

Contacto: pablodg@gmail.com
