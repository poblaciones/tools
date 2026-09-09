import pymysql
import datetime
import json
import argparse

class DumpTable:
    # Database connection configuration
    db_config = {
        'host': '127.0.0.1',
        'user': 'root',
        'password': 'root',
        'database': 'aacademi_maps_prod',
        'charset': 'utf8mb4'
    }

    def format_value(value):
        """Format values to be SQL-compatible."""
        if value is None:
            return "NULL"
        elif isinstance(value, str):
            # Use double quotes to avoid conflict with single quotes in f-strings
            return "'" + value.replace("'", "\\'") + "'"
        elif isinstance(value, (int, float)):
            return str(value)
        elif isinstance(value, datetime.date):
            return f"'{value.isoformat()}'"
        elif isinstance(value, (bytes, bytearray)):  # For spatial binary data
            # Detectar si parece WKB (MySQL puede incluir SRID al inicio)
            if len(value) >= 9:
                possible_wkb = value[4:]  # saltar SRID
            else:
                possible_wkb = value

            if possible_wkb[:5] in (
                b'\x01\x01\x00\x00\x00', b'\x01\x02\x00\x00\x00', b'\x01\x03\x00\x00\x00',
                b'\x01\x04\x00\x00\x00', b'\x01\x05\x00\x00\x00', b'\x01\x06\x00\x00\x00',
                b'\x01\x07\x00\x00\x00'
            ):
                return f"ST_GeomFromWKB(0x{possible_wkb.hex()})"
            else:
                return f"0x{value.hex()}"
        else:
            return f"'{json.dumps(value)}'"

    def export_table_to_file(cursor, table, target_suffix, output_filename, batch_size, offset):
        with open(output_filename, 'w', encoding='utf-8') as output_file:
            DumpTable.export_table(cursor, table, target_suffix, output_file, batch_size, offset)

    def export_table(cursor, table, target_suffix, output_file, batch_size, offset):
        """Generate INSERTs for a specific table."""
        cursor.execute(f"SELECT * FROM {table} LIMIT 0")  # Get column names
        columns = [desc[0] for desc in cursor.description]
        output_file.write(f"-- Data from table {table}{target_suffix}\n")
        group_size = 25
        rows_exported = 0
        output_file.write("/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;\n")
        output_file.write("/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;\n")
        output_file.write("/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;\n")
        output_file.write("/*!50503 SET NAMES utf8mb4 */;\n")
        output_file.write("/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;\n")
        output_file.write("/*!40103 SET TIME_ZONE='+00:00' */;\n")
        output_file.write("/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;\n")
        output_file.write("/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;\n")
        output_file.write("/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;\n")
        output_file.write("/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;\n")
        output_file.write(f"LOCK TABLES `{table}{target_suffix}` WRITE;\n")
        output_file.write(f"/*!40000 ALTER TABLE `{table}{target_suffix}` DISABLE KEYS */;\n")

        while True:
            query = f"SELECT * FROM {table} LIMIT {batch_size} OFFSET {offset}"
            cursor.execute(query)
            rows = cursor.fetchall()

            if not rows:
                break  # No more rows to export

            # Grouping values to minimize INSERT statements
            grouped_values = []
            for row in rows:
                values = "(" + ", ".join([DumpTable.format_value(v) for v in row]) + ")"
                grouped_values.append(values)
                rows_exported += 1

                # When the group is full, write the INSERT statement
                if len(grouped_values) == group_size:
                    insert_statement = (
                        f"INSERT INTO {table}{target_suffix} ({', '.join(columns)}) VALUES "
                        + ",".join(grouped_values) + ";\n"
                    )
                    output_file.write(insert_statement)
                    grouped_values = []  # Reset the group

            # Write remaining values if any
            if grouped_values:
                insert_statement = (
                    f"INSERT INTO {table}{target_suffix} ({', '.join(columns)}) VALUES "
                    + ",".join(grouped_values) + ";\n"
                )
                output_file.write(insert_statement)

            offset += batch_size

        output_file.write(f"\n/*!40000 ALTER TABLE `{table}{target_suffix}` ENABLE KEYS */;\n")
        output_file.write("/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;\n")
        output_file.write("/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;\n")
        output_file.write("/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;\n")
        output_file.write("/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;\n")
        output_file.write("/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;\n")
        output_file.write("/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;\n")
        output_file.write("/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;\n")
        output_file.write("/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;\n")
        output_file.write("UNLOCK TABLES;\n")

def main():
    # Set up command-line arguments
    parser = argparse.ArgumentParser(description="Export MySQL table data to an SQL file.")
    parser.add_argument("table", help="The name of the table to export.")
    parser.add_argument("output", help="The name of the output file (e.g., dump.sql).")
    parser.add_argument("--batch_size", type=int, default=500, help="Number of rows per batch (default: 500).")
    parser.add_argument("--offset", type=int, default=0, help="Starting row offset (default: 0).")

    args = parser.parse_args()

    try:
        # Connect to the database
        connection = pymysql.connect(**db_config)
        cursor = connection.cursor()

        # Open the output file to write INSERTs
        with open(args.output, 'w', encoding='utf-8') as output_file:
            output_file.write(f"-- Dump of the {args.table} table generated via SELECTs in Python\n\n")
            export_table(cursor, args.table, output_file, args.batch_size, args.offset)

        print(f"Export of the {args.table} table completed successfully.")
    except pymysql.MySQLError as err:
        print(f"Error: {err}")
    finally:
        if connection and connection.open:
            cursor.close()
            connection.close()

if __name__ == "__main__":
    main()
