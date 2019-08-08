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
	public partial class frmClippingGeography : Form
	{
		public frmClippingGeography()
		{
			InitializeComponent();
			splitContainer2.Panel2Collapsed = true;
			ListViewColumnSorter.Bind(lstItems);
			ListViewColumnSorter.Bind(lstClippingGeography);
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			var f = new frmClippingGeographyNew();
			f.LoadPartial(GetSelectedNode());
			if (f.ShowDialog(this) == DialogResult.OK)
			{
				ReloadTree();
				LoadList(GetSelectedNode());
			}
		}

		private ClippingRegionGeography GetSelectedItem()
		{
			if (lstClippingGeography.SelectedItems.Count == 0)
				throw new MessageException("Debe seleccionar un ítem del listado para realizar esta acción.");

			return lstClippingGeography.SelectedItems[0].Tag as ClippingRegionGeography;
		}

		private void LoadList(ClippingRegion clipping)
		{
			if (clipping == null)
				return;

			using (new WaitCursor())
			{
				lstClippingGeography.Items.Clear();
				lstItems.Items.Clear();

				var items = UI.GetItems<ClippingRegionGeography>()
					.Where(x => x.ClippingRegion == clipping).ToList();


				foreach (var item in items)
				{
					ListViewItem lvi = new ListViewItem(item.ToArray());
					lvi.Tag = item;
					lstClippingGeography.Items.Add(lvi);
				}
				if (lstClippingGeography.Items.Count > 0)
					lstClippingGeography.Items[0].Selected = true;

				if (lstClippingGeography.SelectedItems.Count > 0)
					LoadItems();
			}
		}

		private void frmClipping_Load(object sender, EventArgs e)
		{
			ReloadTree();
		}

		private void LoadItems()
		{
			lstItems.Items.Clear();
			lstCount.Text = "";

			if (chkItems.Checked == false
			|| lstClippingGeography.SelectedItems.Count == 0)
				return;

			using (new WaitCursor())
			{
				var sel = GetSelectedItem();
				if (sel.ItemsCaptions.Count == 0)
				{
					sel.ItemsCaptions = UI.GetItems<ClippingRegionGeographyItem>()
						.Where(x => x.ClippingRegionGeography == sel).Take(Settings.CantItems)
						.Select(x => new ClippingRegionGeographyCaption(x.Id.Value, x.ClippingRegionItem.Caption,
							x.GeographyItem.Caption, x.GeographyItem.Code)).ToList();
				}

				SetChkItemsText(sel.ItemsCaptions.Count);

				foreach (var item in sel.ItemsCaptions)
				{
					var ele = new ListViewItem(item.ToArray());
					ele.Tag = item;
					lstItems.Items.Add(ele);
				}
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

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (lstClippingGeography.SelectedItems.Count == 0)
				return;

			if (UI.ConfirmDelete(this))
			{
				Invoker.CallProgress(new ClippingRegionGeographyDelete(GetSelectedItem()));
				ReloadTree();
				LoadList(GetSelectedNode());
			}
		}

		private void mnuDelete_Click(object sender, EventArgs e)
		{
			btnDelete.PerformClick();
		}

		private void mnuDeleteItem_Click(object sender, EventArgs e)
		{
			if (lstClippingGeography.SelectedItems.Count == 0)
				return;

			if (UI.ConfirmDelete(this))
			{
				var items = new List<ClippingRegionGeographyItem>();
				foreach (ListViewItem item in lstItems.SelectedItems)
				{
					var ci = item.Tag as ClippingRegionGeographyItem;
					items.Add(ci);
				}
				if (items.Count > 0)
				{
					var c = GetSelectedItem();
					Invoker.CallProgress(new ClippingRegionGeographyItemsDelete(items));
					LoadItems();
				}
			}
		}

		private void mnuCopyItem_Click(object sender, EventArgs e)
		{
			var copy = "";
			foreach (ListViewItem item in lstItems.SelectedItems)
			{
				var ci = item.Tag as ClippingRegionGeographyItem;
				copy += ci.ToString() + System.Environment.NewLine;
			}
			if(copy.Trim() != "")
				Clipboard.SetText(copy.Trim());
		}

		private void chkItems_CheckedChanged(object sender, EventArgs e)
		{
			if(lstClippingGeography.SelectedItems.Count > 0 && chkItems.Checked)
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

		private void trvList_AfterSelect(object sender, TreeViewEventArgs e)
		{
			lstItems.Items.Clear();
			var clipping = e.Node.Tag as ClippingRegion;
			LoadList(clipping);
		}

		private void trvList_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
				btnDelete.PerformClick();
		}

		private void trvList_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			btnNew.PerformClick();
		}

		private bool NoneSelected()
		{
			return trvList.SelectedNode == null || GetSelectedNode() == null;
		}

		private ClippingRegion GetSelectedNode()
		{
			if (trvList.SelectedNode != null)
				return trvList.SelectedNode.Tag as ClippingRegion;
			return null;
		}

		private void ReloadTree()
		{
			using (new KeepSelectedTree(trvList))
			{

				trvList.Nodes.Clear();
				lstClippingGeography.Items.Clear();
				lstItems.Items.Clear();
				using (new WaitCursor())
				{

					TreeNode node = new TreeNode("raíz");
					var relationsCount = UI.GetItems<ClippingRegionGeography>()
						.GroupBy(n => n.ClippingRegion.Id,
									(key, values) => new { ClippingRegionId = key, Count = values.Count() })
									.ToDictionary(x => x.ClippingRegionId, x => x.Count);

					var items = UI.GetItems<ClippingRegion>()
						.OrderBy(x => x.Caption).ToList().GenerateTree();
					TreeItem<ClippingRegion>.MakeNodeTree(items, node, relationsCount);

					foreach (TreeNode item in node.Nodes)
					{
						trvList.Nodes.Add(item);
					}

					trvList.ExpandAll();
					LoadList(GetSelectedNode());
				}
			}
		}

		private void lstClippingGeography_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
				btnDelete.PerformClick();
		}


	}
}
