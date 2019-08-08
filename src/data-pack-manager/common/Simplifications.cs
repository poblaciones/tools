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
using System.IO;
using GeoAPI.Geometries;
using NetTopologySuite.Geometries;
using GeoAPI.CoordinateSystems;
using GeoAPI.CoordinateSystems.Transformations;
using System;

namespace medea.common
{
	public enum QualityEnum
	{
		VeryLow = 1,
		Low = 2,
		Medium = 3,
		High = 4,
		VeryHigh = 5,
		Excellent = 6
	}
	public class Simplifications
	{
		public static IGeometry Simplify(IGeometry geometry, QualityEnum quality)
		{
			double distance;
			switch(quality)
			{
				case QualityEnum.VeryLow:
					distance = 0.015;
					break;
				case QualityEnum.Low:
					distance = 0.0045;
					break;
				case QualityEnum.Medium:
					distance = 0.0020;
					break;
				case QualityEnum.High:
					distance = 0.0010;
					break;
				case QualityEnum.VeryHigh:
					distance = 0.00005;
					break;
				case QualityEnum.Excellent:
					//distance = 0.00000275; // de 25 a 35
					distance = 0.000002; // de 25 a 35
					break;
				default:
					throw new Exception("Invalid quality.");
			}
			var simplifyer = new NetTopologySuite.Simplify.DouglasPeuckerSimplifier(geometry);
			simplifyer.DistanceTolerance = distance;
			var geo = simplifyer.GetResultGeometry();
			if (geo.IsValid == false)
				throw new Exception("La simplificación ha generado una geometría inválida.");
			return geo;
		}


		public static void FillSimplifiedGeometries(Geometry geo, ISimplifiableItem item)
		{
			item.GeometryR1 = (Geometry) Simplifications.Simplify(geo, QualityEnum.VeryLow);
			item.GeometryR2 = (Geometry) Simplifications.Simplify(geo, QualityEnum.Low);
			item.GeometryR3 = (Geometry) Simplifications.Simplify(geo, QualityEnum.Medium);
			item.GeometryR4 = (Geometry) Simplifications.Simplify(geo, QualityEnum.High);
			item.GeometryR5 = (Geometry) Simplifications.Simplify(geo, QualityEnum.VeryHigh);
			item.GeometryR6 = (Geometry) Simplifications.Simplify(geo, QualityEnum.Excellent);

			if (item.GeometryR6.IsEmpty)
				item.GeometryR6 = geo;
			if (item.GeometryR5.IsEmpty)
				item.GeometryR5 = item.GeometryR6;
			if (item.GeometryR4.IsEmpty)
				item.GeometryR4 = item.GeometryR5;
			if (item.GeometryR3.IsEmpty)
				item.GeometryR3 = item.GeometryR4;
			if (item.GeometryR2.IsEmpty)
				item.GeometryR2 = item.GeometryR3;
			if (item.GeometryR1.IsEmpty)
				item.GeometryR1 = item.GeometryR2;

		}
	}
}
