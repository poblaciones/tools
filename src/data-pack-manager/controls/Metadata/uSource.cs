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
	public partial class uSource : UserControl
	{
		private IList<MetadataSource> current = new List<MetadataSource>();
		Metadata metadata;
		public IList<MetadataSource> Current { get { return current; } }

		public uSource()
		{
			InitializeComponent();

		}
		public void LoadData(Metadata metadata, IList<MetadataSource> sources)
		{
			this.metadata = metadata;
			this.current = sources;
			ReloadList();
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			var f = new frmPickSource();
			f.LoadSources();
			if (f.ShowDialog() == DialogResult.OK)
			{
				AddToList(f.current);
			}
		}

		private void AddToList(Source source)
		{
			MetadataSource link = new MetadataSource();
			link.Source = source;
			link.Metadata = this.metadata;
			this.current.Add(link);
			this.ReloadList();
		}

		private void ReloadList()
		{
			lstItems.Items.Clear();
			foreach (var src in this.current)
				lstItems.Items.Add(src);
		}

		private void btnRemove_Click(object sender, EventArgs e)
		{
			this.current.Remove(GetSelectedSource());
			this.ReloadList();
		}

		private MetadataSource GetSelectedSource()
		{
			return lstItems.SelectedItem as MetadataSource;
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			var f = new frmEditSource();
			f.LoadData(GetSelectedSource().Source);
			if (f.ShowDialog() == DialogResult.OK)
				this.ReloadList();
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			var f = new frmEditSource();
			var source = new Source();
			source.IsGlobal = true;
			f.LoadData(source);
			if (f.ShowDialog() == DialogResult.OK)
			{
				AddToList(source);
			}
		}
	}
}
