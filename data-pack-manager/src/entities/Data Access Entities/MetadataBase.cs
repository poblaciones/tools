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
	public abstract class MetadataBase<T> : ActiveBaseEntity<T>, IIdentifiable
	where T: ActiveBaseEntity<T>, new()
	{

		#region Campos privados

		private string _title;
		private string _releaseDate;
		private string _privateStatus;
		private string _authors;
		private string _coverageCaption;
		private string _periodCaption;
		private string _frequency;
		private string _privateType;
		private string _license = "{\"licenseType\":1,\"licenseOpen\":\"always\",\"licenseCommercial\":1,\"licenseVersion\":\"4.0/deed.es\"}";
		private string _abstractLong;
		private string _language = "es; Español";
		private string _wiki;
		private string _url;
		private string _abstract;
		private DateTime _create;
		private DateTime _update;
		private DateTime? _lastOnline;
		private DateTime? _onlineSince;

		private IList<MetadataSource> _sources = new List<MetadataSource>();

		private Contact _contact = new Contact();
		private IList<MetadataInstitution> _metadataInstitutions = new List<MetadataInstitution>();
		private int? _id;
		private IList<MetadataFile> _files = new List<MetadataFile>();

		#endregion


		#region Propiedades públicas
		public virtual string Abstract
		{
			get { return _abstract; }
			set { _abstract = value; }
		}

		public virtual string Authors
		{
			get { return _authors; }
			set { _authors = value; }
		}
		public virtual string Title
		{
			get { return _title; }
			set { _title = value; }
		}
		public virtual string ReleaseDate
		{
			get { return _releaseDate; }
			set { _releaseDate = value; }
		}
		public virtual string PrivateStatus
		{
			get { return _privateStatus; }
			set { _privateStatus = value; }
		}
		public virtual string CoverageCaption
		{
			get { return _coverageCaption; }
			set { _coverageCaption = value; }
		}
		public virtual string PeriodCaption
		{
			get { return _periodCaption; }
			set { _periodCaption= value; }
		}
		public virtual string Frequency
		{
			get { return _frequency; }
			set { _frequency = value; }
		}
		public virtual string PrivateType
		{
			get { return _privateType; }
			set { _privateType = value; }
		}
		public virtual string License
		{
			get { return _license; }
			set { _license = value; }
		}
		public virtual string Language
		{
			get { return _language; }
			set { _language = value; }
		}
		public virtual string AbstractLong
		{
			get { return _abstractLong; }
			set { _abstractLong = value; }
		}
		public virtual string Wiki
		{
			get { return _wiki; }
			set { _wiki= value; }
		}
		public virtual string Url
		{
			get { return _url; }
			set { _url = value; }
		}
		public virtual DateTime Create
		{
			get { return _create; }
			set { _create = value; }
		}
		public virtual DateTime Update
		{
			get { return _update; }
			set { _update = value; }
		}
		public virtual int? Id
		{
			get { return _id; }
			set { _id = value; }
		}
		public virtual DateTime? OnlineSince
		{
			get { return _onlineSince; }
			set { _onlineSince = value; }
		}
		public virtual DateTime? LastOnline
		{
			get { return _lastOnline; }
			set { _lastOnline = value; }
		}

		public virtual Contact Contact
		{
			get { return _contact; }
			set { _contact= value; }
		}


		#endregion


		#region Colecciones públicas

		public virtual IList<MetadataSource> MetadataSources
		{
			get { return _sources; }
			set { _sources = value; }
		}

		public virtual IList<MetadataFile> Files
		{
			get { return _files; }
			set { _files = value; }
		}

		public virtual IList<MetadataInstitution> MetadataInstitutions
		{
			get { return _metadataInstitutions; }
			set { _metadataInstitutions = value; }
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
