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
	public partial class frmBoundary: Form
	{
		public frmBoundary()
		{
			InitializeComponent();
			splitContainer2.Panel2Collapsed = true;
			ListViewColumnSorter.Bind(listView);

		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			var f = new frmBoundaryEdit();
			if(f.ShowDialog(this) == DialogResult.OK)
				ReloadList();
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			if (NoneSelected())
				return;

			var f = new frmBoundaryEdit();
			f.LoadData(GetSelectedNode());
			if (f.ShowDialog(this) == DialogResult.OK)
				ReloadList();

		}

		private Boundary GetSelectedNode()
		{
			if (listView.SelectedItems.Count == 0)
				throw new MessageException("Debe seleccionar un ítem para realizar esta acción.");

			return listView.SelectedItems[0].Tag as Boundary;
		}

		private void ReloadList()
		{
			using (new WaitCursor())
			{

				listView.Items.Clear();
				uEntity.Clear();

				var items = UI.GetItems<Boundary>()
					.OrderBy(x => x.Caption).ToList();

				foreach (var item in items)
				{ 
					var lvItem = listView.Items.Add(item.Caption);
					lvItem.SubItems.Add(item.Group.Caption);
					lvItem.SubItems.Add((!item.Private ? "Sí" : "No"));
					lvItem.Tag = item;
				}
			}
		}

		private void frmBoundary_Load(object sender, EventArgs e)
		{
			ReloadList();
		}

		private Dictionary<string,string> GetDetail(Boundary Boundary)
		{
			using (new WaitCursor())
			{
				var dict = new Dictionary<string, string>();
				dict.Add("Id", Boundary.Id.ToString());
				dict.Add("Nombre", Boundary.Caption);
				dict.Add("Geografía", (Boundary.Geography != null ? Boundary.Geography.Caption : "Nulo"));
				if (Boundary.Order.HasValue)
					dict.Add("Orden", Boundary.Order.ToString());
				else
					dict.Add("Orden", "Nulo");
				dict.Add("Grupo", Boundary.Group.Caption);
				string regions = "";
				foreach (BoundaryClippingRegion c in Boundary.BoundaryClippingRegions)
					regions += (regions.Length > 0 ? ", ": "") + c.ClippingRegion.Caption;
				dict.Add("Contenido", regions);
			
				dict.Add("Metadatos", (Boundary.Metadata == null ? "Hereda" : "Propios"));
				dict.Add("Visible", (!Boundary.Private ? "Sí" : "No"));
			

				return dict;
			}
		}

		private void LoadItems(Boundary clipping)
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
				Invoker.CallProgress(new BoundaryDelete(GetSelectedNode()));
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

		
		private void listView_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
				btnDelete.PerformClick();
		}

		private void listView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			uEntity.Clear();
			var clipping = e.Node.Tag as Boundary;
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
			if (listView.SelectedItems.Count == 0)
				return;
			var Boundary = listView.SelectedItems[0].Tag as Boundary;
			using (new WaitCursor())
			{
				uEntity.Fill(GetDetail(Boundary));
				LoadItems(Boundary);
			}
		}

		private void listView_DoubleClick(object sender, EventArgs e)
		{
			btnEdit.PerformClick();
		}
	}
}
