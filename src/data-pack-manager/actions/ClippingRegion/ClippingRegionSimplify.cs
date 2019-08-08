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

namespace medea.actions
{
	public class ClippingRegionSimplify : action
	{
		public List<string> Errors = new List<string>();
		public ClippingRegionSimplify()
		{
		}

		public override void Call()
		{
			Progress.Caption = "Simplificando regiones...";
			var ci = context.Data.Session.Query<ClippingRegionItem>()
				.ToList();
			Progress.Total = ci.Count;
			var session = medea.context.Data.Session;
			int i = 1;
			foreach (var item in ci)
			{
				IGeometry geo = session.GetGeometry<ClippingRegionItem>(item.Id.Value,
					x => x.Geometry);
				i++;
				item.GeometryR1 = (Geometry) Simplifications.Simplify(geo, QualityEnum.High);
				if (item.GeometryR1.Coordinates.Length == 0)
					item.GeometryR1 = (Geometry) geo;
				string sql = "update clipping_region_item set "
						+ "cli_geometry_r1 = " + InsertGenerator.GetValueEscaped(item.GeometryR1)+
						" where cli_id = " + item.Id.Value;
				try
				{
					session.SqlActions.ExecuteNonQuery(sql);
				}
				catch (Exception e)
				{
					Errors.Add("falló: " + item.Id.Value + " - " + e.ToString());
				}

				Progress.Increment();
			} 
		}

	}
}
