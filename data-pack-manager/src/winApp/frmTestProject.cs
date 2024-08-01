/*
*    Poblaciones - Plataforma abierta de datos espaciales de población.
*    Copyright (C) 2018-2024. Consejo Nacional de Investigaciones Científicas y Técnicas (CONICET)
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
﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NetTopologySuite.IO;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using System.IO;
using NetTopologySuite.Geometries.Utilities;
using medea.common;
using System.Collections;

namespace medea.winApp
{
	public partial class frmTestProject : Form
	{
		string p = @"C:\Users\Administrator\Documents\ArcGIS\Mapas consistidos\radiosunificados 2001\urbanoparcial";
		List<Feature> g1;
		List<Feature> g2;

		public frmTestProject()
		{
			InitializeComponent();
			uGeometry1.NumberPoints = true;
			uGeometry2.NumberPoints = true;
			
		}

		private void loadShp(string pr)
		{
			var lp = readShape(Path.Combine(p, pr + ".shp"));
			var lp_off = readShape(Path.Combine(p, pr + "_off.shp"));

			g1 = lp;
			g2 = lp_off;
			update();
		}


		private void update()
		{
			toList(listBox1, g1, uGeometry1);
			toList(listBox2, g2, uGeometry2);
		}
		private void toList(ListBox list, List<Feature> lp_off, controls.uGeometry uGeometry)
		{
			//Comenté esto porque cambió todo en uGeometry
			//uGeometry.LoadPolygonAsText(lp_off[0].Geometry.ToString());
			//list.Items.Clear();
			//list.Items.Add(lp_off[0].Geometry.Area);
			
		}

		private List<Feature> readShape(string shpFilename)
		{
			var features = new List<Feature>();
			using (ShapefileDataReader dr = new ShapefileDataReader(shpFilename, new GeometryFactory()))
			{
				DbaseFileHeader header = dr.DbaseHeader;
				while (dr.Read())
				{
					AttributesTable attributesTable = new AttributesTable();
					for (int i = 0; i < header.NumFields; i++)
						attributesTable.AddAttribute(header.Fields[i].Name, dr.GetValue(i));

					features.Add(new Feature(dr.Geometry, attributesTable));
				}
			}
			return features;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			string pr = "mend";
			loadShp(pr);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			string pr = "cord";
			loadShp(pr);
		}

		private void button3_Click(object sender, EventArgs e)
		{
			string pr = "lp";
			loadShp(pr);
		}

		private void button4_Click(object sender, EventArgs e)
		{
			Polygon p = g1[0].Geometry as Polygon;
			GeometryTransformer tr = new GeometryTransformer();
			double rad = (g1[0].Geometry.Area / g2[0].Geometry.Area);
			MessageBox.Show(rad.ToString());
		}

		private void frmTestProject_Load(object sender, EventArgs e)
		{
			string fi1 = @"C:\Users\Administrator\Documents\ArcGIS\Mapas consistidos\Finales\2010 - Cartografia\Provincias 2010\provincias2010";
			string fi2 = @"C:\Users\Administrator\Documents\ArcGIS\Mapas consistidos\Finales\2010 - Cartografia\Radios2010\radios2010.shp";

			var features1 = ShapeFile.ReadShapefile(fi1);
			var features2 = ShapeFile.ReadShapefile(fi2);
			var f1 = features1[0];
			var f2 = features2[0];
			var v = new ArrayList() { f1, f2};
			f1 = f2;
		}
	}
}
