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
using medea.entities;
using medea.Data;

namespace medea.actions
{
	public class GradientDelete : action
	{
		private Gradient current;

		public GradientDelete(Gradient gradient)
		{
			current = gradient;
		}

		public override void Call()
		{
			Progress.Total = 1;

			Progress.Increment();

			context.Data.Session.SqlActions.ExecuteNonQuery("DELETE FROM gradient_item WHERE gri_gradient_id = "
						+ current.Id.ToString());
			context.Data.Session.Delete(current);
			VersionUpdater.Increment();
		}

	}
}
