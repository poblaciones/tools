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

namespace medea.entities
{
	public abstract class BoundaryClippingRegionBase<T> : ActiveBaseEntity<T>, IIdentifiable
	where T: ActiveBaseEntity<T>, new()
	{
		
		#region Campos privados
		
		private int? _id;
		private ClippingRegion _clippingRegion;
		private Boundary _boundary;

		#endregion


		#region Propiedades públicas

		public virtual ClippingRegion ClippingRegion
		{
			get { return _clippingRegion; }
			set { _clippingRegion = value; }
		}
		public virtual Boundary Boundary
		{
			get { return _boundary; }
			set { _boundary = value; }
		}
		public virtual int? Id
		{
			get { return _id; }
			set { _id = value; }
		}
		
		#endregion
		
		
		#region Colecciones públicas
		
		
		#endregion
		
		
		#region Overrides
		
		public override int GetHashCode()
		{
			return Id.GetValueOrDefault();
		}
		
		#endregion
		
	}
}
