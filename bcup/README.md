# bcup
Herramienta para realizar backup incremental de bases de datos MySQL. Realiza un backup incremental con granularidad de tablas (guarda las tablas con cambios). 

## Escenario

## Funcionamiento

## Sintaxis

Se puede correr desde src/bcup  como:

python bcup

(equivalente a python bcup backup --help)

También se puede hacer un zip con los archivos, y ejecutar:

python bcup.zip 

En windows, se puede compilar usando pyinstaller (pip install pyinstaller)

- pyinstaller --onefile bcup\__main__.py

## Limitaciones

No distribuye la eliminación de tablas, solo novedades y cambios.

## Contacto

Desarrollado por Pablo De Grande y Rodrigo Queipo.

Contacto: pablodg@gmail.com
