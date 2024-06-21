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
using NetTopologySuite.Features;
using System.Collections.Generic;
using System.Linq;
using NetTopologySuite.Geometries;
using System;
using GeoAPI.Geometries;

namespace medea.actions
{
	public class GeographySave : action
	{
		private Geography current;
		private bool fileAdded;
		string Basename;

		string iHousehold;
		string iChildren;
		string iPopulation;
		string iUrbanity;
		string iParent;
		string iCode;
		string iCaption;

		public GeographySave(Geography geography, bool fileAdded, string household,
					string children, string population, string urbanity, string parent, string code,
			string caption, string basename)
		{
			Basename = basename;
			iHousehold = household;
			iChildren = children;
			iPopulation = population;
			iParent = parent;
			iUrbanity = urbanity;
			iCode = code;
			iCaption = caption;
			current = geography;
			this.fileAdded = fileAdded;
		}

		public override void Call()
		{
			var ci = ValidateItems();

			Save();

			SaveItems(ci);

			VersionUpdater.Increment();
		}

		private void Save()
		{
			MetadataSave met = new MetadataSave(this.current.Metadata);
			met.Call();

			Progress.Caption = "Actualizando geografía";
			Progress.Total = 1;

			context.Data.Session.SaveOrUpdate(current);

			Progress.Increment();
		}
		private Dictionary<string, int> ValidateItems()
		{
			if (!fileAdded)
				return null;

			Progress.Caption = "Obteniendo geografía padre";

			Dictionary<string, int> ci = null;
			if (iParent != "")
			{
				//Trae todos los items de la geografía padre para asociar.
				ci = context.Data.Session.Query<GeographyItem>()
					.Where(x => x.Geography.Id == current.Parent.Id)
					.Select(x => new { Id = x.Id.Value, x.Code })
					.ToDictionary(x => x.Code, x => x.Id);
			}

			Progress.Caption = "Validando ítems";
			ShapeOperations.ValidateParentAndShapes(Progress, ci, Basename + ".shp", iCode, iParent);
		
			return ci;
		}


		private void SaveItems(Dictionary<string, int> ci)
		{
			if (!fileAdded)
				return;

			Progress.Caption = "Guardando ítems";

			//Trae todos los items de la geografía padre para asociar.
			current.GeographyItems.Clear();
			Progress.Total = 0;
			var features = ShapeFile.ReadShapefile(Basename + ".shp");
		
			Progress.Total = features.Count;
			Dictionary<string, bool> done = new Dictionary<string,bool>();

			int n = 0;
			List<GeographyItem> l = new List<GeographyItem>();
			foreach (var feature in features)
			{
				GeographyItem item = new GeographyItem();
				if (iHousehold != "")
					item.Households = ParseOrZero(iHousehold, feature);
				if (iChildren != "")
					item.Children = ParseOrZero(iChildren, feature);
				if (iPopulation != "")
					item.Population = ParseOrZero(iPopulation, feature);
				if (iUrbanity != "")
					item.Urbanity = UrbanityEnumFromInt(ParseOrZero(iUrbanity, feature));
				else
					item.Urbanity = UrbanityEnum.None;
				item.Code = feature.Attributes[iCode].ToString();
				item.CodeAsNumber = decimal.Parse(item.Code);
				if (iCaption != "")
					item.Caption = feature.Attributes[iCaption].ToString();

				if (ci != null)
				{
					var parent = feature.Attributes[iParent].ToString();
					item.Parent = new GeographyItem(ci[parent]);
				}

				item.Geometry = (Geometry)feature.Geometry;
				if (feature.Geometry != null)
				{
					item.AreaM2 = Projections.CalculateM2Area(feature.Geometry);
					item.Centroid = (Point)feature.Geometry.Centroid;

					Simplifications.FillSimplifiedGeometries(item.Geometry, item);
				}
				else
					throw new Exception("La geometría no puede ser nula.");

				item.Geography = current;
				if (done.ContainsKey(item.Code) == false)
				{
					l.Add(item);
					done[item.Code] = true;
				//	if (n % 100 == 99)
					{
						string sql = InsertGenerator.FromList(l);
						context.Data.Session.SqlActions.ExecuteNonQuery(sql);
						l.Clear();
					}
					n++;
				}
				Progress.Increment();
			}
			if (l.Count > 0)
			{
				string sql = InsertGenerator.FromList(l);
				context.Data.Session.SqlActions.ExecuteNonQuery(sql);
			}
			string updateAverage = "UPDATE `geography` SET geo_area_avg_m2=(select avg(gei_area_m2) from "
					+ "geography_item where gei_geography_id = geo_id) WHERE geo_id = " + current.Id.Value.ToString();
			context.Data.Session.SqlActions.ExecuteNonQuery(updateAverage);
			// esta formula es cualquier cosa, que fitea más o menos 6 como maxzoom de provincias y 11 como maxzoom de departamentos.
			//string updateMaxZoom = "update geography set geo_max_zoom = truncate (16-(power(geo_area_avg_m2, .25) / 60),0) "
			//		+ "WHERE geo_id = " + current.Id.Value.ToString();
			//context.Data.Session.SqlActions.ExecuteNonQuery(updateAverage);
		}

		private UrbanityEnum UrbanityEnumFromInt(int i)
		{
			switch (i)
			{
				case 0:
					return UrbanityEnum.Rural;
				case 1:
					return UrbanityEnum.Rural_Disperse;
				case 2:
					return UrbanityEnum.Urban;
				case 3:
					return UrbanityEnum.Urban_Disperse;
				default:
					throw new Exception("Valor no válido para urbano/rural/disperso.");

			}
		}

		private static int ParseOrZero(string key, Feature feature)
		{
			int ret;
			int.TryParse(feature.Attributes[key].ToString(), out ret);
			return ret;
		}

	}
}
