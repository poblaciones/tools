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
	public partial class frmClippingRegionEdit : frmOkCancel
	{
		ClippingRegion current = new ClippingRegion();

		public frmClippingRegionEdit()
		{
			current.Country = UI.CurrentCountry;
			InitializeComponent();
			uFile.Selected += uFile_Selected;
			uFile.Deleted += uFile_Deleted;

			uFile.RequiredExtensions.AddRange(new string[] { "shp", "dbf", "prj" });
			uFile.AutoProcessFileSelection = true;

			using (new WaitCursor())
			{
				var clippingRegions = UI.GetItems<ClippingRegion>().OrderBy(x => x.Caption).ToList();
				cmbParent.FillRecursive(clippingRegions);
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

			current.FieldCodeName = "";

			cmbParentItem.Items.Clear();
			cmbFieldCodeName.Items.Clear();
			cmbFieldCaptionName.Items.Clear();

			cmbParentItem.Enabled = true;
			cmbFieldCodeName.Enabled = true;
			cmbFieldCaptionName.Enabled = true;

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
			cmbFieldCaptionName.Items.Add("");
			cmbFieldCodeName.Items.Add("");

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
					cmbFieldCaptionName.Items.Add(fld.Name);
				}
			}
			cmbParentItem.SelectedIndex = 0;
			cmbFieldCodeName.SelectedIndex = 0;
			cmbFieldCaptionName.SelectedIndex = 0;

			cmbParentItem.SetDropDownWidth();
			cmbFieldCodeName.SetDropDownWidth();
			cmbFieldCaptionName.SetDropDownWidth();
		}

		public void LoadData(ClippingRegion clipping)
		{
			isNew = false;
			current = clipping;
			txtCaption.Text = current.Caption;
			chEnableLabels.Checked = !current.NoAutocomplete;
			txtMinZoom.Text = current.LabelsMinZoom.ToString();
			txtMaxZoom.Text = current.LabelsMaxZoom.ToString();
			cmbParent.SelectItem(current.Parent);
			txtPriority.Text = current.Priority.ToString();
			cmbSymbology.SetSelectedByTag(current.Symbol);

			if (uFile.HasFile)
			{
				cmbFieldCaptionName.Enabled = false;
				cmbFieldCodeName.Enabled = false;
				cmbParentItem.Enabled = false;
				cmbParent.Enabled = false;
				uFile.EnabledButtons = false;

				if (string.IsNullOrEmpty(current.FieldCodeName) == false)
				{
					cmbFieldCodeName.Items.Add(current.FieldCodeName);
					cmbFieldCodeName.SelectedIndex = 0;
				}
			}
		}

		public void SetValueCmbParent(ClippingRegion cr)
		{
			cmbParent.SelectItem(cr);
		}

		private new bool Validate()
		{
			var msg = "";

			if (txtCaption.Text.Trim() == "")
				msg += "Debe indicar un valor para 'Nombre'.\n";
			msg = frmGeographyEdit.CheckZoom(txtMinZoom, "Zoom mínimo", msg);
			msg = frmGeographyEdit.CheckZoom(txtMaxZoom, "Zoom máximo", msg);
			if (uFile.FileAdded)
			{
				if (cmbFieldCodeName.HasSelectedItem == false)
					msg += "Debe indicar un valor para 'Código' cuando hay un archivo.\n";
				if (cmbParentItem.HasSelectedItem && cmbParent.HasSelectedItem == false)
					msg += "Es necesario seleccionar un Padre en General si se selecciona uno en Campos.\n";
				if (cmbParentItem.HasSelectedItem == false && cmbParent.HasSelectedItem)
					msg += "Es necesario seleccionar un Padre en Campos si se selecciona uno en General.\n";
			}
			if (!cmbParent.HasSelectedItem)
				msg += "Debe indicar un valor para 'Padre'.\n";
			int dummy;
			if (!int.TryParse(txtPriority.Text, out dummy))
				msg += "Debe indicar una prioridad numérica.\n";

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
			current.NoAutocomplete = !chEnableLabels.Checked;
			current.Priority = int.Parse(txtPriority.Text);
			current.LabelsMaxZoom = int.Parse(txtMaxZoom.Text);
			current.LabelsMinZoom = int.Parse(txtMinZoom.Text);

			if (cmbParent.Enabled)
			{
				current.Parent = null;
				if (cmbParent.HasSelectedItem)
					current.Parent = cmbParent.GetSelectedItem<ClippingRegion>();
			}

			if (uFile.FileDeleted)
			{
				current.ClippingRegionItems.Clear();
			}
			if (uFile.FileAdded)
				current.FieldCodeName = cmbFieldCodeName.GetValue();

			//TODO: Borrar archivos si están en el directorio Path.GetTempPath()
			//if(name.StartsWith(Path.GetTempPath()))
			//Si zipeo borrar directo, si unzipeo borrar directorio...
			//System.IO.File.Delete(name);
			current.Symbol = cmbSymbology.GetSelectedItemTag();

			var iParent = cmbParentItem.GetValue();
			if (SelectedParentIsCountries())
				iParent = null;
			var iCode = cmbFieldCodeName.GetValue();
			var iCaption = cmbFieldCaptionName.GetValue();
			Call(new ClippingRegionSave(current, uFile.FileAdded, iParent, iCode, iCaption, uFile.Basename, UI.CurrentCountry));
		}

		private bool SelectedParentIsCountries()
		{
			return cmbParent.GetSelectedItem<ClippingRegion>().Parent == null;
		}

		private void cmbParent_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cmbParent.Enabled && cmbParent.HasSelectedItem)
			{
				//cmbParentItem.Enabled = !SelectedParentIsCountries();
				if (cmbParentItem.Enabled)
				{
					var c = cmbParent.GetSelectedItem<ClippingRegion>();
					cmbParentItem.SelectContainsText(c.FieldCodeName);
				}
			}
		}

		private void chEnableLabels_CheckedChanged(object sender, EventArgs e)
		{
			txtMaxZoom.Enabled = chEnableLabels.Checked;
			txtMinZoom.Enabled = chEnableLabels.Checked;
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
