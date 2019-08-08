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
using System;
using System.Collections.Generic;

namespace medea.common
{
	/// <summary>
	/// Migrated from: https://github.com/geotools/geotools/blob/0e6fc022f395194e7670b63d2de785123a178cdf/modules/library/main/src/main/java/org/geotools/filter/function/JenksNaturalBreaksFunction.java
	/// </summary>
	public class NtilesBreaks
	{
		public static List<Tuple<double, double>> Calculate(List<double?> data, List<double?> weights, int classes)
		{
			
			double totalPopulation;
			List<KeyValuePair<double, double>> dataWeighted = CreateSortedList(data, weights, out totalPopulation);
			if (dataWeighted.Count <= classes)
				return JenksNaturalBreaks.CreateMiminalList(GetKeys(dataWeighted), classes);

			// Ahora itera por la lista ordenada armando las clases
			List<Tuple<double, double>> ret = new List<Tuple<double, double>>();
			double cutInterval = totalPopulation / classes;
			double nextCutPoint = cutInterval;
			double lastEffectiveCutPoint = dataWeighted[0].Key;
			double cummulatedSum = 0;
			double lastItemValue = lastEffectiveCutPoint;
			foreach (var item in dataWeighted)
			{
				if (lastItemValue != item.Key && 
					cummulatedSum >= nextCutPoint)
				{
					ret.Add(new Tuple<double, double>(lastEffectiveCutPoint, item.Key));
					lastEffectiveCutPoint = item.Key;
					nextCutPoint += cutInterval;
				}
				lastItemValue = item.Key;
				// Arrastra la suma acumulada
				cummulatedSum += item.Value;
			}
			ret.Add(new Tuple<double, double>(lastEffectiveCutPoint, lastItemValue));

			return ret;
		}

		private static IList<double> GetKeys(List<KeyValuePair<double, double>> dataWeighted)
		{
			List<double> ret = new List<double>();
			foreach (var item in dataWeighted)
				ret.Add(item.Key);
			return ret;
		}

		private static List<KeyValuePair<double, double>> CreateSortedList(List<double?> data, List<double?> weights, out double totalPopulation)
		{
			var dataWeighted = new List<KeyValuePair<double, double>>();
			if (weights != null && weights.Count != data.Count)
				throw new Exception("La cantidad de ponderadores no puede ser diferente a la cantidad de datos.");
			totalPopulation = 0;
			for (int n = 0; n < data.Count; n++)
			{
				if (data[n].HasValue)
				{
					double? weight;
					if (weights != null)
						weight = weights[n];
					else
						weight = 1;
					if (weight.HasValue && weight.Value > 0)
					{
						dataWeighted.Add(new KeyValuePair<double, double>(data[n].Value, weight.Value));
						// precalcula el total poblacional
						totalPopulation += weight.Value;
					}
				}
			}
			Comparison<KeyValuePair<double, double>> c = (a, b) => a.Key.CompareTo(b.Key);
      dataWeighted.Sort(c);
			return dataWeighted;
		}

	}
}
