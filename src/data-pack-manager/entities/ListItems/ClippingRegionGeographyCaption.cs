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
namespace medea.entities
{
	public class ClippingRegionGeographyCaption : IIdentifiable
	{
		public string ClippingRegionItem { get; set; }
		public string GeographyItem { get; set; }
		public string GeographyCode  { get; set; }

		public int? Id { get; set; }


		public ClippingRegionGeographyCaption(int? id, string clippingRegionItem, string geographyItem, string geographyCode)
		{
			Id = id;
			ClippingRegionItem = clippingRegionItem;
			GeographyCode = geographyCode;
			GeographyItem = geographyItem;
		}

		public virtual string[] ToArray()
		{
			return new string[] {
				ClippingRegionItem,
				(string.IsNullOrEmpty(GeographyItem) ?
						GeographyCode : GeographyCode + " - " + GeographyItem),

			};
		}
		public override string ToString()
		{
			return string.Join("\t", ToArray());
		}
	}
}
