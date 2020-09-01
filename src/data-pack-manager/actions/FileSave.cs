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

namespace medea.actions
{
	public class FileSave : action
	{
		private File current;
		public FileSave(action action, File file) : base(action)
		{
			current = file;
		}
		public FileSave(File file)
		{
			current = file;
		}
		public override void Call()
		{
			Progress.Caption = "Actualizando copia de archivo";
			Progress.Total = 2;
			Progress.Increment();
			List<FileChunk> keepItems = new List<FileChunk>();

			if (current.FileChunks.Count > 0 &&
				current.FileChunks[0].Id.HasValue == false)
			{
				// has nuevos
				keepItems.AddRange(current.FileChunks);
							int totalSize = 0;
				foreach (var v in current.FileChunks)
					totalSize += v.Content.Length;
				current.Size = totalSize;
				current.FileChunks.Clear();
			}
			else
			{
				PublicId.SetId(current);
				context.Data.Session.SaveOrUpdate(current);
				return;
			}
			Progress.Total = keepItems.Count + 1;
			PublicId.SetId(current);

			context.Data.Session.SaveOrUpdate(current);

			foreach (var chunk in keepItems)
			{
				current.FileChunks.Add(chunk);

				PublicId.SetId(chunk);

				context.Data.Session.SaveOrUpdate(chunk);
				Progress.Increment();
			}
		}
	}
}
