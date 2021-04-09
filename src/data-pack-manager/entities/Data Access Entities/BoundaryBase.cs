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
using System.Collections.Generic;

namespace medea.entities
{
	public abstract class BoundaryBase<T> : ActiveBaseEntity<T>, IIdentifiable
	where T: ActiveBaseEntity<T>, new()
	{

		#region Campos privados

		private int? _id;
		private string _caption;
		private int? _order;
		private BoundaryGroup _group;
		private bool _private;
		private Geography _geography;
		private IList<BoundaryClippingRegion> _boundaryClippingRegions;

		private Metadata _metadata;
		#endregion


		#region Propiedades públicas

		public virtual int? Id
		{
			get { return _id; }
			set { _id = value; }
		}

		public virtual int? Order
		{
			get { return _order; }
			set { _order = value; }
		}
		
		public virtual string Caption
		{
			get { return _caption; }
			set { _caption = value; }
		}

		public virtual bool Private
		{
			get { return _private; }
			set { _private = value; }
		}

		public virtual BoundaryGroup Group
		{
			get { return _group; }
			set { _group = value; }
		}
		public virtual Geography Geography
		{
			get { return _geography; }
			set { _geography = value; }
		}
		public virtual Metadata Metadata
		{
			get { return _metadata; }
			set { _metadata = value; }
		}

		#endregion

		public virtual IList<BoundaryClippingRegion> BoundaryClippingRegions
		{
			get { return _boundaryClippingRegions; }
			set { _boundaryClippingRegions = value; }
		}


		#region Overrides

		public override int GetHashCode()
		{
			return Id.GetValueOrDefault();
		}

		#endregion

	}
}
