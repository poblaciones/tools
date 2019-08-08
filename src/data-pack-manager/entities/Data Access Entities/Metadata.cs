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
using System;
using System.Collections.Generic;
using System.Linq;

namespace medea.entities
{
	public class Metadata : MetadataBase<Metadata>
	{
		public Metadata()
		{
			this.MetadataSources = new List<MetadataSource>();
		}
		public Metadata(WorkTypeEnum type)
		{
			this.MetadataType = type;
			this.MetadataSources = new List<MetadataSource>();
		}

		public virtual WorkTypeEnum MetadataType
		{
			get
			{
				switch (PrivateType)
				{
					case "M":
						return WorkTypeEnum.Mapping;
					case "P":
						return WorkTypeEnum.PublicData;
					case "R":
						return WorkTypeEnum.Research;
					case "C":
						return WorkTypeEnum.Geography;
					default:
						throw new Exception("Invalid Metadata type");
				}
			}
			set
			{
				switch (value)
				{
					case WorkTypeEnum.Mapping:
						PrivateType = "M";
						break;
					case WorkTypeEnum.PublicData:
						PrivateType = "P";
						break;
					case WorkTypeEnum.Research:
						PrivateType = "R";
						break;
					case WorkTypeEnum.Geography:
						PrivateType = "C";
						break;
				}
			}
		}


		public virtual MetadataStatusEnum MetadataStatus
		{
			get
			{
				switch (PrivateStatus)
				{
					case "C":
						return MetadataStatusEnum.Complete;
					case "P":
						return MetadataStatusEnum.Partial;
					case "B":
						return MetadataStatusEnum.Draft;
					default:
						throw new Exception("Invalid Metadata status");
				}
			}
			set
			{
				switch (value)
				{
					case MetadataStatusEnum.Complete:
						PrivateStatus = "C";
						break;
					case MetadataStatusEnum.Partial:
						PrivateStatus = "P";
						break;
					case MetadataStatusEnum.Draft:
						PrivateStatus = "B";
						break;
				}
			}
		}

		public virtual string SourcesCaption()
		{
			string ret = "";
			foreach(var src in this.MetadataSources)
				ret += ", " + src.Source.ToString();
			if (ret != "") ret = ret.Substring(2);
			return ret;
		}
	}
}
