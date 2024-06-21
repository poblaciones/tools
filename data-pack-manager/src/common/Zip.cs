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
﻿using System.IO;
using Ionic.Zip;
using System.Linq;

namespace medea.common
{
	public class Zip
	{
		public static string ZipFiles(string filename)
		{
			var name = Path.GetFileNameWithoutExtension(filename);
			var files = Directory.GetFiles(Path.GetDirectoryName(filename), name + ".*").ToList();

			if (files.Contains(Path.ChangeExtension(filename, ".zip")))
				files.Remove(Path.ChangeExtension(filename, ".zip"));

			var zipFilename = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName(), name + ".zip");
			Directory.CreateDirectory(Path.GetDirectoryName(zipFilename));

			if (File.Exists(zipFilename))
				File.Delete(zipFilename);

			using (ZipFile zip = new ZipFile())
			{
				zip.AddFiles(files, "");
				zip.Save(zipFilename);
			}
			return zipFilename;
		}

		public static string UnzipFiles(string filename)
		{
			string path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
			using (ZipFile zip = ZipFile.Read(filename))
			{
				zip.ExtractAll(path, ExtractExistingFileAction.OverwriteSilently);
			}
			return path;
		}
	}
}
