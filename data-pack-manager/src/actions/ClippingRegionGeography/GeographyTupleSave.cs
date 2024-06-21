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
using medea.entities;

namespace medea.actions
{
	public class GeographyTupleSave : action
	{
		private GeographyTuple current;

		public GeographyTupleSave(GeographyTuple crc)
		{
			current = crc;
		}

		public override void Call()
		{
			Save();
			SaveItems();
			VersionUpdater.Increment();
		}

		private void SaveItems()
		{
			var session = context.Data.Session;

			Progress.Caption = "Guardando ítems de geografías";

			var sql = InsertGenerator.FromList(current.GeographyTupleItems);
			context.Data.Session.SqlActions.BulkInsert(sql, Progress);
			context.Data.Session.Flush();
			current.GeographyTupleItems.Clear();
		}

		private void Save()
		{
			Progress.Caption = "Actualizando items de geografía";
			Progress.Total = 1;
			context.Data.Session.SaveOrUpdate(current);
			context.Data.Session.Flush();
			Progress.Increment();
		}
	}
}
