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
using System.Collections.Generic;
using System.Linq;
using medea.common;
using medea.entities;

namespace medea.actions
{
	public class ClippingRegionGeographyCalculate : action, INonTransactionAction
	{
		private ClippingRegionGeography current;
#if DEBUG
		const bool IGNORE_TOPOLOGICAL_ERRORS = true;
#else
				const bool IGNORE_TOPOLOGICAL_ERRORS = false;
#endif

		public ClippingRegionGeographyCalculate(ClippingRegionGeography crc)
		{
			current = crc;
		}

		public override void Call()
		{
			CalculateItems();
		}

		private void CalculateItems()
		{
			var session = context.Data.Session;

			Progress.Caption = "Obteniendo ítems de regiones";
			var clippingItems = session.GetGeometries<ClippingRegionItem>(x => x.ClippingRegion, current.ClippingRegion.Id.Value, x => x.Geometry);

			Progress.Caption = "Obteniendo ítems de geografía";
			var geographyItems = GeographyCache.GetGeography(current.Geography.Id.Value);

			Progress.Caption = "Calculando " + current.ClippingRegion.Caption + " por " + current.Geography.GetCaption();
			Progress.Total = clippingItems.Count * geographyItems.Count / 1000;
			long i = 0;
			foreach (var clippingRegionItem in clippingItems)
			{
				var cliGeo = clippingRegionItem.Value;
				var cliId = clippingRegionItem.Key;
				var env = cliGeo.Envelope;

				if (cliGeo.Area == 0)
				{
					i += geographyItems.Count;
					continue;
					//throw new MessageException("El área del ítem no puede ser 0. Ítem Geografía Id: " + dimItem.Id);
				}
				foreach (var geographyItem in geographyItems)
				{
					if (geographyItem.Value.Envelope.Intersects(env))
					{
						try
						{
							var inter = cliGeo.Intersection(geographyItem.Value).Area;
							var percent = inter / geographyItem.Value.Area * 100;
							if (percent > 50)
							{
								percent = System.Math.Round(percent, 1);
								var item = new ClippingRegionItemGeographyItem(current, geographyItem.Key,
								cliId, percent);
								current.ClippingRegionItemGeographyItems.Add(item);
							}
						}
						catch (NetTopologySuite.Geometries.TopologyException)
						{
							if (IGNORE_TOPOLOGICAL_ERRORS == false)
								throw;
						}
					}
					i++;
					if (i % 1000 == 0)
						Progress.Increment();
				}

			}
		}
	}
}
