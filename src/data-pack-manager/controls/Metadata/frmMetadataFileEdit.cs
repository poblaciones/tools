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
	public partial class frmMetadataFileEdit : frmOkCancel
	{
		public MetadataFile current = new MetadataFile();

		public frmMetadataFileEdit()
		{
			InitializeComponent();

			uFile.Mandatory = false;
			uFile.Filter = "Archivos PDF (*.pdf)|*.pdf|Todos los archivos (*.*)|*.*";
			uFile.Download += uFile_Download;
			uFile.Selected += UFile_Selected1;
		}

		private void UFile_Selected1(object sender, EventArgs e)
		{
			string name = uFile.SelectedFile;
			string nameonly = Path.GetFileNameWithoutExtension(name);
			this.txtCaption.Text = nameonly;
		}


		private void uFile_Download(object sender, EventArgs e)
		{
			uFile.ShowDownload(current.File);
		}

		public void LoadData(MetadataFile current)
		{
			isNew = false;
			this.current = current;
			txtCaption.Text = current.Caption;
			txtUrl.Text = current.Web;
			if (current.File != null)
				uFile.SetFile(current.File.Name);

		}

		private new bool Validate()
		{
			var msg = "";

			if (txtCaption.Text.Trim() == "")
				msg += "Debe indicar un valor para 'Nombre'.\n";

			if (txtUrl.Text.Trim() != "" && (uFile.FileAdded || (current.File != null && uFile.FileDeleted == false)))
				msg += "Sólo corresponde indicar un contenido para el adjunto (o la ruta externa, o el archivo).\n";

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
			if (uFile.FileDeleted)
				current.File = null;
			if (uFile.FileAdded)
			{
				current.FileAdded = true;
				var name = uFile.SelectedFile;
				entities.File file = null;
				file = new entities.File();
				file.Name = Path.GetFileName(name);
				file.CreateChunks(name);
								if (name.EndsWith(".pdf"))
				{
					file.Type = "application/pdf";
					file.Pages = GetPages(name);
				}
				current.File = file;
			}
			else
				current.FileAdded = false;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private int? GetPages(string name)
		{
			return null;
		}

	}
}
