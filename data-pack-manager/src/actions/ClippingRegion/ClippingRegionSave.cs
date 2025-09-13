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
using System.Linq;
using System.Collections.Generic;
using NetTopologySuite.Geometries;
using NetTopologySuite.Features;
using GeoAPI.Geometries;

namespace medea.actions
{
	public class ClippingRegionSave : action
	{
		private bool skipOrphans = false;

		private ClippingRegion current;
		private bool fileAdded;
		private string iParent;
		private string iCode;
		private string iCaption;
		private string Basename;
		ClippingRegionItem country;

		public ClippingRegionSave(ClippingRegion current, bool fileAdded, string iParent,
			string iCode, string iCaption, string Basename, ClippingRegionItem country)
		{
			this.current = current;
			this.fileAdded = fileAdded;
			this.iParent = iParent;
			this.iCode = iCode;
			this.iCaption = iCaption;
			this.Basename = Basename;
			this.country = country;
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
			context.Data.Session.SqlActions.ExecuteScalar("SET GLOBAL wait_timeout = 28800;");

			if (this.current.Metadata != null)
			{ 
				MetadataSave met = new MetadataSave(this.current.Metadata);
				met.Call();
			}

			Progress.Caption = "Actualizando región";
			Progress.Total = 1;
			context.Data.Session.SaveOrUpdate(current);
			Progress.Increment();
		}

		private Dictionary<string, int> ValidateItems()
		{
			if (!fileAdded)
				return null;

			Progress.Caption = "Validando ítems";
			Dictionary<string, int> ci = null;
			if (iParent != null)
			{
				// No es países. Trae todos los items de la clipping padre para asociar.
				ci = context.Data.Session.Query<ClippingRegionItem>()
					.Where(x => x.ClippingRegion.Id == current.Parent.Id)
					.Select(x => new { Id = x.Id.Value, x.Code })
					.ToDictionary(x => x.Code, x => x.Id);
			}

			ShapeOperations.ValidateParentAndShapes(Progress, ci, Basename + ".shp", iCode, iParent, skipOrphans);

			return ci;
		}

		private void SaveItems(Dictionary<string, int> ci)
		{
			if (!fileAdded)
				return;

			Progress.Caption = "Leyendo ítems";
			var features = ShapeFile.ReadShapefile(Basename + ".shp");

			Progress.Caption = "Preparando ítems";

			current.ClippingRegionItems.Clear();
			Progress.Total = 0;
			Progress.Total = features.Count;
			foreach (var feature in features)
			{
				ClippingRegionItem item = new ClippingRegionItem();
				item.Centroid = (Point)feature.Geometry.Centroid;
				item.Code = feature.Attributes[iCode].ToString();
				if (iCaption != "")
					item.Caption = feature.Attributes[iCaption].ToString();
				if (item.Caption.Contains("\n"))
					item.Caption = item.Caption.Split('\n')[0];
				var skipItem = false;

				if (ci != null)
				{
					var parent = feature.Attributes[iParent].ToString();
					if (ci.ContainsKey(parent) == false && skipOrphans)
						skipItem = true;
					else
						item.Parent = new ClippingRegionItem(ci[parent]);
				}
				else
				{
					item.Parent = country;
				}

				item.Geometry = (Geometry)feature.Geometry;
				item.GeometryR1 = (Geometry) Simplifications.Simplify(feature.Geometry, QualityEnum.High);
				item.GeometryR2 = (Geometry) Simplifications.Simplify(feature.Geometry, QualityEnum.VeryHigh);
				item.GeometryR3 = (Geometry) feature.Geometry;

				if (item.GeometryR2.IsEmpty)
					item.GeometryR2 = item.GeometryR3;
				if (item.GeometryR1.IsEmpty)
					item.GeometryR1 = item.GeometryR2;
	
				item.AreaM2 = Projections.CalculateM2Area(feature.Geometry);

				item.ClippingRegion = current;
				if (!skipItem)
					current.ClippingRegionItems.Add(item);
				Progress.Increment();
			}

			Progress.Caption = "Guardando ítems";

			var sql = InsertGenerator.FromList(current.ClippingRegionItems);
			context.Data.Session.SqlActions.BulkInsert(sql, Progress);
			context.Data.Session.Flush();

			ClippingRegionSimplify.FinalFix(context.Data.Session);

			current.ClippingRegionItems.Clear();
		}

	}
}
