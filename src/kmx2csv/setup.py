# setup.py build
import os
import sys
from cx_Freeze import setup, Executable

PY_PATH = './libs/'

os.environ['TCL_LIBRARY'] = os.path.join(PY_PATH, 'tcl8.6')
os.environ['TK_LIBRARY'] = os.path.join(PY_PATH, 'tk8.6')

icon_file = 'ico.xbm'
if sys.platform.startswith('win'):
    icon_file = 'ico.ico'

setup(
    name="procesador",
    version="1.0",
    description="procesador",
    options={
        "build_exe": {
            "includes": [],
            "packages": ["tkinter"],
            "include_files": [icon_file,
                              "kmx2csv3.py",
                              os.path.join(PY_PATH, 'tcl86t.dll'),
                              os.path.join(PY_PATH, 'tk86t.dll')]}},
    executables=[Executable("executeRun.py", base="Win32GUI")])
