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
using System.Linq;
using NetTopologySuite.Features;
using System;
using GeoAPI.Geometries;

namespace medea.actions
{
	public class ShapeOperations
	{
		public const int maxErrors = 20;

		public static void ValidateParentAndShapes(Progress Progress,
				Dictionary<string, int> items, string file, string codeColumn, string iParent, bool skipOrphans = false)
		{
			medea.context.Data.Session.Ping();
			List<NetTopologySuite.Features.Feature> features;
			bool validateGeo = true;
			if (file.EndsWith(".shp", System.StringComparison.InvariantCultureIgnoreCase))
				features = ShapeFile.ReadShapefile(file);
			else if (file.EndsWith(".dbf", System.StringComparison.InvariantCultureIgnoreCase))
			{
				features = ShapeFile.ReadDbasefile(file);
				validateGeo = false;
			}
			else
				throw new Exception("Invalid extension.");
			ValidateFeatures(Progress, items, codeColumn, iParent, features, validateGeo, skipOrphans);
			medea.context.Data.Session.Ping();
		}

		private static void ValidateFeatures(Progress Progress, Dictionary<string, int> items, string codeColumn, string iParent, List<NetTopologySuite.Features.Feature> features, bool checkFeatures = true, bool skipOrphans = false)
		{
			Progress.Total = features.Count;
			List<string> invalid = new List<string>();
			List<string> noParent = new List<string>();
			int row = 1;
			List<string> emptyCenter = new List<string>();

			foreach (var feature in features)
			{
				object ocode = feature.Attributes[codeColumn];
				string code = (ocode == null ? null : ocode.ToString());
				if (string.IsNullOrEmpty(code))
					throw new MessageException("El valor para el código no puede se nulo. Fila: " + row.ToString());
				row++;

				if (checkFeatures && feature.Geometry.IsValid == false)
					invalid.Add(code);
				if (checkFeatures && feature.Geometry.Centroid.IsEmpty)
					emptyCenter.Add(code);

				if (items != null)
				{
					object oParent = feature.Attributes[iParent];
					string parent = (oParent == null ? null : oParent.ToString());
					if (!items.ContainsKey(parent))
					{
						if (!skipOrphans)
						{
							if (parent != code)
								noParent.Add(code + "=>" + parent);
							else
								noParent.Add(code);
						}
					}
				}
				if (noParent.Count > maxErrors || invalid.Count > maxErrors)
					break;
				Progress.Increment();
			}

			if (invalid.Count > 0 || noParent.Count > 0 || emptyCenter.Count > 0)
			{
				string message = "";
				if (noParent.Count > 0)
				{
					message += "Hay " + noParent.Count + " " + ItemPlural(invalid.Count) + " ítems existentes en el DBF que no se encuentran en el archivo SAV. "
						+ ": " + FirstOrAll(noParent.Count, maxErrors) + ": \n" + string.Join(", ", noParent.Take(maxErrors)) + " (" + codeColumn + ")"
						+ "\nValores de ejemplo esperados: " + GetFirstKeys(items);
				}
				if (emptyCenter.Count > 0)
				{
					if (message != "") message += "\n\n";
					message += "Hay " + emptyCenter.Count + " " + ShapeOperations.ItemPlural(emptyCenter.Count) + " con centroides vacíos. "
						+ ShapeOperations.FirstOrAll(emptyCenter.Count, ShapeOperations.maxErrors) + ": \n" + string.Join(", ", emptyCenter.Take(ShapeOperations.maxErrors)) + " (" + codeColumn + ")"; ;
				}
				if (invalid.Count > 0)
				{
					if (message != "") message += "\n\n";
					message += "Hay " + invalid.Count + " " + ItemPlural(invalid.Count) + " con geometrías inválidas. "
						+ FirstOrAll(invalid.Count, maxErrors) + ": \n" + string.Join(", ", invalid.Take(maxErrors)) + " (" + codeColumn + ")";
				}
				throw new MessageException(message);
			}
		}

		public static string GetFirstKeys(Dictionary<string, int> ci)
		{
			int n = 0;
			string ret = "";
			foreach (string code in ci.Keys)
			{
				ret += ", '" + code + "'";
				n++;
				if (n > 4)
					break;
			}
			if (ret == "")
				return "(sin valores)";
			else
				return ret.Substring(2);
		}

		public static string ItemPlural(int count)
		{
			if (count == 1)
				return "ítem";
			return "ítems";
		}

		public static string FirstOrAll(int actual, int max)
		{
			string ret = "Los ítems son";
			if (actual == 1)
				ret = "El ítem es";
			if (actual > max)
				ret += " (primeros " + max.ToString() + ")";
			return ret;
		}

		internal static Dictionary<string, IGeometry> GetGeometryDictionary(string file, string shapeColumn)
		{
			Dictionary<string, IGeometry> ret = new Dictionary<string, IGeometry>();
			var features = ShapeFile.ReadShapefile(file + ".shp");
			List<string> invalid = new List<string>();
			List<string> noParent = new List<string>();
			int row = 0;
			foreach (var feature in features)
			{
				row++;
				string currentCode;
				if (feature.Attributes[shapeColumn] != null)
					currentCode = feature.Attributes[shapeColumn].ToString();
				else
					currentCode = null;

				if (string.IsNullOrEmpty(currentCode))
					throw new MessageException("El valor para el código no puede se nulo. Fila: " + row.ToString());
				ret.Add(currentCode, feature.Geometry);
			}
			return ret;
		}

		internal static GeoAPI.Geometries.IGeometry GetGeometryByCode(string file, string shapeColumn, string code)
		{
			var features = ShapeFile.ReadShapefile(file + ".shp");
			List<string> invalid = new List<string>();
			List<string> noParent = new List<string>();
			int row = 0;
			foreach (var feature in features)
			{
				row++;
				string currentCode;
				if (feature.Attributes[shapeColumn] != null)
					currentCode = feature.Attributes[shapeColumn].ToString();
				else
					currentCode = null;

				if (string.IsNullOrEmpty(currentCode))
					throw new MessageException("El valor para el código no puede se nulo. Fila: " + row.ToString());
				if (currentCode == code)
					return feature.Geometry;
			}
			return null;
		}
	}
}
