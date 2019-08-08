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
using System;
using medea.common;

namespace medea.entities
{
	public abstract class InstitutionBase<T> : ActiveBaseEntity<T>, IIdentifiable
	where T: ActiveBaseEntity<T>, new()
	{

		#region Campos privados

		private int? _id;
		private bool _isPublicDataEditor;
		private string _caption;
		private string _web;
		private string _email;
		private string _address;
		private bool _isGlobal;
		private string _phone;
		private string _country;

		#endregion


		#region Propiedades públicas
		public virtual int? Id
		{
			get { return _id; }
			set { _id = value; }
		}

		public virtual bool IsGlobal
		{
			get { return _isGlobal; }
			set { _isGlobal = value; }
		}
		public virtual string Email
		{
			get { return _email; }
			set { _email = value; }
		}
		public virtual bool IsPublicDataEditor
		{
			get { return _isPublicDataEditor; }
			set { _isPublicDataEditor = value; }
		}
		public virtual string Caption
		{
			get { return _caption; }
			set { _caption = value; }
		}
		public virtual string Web
		{
			get { return _web; }
			set { _web = value; }
		}
		public virtual string Address
		{
			get { return _address; }
			set { _address = value; }
		}
		public virtual string Phone
		{
			get { return _phone; }
			set { _phone = value; }
		}
		public virtual string Country
		{
			get { return _country; }
			set { _country = value; }
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
