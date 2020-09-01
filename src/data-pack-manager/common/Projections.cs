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



namespace medea.common
{
	public class Projections
	{
		const string wkt4326 = "GEOGCS[\"WGS 84\",DATUM[\"WGS_1984\",SPHEROID[\"WGS 84\",6378137,298.257223563,AUTHORITY[\"EPSG\",\"7030\"]],AUTHORITY[\"EPSG\",\"6326\"]],PRIMEM[\"Greenwich\",0,AUTHORITY[\"EPSG\",\"8901\"]],UNIT[\"degree\",0.01745329251994328,AUTHORITY[\"EPSG\",\"9122\"]],AUTHORITY[\"EPSG\",\"4326\"]]";
		const string wkt3857 = "PROJCS[\"Popular Visualisation CRS / Mercator\", GEOGCS[\"Popular Visualisation CRS\", DATUM[\"Popular Visualisation Datum\", SPHEROID[\"Popular Visualisation Sphere\", 6378137, 0, AUTHORITY[\"EPSG\",\"7059\"]], TOWGS84[0, 0, 0, 0, 0, 0, 0], AUTHORITY[\"EPSG\",\"6055\"] ], PRIMEM[\"Greenwich\", 0, AUTHORITY[\"EPSG\", \"8901\"]], UNIT[\"degree\", 0.0174532925199433, AUTHORITY[\"EPSG\", \"9102\"]], AXIS[\"E\", EAST], AXIS[\"N\", NORTH], AUTHORITY[\"EPSG\",\"4055\"] ], PROJECTION[\"Mercator\"], PARAMETER[\"False_Easting\", 0], PARAMETER[\"False_Northing\", 0], PARAMETER[\"Central_Meridian\", 0], PARAMETER[\"Latitude_of_origin\", 0], UNIT[\"metre\", 1, AUTHORITY[\"EPSG\", \"9001\"]], AXIS[\"East\", EAST], AXIS[\"North\", NORTH], AUTHORITY[\"EPSG\",\"3785\"]]";
		const string wkt22183 = "PROJCS[\"POSGAR 94 / Argentina 3\",GEOGCS[\"POSGAR 94\",DATUM[\"D_POSGAR_1994\",SPHEROID[\"WGS_1984\",6378137,298.257223563]],PRIMEM[\"Greenwich\",0],UNIT[\"Degree\",0.017453292519943295]],PROJECTION[\"Transverse_Mercator\"],PARAMETER[\"latitude_of_origin\",-90],PARAMETER[\"central_meridian\",-66],PARAMETER[\"scale_factor\",1],PARAMETER[\"false_easting\",3500000],PARAMETER[\"false_northing\",0],UNIT[\"Meter\",1]]";
		const string albers =  "PROJCS[\"Albers Equal Area\", GEOGCS[\"WGS 84\", DATUM[\"World Geodetic System 1984\", SPHEROID[\"WGS 84\", 6378137.0, 298.257223563, AUTHORITY[\"EPSG\",\"7030\"]], AUTHORITY[\"EPSG\",\"6326\"]], PRIMEM[\"Greenwich\", 0.0, AUTHORITY[\"EPSG\",\"8901\"]], UNIT[\"degree\", 0.017453292519943295], AXIS[\"Geodetic longitude\", EAST], AXIS[\"Geodetic latitude\", NORTH], AUTHORITY[\"EPSG\",\"4326\"]], PROJECTION[\"Albers_Conic_Equal_Area\"], PARAMETER[\"central_meridian\", -96.0], PARAMETER[\"latitude_of_origin\", 37.5], PARAMETER[\"standard_parallel_1\", 29.833333333333336], PARAMETER[\"false_easting\", 0.0], PARAMETER[\"false_northing\", 0.0], PARAMETER[\"standard_parallel_2\", 45.833333333333336], UNIT[\"m\", 1.0], AXIS[\"Easting\", EAST], AXIS[\"Northing\", NORTH], AUTHORITY[\"EPSG\",\"41111\"]]";
		const string albers2 = "PROJCS[\"South_America_Albers_Equal_Area_Conic\",GEOGCS[\"GCS_South_American_1969\",DATUM[\"D_South_American_1969\",SPHEROID[\"GRS_1967_Truncated\",6378160,298.25]],PRIMEM[\"Greenwich\",0],UNIT[\"Degree\",0.017453292519943295]],PROJECTION[\"Albers\"],PARAMETER[\"False_Easting\",0],PARAMETER[\"False_Northing\",0],PARAMETER[\"central_meridian\",-60],PARAMETER[\"Standard_Parallel_1\",-5],PARAMETER[\"Standard_Parallel_2\",-42],PARAMETER[\"latitude_of_origin\",-32],UNIT[\"Meter\",1]]";
		public static double CalculateM2Area(IGeometry geomIn)
		{
			//4326 a 3857 
			// http://spatialreference.org/ref/esri/south-america-albers-equal-area-conic/

			ICoordinateSystem csSource = ProjNet.Converters.WellKnownText.CoordinateSystemWktReader.Parse(wkt4326) as ICoordinateSystem;
			ICoordinateSystem csTarget = ProjNet.Converters.WellKnownText.CoordinateSystemWktReader.Parse(albers2) as ICoordinateSystem;
			ICoordinateTransformation trans = new ProjNet.CoordinateSystems.Transformations.CoordinateTransformationFactory().CreateFromCoordinateSystems(csSource, csTarget);

			IGeometry geomOut = NetTopologySuite.CoordinateSystems.Transformations.GeometryTransform.TransformGeometry(GeometryFactory.Default, geomIn, trans.MathTransform);
			return geomOut.Area;
		}
		public static IGeometry UnprojectFromPosgar3(IGeometry geomIn)
		{
			// wkt22183 a 4326 
			return Unproject(geomIn, wkt22183);
		}
		private static IGeometry Unproject(IGeometry geomIn, string zone)
		{
			ICoordinateSystem csSource = ProjNet.Converters.WellKnownText.CoordinateSystemWktReader.Parse(zone) as ICoordinateSystem;
			ICoordinateSystem csTarget = ProjNet.Converters.WellKnownText.CoordinateSystemWktReader.Parse(wkt4326) as ICoordinateSystem;
			ICoordinateTransformation trans = new ProjNet.CoordinateSystems.Transformations.CoordinateTransformationFactory().CreateFromCoordinateSystems(csSource, csTarget);

			IGeometry geomOut = NetTopologySuite.CoordinateSystems.Transformations.GeometryTransform.TransformGeometry(GeometryFactory.Default, geomIn, trans.MathTransform);
			return geomOut;
		}

		private static IGeometry Project(IGeometry geomIn, string zone)
		{
			ICoordinateSystem csSource = ProjNet.Converters.WellKnownText.CoordinateSystemWktReader.Parse(wkt4326) as ICoordinateSystem;
			ICoordinateSystem csTarget = ProjNet.Converters.WellKnownText.CoordinateSystemWktReader.Parse(zone) as ICoordinateSystem;
			ICoordinateTransformation trans = new ProjNet.CoordinateSystems.Transformations.CoordinateTransformationFactory().CreateFromCoordinateSystems(csSource, csTarget);

			IGeometry geomOut = NetTopologySuite.CoordinateSystems.Transformations.GeometryTransform.TransformGeometry(GeometryFactory.Default, geomIn, trans.MathTransform);
			return geomOut;
		}
		public static string ErrorMessage
		{
			get
			{
				return "Los archivos seleccionados no utilizan la proyección: \"" + proj + ", " + descr
					+ "\".\nModifique la misma con una aplicación de GIS (o en https://mygeodata.cloud) y vuelva a intentarlo.\nMás info: https://epsg.io/4326";
			}
		}
		const string proj = "GCS_WGS_1984";
		const string descr = "Geographic coordinate system > Word > WGS1984";
		public static bool Validate(string file)
		{
			var text = File.ReadAllText(file).Trim();
			text = text.Replace("\0", "");
			if (text.StartsWith("GEOGCS[\"" + proj + "\""))
				return true;
			else
				return false;
		}

		public void OffsetLatLongInMeters()
		{
			 //Position, decimal degrees
			// Con error 5%... https://gis.stackexchange.com/questions/2951/algorithm-for-offsetting-a-latitude-longitude-by-some-amount-of-meters
 //lat = 51.0
 //lon = 0.0

 ////Earth’s radius, sphere
 //R=6378137

 ////offsets in meters
 //dn = 100
 //de = 100

 ////Coordinate offsets in radians
 //dLat = dn/R
 //dLon = de/(R*Cos(Pi*lat/180))

 ////OffsetPosition, decimal degrees
 //latO = lat + dLat * 180/Pi
 //lonO = lon + dLon * 180/Pi 
		}

		public static IGeometry ProjectToPosgar3(IGeometry geomIn)
		{
			// 4326 a wkt22183 
			return Project(geomIn, wkt22183);
		}

	}
}
