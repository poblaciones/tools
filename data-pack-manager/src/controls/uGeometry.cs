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
﻿using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using medea.common;

namespace medea.controls
{
	public partial class uGeometry : UserControl
	{
		double minX = double.MaxValue;
		double minY = double.MaxValue;
		double maxX = double.MinValue;
		double maxY = double.MinValue;

		List<List<PointD>> polygons = new List<List<PointD>>();
		List<Point[]> polygonsPixel = new List<Point[]>();
		NetTopologySuite.Geometries.Geometry geo;

		public Color Color = Color.LightPink;
		public Color ColorLine = Color.Magenta;
		public bool InvertY = true;
		public bool NumberPoints;

		public uGeometry()
		{
			InitializeComponent();
		}
		public void Clear()
		{
			polygons = new List<List<PointD>>();
			polygonsPixel = new List<Point[]>();
		}

		public void LoadData<T>(int id)
			where T : ActiveBaseEntity<T>, IIdentifiable
		{
			using (new WaitCursor())
			{
				geo = context.Data.Session.GetGeometry<T>(id);
				CalculateMaxMin();
				Project();
				Invalidate();
			}
		}

		private void Project()
		{
			if (geo == null)
				return;
			polygonsPixel = new List<Point[]>();

			const float MARGIN_PERC = 0.05F;

			RangeD range = new RangeD((maxX - minX), (maxY - minY));
			RangeD scale = new RangeD(ClientSize.Width * (1 - 2 * MARGIN_PERC) / range.Width,
															ClientSize.Height * (1 - 2 * MARGIN_PERC) / range.Height);
			Point margin;
			if (scale.Width < scale.Height)
			{
				scale = new RangeD(scale.Width, scale.Width);
				margin = new Point((int) (ClientSize.Width * MARGIN_PERC),
									(int) ((ClientSize.Height -
											(range.Height * scale.Height)) / 2));
			}
			else
			{
				scale = new RangeD(scale.Height, scale.Height);
				margin = new Point((int) (
									(ClientSize.Width -
											(range.Width * scale.Width)) / 2),
											(int)(ClientSize.Height * MARGIN_PERC));
			}
			List<GeoAPI.Geometries.IGeometry> polygons = new List<GeoAPI.Geometries.IGeometry>();
			if (geo.OgcGeometryType == GeoAPI.Geometries.OgcGeometryType.Polygon)
				polygons.Add(geo);
			else
			{
				NetTopologySuite.Geometries.MultiPolygon mp = geo as NetTopologySuite.Geometries.MultiPolygon;
				polygons.AddRange(mp.Geometries);
			}

			foreach (var polygon in polygons)
			{
				List<Point> points = new List<Point>();
				foreach (var p in polygon.Coordinates)
				{
					var y = p.Y;
					if (InvertY) y *= -1;

					Point point = new Point(margin.X +
										(int)((p.X - minX) * scale.Width),
										margin.Y +
										(int)((y - minY) * scale.Height));
					points.Add(point);
				}
				polygonsPixel.Add(points.ToArray());
			}
		}


		private void CalculateMaxMin()
		{
			minX = double.MaxValue;
			minY = double.MaxValue;
			maxX = double.MinValue;
			maxY = double.MinValue;
			foreach (var coord in geo.Coordinates)
			{
				var y = coord.Y;
				if (InvertY) y *= -1;

				if (coord.X < minX) minX = coord.X;
				if (y < minY) minY = y;
				if (coord.X > maxX) maxX = coord.X;
				if (y > maxY) maxY = y;
			}
		}

		private void uGeometry_Paint(object sender, PaintEventArgs e)
		{
			if (polygonsPixel.Count == 0)
				return;

			InvokePaintBackground(this, e);

			foreach (var polygon in polygonsPixel)
			{
				e.Graphics.FillPolygon(new SolidBrush(Color), polygon);
				e.Graphics.DrawPolygon(new Pen(ColorLine), polygon);
			}

			if (NumberPoints)
			{
				foreach (var polygon in polygonsPixel)
				{
					int i = 1;
					foreach (Point p in polygon)
						e.Graphics.DrawString((i++).ToString(), Font, Brushes.Black, p);
				}
			}
		}

		private void uGeometry_Resize(object sender, EventArgs e)
		{
			Project();
			Invalidate();
		}

	}
}
