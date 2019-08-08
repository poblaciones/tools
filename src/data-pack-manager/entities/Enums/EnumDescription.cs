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
using System.Reflection;
using System.ComponentModel;
using medea.entities;

namespace medea.common
{
	public static class EnumDescription
	{
		public static string GetDescription(this Enum value)
		{
			Type type = value.GetType();
			string name = Enum.GetName(type, value);
			if (name != null)
			{
				FieldInfo field = type.GetField(name);
				if (field != null)
				{
					DescriptionAttribute attr =
								 Attribute.GetCustomAttribute(field,
									 typeof(DescriptionAttribute)) as DescriptionAttribute;
					if (attr != null)
						return attr.Description;
				}
			}
			return "";
		}

		public static T GetValue<T>(string description)
		{
			Type type = typeof(T);
			int n = 0;
			foreach(var ele in type.GetEnumNames())
			{
				var field = type.GetField(ele);
				DescriptionAttribute attr =
								Attribute.GetCustomAttribute(field,
									typeof(DescriptionAttribute)) as DescriptionAttribute;
				if (attr != null && attr.Description == description)
					return (T) (type.GetEnumValues().GetValue(n));
				n++;
			}
			throw new Exception("Description not found in array ('" + description + "'");
		}
	}
}
