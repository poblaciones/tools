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
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using medea.common;

namespace medea.controls
{
	public partial class uFile : UserControl
	{
		private bool fromDb = false;
		public string SelectedFile { get; set; }
		public bool Mandatory = true;

		public bool HasFile { get { return string.IsNullOrEmpty(SelectedFile) == false; } }
		public bool FileAdded { get; set; }
		public bool FileDeleted { get; set; }
		public string Filter = "";
		public System.Collections.Generic.List<string> RequiredExtensions = new System.Collections.Generic.List<string>();

		public bool AutoProcessFileSelection = false;
		public event FileDownload Download;
		public delegate void FileDownload(object sender, EventArgs e);

		public event FileSelectedEventHandler Selected;
		public delegate void FileSelectedEventHandler(object sender, EventArgs e);

		public event FileDeletedEventHandler Deleted;
		private string _basename;
		public string Basename { get { return _basename; } }

		public delegate void FileDeletedEventHandler(object sender, EventArgs e);
		public uFile()
		{
			InitializeComponent();
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			if (this.Filter != "")
				ofd.Filter = Filter;
			//ofd.Filter = "Archivos dbf (*.dbf)|*.dbf|Archivos shp (*.shp)|*.shp";
			var res = ofd.ShowDialog();
			if (res == DialogResult.OK)
			{
				FileAdded = true;
				FileDeleted = false;
				SelectedFile = ofd.FileName;

				lnkFile.Text = Path.GetFileNameWithoutExtension(SelectedFile);
				lnkFile.Visible = true;
				if (AutoProcessFileSelection)
				{
					using (new WaitCursor())
					{
						_basename = "";
						var path = Path.GetDirectoryName(SelectedFile);
						if (Path.GetExtension(SelectedFile) == ".zip")
							path = Zip.UnzipFiles(SelectedFile);

						var name = Path.GetFileNameWithoutExtension(SelectedFile);

						_basename = Path.Combine(path, name);
						if (!ValidateSelectedFile())
						{
							return;
						}
					}
				}

				OnSelect();
			}
		}

		private bool ValidateSelectedFile()
		{
			string msg = "";
			foreach(string ext in this.RequiredExtensions)
			{
				if (System.IO.File.Exists(Basename + "." + ext) == false)
					msg += "No se encontró el archivo ." + ext + " debe estar en el mismo directorio y con el mismo nombre que el archivo seleccionado.\n";

				if (ext == "prj" && Projections.Validate(Basename + ".prj") == false)
					msg += Projections.ErrorMessage;
			}
			if (msg != "")
			{
				MessageBox.Show(msg, "Errores de archivos");
				return false;
			}
			return true;
		}

		private void OnSelect()
		{
			if (Selected != null)
				Selected.Invoke(this, EventArgs.Empty);
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (HasFile == false)
				return;

			if (MessageBox.Show("Se procederá a eliminar el elemento seleccionado.\n\n¿Está seguro de que desea hacer esto?",
				"Atención", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
			{
				Delete();
			}
			_basename = "";
		}

		public void Clear()
		{
			lnkFile.Text = "";
			lnkFile.Visible = false;
			SelectedFile = null;
			FileAdded = false;
		}

		private void lnkFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (HasFile == false)
				return;

			if (fromDb && Download != null)
				Download.Invoke(this, EventArgs.Empty);
		}

		public void SetFile(string name)
		{
			fromDb = true;
			lnkFile.Text = Path.GetFileNameWithoutExtension(name);
			lnkFile.Visible = true;
			SelectedFile = name;
		}

		public void ShowDownload(entities.File file)
		{
			if (file == null)
				throw new Exception("Archivo no encontrado en la base de datos.");
			var data = file.FileChunks;
			SaveFileDialog s = new SaveFileDialog();
			s.FileName = SelectedFile;
			s.Filter = "Todos los archivos (*.*)|*.*";
			if (s.ShowDialog(Form.ActiveForm) == DialogResult.Cancel)
				return;
			var path = s.FileName;
			if (File.Exists(path)) File.Delete(path);
			using (FileStream w = new FileStream(path, FileMode.Append))
			{
				foreach (var chunk in data)
					w.Write(chunk.Content, 0, chunk.Content.Length);
				w.Close();
			}
			var argument = "/select, \"" + path + "\"";
			Process.Start("explorer.exe", argument);

		}

		public bool EnabledButtons
		{
			get { return btnDelete.Enabled && btnEdit.Enabled; }
			set
			{
				btnDelete.Enabled = value;
				btnEdit.Enabled = value;
			}
		}


		public void Delete()
		{
			Clear();
			FileDeleted = true;
			if (Deleted != null)
				Deleted.Invoke(this, EventArgs.Empty);
		}

		private void uFile_Load(object sender, EventArgs e)
		{
			if (Mandatory == false)
				lblFile.Text = lblFile.Text.Replace("*", "");
		}
	}
}
