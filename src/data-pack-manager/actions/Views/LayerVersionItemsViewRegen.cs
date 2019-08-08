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
using System.Collections.Generic;
using System.Globalization;
using System;

namespace medea.actions
{
	public class LayerVersionItemsViewRegen : action
	{
		public int rowsAffected;
		RegenEnum update;
		public LayerVersionItemsViewRegen(action action, RegenEnum update)
			: base(action)
		{
			this.update = update;
		}

		public override void Call()
		{
			Progress.Caption = "Vaciando vista de ítems de capas";
			if (update == RegenEnum.Incremental)
				Progress.Caption += " (sólo modificados)";
			LayersViewCleanRegen.TruncateIncremental(update, "layer_version_item_view", "lvi_layer_id");

			Progress.Caption = "Regenerando vista de ítems de capas";
			string sql = "INSERT INTO layer_version_item_view(`lvi_layer_id`,`lvi_layer_version_id`, `lvi_cartography_id`, `lvi_cartography_item_id`, `lvi_urbanity`, `lvi_data_item_id`,"
				+ "`lvi_all_values`, `lvi_all_version_value_label_ids`, `lvi_description`, `lvi_summary`, lvi_feature_id, `lvi_area_m2`, "
				+ "`lvi_population`, `lvi_households`, `lvi_children`, lvi_envelope, `lvi_shape_dataset_item_id`, `lvi_shape_area_m2`, `lvi_location`) ";

			// trae todos los layers
			var layers = LayersViewRegen.GetLayersForUpdate();
			Progress.Total = layers.Count;
			foreach (var l in layers)
			{
				foreach (var lv in l.Versions)
				{
					ProcessLayerVersion(sql, lv);

					ProcessLayerVersionAggregations(sql, lv);
				}

				Progress.Increment();
			}
		}

		private void ProcessLayerVersionAggregations(string sql, LayerVersion lv)
		{
			foreach (var level in lv.Dataset.Levels)
				ProcessLayerVersion(sql, lv, level);
		}

		private void ProcessLayerVersion(string sql, LayerVersion lv, Cartography aggregateLevel = null)
		{
			List<object> fieldList = new List<object>();
			fieldList.Add(lv.Layer.Id.Value);
			fieldList.Add(lv.Id.Value);
            if (aggregateLevel == null)
                fieldList.Add("null");
            else
                fieldList.Add("cai_cartography_id");
			fieldList.Add("cai_id");
			fieldList.Add("cai_urbanity");
			fieldList.Add("id");
			// Valores
			bool useAggregateFunction = (aggregateLevel != null);
			string allValues = calculateAllValuesField(lv, useAggregateFunction);
			fieldList.Add(allValues);
			// Etiquetas
			string allVersionValueLabels = calculateAllVersionValueLabelId(lv, useAggregateFunction, aggregateLevel);
			fieldList.Add(allVersionValueLabels);

			// Descripción
			if (useAggregateFunction && lv.DatasetCaptionColumn != null && lv.DatasetCaptionColumn.Aggregation == AggregationEnum.Transpose && useAggregateFunction)
			{
				fieldList.Add("null");
			}
			else
				fieldList.Add(LviFunctions.GetRichColumn(lv.DatasetCaption, lv.DatasetCaptionColumn));
			// Summary
			if (useAggregateFunction && lv.SummaryColumn != null && lv.SummaryColumn.Aggregation == AggregationEnum.Transpose)
			{
				fieldList.Add("0");
			}
			else
				fieldList.Add(LviFunctions.GetRichColumn(lv.Summary, lv.SummaryColumn));

			// featureId
			if (lv.Dataset.DatasetType == DatasetTypeEnum.Shapes ||
				lv.Dataset.DatasetType == DatasetTypeEnum.Locations)
			{
				fieldList.Add(((long) lv.Dataset.Id.Value * 0x100000000) + "+id");
			}
			else
				fieldList.Add("cai_id");

			fieldList.Add(SpecialColumnEnum.AreaM2);
			fieldList.Add(SpecialColumnEnum.People);
			fieldList.Add(SpecialColumnEnum.Household);
			fieldList.Add(SpecialColumnEnum.Children);
			if (lv.Dataset.DatasetType == DatasetTypeEnum.Shapes)
			{
				fieldList.Add("Envelope(geometry)");
				fieldList.Add("id");
				fieldList.Add("area_m2");
				fieldList.Add("centroid");
			}
			else if (lv.Dataset.DatasetType == DatasetTypeEnum.Locations)
			{
				string point = "POINT(" + lv.Dataset.LatitudeColumn.Field + ", " +
																	lv.Dataset.LongitudeColumn.Field + ")";
				fieldList.Add("Envelope(" + point + ")");
				fieldList.Add(null);
				fieldList.Add(null);
				fieldList.Add(point);
			}
			else if (lv.Dataset.DatasetType == DatasetTypeEnum.Data)
			{
				fieldList.Add("Envelope(cai_geometry)");
				fieldList.Add(null);
				fieldList.Add(null);
				fieldList.Add("cai_centroid");
			}
			else
				throw new Exception("Invalid dataset type.");

			int joinShapesId;
			if (lv.Dataset.DatasetType == DatasetTypeEnum.Shapes)
				joinShapesId = lv.Dataset.Id.Value;
			else
				joinShapesId = 0;
			string select = DatasetTable.GetSelectValuesSql(lv.Dataset, fieldList, aggregateLevel);

			select += LviFunctions.AppendGeometryNotNullCondition(lv);

			rowsAffected += medea.context.Data.Session.SqlActions.ExecuteNonQuery(sql + select, true);
		}



		private string calculateAllVersionValueLabelId(LayerVersion lv, bool useAggregateFunction, Cartography level)
		{
			List<string> ret = new List<string>();
			foreach (var v in lv.Variables)
			{
				if (v.Use(useAggregateFunction))
				{
					string value = LviFunctions.calculateValueField(v);
					ret.Add(LviFunctions.calculateVersionValueLabelId(v, value, level));
					ret.Add("'\t'");
				}
			}
			ret.RemoveAt(ret.Count - 1);
			return "CONCAT(" + String.Join(",", ret) + ")";
		}

		private string calculateAllValuesField(LayerVersion lv, bool useAggregateFunction)
		{
			List<string> ret = new List<string>();
			if (lv.Variables.Count == 0)
				throw new Exception("La capa '" + lv.Layer.Caption + "' no tiene indicadores en su revisión '" + lv.Caption + "'.");

			foreach (var v in lv.Variables)
			{
				if (v.Use(useAggregateFunction))
				{
					ret.Add(LviFunctions.calculateValueField(v));
					ret.Add("'\t'");
				}
			}
			if (ret.Count > 0)
				ret.RemoveAt(ret.Count - 1);
			return "CONCAT(" + String.Join(",", ret) + ")";
		}

	}
}
