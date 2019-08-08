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
using NetTopologySuite.Features;
using NetTopologySuite.IO;
using NetTopologySuite.Geometries;
using System.Collections;

namespace medea.common
{
	public class ShapeFile
	{
		public static List<Feature> ReadDbasefile(string dbfFilename)
		{
			var features = new List<Feature>();

			DbaseFileReader dr = new DbaseFileReader(dbfFilename);

			DbaseFileHeader header = dr.GetHeader();
			foreach (ArrayList atts in dr)
			{
				AttributesTable attributesTable = new AttributesTable();
				for (int i = 0; i < header.NumFields; i++)
					attributesTable.AddAttribute(header.Fields[i].Name, atts[i]);

				features.Add(new Feature(null, attributesTable));
			}

			return features;
		}

		public static List<Feature> ReadShapefile(string shpFilename)
		{
			var features = new List<Feature>();

			using (ShapefileDataReader dr = new ShapefileDataReader(shpFilename, new GeometryFactory()))
			{
				DbaseFileHeader header = dr.DbaseHeader;
				for(int n = 0; n < dr.RecordCount; n++)
				{
					dr.Read();
					AttributesTable attributesTable = new AttributesTable();
					for (int i = 0; i < header.NumFields; i++)
						attributesTable.AddAttribute(header.Fields[i].Name, dr.GetValue(i));

					features.Add(new Feature(dr.Geometry, attributesTable));
				}
			}
			return features;
		}
	}
}
