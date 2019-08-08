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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using medea.entities;
using medea.common;

namespace medea.controls
{
	public partial class uInstitution: UserControl
	{
		private Institution current;

		public Institution Current { get { return current; } }

		public uInstitution()
		{
			InitializeComponent();
			cmbInstitution.AutoSizeByContent = false;
		}
		bool isLoading = false;
		internal void LoadData(Institution institution, bool loadItems = false)
		{
			if (loadItems)
				LoadInstitutions();
			if (isLoading) return;
			isLoading = true;
			current = institution;
			if (current.IsNew == false)
			{
				cmbInstitution.SelectItem<Institution>(current);
				EnableControls(true);
			}
			else
				EnableControls(true);

			txtEmail.Text = current.Email;
			txtAddress.Text = current.Address;
			txtName.Text = current.Caption;
			txtPhone.Text = current.Phone;
			txtWeb.Text = current.Web;
			txtCountry.Text = current.Country;
			isLoading = false;
		}
		private void LoadInstitutions()
		{
			using (new WaitCursor())
			{
				IList<Institution> institutions;
				institutions= UI.GetPublicInstitutions();
				cmbInstitution.Optional = true;
				cmbInstitution.Fill(institutions, x => x.Caption);
				cmbInstitution.SelectedIndex = 0;
				cmbInstitution.AddItem<Institution>(null, "N", "<Nueva>");
			}
		}
		internal void ControlsToValues()
		{
			if (current == null || cmbInstitution.SelectedIndex == 0)
			{
				current = null;
				return;
			}
			current.Email = txtEmail.Text ;
			current.Address = txtAddress.Text ;
			current.Caption = txtName.Text ;
			current.Phone = txtPhone.Text ;
			current.Web = txtWeb.Text ;
			current.Country = txtCountry.Text;

		}

		private void cmbInstitution_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cmbInstitution.SelectedIndex == 0)
			{
				EnableControls(false);
				return;
			}
			else
				EnableControls(true);

			if (SelectedNew())
			{
				this.current = Activator.CreateInstance(this.current.GetType()) as Institution;
			}
			else
				this.current = cmbInstitution.GetSelectedItem<Institution>();
			LoadData(this.current);
		}
		private bool SelectedNew()
		{
			if (cmbInstitution.HasSelectedItem == false)
				return false;
			return cmbInstitution.GetSelectedComboItem<Institution>().Key == "N";
		}


		private void EnableControls(bool v)
		{

			txtEmail.Enabled = v;
			txtAddress.Enabled = v;
			txtName.Enabled = v;
			txtPhone.Enabled = v;
			txtWeb.Enabled = v;
			txtCountry.Enabled = v;
		}
	}
}
