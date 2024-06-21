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
using medea.common;
using medea.entities;

namespace medea.actions
{
	public class DbCheck : action
	{

		public DbCheck()
		{
		}
		public override void Call()
		{
			List<Type> ok = GetMappedTypes();
			Progress.Caption = "Verificando mapeos";
			Progress.Total = ok.Count;
			foreach(Type t in ok)
			{
				Progress.Increment();
				var all = context.Data.Session.NullQuery(t);
			}
		}

		private static List<Type> GetMappedTypes()
		{
			List<Type> ok = new List<Type>();
			foreach (Type t in typeof(Geography).Assembly.GetTypes())
			{
				if (typeof(BaseEntity).IsAssignableFrom(t) && t.IsAbstract == false)
				{
					ok.Add(t);
				}
			}
			return ok;
		}
	}
}
