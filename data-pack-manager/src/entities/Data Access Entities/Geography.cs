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
	public class Geography : GeographyBase<Geography>, IRecursive<Geography>
	{

		public virtual IList<GeographyItem> GeographyItems { get; set; }
		public virtual IList<GeographyCaption> GeographyCaptions { get; set; }
		public virtual int? GeographyItemsCount { get; set; }

		public Geography()
		{
			Children = new List<Geography>();
			GeographyItems = new List<GeographyItem>();
			GeographyCaptions = new List<GeographyCaption>();
		}

		public virtual string GetCaption()
		{
			if (string.IsNullOrEmpty(Revision) == false)
				return Caption + " " + "(" + Revision + ")";
			else
				return Caption;
		}
		public override string ToString()
		{
			return GetCaption();
		}
	}
}
