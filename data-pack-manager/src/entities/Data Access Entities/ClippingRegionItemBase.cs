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
using NetTopologySuite.Geometries;

namespace medea.entities
{
	public abstract class ClippingRegionItemBase<T> : ActiveBaseEntity<T>, IIdentifiable
	where T: ActiveBaseEntity<T>, new()
	{

		#region Campos privados

		private string _caption;
		private string _wiki;
		private Point _centroid;
		private Geometry _geometry;
		private Geometry _geometryR1;
		private Geometry _geometryR2;
		private Geometry _geometryR3;
		private ClippingRegion _clippingRegion;
		private string _code;
		private int? _id;
		private ClippingRegionItem _parent;
		private IList<ClippingRegionItem> _children;
		private double _areaM2;
		
		#endregion


		#region Propiedades públicas
		public virtual string Wiki
		{
			get { return _wiki; }
			set { _wiki = value; }
		}

		public virtual string Caption
		{
			get { return _caption; }
			set { _caption = value; }
		}

		
		public virtual double AreaM2
		{
			get { return _areaM2; }
			set { _areaM2 = value; }
		}

		public virtual Geometry Geometry
		{
			get { return _geometry; }
			set { _geometry = value; }
		}
		public virtual Geometry GeometryR1
		{
			get { return _geometryR1; }
			set { _geometryR1 = value; }
		}
		public virtual Geometry GeometryR2
		{
			get { return _geometryR2; }
			set { _geometryR2 = value; }
		}
		public virtual Geometry GeometryR3
		{
			get { return _geometryR3; }
			set { _geometryR3 = value; }
		}
		public virtual Point Centroid
		{
			get { return _centroid; }
			set { _centroid = value; }
		}

		public virtual ClippingRegion ClippingRegion
		{
			get { return _clippingRegion; }
			set { _clippingRegion = value; }
		}
		public virtual string Code
		{
			get { return _code; }
			set { _code = value; }
		}
		public virtual int? Id
		{
			get { return _id; }
			set { _id = value; }
		}
		public virtual ClippingRegionItem Parent
		{
			get { return _parent; }
			set { _parent = value; }
		}

		#endregion


		#region Colecciones públicas

		public virtual IList<ClippingRegionItem> Children
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
