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
using NetTopologySuite.Geometries;
using System.Collections.Generic;

namespace medea.actions
{
	public class GeographyCache
	{
		private static Dictionary<int, Dictionary<int, Geometry>> cache = new Dictionary<int, Dictionary<int, Geometry>>();

		public static void Clear()
		{
			cache.Clear();
		}
		public static Dictionary<int, Geometry> GetGeography(int geographyId)
		{
			if (cache.ContainsKey(geographyId))
				return cache[geographyId];

			var ret = context.Data.Session.GetGeometries<GeographyItem>(x => x.Geography, geographyId, x => x.Geometry, true);
			cache.Add(geographyId, ret);
			return ret;
		}
	}
}
