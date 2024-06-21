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
	public partial class frmEditSource : frmOkCancel
	{
		public Source current = new Source();

		public frmEditSource()
		{
			InitializeComponent();

		}

		protected override void OnSubmit()
		{

			ControlsToValues();
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		internal void LoadData(Source source)
		{
			this.current = source;

			if (current == null)
				return;

			txtCaption2.Text = current.Caption;
			txtVersion.Text = current.Version;
			txtAuthors.Text = current.Authors;
			txtWiki.Text = current.Wiki;
			txtWeb.Text = current.Web;
			if (current.Contact == null)
			{
				current.Contact = new Contact();
			}
			uContact.LoadData(current.Contact);
			if (current.Institution == null)
			{
				current.Institution.IsGlobal = true;
				current.Institution = new Institution();
			}
			uInstitution.LoadData(current.Institution, true);
		}

		public void ControlsToValues()
		{
			uInstitution.ControlsToValues();
			uContact.ControlsToValues();
			this.current.Institution = uInstitution.Current;
			current.Caption = txtCaption2.Text;
			current.Version = txtVersion.Text;
			current.Authors = txtAuthors.Text;
			current.Wiki = txtWiki.Text;
			current.Web = txtWeb.Text;
		}
	}
}
