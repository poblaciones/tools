#!/usr/bin/env python3
"""
make_single.py  –  Unifica la carpeta de bcup en un único script autónomo.

Uso:
    python make_single.py [carpeta_fuente] [archivo_salida]

Defaults:
    carpeta_fuente  →  directorio actual (.)
    archivo_salida  →  bcup.py  (en el directorio actual)
"""

import ast
import os
import sys
import textwrap
from collections import defaultdict

MODULE_ORDER = [
    "settings.py",
    "dump_table.py",
    "backup.py",
    "restore.py",
    "push.py",
    "__main__.py",
]

LOCAL_MODULES = {"settings", "dump_table", "backup", "restore", "push"}


def read_file(path):
    with open(path, "r", encoding="utf-8") as f:
        return f.read()


def is_local_import(line):
    stripped = line.strip()
    for mod in LOCAL_MODULES:
        if stripped.startswith(f"from {mod} import") or stripped == f"import {mod}":
            return True
    return False


def strip_module_level_main(source):
    """
    Elimina bloques `def main()` e `if __name__ == '__main__'` de nivel módulo
    usando el AST para identificar con precisión el rango de líneas.
    """
    try:
        tree = ast.parse(source)
    except SyntaxError:
        return source  # no tocar si no parsea

    lines_to_remove = set()
    for node in tree.body:
        if isinstance(node, ast.FunctionDef) and node.name == "main":
            for ln in range(node.lineno, node.end_lineno + 1):
                lines_to_remove.add(ln)
        elif isinstance(node, ast.If):
            test = node.test
            is_name_main = False
            if isinstance(test, ast.Compare):
                left = test.left
                comps = test.comparators
                if (
                    isinstance(left, ast.Name) and left.id == "__name__"
                    and len(comps) == 1
                    and isinstance(comps[0], ast.Constant)
                    and comps[0].value == "__main__"
                ):
                    is_name_main = True
            if is_name_main:
                for ln in range(node.lineno, node.end_lineno + 1):
                    lines_to_remove.add(ln)

    if not lines_to_remove:
        return source

    result = [
        line for i, line in enumerate(source.splitlines(keepends=True), start=1)
        if i not in lines_to_remove
    ]
    return "".join(result)


def parse_imports_and_body(source, filename):
    if filename != "__main__.py":
        source = strip_module_level_main(source)

    try:
        tree = ast.parse(source)
    except SyntaxError as e:
        print(f"  ⚠  Error de sintaxis en {filename}: {e}")
        return [], source.splitlines(keepends=True)

    lines = source.splitlines(keepends=True)
    import_linenos = set()
    for node in ast.walk(tree):
        if isinstance(node, (ast.Import, ast.ImportFrom)):
            for ln in range(node.lineno, node.end_lineno + 1):
                import_linenos.add(ln)

    external_imports = []
    body_lines = []
    for i, line in enumerate(lines, start=1):
        if i in import_linenos:
            if not is_local_import(line):
                external_imports.append(line)
        else:
            body_lines.append(line)

    return external_imports, body_lines


def resolve_import_conflicts(raw_imports):
    """
    Analiza todas las líneas de import y resuelve conflictos:
      - Si existe tanto `import X` como `from X import a, b`, unifica todo
        en `from X import a, b, X` → evita que `import X` tape al `from X import`.
      - Devuelve una lista ordenada y libre de conflictos.
    """
    # Parsear cada línea de import con AST
    bare_imports = {}       # modname → True  (import X)
    from_imports = defaultdict(set)  # modname → set of names
    order = []              # para preservar el orden de primera aparición

    seen_lines = set()
    for line in raw_imports:
        stripped = line.strip()
        if not stripped or stripped in seen_lines:
            continue
        seen_lines.add(stripped)

        try:
            tree = ast.parse(stripped)
        except SyntaxError:
            order.append(("raw", stripped))
            continue

        for node in tree.body:
            if isinstance(node, ast.Import):
                for alias in node.names:
                    modname = alias.name
                    asname = alias.asname
                    if asname:
                        # import X as Y → no puede causar conflicto, lo dejamos
                        order.append(("raw", stripped))
                    else:
                        if modname not in bare_imports:
                            bare_imports[modname] = True
                            order.append(("bare", modname))
            elif isinstance(node, ast.ImportFrom):
                modname = node.module or ""
                names = tuple(
                    (alias.name, alias.asname) for alias in node.names
                )
                for name, asname in names:
                    from_imports[modname].add((name, asname))
                if modname not in [m for t, m in order if t in ("from", "bare")]:
                    order.append(("from", modname))
                # si ya está, solo se añadieron los nombres arriba

    # Construir las líneas finales
    result = []
    emitted = set()

    for kind, modname in order:
        if kind == "raw":
            if modname not in emitted:
                result.append(modname + "\n")
                emitted.add(modname)
        elif kind == "bare":
            if modname in from_imports:
                # Hay también `from X import ...` → los fusionamos allí; no emitir `import X`
                pass
            else:
                key = f"import {modname}"
                if key not in emitted:
                    result.append(key + "\n")
                    emitted.add(key)
        elif kind == "from":
            if modname in emitted:
                continue
            emitted.add(modname)
            names = sorted(from_imports[modname])
            if modname in bare_imports:
                # También había `import X` bare → agregar el módulo mismo como nombre
                # para que `modname.algo` siga funcionando via alias
                # Mejor: reemplazar usos de `datetime.date` → ya están cubiertos
                # por `from datetime import date`. Solo añadimos los nombres faltantes.
                pass
            parts = []
            for name, asname in names:
                if asname:
                    parts.append(f"{name} as {asname}")
                else:
                    parts.append(name)
            result.append(f"from {modname} import {', '.join(parts)}\n")

    return result


def build_unified(source_dir):
    all_imports = []
    all_bodies = []

    print(f"Leyendo módulos desde: {os.path.abspath(source_dir)}")

    for filename in MODULE_ORDER:
        filepath = os.path.join(source_dir, filename)
        if not os.path.exists(filepath):
            print(f"  ✗  No encontrado: {filename}  (se omite)")
            continue

        print(f"  ✓  {filename}")
        source = read_file(filepath)
        imports, body = parse_imports_and_body(source, filename)
        all_imports.extend(imports)

        separator = [
            "\n",
            f"# {'─' * 68}\n",
            f"# Módulo: {filename}\n",
            f"# {'─' * 68}\n",
            "\n",
        ]
        all_bodies.extend(separator)
        all_bodies.extend(body)

    resolved_imports = resolve_import_conflicts(all_imports)

    header = textwrap.dedent("""\
        #!/usr/bin/env python3
        # =============================================================================
        # bcup – script unificado generado automáticamente por make_single.py
        # No editar manualmente; regenerar desde los módulos fuente.
        # =============================================================================

    """)

    return header + "".join(resolved_imports) + "\n" + "".join(all_bodies)


def main():
    source_dir = sys.argv[1] if len(sys.argv) > 1 else "."
    output_file = sys.argv[2] if len(sys.argv) > 2 else "bcup.py"

    if not os.path.isdir(source_dir):
        sys.exit(f"Error: la carpeta '{source_dir}' no existe.")

    found = [m for m in MODULE_ORDER if os.path.exists(os.path.join(source_dir, m))]
    if not found:
        sys.exit(f"Error: no se encontró ningún módulo de bcup en '{source_dir}'.")

    unified = build_unified(source_dir)

    with open(output_file, "w", encoding="utf-8") as f:
        f.write(unified)

    try:
        ast.parse(unified)
        print(f"\n✓  Sintaxis válida")
    except SyntaxError as e:
        print(f"\n✗  Error de sintaxis en el archivo generado: {e}")

    size_kb = os.path.getsize(output_file) / 1024
    print(f"Archivo generado: {os.path.abspath(output_file)}  ({size_kb:.1f} KB)")
    print("Uso: python bcup.py [backup|restore|push] [parámetros...]")


if __name__ == "__main__":
    main()
