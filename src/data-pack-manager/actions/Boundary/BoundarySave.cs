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
using medea.common;
using medea.actions;
using System.Collections.Generic;
using medea.entities;
using System.Data.SQLite;

namespace medea.actions
{
	public class BoundarySave : action
	{
		private Boundary current;

		public BoundarySave(action action, Boundary boundary) : base(action)
		{
			current = boundary;
		}

		public BoundarySave(Boundary current)
		{
			this.current = current;
		}

		public override void Call()
		{
			Progress.Caption = "Actualizando límite";
			Progress.Total = 1;
			var meta = new MetadataSave(current.Metadata);
			meta.Call();

			if (current.Id.HasValue && current.BoundaryClippingRegions.Count > 0)
			{
				//string deleteRelations = "DELETE FROM boundary_clipping_region WHERE bcr_boundary_id = " + current.Id.Value.ToString();
				//context.Data.Session.SqlActions.ExecuteNonQuery(deleteRelations);
				List<BoundaryClippingRegion> tmp = new List<BoundaryClippingRegion>();
				tmp.AddRange(current.BoundaryClippingRegions);
				current.BoundaryClippingRegions.Clear();
				context.Data.Session.SaveOrUpdate(current);
				context.Data.Session.Flush();
				foreach(var c in tmp)
					current.BoundaryClippingRegions.Add(c);
			}
			context.Data.Session.SaveOrUpdate(current);
		}
	}	
}