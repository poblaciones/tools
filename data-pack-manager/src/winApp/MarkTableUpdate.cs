/*
*    Poblaciones - Plataforma abierta de datos espaciales de población.
*    Copyright (C) 2018-2024. Consejo Nacional de Investigaciones Científicas y Técnicas (CONICET)
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
using Ionic.Zip;
using medea.common;
using medea.controls;
using medea.entities;
using System;

namespace medea.winApp
{
	public class MarkTableUpdate : action
	{
		private string[] tables;

		public static void UpdateTables(string[] tables )
		{
			var me = new MarkTableUpdate(tables);
			me.Call();
		}
		public MarkTableUpdate(string[] tables)
		{
			this.tables = tables;
		}

		public MarkTableUpdate(string table)
		{
			this.tables = new string[] { table };
		}
		public override void Call()
		{
			Progress.Caption = "Marcando modificación de tabla";
			Progress.Total = 1;

			var start = ResolveStartUrl();
			HttpResult res = null;
			res = HttpInvoker.CallProgress(start, null, false);
		}

		private string ResolveStartUrl()
		{
			string start = "services/admin/MarkTable";
			start += "?t=" + string.Join(",", this.tables);
			return start;
		}

		public static void UpdateMetadata()
		{
			UpdateTables(new string[] { "metadata", "contact", "institution", "metadata_source", "source", "file", "file_chunk" });
		}
}
}
