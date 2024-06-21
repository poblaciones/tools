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
	public abstract class UserBase<T> : ActiveBaseEntity<T>, IIdentifiable
	where T: ActiveBaseEntity<T>, new()
	{
		
		#region Campos privados
		
		private int? _id;
		//TODO: Convertir a DATETIME en la base.
		private DateTime _createTime;
		private bool _deleted;
		private string _email;
		private string _firstname;
		private string _lastname;
		private string _password;
		private string _privileges;
		
		#endregion
		
		
		#region Propiedades públicas
		
		public virtual DateTime CreateTime
		{
			get { return _createTime; }
			set { _createTime = value; }
		}
		public virtual string Email
		{
			get { return _email; }
			set { _email = value; }
		}
		public virtual bool Deleted
		{
			get { return _deleted; }
			set { _deleted = value; }
		}
		public virtual string LastName
		{
			get { return _lastname; }
			set { _lastname = value; }
		}
		public virtual string FirstName
		{
			get { return _firstname; }
			set { _firstname = value; }
		}
		public virtual int? Id
		{
			get { return _id; }
			set { _id = value; }
		}
		public virtual string Password
		{
			get { return _password; }
			set { _password = value; }
		}
		public virtual string Privileges
		{
			get { return _privileges; }
			set { _privileges = value; }
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
