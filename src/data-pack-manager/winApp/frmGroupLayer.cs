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
using System.Linq;
using System.Windows.Forms;
using medea.actions;
using medea.controls;
using medea.entities;
using medea.common;

namespace medea.winApp
{
	public partial class frmGroupLayer : Form
	{
		public frmGroupLayer()
		{
			InitializeComponent();
		}

		private void btnNewGroup_Click(object sender, EventArgs e)
		{
			var f = new frmLayerGroupEdit();
			if (f.ShowDialog(this) == DialogResult.OK)
				ReloadTree();
		}

		private void ReloadTree()
		{
			trvList.Nodes.Clear();
			//lstItems.Items.Clear();

			var items = UI.GetItems<LayerGroup>()
				.OrderBy(x => x.Caption).ToList().GenerateTree();

			TreeNode node = new TreeNode("raíz");

			TreeItem<LayerGroup>.MakeNodeTree(items, node);
			foreach (TreeNode item in node.Nodes)
				trvList.Nodes.Add(item);

			trvList.ExpandAll();

			if (NoneSelected() == false)
				LoadItems(GetSelectedNode());

		}

		private void LoadItems(LayerGroup layerGroup)
		{
			lstItems.Items.Clear();
			foreach (var item in layerGroup.Layers.Where(x => x.User == UI.CurrentUser &&
						x.Versions.Any(l => l.Dataset.Cartography.Country == UI.CurrentCountry)).OrderBy(x => x.Caption))
			{
				var ele = new ListViewItem(item.ToArray());
				ele.Tag = item;
				lstItems.Items.Add(ele);
			}
		}

		private bool None()
		{
			return trvList.Nodes.Count == 0;
		}
		private bool NoneSelected()
		{
			return trvList.SelectedNode == null || GetSelectedNode() == null;
		}

		private LayerGroup GetSelectedNode()
		{
			if (trvList.SelectedNode == null)
				throw new MessageException("Debe seleccionar un ítem del árbol para realizar esta acción.");

			return trvList.SelectedNode.Tag as LayerGroup;
		}

		private void btnEditGroup_Click(object sender, EventArgs e)
		{
			if (None())
				return;

			var f = new frmLayerGroupEdit();
			f.LoadData(GetSelectedNode());
			if (f.ShowDialog(this) == DialogResult.OK)
				ReloadTree();
		}

		private void trvList_DoubleClick(object sender, EventArgs e)
		{
			btnEditGroup.PerformClick();
		}

		private void btnDeleteGroup_Click(object sender, EventArgs e)
		{
			if (None())
				return;

			if (UI.ConfirmDeleteRecursive(this))
			{
				Invoker.CallProgress(new LayerGroupDelete(GetSelectedNode()));
				ReloadTree();
			}
		}

		private void mnuDeleteGroup_Click(object sender, EventArgs e)
		{
			btnDeleteGroup.PerformClick();
		}

		private void mnuEditGroup_Click(object sender, EventArgs e)
		{
			btnEditGroup.PerformClick();
		}

		private void frmLayer_Load(object sender, EventArgs e)
		{
			ReloadTree();
		}

		private void trvList_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (NoneSelected() == false)
				LoadItems(GetSelectedNode());
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{

		}
	}
}
