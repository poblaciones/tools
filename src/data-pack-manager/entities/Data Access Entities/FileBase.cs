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
	public abstract class FileBase<T> : ActiveBaseEntity<T>, IIdentifiable
	where T: ActiveBaseEntity<T>, new()
	{

		#region Campos privados

		private int? _id;
		private IList<FileChunk> _fileChunks = new List<FileChunk>();
		private string _name;
		private string _type;
		private int? _size;
		private int? _pages;

		#endregion


		#region Propiedades públicas

		public virtual int? Id
		{
			get { return _id; }
			set { _id = value; }
		}

		public virtual int? Pages
		{
			get { return _pages; }
			set { _pages = value; }
		}

		public virtual int? Size
		{
			get { return _size; }
			set { _size = value; }
		}

		public virtual string Type
		{
			get { return _type; }
			set { _type = value; }
		}
		public virtual string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		#endregion


		#region Colecciones públicas
		public virtual IList<FileChunk> FileChunks
		{
			get { return _fileChunks; }
			set { _fileChunks = value; }
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
