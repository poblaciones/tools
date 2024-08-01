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
using System.Windows.Forms;
using medea.entities;
using System.Linq;
using System.Collections.Generic;
using medea.common;

namespace medea.winApp
{
	public partial class frmPickName : frmOkCancel
	{
		public string Caption;
		public frmPickName()
		{
			InitializeComponent();
		}

		protected override void OnSubmit()
		{
			this.Caption = txtName.Text;
			if (this.Caption == "")
				throw new MessageException("Debe indicar un nombre.");

			DialogResult = DialogResult.OK;
			Close();
		}
	}
}
