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
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using medea.actions;
using medea.entities;
using medea.controls;
using medea.common;

namespace medea.winApp
{
	public partial class frmGeographyTuple : Form
	{
		// 2022 generadas con:
		/* 
		 * insert into geography_tuple (gtu_geography_id, gtu_previous_geography_id)

		SELECT g_to.geo_id, g_from.geo_id FROM `geography` g_to 
		join `geography` g_from
		where g_to.geo_caption = replace( g_from.geo_caption, 'Dist.Esc.', 'Comunas')
		and g_to.geo_revision = '2022'
		and g_from.geo_revision != '2022'; 


		*/
		public frmGeographyTuple()
		{
			InitializeComponent();
			splitContainer2.Panel2Collapsed = true;
			ListViewColumnSorter.Bind(lstItems);
			ListViewColumnSorter.Bind(lstTupleGeography);
		}


		private GeographyTuple GetSelectedItem()
		{
			if (lstTupleGeography.SelectedItems.Count == 0)
				throw new MessageException("Debe seleccionar un ítem del listado para realizar esta acción.");

			return lstTupleGeography.SelectedItems[0].Tag as GeographyTuple;
		}

		private void LoadList()
		{
			using (new WaitCursor())
			{
				lstTupleGeography.Items.Clear();
				lstItems.Items.Clear();

				var items = UI.GetItems<GeographyTuple>().ToList();
				foreach (var item in items)
				{
					ListViewItem lvi = new ListViewItem();
					lvi.Text = item.Geography.Caption;
					lvi.SubItems.Add(item.PreviousGeography.Revision);
					lvi.SubItems.Add(item.Geography.Revision);
					if (item.PreviousLowerGeography != null)
					{
						lvi.SubItems.Add(item.PreviousLowerGeography.Caption);
					}
					lvi.Tag = item;
					lstTupleGeography.Items.Add(lvi);
				}
				if (lstTupleGeography.Items.Count > 0)
					lstTupleGeography.Items[0].Selected = true;

				if (lstTupleGeography.SelectedItems.Count > 0)
					LoadItems();
			}
		}

		private void LoadItems()
		{
			lstItems.Items.Clear();
			lstCount.Text = "";

			if (chkItems.Checked == false
			|| lstTupleGeography.SelectedItems.Count == 0)
				return;

			using (new WaitCursor())
			{
				var sel = GetSelectedItem();
				var items = UI.GetItems<GeographyTupleItem>()
					.Where(x => x.GeographyTuple == sel).ToList();
				lstItems.BeginUpdate();
				int i = 0;
				foreach (var item in items)
				{
					var ele = new ListViewItem(item.GeographyPreviousItemId.ToString());
					ele.SubItems.Add(item.GeographyItemId.ToString());
					ele.SubItems.Add(item.GeographyPreviousId.ToString());
					ele.Tag = item;
					lstItems.Items.Add(ele);
					i++;
					if (i > 2000) break;
				}
				lstItems.EndUpdate();

			}
			lstCount.Text = UI.GetCountLegend(lstItems.Items);
		}

		private void SetChkItemsText(int cant)
		{
			if (cant == 0)
				chkItems.Text = "No hay ítems para mostrar";
			else if (cant > Settings.CantItems)
				chkItems.Text = "Mostrar primeros " + Settings.CantItems + " ítems";
			else
				chkItems.Text = "Mostrar ítems";
		}

		private void mnuDeleteItem_Click(object sender, EventArgs e)
		{
			if (lstTupleGeography.SelectedItems.Count == 0)
				return;

			if (UI.ConfirmDelete(this))
			{
				var items = new List<ClippingRegionItemGeographyItem>();
				foreach (ListViewItem item in lstItems.SelectedItems)
				{
					var ci = item.Tag as ClippingRegionItemGeographyItem;
					items.Add(ci);
				}
				if (items.Count > 0)
				{
					var c = GetSelectedItem();
					Invoker.CallProgress(new ClippingRegionItemGeographyItemsDelete(items));
					MarkTableUpdate.UpdateTables(new string[] { "clipping_region_item_geography_item" });

					LoadItems();
				}
			}
		}

		private void mnuCopyItem_Click(object sender, EventArgs e)
		{
			var copy = "";
			foreach (ListViewItem item in lstItems.SelectedItems)
			{
				var ci = item.Tag as ClippingRegionItemGeographyItem;
				copy += ci.ToString() + System.Environment.NewLine;
			}
			if (copy.Trim() != "")
				Clipboard.SetText(copy.Trim());
		}

		private void chkItems_CheckedChanged(object sender, EventArgs e)
		{
			if (lstTupleGeography.SelectedItems.Count > 0 && chkItems.Checked)
				using (new WaitCursor())
				{
					LoadItems();
				}
			splitContainer2.Panel2Collapsed = !chkItems.Checked;
		}

		private void lstClippingGeography_SelectedIndexChanged(object sender, EventArgs e)
		{
			LoadItems();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{

		}

		private void btnRegenerate_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem item in lstTupleGeography.CheckedItems)
			{
				GeographyTuple current = item.Tag as GeographyTuple;
				if (Invoker.CallProgress(new GeographyTupleCalculate(current)))
					Invoker.CallProgress(new GeographyTupleSave(current));
				MarkTableUpdate.UpdateTables(new string[] { "geography_tuple", "geography_tuple_item" });
			}
		}
	}
}
