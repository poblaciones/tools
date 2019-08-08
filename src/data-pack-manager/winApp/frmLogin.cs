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
using medea.controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace medea.winApp
{
	public partial class frmLogin : Form
	{
		public string User {  get { return txtUsername.Text;  } set { txtUsername.Text = value; } }
		public string Password {  get { return txtPassword.Text;  } set {
				txtPassword.Text = value;
				if (value == null && User != null)
				{
					chStorePassword.Checked = false;
				}
			} }
		public string Server { get { return txtServer.Text; } set { txtServer.Text = value; } }
		public frmLogin()
		{
			InitializeComponent();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			if (txtServer.Text == "" )
			{
				MessageBox.Show(this, "Atención", "Debe indicar un servidor.");
				return;
			}
			if (txtUsername.Text == "" || txtPassword.Text == "")
			{
				MessageBox.Show(this, "Atención", "Debe indicar un usuario y contraseña.");
				return;
			}
			RegistryPersistence.SaveUserToRegistry(User, (chStorePassword.Checked ? Password : ""), Server);
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
