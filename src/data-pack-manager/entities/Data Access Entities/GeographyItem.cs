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
using System;

namespace medea.entities
{
	public class GeographyItem : GeographyItemBase<GeographyItem>, IRecursive<GeographyItem>, ISimplifiableItem
	{
		public GeographyItem()
		{
			Childrens = new List<GeographyItem>();
		}

		public GeographyItem(int id)
			: this()
		{
			Id = id;
		}

		public virtual string GetCaption()
		{
			return Caption;
		}

		public virtual string[] ToArray()
		{
			return new string[] {
				Code,
				Caption,
				Parent != null ? Parent.GetCaption() : "",
				(AreaM2 / 1000 / 1000).ToString(),
				Population.ToString(),
				Households.ToString(),
				Children.ToString(),
			};
		}
		public virtual UrbanityEnum Urbanity
		{
			get
			{
				switch (PrivateUrbanity)
				{
					case "N":
					case null:
						return UrbanityEnum.None;
					case "U":
						return UrbanityEnum.Urban;
					case "D":
						return UrbanityEnum.Urban_Disperse;
					case "R":
						return UrbanityEnum.Rural;
					case "L":
						return UrbanityEnum.Rural_Disperse;
					default:
						throw new Exception("Valor inv�lido.");
				}
			}
			set
			{
				switch (value)
				{
					case UrbanityEnum.Rural:
						PrivateUrbanity = "R";
						break;
					case UrbanityEnum.Rural_Disperse:
						PrivateUrbanity = "L";
						break;
					case UrbanityEnum.Urban:
						PrivateUrbanity = "U";
						break;
					case UrbanityEnum.Urban_Disperse:
						PrivateUrbanity = "D";
						break;
					case UrbanityEnum.None:
						PrivateUrbanity = "N";
						break;
					default:
						throw new Exception("Valor inv�lido.");

				}
			}
		}

		public override string ToString()
		{
			return string.Join("\t", ToArray());
		}
	}
}
