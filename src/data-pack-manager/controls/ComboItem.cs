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
﻿using medea.common;

namespace medea.controls
{
	public class ComboItem<T> where T : ActiveBaseEntity<T>
	{
		public ComboItem(bool empty = false)
		{
			Empty = empty;
		}
		public bool Empty { get; set; }
		public T Tag { get; set; }
		public string Text { get; set; }
		public string Key { get; set; }

		public override string ToString()
		{
			if (Text == null)
				return "";
			return Text;
		}
	}
}
