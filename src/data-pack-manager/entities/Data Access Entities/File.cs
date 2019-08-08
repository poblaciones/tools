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
using System.IO;
using System;
using System.Collections.Generic;

namespace medea.entities
{
	public class File : FileBase<File>
	{

		public File()
		{
			FileChunks = new List<FileChunk>();
		}

		const int PAGESIZE = 1024 * 1024;
		public virtual void CreateChunks(string filename)
		{
			FileChunks.Clear();
			long unread = new FileInfo(filename).Length;
			using (FileStream r = new FileStream(filename, FileMode.Open, FileAccess.Read))
			{
				while(unread > 0)
				{
					long bytesToRead = Math.Min(PAGESIZE, unread);
					byte[] data = new byte[bytesToRead];
					r.Read(data, 0, (int) bytesToRead);
					FileChunk f = new FileChunk();
					f.Content = data;
					f.File = this;
					FileChunks.Add(f);
					unread -= PAGESIZE;
				}
			}
		}
	}
}
