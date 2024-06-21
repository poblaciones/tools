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
using medea.controls;
using medea.actions;
using medea.entities;
using System.Windows.Forms;
using System.IO;
using medea.common;

namespace medea.winApp
{
	public partial class frmMetadataEdit : frmOkCancel
	{
		Metadata current = new Metadata();

		public frmMetadataEdit()
		{
//			current.User = UI.CurrentUser;
			InitializeComponent();
		}


		protected override void OnSubmit()
		{
			if (uMetadata.ValidateValues() == false)
				return;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}


		internal void LoadData(Metadata metadata)
		{
			uMetadata.LoadData(metadata);
		}

		public void ControlsToValues()
		{
			uMetadata.ControlsToValues();
		}
	}
}
