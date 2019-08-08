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
	public class GeographySimplify : action
	{
		public List<string> Errors = new List<string>();
		NHibernateSession session;
		public static string outFolder = @"d:\output";


		public GeographySimplify()
		{
		}

		public void CallDirect()
		{
			Progress.Caption = "Simplificando geografías...";
			var ci = context.Data.Session.Query<GeographyItem>()
				.ToList();
			Progress.Total = ci.Count;
			var session = medea.context.Data.Session;
			string cad = "";
			int i = 0;
			foreach (var item in ci)
			{
				i++;
				IGeometry geo = session.GetGeometry<GeographyItem>(item.Id.Value,
					x => x.Geometry);

				Simplifications.FillSimplifiedGeometries((Geometry)geo, item);

				string sql = "update geography_item set "
						+ "gei_geometry_r1 = " + InsertGenerator.GetValueEscaped(item.GeometryR1)
						+ ", gei_geometry_r2 = " + InsertGenerator.GetValueEscaped(item.GeometryR2)
						+ ", gei_geometry_r3 = " + InsertGenerator.GetValueEscaped(item.GeometryR3)
						+ ", gei_geometry_r4 = " + InsertGenerator.GetValueEscaped(item.GeometryR4)
						+ ", gei_geometry_r5 = " + InsertGenerator.GetValueEscaped(item.GeometryR5)
						+ ", gei_geometry_r6 = " + InsertGenerator.GetValueEscaped(item.GeometryR6)
					+ " where gei_id = " + item.Id.Value + ";";
				cad = cad + sql;
				try
				{
					if (i % 20 == 0)
					{
						session.SqlActions.ExecuteNonQuery(cad);
						cad = "";
					}
				}
				catch (Exception e)
				{
					Errors.Add("falló: " + item.Id.Value + " - " + e.ToString());
				}

				Progress.Increment();
			}

			session.SqlActions.ExecuteNonQuery(cad);
			cad = "";
		}

		public override void Call()
		{
			Progress.Caption = "Generando geografías...";
			var ci = context.Data.Session.Query<GeographyItem>()
				.ToList();
			Progress.Total = ci.Count;
			session = medea.context.Data.Session;
			int i = 0;


			List<GeographyItem> items = new List<GeographyItem>();
			foreach (var item in ci)
			{
				i++;
				if (item.Id.Value >= 862094)
				{
					items.Add(item);
					if (items.Count == 100)
					{
						ProcessIds(items, i);
						items.Clear();
					}
				}
				Progress.Increment();

			}
			if (items.Count > 0)
				ProcessIds(items, i);
		}

		void ProcessIds(List<GeographyItem> items, int i)
		{
			List<int> ids = GetIds(items);
			var geos = session.GetGeometries<GeographyItem>(ids, x => x.Geometry);
			string cad = "";
			foreach (var item in items)
			{
				Simplifications.FillSimplifiedGeometries((Geometry)geos[item.Id.Value], item);

				string sql = "update geography_item set "
						+ "gei_geometry_r1 = GeomFromText('" + item.GeometryR1.ToString() + "')"
						+ ", gei_geometry_r2 = GeomFromText('" + item.GeometryR2.ToString() + "')"
						+ ", gei_geometry_r3 = GeomFromText('" + item.GeometryR3.ToString() + "')"
						+ ", gei_geometry_r4 = GeomFromText('" + item.GeometryR4.ToString() + "')"
						+ ", gei_geometry_r5 = GeomFromText('" + item.GeometryR5.ToString() + "')"
						+ ", gei_geometry_r6 = GeomFromText('" + item.GeometryR6.ToString() + "')"
					+ " where gei_id = " + item.Id.Value + ";";
				cad = cad + sql;
			}
			System.IO.File.WriteAllText(Path.Combine(outFolder, i.ToString() + ".txt"), cad);
		}

		private static List<int> GetIds(List<GeographyItem> items)
		{
			List<int> ids = new List<int>();
			foreach (var item in items)
			{
				ids.Add(item.Id.Value);
			}
			return ids;
		}
	}
}