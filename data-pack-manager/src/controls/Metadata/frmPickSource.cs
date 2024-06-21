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
using medea.actions;
using medea.entities;
using System.Linq;
using System.Windows.Forms;
using medea.common;
using System.IO;
using System;

namespace medea.controls
{
	public partial class frmPickSource : frmOkCancel
	{
		public Source current = null;
		public frmPickSource()
		{
			InitializeComponent();

		}

		public void LoadSources()
		{
			using (new WaitCursor())
			{
				cmbSources.Optional = true;
				var sources = UI.GetPublicSources();
				cmbSources.Fill(sources, x => x.CaptionVersion);
				cmbSources.SelectedIndex = 0;
			}
		}

		protected override void OnSubmit()
		{
			this.current = cmbSources.GetSelectedItem<Source>();
			if (this.current == null)
			{
				UI.ShowMessage(this, "Debe seleccionar un valor.");
				return;
				}
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

	}
}
