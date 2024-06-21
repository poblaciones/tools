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
using System.Collections.Generic;
using medea.entities;
using System.Data.SQLite;

namespace medea.actions
{
	public class GradientSave : action
	{
		private Gradient current;
		private bool fileAdded;
		private bool fileDeleted;
		private string filename;
		private string table;
		private ClippingRegionItem currentCountry;

		public GradientSave(action action, Gradient gradient) : base(action)
		{
			current = gradient;
		}

		public GradientSave(Gradient current, bool fileAdded, bool fileDeleted, string table, string filename, ClippingRegionItem currentCountry)
		{
			this.current = current;
			this.fileAdded = fileAdded;
			this.fileDeleted = fileDeleted;
			this.table = table;
			this.filename = filename;
			this.currentCountry = currentCountry;
		}

		public override void Call()
		{
			Progress.Caption = "Actualizando gradiente";
			Progress.Total = 1;
			context.Data.Session.SaveOrUpdate(current);
			if (fileDeleted)
				context.Data.Session.SqlActions.ExecuteNonQuery("DELETE FROM gradient_item WHERE gri_gradient_id = "
						+ current.Id.ToString());
			if (fileAdded)
			{
				// abre el archivo de sqlite
				string cs = "Data Source='" + filename + "';Version=3;";

				var con = new SQLiteConnection(cs);
				con.Open();

				UpdateCount(con);
				int i = 0;

				var cmd = new SQLiteCommand(con);
				cmd.CommandText = "SELECT tile_column, tile_row, zoom_level, tile_data FROM " + table;
				SQLiteDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					GradientItem item = new GradientItem();
					item.Gradient = current;
					item.X = reader.GetInt32(0);
					item.Y = reader.GetInt32(1);
					item.Z = reader.GetInt32(2);
					item.Content = GetBytes(reader, 3);
					context.Data.Session.Save(item);
					Progress.Increment();
					if (i % 1000 == 0)
					{
						context.Data.Session.Commit();
						context.Data.Session.BeginTransaction();
					}
					i++;
				};
			}

		}

		private void UpdateCount(SQLiteConnection con)
		{
			var cmdCount = new SQLiteCommand(con);
			cmdCount.CommandText = "SELECT COUNT(*) FROM " + table;
			SQLiteDataReader readerCount = cmdCount.ExecuteReader();
			while (readerCount.Read())
				Progress.Total = readerCount.GetInt32(0);
			readerCount.Close();
		}

		/// <summary>
		/// Reads all available bytes from reader
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="ordinal"></param>
		/// <returns></returns>
		private byte[] GetBytes(SQLiteDataReader reader, int ordinal)
		{
			byte[] result = null;

			if (!reader.IsDBNull(ordinal))
			{
				long size = reader.GetBytes(ordinal, 0, null, 0, 0); //get the length of data 
				result = new byte[size];
				int bufferSize = 1024;
				long bytesRead = 0;
				int curPos = 0;
				while (bytesRead < size)
				{
					bytesRead += reader.GetBytes(ordinal, curPos, result, curPos, bufferSize);
					curPos += bufferSize;
				}
			}

			return result;
		}
	}
}