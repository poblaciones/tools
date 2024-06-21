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
using medea.common;

namespace medea.controls
{
	public partial class uMetadataFiles : UserControl
	{
		public Metadata current;
		public uMetadataFiles()
		{
			InitializeComponent();
		}

		public void LoadData(Metadata metadata)
		{
			this.current = metadata;
			ReloadFiles();
		}
		private void lstFiles_DoubleClick(object sender, EventArgs e)
		{
				btnEditFile.PerformClick();
		}

		private void btnDown_Click(object sender, EventArgs e)
		{
			if (lstFiles.SelectedIndices.Count == 0)
				return;
			int i = lstFiles.SelectedIndices[0];
			if (i == lstFiles.Items.Count - 1)
				return;
			var keep = current.Files[i];
			current.Files.RemoveAt(i);
			current.Files.Insert(i + 1, keep);
			ReloadFiles();
		}


		private void btnUp_Click(object sender, EventArgs e)
		{
			if (lstFiles.SelectedIndices.Count == 0)
				return;
			int i = lstFiles.SelectedIndices[0];
			if (i == 0)
				return;
			var keep = current.Files[i];
			current.Files.RemoveAt(i);
			current.Files.Insert(i - 1, keep);
			ReloadFiles();
		}


		private void btnEditFile_Click(object sender, EventArgs e)
		{
			if (lstFiles.SelectedItems.Count == 0)
				return;

			frmMetadataFileEdit f = new frmMetadataFileEdit();
			f.LoadData(GetSelectedFile());
			f.ShowDialog(this);
			if (f.DialogResult == DialogResult.OK)
				ReloadFiles();
		}

		private void ReloadFiles()
		{
			using (new KeepSelected(lstFiles))
			{
				lstFiles.Items.Clear();
				foreach (var file in current.Files)
				{
					ListViewItem item = new ListViewItem(file.Caption);
					item.SubItems.Add((file.File != null ? "Sí" : "No"));
					item.SubItems.Add(file.Web);
					item.Tag = file;
					lstFiles.Items.Add(item);
				}
			}
		}

		private void btnDeleteFile_Click(object sender, EventArgs e)
		{
			if (lstFiles.SelectedIndices.Count == 0)
				return;
			int i = lstFiles.SelectedIndices[0];
			current.Files.RemoveAt(i);
			ReloadFiles();
		}

		private void btnNewFile_Click(object sender, EventArgs e)
		{
			frmMetadataFileEdit f = new frmMetadataFileEdit();
			f.ShowDialog(this);
			if (f.DialogResult == DialogResult.OK)
			{
				f.current.Metadata = this.current;
				this.current.Files.Add(f.current);
				ReloadFiles();
				}
		}

		private MetadataFile GetSelectedFile()
		{
			return (lstFiles.SelectedItems[0] as ListViewItem).Tag as MetadataFile;
		}

	}
}
