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
using System.Collections.Generic;

namespace medea.winApp
{
	public partial class frmGeographyClippingNew : frmOkCancel
	{
		public frmGeographyClippingNew()
		{
			InitializeComponent();

			using (new WaitCursor())
			{
				var geographies = UI.GetItems<Geography>().OrderBy(x => x.Caption).ToList();
				cmbGeography.FillRecursive(geographies);

				var regions = UI.GetItems<ClippingRegion>().OrderBy(x => x.Caption).ToList();
				FillRecursive(lstRegions, regions);
			}	
		}
		public void FillRecursive<T>(CheckedListBox list, IEnumerable<T> items)
			where T : ActiveBaseEntity<T>, IIdentifiable, IRecursive<T>
		{
			list.Items.Clear();
			var res = new Dictionary<int, string>();
			TreeItem<T>.MakeDictionary(items.GenerateTree(), res);
			foreach (var item in res)
			{
				T tag = items.Where(x => x.Id == item.Key).First();
				string key = item.Key.ToString();
				string text = item.Value;
				list.Items.Add(tag);
			}
		}
		public void LoadPartial(Geography geography)
		{
			if (geography == null)
				return;

			cmbGeography.SelectItem(geography);
		}

		private new bool Validate()
		{
			var msg = "";

			if (lstRegions.CheckedItems.Count == 0)
				msg += "Debe indicar un valor para 'Región'.\n";

			if (cmbGeography.HasSelectedItem == false)
				msg += "Debe indicar un valor para 'Geografía'.\n";

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
			foreach (ClippingRegion region in lstRegions.CheckedItems)
			{
				ClippingRegionGeography current = new ClippingRegionGeography();
				current.ClippingRegion = region;
				current.Geography = cmbGeography.GetSelectedItem<Geography>();

				if (Call(new ClippingRegionGeographyCalculate(current), false))
					Call(new ClippingRegionGeographySave(current));
			}
		}

	}
}
