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
	public class GeographyCaption : IIdentifiable
	{
		public string Code { get; set; }
		public string Caption { get; set; }
		public string Parent { get; set; }
		public int? Id { get; set; }
		public double AreaM2 { get; set; }
		public int Population { get; set; }
		public int Households { get; set; }
		public int Children { get; set; }
		public string ParentCode { get; set; }

		public GeographyCaption(int? id, string code, string caption, string parent, string parentCode,
			double areaM2, int population, int households, int children)
		{
			Id = id;
			Caption = caption;
			Code = code;
			Parent = parent;
			ParentCode = parentCode;
			AreaM2 = areaM2;
			Population = population;
			Households = households;
			Children = children;
		}

		public virtual string[] ToArray()
		{
			return new string[] {
				Code,
				Caption,
				(string.IsNullOrEmpty(Parent) ?
						ParentCode : ParentCode + " - " + Parent),
				(((long) (AreaM2 / 1000 / 100)) / 10F).ToString(),
				Population.ToString(),
				Households.ToString(),
				Children.ToString(),
			};
		}
		public override string ToString()
		{
			return string.Join("\t", ToArray());
		}

	}
}
