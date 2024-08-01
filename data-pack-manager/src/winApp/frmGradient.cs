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
	public partial class frmGradient: Form
	{
		public frmGradient()
		{
			InitializeComponent();
			splitContainer2.Panel2Collapsed = true;
			ListViewColumnSorter.Bind(lstItems);
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			var f = new frmGradientEdit();
			if(f.ShowDialog(this) == DialogResult.OK)
				ReloadList();
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			if (NoneSelected())
				return;

			var f = new frmGradientEdit();
			f.LoadData(GetSelectedNode());
			if (f.ShowDialog(this) == DialogResult.OK)
				ReloadList();

		}

		private Gradient GetSelectedNode()
		{
			if (listView.SelectedItems.Count == 0)
				throw new MessageException("Debe seleccionar un ítem para realizar esta acción.");

			return listView.SelectedItems[0].Tag as Gradient;
		}

		private void ReloadList()
		{
			using (new WaitCursor())
			{

				listView.Items.Clear();
				lstItems.Items.Clear();
				uEntity.Clear();

				var items = UI.GetItems<Gradient>()
					.OrderBy(x => x.Caption).ToList();

				foreach (var item in items)
				{ 
					var lvItem = listView.Items.Add(item.Caption);
					lvItem.Tag = item;
				}
			}
		}

		private void frmGradient_Load(object sender, EventArgs e)
		{
			ReloadList();
		}

		private Dictionary<string,string> GetDetail(Gradient gradient)
		{
			using(new WaitCursor())
			{
				var dict = new Dictionary<string, string>();
				dict.Add("Id", gradient.Id.ToString());
				dict.Add("Nombre", gradient.Caption);
				dict.Add("Máximo nivel de zoom", gradient.MaxZoomLevel.ToString());
				dict.Add("Tipo", gradient.ImageType);

				return dict;
			}
		}

		private void LoadItems(Gradient clipping)
		{

			if (chkItems.Checked == false)
				return;


		}

		private void listView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			btnEdit.PerformClick();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (NoneSelected())
				return;

			if (UI.ConfirmDeleteRecursive(this))
			{
				Invoker.CallProgress(new GradientDelete(GetSelectedNode()));
				MarkTableUpdate.UpdateTables(new string[] { "gradient", "gradient_item" });

				ReloadList();
			}
		}

		private bool NoneSelected()
		{
			return listView.SelectedItems.Count == 0 || GetSelectedNode() == null;
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
				var ci = item.Tag as GradientItem;
				copy += ci.ToString() + System.Environment.NewLine;
			}
			if(copy.Trim() != "")
				Clipboard.SetText(copy.Trim());
		}

		private void listView_KeyDown(object sender, KeyEventArgs e)
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
			uGeometry1.LoadData<GradientItem>(c.Id.Value);
		}

		private void listView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			uEntity.Clear();
			lstItems.Items.Clear();
			var clipping = e.Node.Tag as Gradient;
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

		private void listView_SelectedIndexChanged(object sender, EventArgs e)
		{
			uEntity.Clear();
			lstItems.Items.Clear();
			if (listView.SelectedItems.Count == 0)
				return;
			var gradient = listView.SelectedItems[0].Tag as Gradient;
			using (new WaitCursor())
			{
				uEntity.Fill(GetDetail(gradient));
				LoadItems(gradient);
			}
		}

		private void listView_DoubleClick(object sender, EventArgs e)
		{
			btnEdit.PerformClick();
		}
	}
}
