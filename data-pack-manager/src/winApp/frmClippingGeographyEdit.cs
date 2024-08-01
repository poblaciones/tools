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
using medea.entities;
using System.Linq;
using System.Windows.Forms;
using medea.common;

namespace medea.winApp
{
	public partial class frmClippingGeographyEdit : frmOkCancel
	{
		ClippingRegionGeography current = new ClippingRegionGeography();

		public frmClippingGeographyEdit()
		{
			InitializeComponent();

			using (new WaitCursor())
			{
				var geographies = UI.GetItems<Geography>().OrderBy(x => x.Caption).ToList();
				cmbGeography.FillRecursive(geographies);

				var regions = UI.GetItems<ClippingRegion>().OrderBy(x => x.Caption).ToList();
				cmbRegion.FillRecursive(regions);
			}
		}

		public void LoadPartial(ClippingRegion clippingRegion)
		{
			if (clippingRegion == null)
				return;

			current.ClippingRegion = clippingRegion;
			cmbRegion.SelectItem(current.ClippingRegion);
		}

		public void LoadData(ClippingRegionGeography crc)
		{
			isNew = false;
			current = crc;

			cmbRegion.SelectItem(current.ClippingRegion);
			cmbGeography.SelectItem(current.Geography);
		}

		private new bool Validate()
		{
			var msg = "";

			if (cmbGeography.HasSelectedItem == false)
				msg += "Debe indicar un valor para 'Geografía'.\n";
			if (cmbRegion.HasSelectedItem == false)
				msg += "Debe indicar un valor para 'Región'.\n";

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

			current.ClippingRegion = cmbRegion.GetSelectedItem<ClippingRegion>();
			current.Geography = cmbGeography.GetSelectedItem<Geography>();

			if (Call(new ClippingRegionGeographyCalculate(current), false))
				Call(new ClippingRegionGeographySave(current));
			MarkTableUpdate.UpdateTables(new string[] { "clipping_region_geography", "clipping_region_item_geography_item" });

		}

	}
}
