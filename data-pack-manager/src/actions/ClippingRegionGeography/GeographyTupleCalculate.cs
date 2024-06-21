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
using NetTopologySuite.Geometries;

namespace medea.actions
{
	public class GeographyTupleCalculate : action, INonTransactionAction
	{
		private GeographyTuple current;
#if DEBUG
		const bool IGNORE_TOPOLOGICAL_ERRORS = true;
#else
				const bool IGNORE_TOPOLOGICAL_ERRORS = false;
#endif

		public GeographyTupleCalculate(GeographyTuple crc)
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

			Progress.Caption = "Obteniendo ítems de geografía destino";
			var geographyItems = GeographyCache.GetGeography(current.Geography.Id.Value);

            Progress.Caption = "Obteniendo ítems de geografía origen";
            var geographyItemsFrom = GeographyCache.GetGeography(current.PreviousGeography.Id.Value);


			Dictionary<int, Geometry> geographyItemsFromLower = new Dictionary<int, Geometry>();
			 if (current.PreviousLowerGeography != null)
			{
                geographyItemsFromLower = GeographyCache.GetGeography(current.PreviousLowerGeography.Id.Value);
            }
            Progress.Caption = "Calculando " + current.PreviousGeography.Caption + " " + current.PreviousGeography.Revision + " hacia " + current.Geography.Revision;
			Progress.Total = geographyItemsFrom.Count * geographyItems.Count;
			Dictionary<int, Geometry> pending = new Dictionary<int, Geometry>();
			foreach (var item in geographyItems)
			{
				var cliGeo = item.Value;
				var cliId = item.Key;
				var env = cliGeo.Envelope;
				bool found = false;
                if (cliGeo.Area > 0)
				{
					foreach (var geographyItemFrom in geographyItemsFrom)
					{
						if (geographyItemFrom.Value.Envelope.Intersects(env))
						{
							try
							{
								var inter = cliGeo.Intersection(geographyItemFrom.Value).Area;
								var percent = inter / cliGeo.Area * 100;
								var percentReversed = inter / geographyItemFrom.Value.Area * 100;
								if (percent > 50)
								{
									// si no cubrió todo el elemento, o si consumió una parte del otro
									bool partial = (percent < 95 || percentReversed < 95);
									if (current.PreviousLowerGeography == null)
										partial = false;
									if (partial)
									{
                                        pending.Add(item.Key, item.Value);
                                    }
                                    var tupleItem = new GeographyTupleItem(current, current.PreviousGeography, cliId, geographyItemFrom.Key, partial);
									current.GeographyTupleItems.Add(tupleItem);
									found = true;
									break;
								}
							}
							catch (NetTopologySuite.Geometries.TopologyException)
							{
								if (IGNORE_TOPOLOGICAL_ERRORS == false)
									throw;
							}
						}
					}
                }
				if (!found)
					pending.Add(item.Key, item.Value);
                Progress.Increment(geographyItemsFrom.Count);
            }

            if (current.PreviousLowerGeography == null)
                return;

            Progress.Caption = "Calculando " + current.PreviousLowerGeography.Caption + " " + current.PreviousLowerGeography.Revision + " hacia " + current.Geography.Revision;
            Progress.Total = geographyItemsFromLower.Count * pending.Count;
            // ahora recorre todos los que no matchearon o quedaron parciales...
            foreach (var item in pending)
            {
                var cliGeo = item.Value;
                var cliId = item.Key;
                var env = cliGeo.Envelope;
                bool found = false;
                if (cliGeo.Area > 0)
                {
                    foreach (var geographyItemFrom in geographyItemsFromLower)
                    {
                        if (geographyItemFrom.Value.Envelope.Intersects(env))
                        {
                            try
                            {
                                var inter = cliGeo.Intersection(geographyItemFrom.Value).Area;
                                var percent = inter / cliGeo.Area * 100;
                                var percentReversed = inter / geographyItemFrom.Value.Area * 100;
                                if (percentReversed > 50)
                                {
                                    // si no cubrió todo el elemento, o si consumió una parte del otro
                                    var tupleItem = new GeographyTupleItem(current, current.PreviousLowerGeography, cliId, geographyItemFrom.Key, false);
                                    current.GeographyTupleItems.Add(tupleItem);
                                }
                            }
                            catch (NetTopologySuite.Geometries.TopologyException)
                            {
                                if (IGNORE_TOPOLOGICAL_ERRORS == false)
                                    throw;
                            }
                        }
                    }
                }
                Progress.Increment(geographyItemsFromLower.Count);
            }
        }
    }
}
