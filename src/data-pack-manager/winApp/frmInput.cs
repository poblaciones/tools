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
﻿using medea.controls;
using System.Windows.Forms;

namespace medea.winApp
{
	public partial class frmInput : frmOkCancel
	{
		public string Value {
			get { return txtCaption.Text.Trim(); }
			set { txtCaption.Text = value; }
		}
		public string Title
		{
			get { return Text; }
			set { Text = value; }
		}

		public string Label {
			get { return lblTitle.Text; }
			set { lblTitle.Text = value; }
		}

		public bool Required { get; set; }

		public frmInput()
		{
			InitializeComponent();
			Required = true;
		}

		protected override void OnSubmit()
		{
			if(Required && txtCaption.Text.Trim() == "")
			{
				UI.ShowInfoMessage(this, "Debe completar el texto.");
				return;
			}
			DialogResult = DialogResult.OK;
			Close();
		}
	}
}
