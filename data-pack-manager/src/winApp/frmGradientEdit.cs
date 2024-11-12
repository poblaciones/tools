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
using medea.actions;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using System.IO;
using medea.entities;
using System.Linq;
using System;
using System.Windows.Forms;
using medea.common;
using System.Data.SQLite;

namespace medea.winApp
{
	public partial class frmGradientEdit : frmOkCancel
	{
		Gradient current = new Gradient();

		public frmGradientEdit()
		{
			current.Country = UI.CurrentCountry;
			InitializeComponent();
			uFile.Selected += uFile_Selected;
			uFile.Deleted += uFile_Deleted;

			uFile.RequiredExtensions.Add("gpkg");
			uFile.AutoProcessFileSelection = true;

		}

		private void uFile_Deleted(object sender, EventArgs e)
		{
			ClearFileFields();
		}

		private void ClearFileFields()
		{
			cmbTable.Items.Clear();
			txtMaxZoomLevel.Text = "";
		}


		public void LoadData(Gradient gradient)
		{
			isNew = false;
			current = gradient;
			txtCaption.Text = current.Caption;
			txtMaxZoomLevel.Text = current.MaxZoomLevel.ToString();
			txtMaxZoomLevel.ReadOnly = true;
			if (current.ImageType == "image/jpeg") radJpg.Checked = true;
			if (current.ImageType == "image/png") radPng.Checked = true;
		
			if (uFile.HasFile)
			{
				cmbTable.Enabled = false;
				uFile.EnabledButtons = false;
			}
		}

		private new bool Validate()
		{
			var msg = "";

			if (txtCaption.Text.Trim() == "")
				msg += "Debe indicar un valor para 'Nombre'.\n";
			msg = frmGeographyEdit.CheckZoom(txtMaxZoomLevel, "Máximo nivel de zoom", msg);
			if (uFile.FileAdded)
			{
				if (cmbTable.SelectedIndex <= 0)
					msg += "Es necesario seleccionar un nombre de tabla.\n";
			}
			int dummy;
			if (!int.TryParse(txtMaxZoomLevel.Text, out dummy))
				msg += "Debe indicar un máximo nivel de zoom.\n";

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
			string table = cmbTable.SelectedItem as string;
			current.Caption = txtCaption.Text.Trim();
			current.ImageType = (radJpg.Checked ? "image/jpeg" : "image/png");
			if (uFile.FileAdded)
				UpdateMaxLevel(table);
			else
				current.MaxZoomLevel = int.Parse(txtMaxZoomLevel.Text);
			
			Call(new GradientSave(current, uFile.FileAdded, uFile.FileDeleted,
								table, uFile.SelectedFile, UI.CurrentCountry));
			MarkTableUpdate.UpdateTables(new string[] { "gradient", "gradient_item" });

		}
		private void UpdateMaxLevel(string table)
		{
			// abre el archivo de sqlite
			string cs = "Data Source='" + uFile.SelectedFile + "';Version=3;";

			var con = new SQLiteConnection(cs);
			con.Open();
			var cmdCount = new SQLiteCommand(con);
			cmdCount.CommandText = "SELECT MAX(zoom_level) FROM " + table;
			SQLiteDataReader readerCount = cmdCount.ExecuteReader();
			while (readerCount.Read())
				current.MaxZoomLevel = readerCount.GetInt32(0);
			readerCount.Close();
		}

		private void uFile_Selected(object sender, EventArgs e)
		{
			string cs = "Data Source='" + uFile.SelectedFile + "';Version=3;";
			var con = new SQLiteConnection(cs);
			con.Open();
			var cmd = new SQLiteCommand(con);
			cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table'";
			cmbTable.Items.Clear();
			SQLiteDataReader reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				cmbTable.Items.Add(reader.GetString(0));
			}
			reader.Close();
			con.Close();
		}

		private void uFile_Load(object sender, EventArgs e)
		{

		}
	}
}
