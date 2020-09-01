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
	public abstract class GeographyBase<T> : ActiveBaseEntity<T>, IIdentifiable
	where T: ActiveBaseEntity<T>, new()
	{

		#region Campos privados

		private int? _id;
		private string _caption;
		private string _revision;
		private string _fieldCaptionName;
		private Metadata _metadata = new Metadata(WorkTypeEnum.Geography);
		private bool _isTrackingLevel;
		private int _maxZoom;
		private int _minZoom;
		private string _fieldCodeName;
		private int _fieldCodeSize = 10;
		private float? _gradientLuminance;
		private string _fieldUrbanityName;
		private string _partialCoverage;
		private string _fieldCodeType;
		private string _rootCaption;
		private Geography _parent;
		private Gradient _gradient;
		private IList<Geography> _children;
		private ClippingRegionItem _country;
		#endregion


		#region Propiedades públicas

		public virtual string Caption
		{
			get { return _caption; }
			set { _caption = value; }
		}
		public virtual string Revision
		{
			get { return _revision; }
			set { _revision = value; }
		}
		public virtual string RootCaption
		{
			get { return _rootCaption; }
			set { _rootCaption = value; }
		}
		public virtual Metadata Metadata
		{
			get { return _metadata; }
			set { _metadata = value; }
		}
		public virtual Gradient Gradient
		{
			get { return _gradient; }
			set { _gradient = value; }
		}
		public virtual bool IsTrackingLevel
		{
			get { return _isTrackingLevel; }
			set { _isTrackingLevel= value; }
		}
		public virtual int MinZoom
		{
			get { return _minZoom; }
			set { _minZoom = value; }
		}
		public virtual int MaxZoom
		{
			get { return _maxZoom; }
			set { _maxZoom = value; }
		}

		public virtual float ? GradientLuminance
		{
			get { return _gradientLuminance; }
			set { _gradientLuminance = value; }
		}


		public virtual ClippingRegionItem Country
		{
			get { return _country; }
			set { _country = value; }
		}
		public virtual string PartialCoverage
		{
			get { return _partialCoverage; }
			set { _partialCoverage = value; }
		}

		public virtual string FieldCaptionName
		{
			get { return _fieldCaptionName; }
			set { _fieldCaptionName = value; }
		}

		public virtual string FieldCodeName
		{
			get { return _fieldCodeName; }
			set { _fieldCodeName = value; }
		}
		public virtual int FieldCodeSize
		{
			get { return _fieldCodeSize; }
			set { _fieldCodeSize = value; }
		}
		public virtual string FieldUrbanityName
		{
			get { return _fieldUrbanityName; }
			set { _fieldUrbanityName = value; }
		}
		public virtual string FieldCodeType
		{
			get { return _fieldCodeType; }
			set { _fieldCodeType = value; }
		}
		public virtual int? Id
		{
			get { return _id; }
			set { _id = value; }
		}
		public virtual Geography Parent
		{
			get { return _parent; }
			set { _parent = value; }
		}

		#endregion


		#region Colecciones públicas

		public virtual IList<Geography> Children
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
