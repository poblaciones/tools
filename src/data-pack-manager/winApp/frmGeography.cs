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
	public partial class frmGeography : Form
	{
		public frmGeography()
		{
			InitializeComponent();
			splitContainer2.Panel2Collapsed = true;

			ListViewColumnSorter.Bind(lstItems);
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			var f = new frmGeographyEdit();
			if (trvList.SelectedNode != null)
				f.SetValueCmbParent((Geography)trvList.SelectedNode.Tag);
			if(f.ShowDialog(this) == DialogResult.OK)
				ReloadTree();
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			if (NoneSelected())
				return;

			var f = new frmGeographyEdit();
			f.LoadData(GetSelectedNode());
			if (f.ShowDialog(this) == DialogResult.OK)
				ReloadTree();

		}

		private Geography GetSelectedNode()
		{
			if (trvList.SelectedNode == null)
				throw new MessageException("Debe seleccionar un ítem del árbol para realizar esta acción.");

			return trvList.SelectedNode.Tag as Geography;
		}

		private void ReloadTree()
		{
			using (new WaitCursor())
			{
				using (new KeepSelectedTree(trvList))
				{
					trvList.Nodes.Clear();
					lstItems.Items.Clear();
					uEntity.Clear();

					var items = UI.GetGeographies().GenerateTree();

					TreeNode node = new TreeNode("raíz");

					TreeItem<Geography>.MakeNodeTree(items, node);
					foreach (TreeNode item in node.Nodes)
						trvList.Nodes.Add(item);

					trvList.ExpandAll();

					if (NoneSelected() == false)
					{
						var c = GetSelectedNode();
						c.GeographyItemsCount = null;
						uEntity.Fill(GetDetail(c));
						LoadItems(c);
					}
				}
			}
		}

		private void frmGeography_Load(object sender, EventArgs e)
		{
			ReloadTree();
		}

		private Dictionary<string,string> GetDetail(Geography geography)
		{
			using (new WaitCursor())
			{
				var dict = new Dictionary<string, string>();
				dict.Add("Id", geography.Id.ToString());
				dict.Add("Nombre", geography.Caption);
				dict.Add("Revisión", geography.Revision);
				dict.Add("Máximo zoom", geography.MaxZoom.ToString());

				dict.Add("Campo código", geography.FieldCodeName);
				dict.Add("Campo descripción", geography.FieldCaptionName);
				dict.Add("Campo urbano/rural", geography.FieldUrbanityName);
				if (geography.PartialCoverage != null)
					dict.Add("Cobertura parcial", geography.PartialCoverage);

				dict.Add("Metadatos", (geography.Metadata != null ? geography.Metadata.Title : ""));
				dict.Add("Fuente", (geography.Metadata != null ? geography.Metadata.SourcesCaption() : ""));

				if (geography.GeographyItemsCount.HasValue == false)
					geography.GeographyItemsCount = UI.GetItems<GeographyItem>().Where(x => x.Geography == geography).Count();
				dict.Add("Ítems", geography.GeographyItemsCount.ToString());

				SetChkItemsText(geography);

				return dict;
			}
		}

		private void SetChkItemsText(Geography c)
		{
			int cant = c.GeographyItemsCount.GetValueOrDefault();
			if (cant == 0)
				chkItems.Text = "No hay ítems para mostrar";
			else if (cant > Settings.CantItems)
			{
				chkItems.Text = "Mostrar ítems (primeros " + Settings.CantItems + ")";
				chShowAll.Visible = true;
			}
			else
			{
				chkItems.Text = "Mostrar ítems";
				chShowAll.Visible = false;
			}
		}

		private void LoadItems(Geography geography)
		{
			if (chkItems.Checked == false)
				return;

			using (new WaitCursor())
			{
				lstItems.Items.Clear();
				lstContainer.Panel2Collapsed = true;
				lstCount.Text = "";
				int max = Settings.CantItems;
				if (chShowAll.Visible && chShowAll.Checked)
					max = int.MaxValue;

				if (geography.GeographyCaptions.Count == 0 ||
					(chShowAll.Checked &&
					geography.GeographyCaptions.Count == Settings.CantItems))
				{
					geography.GeographyCaptions = UI.GetItems<GeographyItem>()
						.Where(x => x.Geography == geography).Take(max)
						.Select(x => new GeographyCaption(x.Id.Value, x.Code, x.Caption, x.Parent.Caption, x.Parent.Code,
							x.AreaM2, x.Population, x.Households, x.Children)).ToList();
				}

				foreach (var item in geography.GeographyCaptions)
				{
					var ele = new ListViewItem(item.ToArray());
					ele.Tag = item;
					lstItems.Items.Add(ele);
				}
			}
			lstCount.Text = UI.GetCountLegend(lstItems.Items);
		}

		private void trvList_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			btnEdit.PerformClick();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (NoneSelected())
				return;

			if (UI.ConfirmDeleteRecursive(this))
			{
				Invoker.CallProgress(new GeographyDelete(GetSelectedNode()));
				ReloadTree();
			}
		}

		private bool NoneSelected()
		{
			return trvList.SelectedNode == null || GetSelectedNode() == null;
		}

		private void mnuEdit_Click(object sender, EventArgs e)
		{
			btnEdit.PerformClick();
		}

		private void mnuDelete_Click(object sender, EventArgs e)
		{
			btnDelete.PerformClick();
		}

		private void mnuDeleteItem_Click(object sender, EventArgs e)
		{
			if (NoneSelected())
				return;

			if (UI.ConfirmDeleteRecursive(this))
			{
				List<GeographyItem> items = new List<GeographyItem>();
				foreach (ListViewItem item in lstItems.SelectedItems)
				{
					var ci = item.Tag as GeographyItem;
					items.Add(ci);
				}
				if (items.Count > 0)
				{
					var c = GetSelectedNode();

					Invoker.CallProgress(new GeographyItemsDelete(items));
					c.GeographyItemsCount = null;
					uEntity.Fill(GetDetail(c));
					LoadItems(c);
				}
			}
		}

		private void mnuCopyItem_Click(object sender, EventArgs e)
		{
			var copy = "";
			foreach (ListViewItem item in lstItems.SelectedItems)
			{
				var ci = item.Tag as GeographyItem;
				copy += ci.ToString() + System.Environment.NewLine;
			}
			if(copy.Trim() != "")
				Clipboard.SetText(copy.Trim());
		}

		private void trvList_KeyDown(object sender, KeyEventArgs e)
		{
			if (trvList.SelectedNode != null)
			{
				if (e.KeyCode == Keys.Delete)
					btnDelete.PerformClick();
			}
		}

		private void lstItems_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lstItems.SelectedItems.Count == 0)
				return;
			lstContainer.Panel2Collapsed = false;
			var c = lstItems.SelectedItems[0].Tag as IIdentifiable;
			if (c != null)
				uGeometry1.LoadData<GeographyItem>(c.Id.Value);
		}

		private void trvList_AfterSelect(object sender, TreeViewEventArgs e)
		{
			uEntity.Clear();
			lstItems.Items.Clear();
			uGeometry1.Clear();
			lstContainer.Panel2Collapsed = true;

			var geography = e.Node.Tag as Geography;
			if (geography == null)
				return;

			using (new WaitCursor())
			{
				uEntity.Fill(GetDetail(geography));
				LoadItems(geography);
			}

		}

		private void chkItems_CheckedChanged(object sender, EventArgs e)
		{
			if (NoneSelected() == false && chkItems.Checked)
			{
				using (new WaitCursor())
				{
					uEntity.Fill(GetDetail(GetSelectedNode()));
					LoadItems(GetSelectedNode());
				}
			}
			splitContainer2.Panel2Collapsed = !chkItems.Checked;
			if (!chkItems.Checked)
			{
				chShowAll.Visible = false;
				chShowAll.Checked = false;
			}
		}

		private void chShowAll_CheckedChanged(object sender, EventArgs e)
		{
			if (chShowAll.Checked && !NoneSelected())
			{
				if (chkItems.Checked == false)
					chkItems.Checked = true;
				else
					LoadItems(GetSelectedNode());
			}
		}


	}
}
