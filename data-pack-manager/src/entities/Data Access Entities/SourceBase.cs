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
using System.Collections.Generic;

namespace medea.entities
{
	public abstract class SourceBase<T> : ActiveBaseEntity<T>, IIdentifiable
	where T : ActiveBaseEntity<T>, new()
	{

		#region Campos privados

		private int? _id;
		private string _caption;
		private string _authors;
		private string _version;
		private string _web;
		private string _wiki;
		private bool _isGlobal;
		private Institution _institution = new Institution();
		private Contact _contact = new Contact();
		#endregion


		#region Propiedades públicas

		public virtual string Authors
		{
			get { return _authors; }
			set { _authors = value; }
		}

		public virtual bool IsGlobal
		{
			get { return _isGlobal; }
			set { _isGlobal = value; }
		}

		public virtual string Caption
		{
			get { return _caption; }
			set { _caption = value; }
		}

		public virtual Institution Institution
		{
			get { return _institution; }
			set { _institution = value; }
		}

		public virtual string Version
		{
			get { return _version; }
			set { _version = value; }
		}

		public virtual string Web
		{
			get { return _web; }
			set { _web = value; }
		}

		public virtual string Wiki
		{
			get { return _wiki; }
			set { _wiki = value; }
		}

		public virtual int? Id
		{
			get { return _id; }
			set { _id = value; }
		}

		public virtual Contact Contact
		{
			get { return _contact; }
			set { _contact= value; }
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
