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
using medea.common;
using medea.context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace medea.actions
{
	class PublicId
	{
		private static Dictionary<string, List<int>> used = new Dictionary<string, List<int>>();
		public static void SetId(dynamic element)
		{
			if (element.Id != null)
				return;

			string table = ObjectMetadata.GetTableName(element.GetType());
			string idColumn = ObjectMetadata.GetKeyColumn(element.GetType());
			//
			string sql = "SELECT IFNULL(MAX(" + idColumn + "), 0) + 100 id FROM " + table + " WHERE " + idColumn + " % 100 = 0";
			int nextId = context.Data.Session.SqlActions.ExecuteScalarIntNoFlush(sql);
			while (used.ContainsKey(table) && used[table].Contains(nextId))
			{
				nextId += 100;
			}
			element.Id = nextId;
			if (!used.ContainsKey(table))
				used.Add(table, new List<int>());
			used[table].Add(nextId);
		}

		public static void SetLowestFreeId(dynamic element)
		{
			if (element.Id != null)
				return;

			string table = ObjectMetadata.GetTableName(element.GetType());
			string idColumn = ObjectMetadata.GetKeyColumn(element.GetType());
			//
			string sql = "SELECT MIN(t1." + idColumn + " + 1) AS nextID "
											+ " FROM (SELECT 0 as " + idColumn + " UNION SELECT " + idColumn + " FROM " + table + ") as t1 "
											+ "	 LEFT JOIN " + table + " t2 "
											+ "			 ON t1." + idColumn + " + 1 = t2." + idColumn + " "
											+ " WHERE t2." + idColumn + " IS NULL";

			int nextId = context.Data.Session.SqlActions.ExecuteScalarIntNoFlush(sql);
			while (used.ContainsKey(table) && used[table].Contains(nextId))
			{
				nextId += 1;
			}
			element.Id = nextId;
			if (!used.ContainsKey(table))
				used.Add(table, new List<int>());
			used[table].Add(nextId);
		}
	}
}
