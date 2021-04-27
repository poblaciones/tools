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
using medea.common;

namespace medea.entities
{
	public abstract class ClippingRegionBase<T> : ActiveBaseEntity<T>, IIdentifiable
	where T: ActiveBaseEntity<T>, new()
	{

		#region Campos privados

		private string _caption;
		private string _fieldCodeName;
		private string _symbol;
		private Metadata _metadata = new Metadata(WorkTypeEnum.Geography);

		private int? _id;
		private int _labelsMaxZoom;
		private int _labelsMinZoom;
		private bool _noAutocomplete;
		private bool _indexCodes;
		private int _priority;
		private ClippingRegionItem _country;
		private ClippingRegion _parent;
		private IList<ClippingRegion> _children;

		#endregion


		#region Propiedades públicas

		public virtual string Caption
		{
			get { return _caption; }
			set { _caption = value; }
		}
		public virtual string Symbol
		{
			get { return _symbol; }
			set { _symbol = value; }
		}
		public virtual Metadata Metadata
		{
			get { return _metadata; }
			set { _metadata = value; }
		}

		public virtual string FieldCodeName
		{
			get { return _fieldCodeName; }
			set { _fieldCodeName = value; }
		}

		public virtual int LabelsMinZoom
		{
			get { return _labelsMinZoom; }
			set { _labelsMinZoom = value; }
		}
		public virtual int LabelsMaxZoom
		{
			get { return _labelsMaxZoom; }
			set { _labelsMaxZoom = value; }
		}

		public virtual int Priority
		{
			get { return _priority; }
			set { _priority = value; }
		}
		public virtual int? Id
		{
			get { return _id; }
			set { _id = value; }
		}

		public virtual ClippingRegionItem Country
		{
			get { return _country; }
			set { _country = value; }
		}
		public virtual bool NoAutocomplete
		{
			get { return _noAutocomplete; }
			set { _noAutocomplete = value; }
		}
		public virtual bool IndexCodes
		{
			get { return _indexCodes; }
			set { _indexCodes = value; }
		}
		public virtual ClippingRegion Parent
		{
			get { return _parent; }
			set { _parent = value; }
		}

		#endregion


		#region Colecciones públicas

		public virtual IList<ClippingRegion> Children
		{
			get { return _children; }
			set { _children = value; }
		}

		#endregion


		#region Overrides

		public override int GetHashCode()
		{
			return Id.GetValueOrDefault();
		}

		#endregion

	}
}
