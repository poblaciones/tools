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
using medea.controls;
using medea.actions;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using System.IO;
using medea.entities;
using System.Linq;
using System;
using System.Windows.Forms;
using medea.common;
using System.Data.SQLite;
using System.Collections.Generic;

namespace medea.winApp
{
	public partial class frmBoundaryEdit : frmOkCancel
	{
		Boundary current = new Boundary();

		public frmBoundaryEdit()
		{
			InitializeComponent();

			var groups = UI.GetItems<BoundaryGroup>().OrderBy(x => x.Caption).ToList();
			cmbGroup.Fill<BoundaryGroup>(groups, x => x.Caption);
		}

		public void FillRecursive<T>(CheckedListBox list, IEnumerable<T> items)
			where T : ActiveBaseEntity<T>, IIdentifiable, IRecursive<T>
		{
			list.Items.Clear();
			var res = new Dictionary<int, string>();
			TreeItem<T>.MakeDictionary(items.GenerateTree(), res);
			foreach (var item in res)
			{
				T tag = items.Where(x => x.Id == item.Key).First();
				string key = item.Key.ToString();
				string text = item.Value;
				ObjectCaption i = new ObjectCaption(tag, text);
				list.Items.Add(i);
			}
		}
		public void LoadData(Boundary boundary)
		{
			isNew = false;
			current = boundary;
			txtCaption.Text = current.Caption;
			if (current.Order.HasValue)
				txtOrden.Text = current.Order.ToString();
			else
				txtOrden.Text = "";
			cmbGroup.SelectItem(current.Group);
			chVisible.Checked = !current.Private;
		}


		private new bool Validate()
		{
			var msg = "";

			if (txtCaption.Text.Trim() == "")
				msg += "Debe indicar un valor para 'Nombre'.\n";
		
			if (msg != "")
			{
				MessageBox.Show(msg.Trim(), "Errores del formulario:");
				return false;
			}
			return true;
		}

		protected override void OnSubmit()
		{
			if (Validate() == false)
				return;

			current.Caption = txtCaption.Text.Trim();

			if (txtOrden.Text.Length > 0)
				current.Order = int.Parse(txtOrden.Text);
			else
				current.Order = null;
			current.Group = cmbGroup.GetSelectedItem<BoundaryGroup>();
			current.Private = !chVisible.Checked;
			Call(new BoundarySave(current));
			MarkTableUpdate.UpdateTables(new string[] { "boundary", "boundary_item" });

		}

	}
}
