/*
*    Poblaciones - Plataforma abierta de datos espaciales de población.
*    Copyright (C) 2018-2019. Consejo Nacional de Investigaciones Científicas y Técnicas (CONICET)
*		 y Universidad Católica Argentina (UCA).
*
*    This program is free software: you can redistribute it and/or modify
*    it under the terms of the GNU General Public License as published by
*    the Free Software Foundation, either version 3 of the License, or
*    (at your option) any later version.
*
*    This program is distributed in the hope that it will be useful,
*    but WITHOUT ANY WARRANTY; without even the implied warranty of
*    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*    GNU General Public License for more details.
*
*    You should have received a copy of the GNU General Public License
*    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

namespace medea.common
{
	public class InsertGenerator
	{
		static int FieldsEvery { get { return Settings.InsertGeneratorFieldsEvery; } }

		public static string FromList<T>(IList<T> items, int from = 0, int maxTo = -1) where T : ActiveBaseEntity<T>, IIdentifiable
		{
			if (maxTo == -1)
				maxTo = items.Count;
			if (items.Count == 0)
				return "";

			var properties = GetProperties<T>();
			var columns = GetColumns<T>(properties);

			return Generate(items, properties, columns, from, maxTo);
		}

		private static string Generate<T>(IList<T> items, PropertyInfo[] properties, List<string> columns,
			int from, int maxTo)
			where T : ActiveBaseEntity<T>, IIdentifiable
		{
			var tableName = context.Metadata<T>.TableName;
			int n = 0;
			StringBuilder sb = new StringBuilder();
			for(n = from; n < maxTo; n++)
			{
				var item = items[n];
				if ((n - from) % FieldsEvery == 0)
				{
					if ((n - from) > 0)
						sb.Remove(sb.Length - 1, 1).Append(";\n");

					sb.Append("INSERT INTO `");
					sb.Append(tableName).Append("` (`");
					foreach (var col in columns)
						sb.Append(col).Append("`,`");

					sb.Remove(sb.Length - 2, 2).Append(") VALUES");
				}
				sb.Append("(");
				foreach (var prop in properties)
				{
					sb.Append(GetValue(item, prop)).Append(",");
				}
				sb.Remove(sb.Length - 1, 1).Append("),");
			}
			sb.Remove(sb.Length - 1, 1).Append(";\n");
			return sb.ToString();
		}

		public static string FromValues(string tableName, IEnumerable<string> columns, IEnumerable<IEnumerable<object>> items)
		{
			int n = 0;
			StringBuilder sb = new StringBuilder();
			foreach (var item in items)
			{
				if (n % FieldsEvery == 0)
				{
					if (n > 0)
						sb.Remove(sb.Length - 1, 1).Append(";\n");

					sb.Append("INSERT INTO `");
					sb.Append(tableName).Append("` (`");
					foreach (var col in columns)
						sb.Append(col).Append("`,`");

					sb.Remove(sb.Length - 2, 2).Append(") VALUES");
				}
				sb.Append("(");
				foreach (var value in item)
					if (value == null || value is string)
						sb.Append(GetValue(value as string)).Append(",");
					else
						sb.Append(GetValueEscaped(value)).Append(",");

				sb.Remove(sb.Length - 1, 1).Append("),");
				n++;
			}
			sb.Remove(sb.Length - 1, 1).Append(";\n");
			return sb.ToString();
		}

		private static PropertyInfo[] GetProperties<T>() where T : ActiveBaseEntity<T>, IIdentifiable
		{
			List<PropertyInfo> ret = new List<PropertyInfo>();
			foreach(var prop in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
				{
					if (!Skip<T>(prop))
						ret.Add(prop);
			}
			return ret.ToArray();
		}

		private static List<string> GetColumns<T>(PropertyInfo[] properties) where T : ActiveBaseEntity<T>, IIdentifiable
		{
			List<string> cols = new List<string>();
			foreach (var prop in properties)
			{
				if (Skip<T>(prop))
					continue;

				cols.Add(context.Metadata<T>.GetColumn(prop));
			}
			return cols;
		}

		private static bool Skip<T>(PropertyInfo prop) where T : ActiveBaseEntity<T>, IIdentifiable
		{
			return prop.Name == "Id" || prop.Name == "IsNew" || prop.DeclaringType == typeof(T)
				|| (prop.PropertyType.IsGenericType
					&& prop.PropertyType.GetGenericTypeDefinition() == typeof(IList<>));
		}

		private static string GetValue(string value)
		{
			if (string.IsNullOrEmpty(value))
				return "null";
			return CheapEscape(value);
		}
		private static string GetValue<T>(T item, PropertyInfo prop) where T : ActiveBaseEntity<T>, IIdentifiable
		{
			var val = prop.GetValue(item, new object[0]);
			return GetValueEscaped(val, prop);
		}
		public static string GetValuesEscaped(List<object> values)
		{
			string ret = "";
			foreach(var val in values)
				{
					if (ret != "") ret += ",";
					ret += GetValueEscaped(val);
				}
			return ret;
		}
		public static string GetValueEscaped(object val, PropertyInfo prop = null)
		{
			if (val == null)
				return "null";
			Type t;
			if (prop == null)
				t = val.GetType();
			else
				t = prop.PropertyType;
			if (t == typeof(double) || t == typeof(double?))
				return CheapEscape(((double)val).ToString(CultureInfo.InvariantCulture));
			else if (t == typeof(int) || t == typeof(int?))
				return val.ToString();
			else if (t == typeof(bool) || t == typeof(bool?))
			{
				if ((bool)val)
					return "1";
				else
					return "0";
			}
			else if (t == typeof(byte[]))
			{
				//No se puede insertar byte array porque rompe el max_allowed_packet, además
				//duplica el tamaño porque hay que mandarlo como hexa.
				throw new Exception("Invalid type.");
			}
			else if (t == typeof(Geometry))
			{
				WKBWriter writer = new WKBWriter();
				var bytes = writer.Write((Geometry)val);
				return "0x00000000" + BitConverter.ToString(bytes).Replace("-", "");
			}
			else if (t == typeof(Point))
			{
				WKBWriter writer = new WKBWriter();
				var bytes = writer.Write((Point)val);
				return "0x00000000" + BitConverter.ToString(bytes).Replace("-", "");
			}
			else if (t == typeof(MultiPolygon))
			{
				WKBWriter writer = new WKBWriter();
				var bytes = writer.Write((MultiPolygon)val);
				return "0x00000000" + BitConverter.ToString(bytes).Replace("-", "");
			}
			else if (t == typeof(LineString))
			{
				WKBWriter writer = new WKBWriter();
				var bytes = writer.Write((LineString)val);
				return "0x00000000" + BitConverter.ToString(bytes).Replace("-", "");
			}
			else if (t == typeof(MultiLineString))
			{
				WKBWriter writer = new WKBWriter();
				var bytes = writer.Write((MultiLineString)val);
				return "0x00000000" + BitConverter.ToString(bytes).Replace("-", "");
			}
			else if (t == typeof(Polygon))
			{
				WKBWriter writer = new WKBWriter();
				var bytes = writer.Write((Polygon)val);
				return "0x00000000" + BitConverter.ToString(bytes).Replace("-", "");
			}
			else if (typeof(IIdentifiable).IsAssignableFrom(t))
				return ((IIdentifiable)val).Id.ToString();
			else
				return CheapEscape(val.ToString());
		}

		public static string CheapEscape(string val)
		{
			if (val == null)
				return "null";
			else
				return "'" + val.Replace("'", "''") + "'";
		}

	}
}
