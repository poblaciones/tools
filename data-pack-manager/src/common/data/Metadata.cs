/*
*    Poblaciones - Plataforma abierta de datos espaciales de poblaci√≥n.
*    Copyright (C) 2018-2019. Consejo Nacional de Investigaciones Cient√≠ficas y T√©cnicas (CONICET)
*		 y Universidad Cat√≥lica Argentina (UCA).
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

using medea.Data;
using medea.common;
using NHibernate.Persister.Entity;
using System.Linq;
using System.Linq.Expressions;
using NetTopologySuite.Geometries;
using System.Reflection;

namespace medea.context
{
	/// <summary>
	/// Permite acceder a los nombres de tablas y columnas, entre otros
	/// mapeados en hbm.
	/// </summary>
	public class Metadata<T> where T: ActiveBaseEntity<T>
	{
		public static AbstractEntityPersister TableData
		{
			get
			{
				var factory = NHibernateHelper.CurrentFactory;
				return factory.GetClassMetadata(typeof(T)) as AbstractEntityPersister;
			}
		}

		public static string TableName
		{
			get { return TableData.TableName; }
		}

		public static string KeyColumn
		{
			get { return TableData.KeyColumnNames[0]; }
		}

		public static string GetColumn(Expression<Func<T, object>> field)
		{
			var prop = GetField(field);
			return TableData.GetPropertyColumnNames(prop).FirstOrDefault();
		}
		public static string GetColumn(PropertyInfo prop)
		{
			return TableData.GetPropertyColumnNames(prop.Name).FirstOrDefault();
		}


		public static string GetField(Expression<Func<T, object>> field)
		{
			MemberExpression expr = null;
			if (field.Body is MemberExpression)
				expr = (MemberExpression)field.Body;
			else if (field.Body is UnaryExpression)
				expr = (MemberExpression)((UnaryExpression)field.Body).Operand;
			else
				throw new ArgumentException("field");

			return expr.Member.Name;
		}

		public static string GetPointColumn()
		{
			var props = typeof(T).GetProperties();
			var point = "";
			bool found = false;
			foreach (var prop in props)
			{
				if (prop.PropertyType == typeof(Point))
				{
					if (found)
						throw new Exception("La entidad " + typeof(T).Name + " tiene m·s de un campo tipo Point.");
					found = true;
					point = TableData.GetPropertyColumnNames(prop.Name).FirstOrDefault();
				}
			}
			if (found)
				return point;
			throw new Exception("No se encontrÛ campo tipo Point en entidad: " + typeof(T).Name);
		}

		public static string GetGeometryColumn()
		{
			var props = typeof(T).GetProperties();
			var geo = "";
			bool found = false;
			foreach (var prop in props)
			{
				if (prop.PropertyType.Name == "Byte[]")
				{
					if (found)
						throw new Exception("La entidad " + typeof(T).Name + " tiene m·s de un campo tipo Geometry.");
					found = true;
					geo = TableData.GetPropertyColumnNames(prop.Name).FirstOrDefault();
					break;
				}
			}
			if (found)
				return geo;
			throw new Exception("No se encontrÛ campo tipo Geometry en entidad: " + typeof(T).Name);
		}
	}
}
