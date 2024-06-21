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
﻿using System.Text.RegularExpressions;
using NHibernate;
using NHibernate.SqlCommand;

namespace medea.Data
{
	/// <summary>
	/// Ref: https://weblogs.asp.net/ricardoperes/strongly-typed-delete-with-nhibernate
	/// https://github.com/rjperes/DevelopmentWithADot.NHibernateExtensions/blob/master/DevelopmentWithADot.NHibernateExtensions/QueryableExtensions.cs
	/// </summary>
	class DeleteInterceptor : EmptyInterceptor
	{
		private static readonly Regex regex = new Regex("\\s+from\\s+([^\\s]+)\\s+([^\\s]+)\\s+", 
			RegexOptions.IgnoreCase | RegexOptions.Compiled);

		public override SqlString OnPrepareStatement(SqlString sql)
		{
			if (sql.IndexOfCaseInsensitive("delete ") == 0
				|| sql.IndexOfCaseInsensitive("update ") == 0)
				return sql;

			Match match = regex.Match(sql.ToString());
			string tableName = match.Groups[1].Value;
			string tableAlias = match.Groups[2].Value;

			sql = sql.Substring(match.Groups[2].Index);
			sql = sql.Replace(tableAlias, tableName);
			sql = sql.Insert(0, "delete from ");

			int orderByIndex = sql.IndexOfCaseInsensitive(" order by ");
			if (orderByIndex > -1)
				sql = sql.Substring(0, orderByIndex);

			int limitIndex = sql.IndexOfCaseInsensitive(" limit ");
			if (limitIndex > -1)
				sql = sql.Substring(0, limitIndex);

			return (sql);
		}
	}

}
