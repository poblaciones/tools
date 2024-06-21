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
using System.Linq;
using medea.common;
using medea.Data;
using medea.entities;

namespace medea.actions
{
	public class ClippingRegionDelete : action
	{
		private ClippingRegion current;

		public ClippingRegionDelete(ClippingRegion clipping)
		{
			current = clipping;
		}

		public override void Call()
		{
			Progress.Total = 1;

			DeleteRecursive(current);
			Progress.Increment();
			VersionUpdater.Increment();
		}

		private void DeleteRecursive(ClippingRegion clipping)
		{
			foreach (var item in clipping.Children)
			{
				Progress.Total++;
				DeleteRecursive(item);
				Progress.Increment();
			}
			// Borra los items
			context.Data.Session.Query<ClippingRegionItem>()
				.Where(x => x.ClippingRegion.Id == clipping.Id).DeleteQ();

			context.Data.Session.Delete(clipping);
		}

	}
}
