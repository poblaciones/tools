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

namespace medea.controls
{
	public partial class uMetadata : UserControl
	{
		public Metadata current;
		private bool isNew = true;

		public uMetadata()
		{
			InitializeComponent();

		}
		private void UpdateFileOrder()
		{
			for (int n = 0; n < current.Files.Count; n++)
				current.Files[n].Order = n + 1;
		}
		public void ControlsToValues()
		{
			uInstitution.ControlsToValues();
			if (current.MetadataInstitutions.Count == 0)
			{
				var meta = new MetadataInstitution();
				meta.Metadata = current;
				current.MetadataInstitutions.Add(meta);
			}
			current.MetadataInstitutions[0].Institution = uInstitution.Current;

			uContact.ControlsToValues();
			current.Title = txtTitle.Text;
			current.Abstract = txtDescription.Text;
			current.AbstractLong = textBox1.Text;
			current.Authors = txtAuthor.Text;
			current.License = txtLicense.Text;
			current.ReleaseDate = txtReleaseDate.Text;

			current.CoverageCaption = txtCoverage.Text;
			current.Frequency = txtFrequency.Text;
			current.PeriodCaption = txtPeriod.Text;

			current.MetadataSources = uSource.Current;
		
			current.MetadataStatus = MetadataStatusEnum.Complete;
		}

		public bool ValidateValues()
		{
			var msg = "";

			if (txtTitle.Text.Trim() == "")
				msg += "Debe indicar un valor para 'Nombre'.\n";
			if (txtCoverage.Text.Trim() == "")
				msg += "Debe indicar un valor para 'Cobertura'.\n";
			if (txtDescription.Text.Trim() == "")
				msg += "Debe indicar un valor para 'Resumen'.\n";
			if (txtReleaseDate.Text.Trim() == "")
				msg += "Debe indicar un valor para 'Fecha de publicación'.\n";

			if (msg != "")
			{
				MessageBox.Show(msg.Trim(), "Errores del formulario:");
				return false;
			}
			return true;

		}

		public void LoadData(Metadata metadata)
		{
			isNew = false;
			current = metadata;
			txtTitle.Text = current.Title;
			txtDescription.Text = current.Abstract;
			textBox1.Text = current.AbstractLong;
			txtAuthor.Text = current.Authors;
			txtLicense.Text = current.License;

			txtCoverage.Text = current.CoverageCaption;
			txtFrequency.Text = current.Frequency;
			txtPeriod.Text = current.PeriodCaption;
			SetType(current.MetadataType);
			uSource.LoadData(current, metadata.MetadataSources);

			uFiles.LoadData(current);
			txtReleaseDate.Text = current.ReleaseDate;
		}

		public void SetType(WorkTypeEnum metadataType)
		{
			uContact.LoadData(current.Contact);
			if (current.MetadataInstitutions.Count == 0)
				throw new Exception("Los metadatos deben tener al menos una institución.");
			uInstitution.LoadData(current.MetadataInstitutions[0].Institution, true);
		}
	}
}