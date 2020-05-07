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
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using System.IO;
using medea.entities;
using System.Linq;
using System;
using System.Windows.Forms;
using medea.common;

namespace medea.winApp
{
	public partial class frmGeographyEdit : frmOkCancel
	{
		Geography current = new Geography();

		public frmGeographyEdit()
		{
			current.Country = UI.CurrentCountry;

			InitializeComponent();
			uFile.Selected += uFile_Selected;
			uFile.Deleted += uFile_Deleted;
			uFile.Filter = "Archivos shapefile (*.shp)|*.shp|Todos los archivos (*.*)|*.*";

			uFile.RequiredExtensions.AddRange(new string[] { "shp", "dbf", "prj" });
			uFile.AutoProcessFileSelection = true;

			uFileMissings.RequiredExtensions.AddRange(new string[] { "dbf" });
			uFile.AutoProcessFileSelection = true;

			using (new WaitCursor())
			{
				var parents = UI.GetGeographies().ToList();
				cmbParent.FillRecursive(parents);

				cmbGradient.Fill(UI.GetGradients(), x => x.Caption);
			}
		}

		private void uFile_Deleted(object sender, EventArgs e)
		{
			ClearFileFields();
		}

		private void ClearFileFields()
		{
			lblRecords.Text = "";
			lblRecords.Visible = false;
			lblShapeType.Visible = false;
			lblShapeType.Text = "";

			current.FieldCaptionName = "";
			current.FieldCodeName = "";

			cmbParentItem.Items.Clear();
			cmbFieldCodeName.Items.Clear();
			cmbChildren.Items.Clear();
			cmbFieldCaptionName.Items.Clear();
			cmbHousehold.Items.Clear();
			cmbPopulation.Items.Clear();
			cmbUrbanity.Items.Clear();

			cmbParentItem.Enabled = true;
			cmbFieldCodeName.Enabled = true;
			cmbChildren.Enabled = true;
			cmbFieldCaptionName.Enabled = true;
			cmbHousehold.Enabled = true;
			cmbPopulation.Enabled = true;

			chPartialCoverage.Enabled = true;
			chPartialCoverage_CheckedChanged(null, null);
		}

		private void uFile_Selected(object sender, EventArgs e)
		{
			using (new WaitCursor())
			{
				ClearFileFields();

				SetColumns(uFile.Basename + ".shp");

				cmbParent_SelectedIndexChanged(sender, e);
			}
		}

		private void SetColumns(string file)
		{
			cmbParentItem.Items.Add("");
			cmbFieldCodeName.Items.Add("");
			cmbChildren.Items.Add("");
			cmbFieldCaptionName.Items.Add("");
			cmbHousehold.Items.Add("");
			cmbPopulation.Items.Add("");
			cmbUrbanity.Items.Add("");

			using (ShapefileDataReader dr = new ShapefileDataReader(file, new GeometryFactory()))
			{
				ShapefileHeader shpHeader = dr.ShapeHeader;
				var header = dr.DbaseHeader;

				lblShapeType.Visible = true;
				lblShapeType.Text = "Tipo de shp: " + shpHeader.ShapeType.ToString();

				lblRecords.Visible = true;
				lblRecords.Text = "Registros dbf: " + header.NumRecords;

				for (int i = 0; i < header.NumFields; i++)
				{
					var fld = header.Fields[i];
					cmbParentItem.Items.Add(fld.Name);
					cmbFieldCodeName.Items.Add(fld.Name);
					cmbChildren.Items.Add(fld.Name);
					cmbFieldCaptionName.Items.Add(fld.Name);
					cmbHousehold.Items.Add(fld.Name);
					cmbPopulation.Items.Add(fld.Name);
					cmbUrbanity.Items.Add(fld.Name);
				}
			}
			cmbParentItem.SelectedIndex = 0;
			cmbFieldCodeName.SelectedIndex = 0;
			cmbChildren.SelectedIndex = 0;
			cmbFieldCaptionName.SelectedIndex = 0;
			cmbHousehold.SelectedIndex = 0;
			cmbPopulation.SelectedIndex = 0;
			cmbUrbanity.SelectedIndex = 0;

			cmbParentItem.SetDropDownWidth();
			cmbFieldCodeName.SetDropDownWidth();
			cmbChildren.SetDropDownWidth();
			cmbFieldCaptionName.SetDropDownWidth();
			cmbHousehold.SetDropDownWidth();
			cmbPopulation.SetDropDownWidth();
		}

		public void LoadData(Geography geography)
		{
			isNew = false;
			current = geography;
			txtCaption.Text = current.Caption;
			txtRevision.Text = current.Revision;
			cmbParent.SelectItem(current.Parent);
			txtMaxZoom.Text = current.MaxZoom.ToString();
			txtFieldCodeSize.Text = current.FieldCodeSize.ToString();
			cmbGradient.SelectItem<Gradient>(current.Gradient);
			txtLuminance.Text = (current.GradientLuminance.HasValue ? current.GradientLuminance.Value.ToString() : "");

			chPartialCoverage.Checked = current.PartialCoverage != null;
			if (chPartialCoverage.Checked)
				txtCoverage.Text = current.PartialCoverage;

			if (uFile.HasFile)
			{
				cmbChildren.Enabled = false;
				cmbHousehold.Enabled = false;
				cmbPopulation.Enabled = false;
				cmbFieldCaptionName.Enabled = false;
				cmbFieldCodeName.Enabled = false;
				cmbParentItem.Enabled = false;
				cmbParent.Enabled = false;
				cmbUrbanity.Enabled = false;
				uFile.EnabledButtons = false;
				chPartialCoverage.Enabled = false;
				uFileMissings.Enabled = false;

				if (string.IsNullOrEmpty(current.FieldCodeName) == false)
				{
					cmbFieldCodeName.Items.Add(current.FieldCodeName);
					cmbFieldCodeName.SelectedIndex = 0;
				}

				if (string.IsNullOrEmpty(current.FieldUrbanityName) == false)
				{
					cmbUrbanity.Items.Add(current.FieldUrbanityName);
					cmbUrbanity.SelectedIndex = 0;
				}
				if (string.IsNullOrEmpty(current.FieldCaptionName) == false)
				{
					cmbFieldCaptionName.Items.Add(current.FieldCaptionName);
					cmbFieldCaptionName.SelectedIndex = 0;
				}
			}
		}

		public void SetValueCmbParent(Geography geography)
		{
			cmbParent.SelectItem(geography);
		}

		private new bool Validate()
		{
			var msg = "";
			if (txtCaption.Text.Trim() == "")
				msg += "Debe indicar un valor para 'Nombre'.\n";
			msg = CheckZoom(txtMaxZoom, "Zoom máximo", msg);

			if (uFile.FileAdded)
			{
				if (cmbFieldCodeName.HasSelectedItem == false)
					msg += "Debe indicar un valor para 'Código' cuando hay un archivo.\n";
				if (cmbParentItem.HasSelectedItem && cmbParent.HasSelectedItem == false)
					msg += "Es necesario seleccionar un Padre en General si se selecciona uno en Campos.\n";
				if (cmbParentItem.HasSelectedItem == false && cmbParent.HasSelectedItem)
					msg += "Es necesario seleccionar un Padre en Campos si se selecciona uno en General.\n";
				if (cmbPopulation.HasSelectedItem == false)
					msg += "Debe indicar un valor para 'Población' cuando hay un archivo.\n";
				if (cmbChildren.HasSelectedItem == false)
					msg += "Debe indicar un valor para 'Niños' cuando hay un archivo.\n";
				if (cmbHousehold.HasSelectedItem == false)
					msg += "Debe indicar un valor para 'Hogares' cuando hay un archivo.\n";
			}
			if (chPartialCoverage.Checked && txtCoverage.Text.Trim() == "")
				msg += "Debe indicar un detalle para la cobertura parcial.\n";

			if (msg != "")
			{
				MessageBox.Show(msg.Trim(), "Errores del formulario:");
				return false;
			}
			return true;
		}

		public static string CheckZoom(TextBox controlZoom, string label, string msg)
		{
			int zoom;
			if (int.TryParse(controlZoom.Text, out zoom) == false)
				msg += "Debe indicar un valor numérico para '" + label + "'.\n";
			else if (zoom < 0 || zoom > 21)
				msg += "El valor para '" + label + "' debe ser entre 1 y 22.\n";

			return msg;
		}

		protected override void OnSubmit()
		{
			if (Validate() == false)
				return;

			ControlsToValues();

			//TODO: Borrar archivos si están en el directorio Path.GetTempPath()
			//if(name.StartsWith(Path.GetTempPath()))
			//Si zipeo borrar directo, si unzipeo borrar directorio...
			//System.IO.File.Delete(name);
			var household = cmbHousehold.GetValue();
			var children = cmbChildren.GetValue();
			var population = cmbPopulation.GetValue();
			var parent = cmbParentItem.GetValue();
			var code = cmbFieldCodeName.GetValue();
			var caption = cmbFieldCaptionName.GetValue();
			var urbanity = cmbUrbanity.GetValue();
			string filedbf = null;

			if (chPartialCoverage.Checked && uFileMissings.FileAdded)
				filedbf = uFile.Basename + ".dbf";
			Call(new GeographySave(current, uFile.FileAdded, household,
					 children, population, urbanity, parent, code, caption, uFile.Basename, filedbf));

			Call(new MetadataClearRemoteCache(current.Metadata));
		}

		private void ControlsToValues()
		{
			current.Caption = txtCaption.Text.Trim();
			current.Revision = txtRevision.Text.Trim();
			current.MaxZoom = int.Parse(txtMaxZoom.Text);
			foreach (var child in current.Children)
				child.MinZoom = current.MaxZoom + 1;

			current.Gradient = cmbGradient.GetSelectedItem<Gradient>();
			current.GradientLuminance = (txtLuminance.Text == "" ? (float?) null : float.Parse(txtLuminance.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture));
			
			if (current.Parent != null)
				current.MinZoom = current.Parent.MaxZoom + 1;
			else
				current.MinZoom = 0;

			if (chPartialCoverage.Checked)
			{
				current.PartialCoverage = txtCoverage.Text.Trim();
			}
			else
				current.PartialCoverage = null;
			if (cmbParent.Enabled)
			{
				current.Parent = null;
				if (cmbParent.HasSelectedItem)
					current.Parent = cmbParent.GetSelectedItem<Geography>();
			}

			if (uFile.FileAdded)
			{
				current.FieldCaptionName = cmbFieldCaptionName.GetValue();
				current.FieldCodeName = cmbFieldCodeName.GetValue();
				current.FieldCodeSize = int.Parse(txtFieldCodeSize.Text);
				current.FieldUrbanityName = cmbUrbanity.GetValue();
			}

			if (uFile.FileDeleted)
			{
				current.GeographyItems.Clear();
			}
		}

		private void cmbParent_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cmbParent.Enabled && cmbParent.HasSelectedItem)
			{
				var c = cmbParent.GetSelectedItem<Geography>();
				cmbParentItem.SelectContainsText(c.FieldCodeName);
			}
		}

		private void chPartialCoverage_CheckedChanged(object sender, EventArgs e)
		{
			txtCoverage.Enabled = chPartialCoverage.Checked;
			uFileMissings.Enabled = chPartialCoverage.Checked && uFile.Enabled;

		}

		private void btnEditMetadata_Click(object sender, EventArgs e)
		{
			frmMetadataEdit edit = new frmMetadataEdit();
			if (current.Metadata == null) current.Metadata = new Metadata(WorkTypeEnum.Geography);
			edit.LoadData(current.Metadata);
			if (edit.ShowDialog(this) != DialogResult.Cancel)
				edit.ControlsToValues();
		}
	}
}
