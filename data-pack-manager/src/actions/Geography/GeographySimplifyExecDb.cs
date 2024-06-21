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
using System.Linq;
using medea.common;
using medea.entities;
using medea.Data;
using NetTopologySuite.Geometries;
using GeoAPI.Geometries;
using System.Collections.Generic;
using System;
using System.IO;

namespace medea.actions
{
	public class GeographySimplifyExecDb : action
	{
		public List<string> Errors = new List<string>();
		public GeographySimplifyExecDb()
		{
		}
				NHibernateSession session;

		public override void Call()
		{
			 session = medea.context.Data.Session;

			List<string> files = new List<string>();
			int i = 0;
			string cad = "";
			Progress.Caption = "Insertando geografías...";

			var inFiles = Directory.GetFiles(GeographySimplify.outFolder);
			Progress.Total = inFiles.Length;
			foreach (string file in inFiles)
			{
				i++;
				string text = System.IO.File.ReadAllText(file);

				cad += TrimNumbers( text);
					files.Add(file);

				int l = cad.Length;
				cad = Dump(files, cad);

				Progress.Increment();
			}

			if (cad != "")
					Dump(files, cad);
		}

		private string TrimNumbers(string str)
		{
			const int MAX = 6;

			var parts = str.Split('.');
			for(int n = 1; n < parts.Length; n++)
			{
				string s = parts[n];
				int end1 = s.IndexOf(',');
				int end2 = s.IndexOf(' ');
				int end3 = s.IndexOf(')');
				if (end1 == -1) end1 = int.MaxValue;
				if (end2 == -1) end2 = int.MaxValue;
				if (end3 == -1) end3 = int.MaxValue;

				if (end2 < end1) end1 = end2;
				if (end3 < end1) end1 = end3;
				//
				if (end1 > MAX)
				{
					double next = int.Parse(s.Substring(0, MAX + 1));
					next = next / 10;
					next = Math.Round(next, 0);
					string ntext = next.ToString();
					if (ntext.Length < MAX)
						ntext = new String('0', MAX - ntext.Length) + ntext;
					parts[n] = ntext + s.Substring(end1);
				}
			}
			return String.Join(".", parts);
		}

		private string Dump(List<string> files, string cad)
		{
			session.SqlActions.ExecuteNonQuery(cad);
			foreach (string del in files)
				System.IO.File.Delete(del);
			files.Clear();
			return "";
		}
	}
}
