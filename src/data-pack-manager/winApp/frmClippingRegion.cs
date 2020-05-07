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
	public partial class frmClippingRegion : Form
	{
		public frmClippingRegion()
		{
			InitializeComponent();
			splitContainer2.Panel2Collapsed = true;
			ListViewColumnSorter.Bind(lstItems);
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			var f = new frmClippingRegionEdit();
			if (trvList.SelectedNode != null)
				f.SetValueCmbParent((ClippingRegion)trvList.SelectedNode.Tag);
			if(f.ShowDialog(this) == DialogResult.OK)
				ReloadTree();
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			if (NoneSelected())
				return;

			var f = new frmClippingRegionEdit();
			f.LoadData(GetSelectedNode());
			if (f.ShowDialog(this) == DialogResult.OK)
				ReloadTree();

		}

		private ClippingRegion GetSelectedNode()
		{
			if (trvList.SelectedNode == null)
				throw new MessageException("Debe seleccionar un ítem del árbol para realizar esta acción.");

			return trvList.SelectedNode.Tag as ClippingRegion;
		}

		private void ReloadTree()
		{
			using (new WaitCursor())
			{

				trvList.Nodes.Clear();
				lstItems.Items.Clear();
				uEntity.Clear();

				var items = UI.GetItems<ClippingRegion>()
					.OrderBy(x => x.Caption).ToList().GenerateTree();

				TreeNode node = new TreeNode("raíz");

				TreeItem<ClippingRegion>.MakeNodeTree(items, node);
				foreach (TreeNode item in node.Nodes)
					trvList.Nodes.Add(item);

				trvList.ExpandAll();

				if (NoneSelected() == false)
				{
					var c = GetSelectedNode();
					c.ClippingRegionItemsCount = null;
					uEntity.Fill(GetDetail(c));
					LoadItems(c);
				}
			}
		}

		private void frmClippingRegion_Load(object sender, EventArgs e)
		{
			ReloadTree();
		}

		private Dictionary<string,string> GetDetail(ClippingRegion clipping)
		{
			using(new WaitCursor())
			{
				var dict = new Dictionary<string, string>();
				dict.Add("Id", clipping.Id.ToString());
				dict.Add("Nombre", clipping.Caption);
				dict.Add("Campo código", clipping.FieldCodeName);
				dict.Add("Prioridad", clipping.Priority.ToString());

				dict.Add("Mostrar en etiquetas y búsqueda", clipping.NoAutocomplete ? "No" : "Sí");

				dict.Add("Mínimo nivel de zoom para labels", clipping.LabelsMinZoom.ToString());
				dict.Add("Máximo nivel de zoom para labels", clipping.LabelsMaxZoom.ToString());
				dict.Add("Símbolo", clipping.Symbol == null ? "Ninguno" : clipping.Symbol);

				dict.Add("Metadatos", (clipping.Metadata != null ? clipping.Metadata.Title : ""));
				dict.Add("Fuente", (clipping.Metadata != null ? clipping.Metadata.SourcesCaption() : ""));

				if (clipping.ClippingRegionItemsCount.HasValue == false)
					clipping.ClippingRegionItemsCount = UI.GetItems<ClippingRegionItem>().Where(x => x.ClippingRegion == clipping).Count();

				dict.Add("Ítems", clipping.ClippingRegionItemsCount.ToString());

				SetChkItemsText(clipping);

				return dict;
			}
		}

		private void SetChkItemsText(ClippingRegion c)
		{
			int cant = c.ClippingRegionItemsCount.GetValueOrDefault();
			if (cant == 0)
				chkItems.Text = "No hay ítems para mostrar";
			else if (cant > Settings.CantItems)
				chkItems.Text = "Mostrar primeros " + Settings.CantItems + " ítems";
			else
				chkItems.Text = "Mostrar ítems";
		}



		private void LoadItems(ClippingRegion clipping)
		{

			if (chkItems.Checked == false)
				return;

			using (new WaitCursor())
			{
				lstItems.Items.Clear();
				lstContainer.Panel2Collapsed = true;
				lstCount.Text = "";

				if (clipping.ClippingRegionCaptions.Count == 0)
				{
					clipping.ClippingRegionCaptions = UI.GetItems<ClippingRegionItem>()
						.Where(x => x.ClippingRegion == clipping).Take(Settings.CantItems)
						.Select(x => new ClippingRegionCaption(x.Id.Value, x.Code,
							x.Caption, x.Parent.Caption, x.Parent.Code)).ToList();
				}

				foreach (var item in clipping.ClippingRegionCaptions)
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
				Invoker.CallProgress(new ClippingRegionDelete(GetSelectedNode()));
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


		private void mnuCopyItem_Click(object sender, EventArgs e)
		{
			var copy = "";
			foreach (ListViewItem item in lstItems.SelectedItems)
			{
				var ci = item.Tag as ClippingRegionItem;
				copy += ci.ToString() + System.Environment.NewLine;
			}
			if(copy.Trim() != "")
				Clipboard.SetText(copy.Trim());
		}

		private void trvList_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
				btnDelete.PerformClick();
		}

		private void lstItems_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lstItems.SelectedItems.Count == 0)
				return;
			lstContainer.Panel2Collapsed = false;
			IIdentifiable c = lstItems.SelectedItems[0].Tag as IIdentifiable;
			uGeometry1.LoadData<ClippingRegionItem>(c.Id.Value);
		}

		private void trvList_AfterSelect(object sender, TreeViewEventArgs e)
		{
			uEntity.Clear();
			lstItems.Items.Clear();
			var clipping = e.Node.Tag as ClippingRegion;
			if (clipping == null)
				return;

			using (new WaitCursor())
			{
				uEntity.Fill(GetDetail(clipping));
				LoadItems(clipping);
			}
		}
		private void chkItems_CheckedChanged(object sender, EventArgs e)
		{
			if(NoneSelected() == false && chkItems.Checked)
			{
				using (new WaitCursor())
				{
					uEntity.Fill(GetDetail(GetSelectedNode()));
					LoadItems(GetSelectedNode());
				}
			}
			splitContainer2.Panel2Collapsed = !chkItems.Checked;
		}

	}
}
