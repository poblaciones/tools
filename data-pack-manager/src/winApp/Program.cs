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
using System;
using System.Windows.Forms;
using medea.common;
using GeoAPI.Geometries;
using NetTopologySuite.Geometries;
using medea.entities;

namespace medea.winApp
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
//			TrimDecimals();
//			Application.Exit();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
			Application.Run(new frmMain());
			//Application.Run(new frmTestHttp());
		}

		private static void TrimDecimals()
		{
			for (int n = 589; n < 7410; n += 5)
			{
				try
				{
					Console.WriteLine("CLI : " +n);
					string range = " where cli_id between " + n + " AND " + (n + 5);
					string sql = null;
					sql = "update  `clipping_region_item` set cli_geometry_r1 = GeomFromText(TrimDecimals(asText(cli_geometry_r1))) " + range;
					context.Data.Session.SqlActions.ExecuteNonQuery(sql);
					//sql = "update  `clipping_region_item` set cli_geometry = GeomFromText(TrimDecimals(asText(cli_geometry))) " + range;
					//context.Data.Session.SqlActions.ExecuteNonQuery(sql);
				}
				catch
				{
					Console.WriteLine("Retrying... CLI : " +n);
					n -= 5;
				}
			}

			MessageBox.Show("done");
		}
	}
}
