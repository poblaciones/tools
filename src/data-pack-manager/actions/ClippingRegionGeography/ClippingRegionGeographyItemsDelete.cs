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
using System.Collections.Generic;
using System.Linq;
using medea.common;
using medea.Data;
using medea.entities;

namespace medea.actions
{
	public class ClippingRegionGeographyItemsDelete : action
	{
		List<ClippingRegionGeographyItem> currentItems;
		List<int> toDelete;

		public ClippingRegionGeographyItemsDelete(List<ClippingRegionGeographyItem> items)
		{
			currentItems = items;
			toDelete = new List<int>();
		}

		public override void Call()
		{
			Progress.Total = 1;
			foreach (var item in currentItems)
			{
				item.ClippingRegionGeography.ClippingRegionGeographyItems.Remove(item);
				toDelete.Add(item.Id.Value);
			}
			context.Data.Session.Query<ClippingRegionGeographyItem>().Where(x => toDelete.Contains(x.Id.Value)).DeleteQ();
			Progress.Increment();
		}
	}
}
