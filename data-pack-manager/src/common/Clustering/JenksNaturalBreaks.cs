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
	/// // weighted: https://github.com/pschoepf/naturalbreaks/blob/master/src/main/java/de/pschoepf/naturalbreaks/JenksFisher.java
	///
	/// </summary>
	public class JenksNaturalBreaks
	{
		public static List<Tuple<double, double>> Calculate(List<double?> data, int classes)
		{
			List<double> dataNoNull = new List<double>();
			foreach (var val in data)
				if (val.HasValue) dataNoNull.Add(val.Value);
			return Calculate(dataNoNull, classes);
		}

		public static List<Tuple<double, double>> Calculate(List<double> data, int classes)
		{
			return Calculate(data.ToArray(), classes);
		}

		public static List<Tuple<double, double>> Calculate(double[] data, int classes)
		{
			using (new WaitCursor())
			{
				Array.Sort(data);
				int k = classes;
				int m = data.Length;
				if (k >= m)
					return CreateMiminalList(data, classes);

				var iwork = new int[m + 1, k + 1];
				var work = new double[m + 1, k + 1];

				for (int j = 1; j <= k; j++)
				{
					// the first item is always in the first class!
					iwork[0, j] = 1;
					iwork[1, j] = 1;
					// initialize work matirix
					work[1, j] = 0;
					for (int i = 2; i <= m; i++)
						work[i, j] = double.MaxValue;
				}

				// calculate the class for each data item
				for (int i = 1; i <= m; i++)
				{
					// sum of data values
					double s1 = 0.0;
					// sum of squares of data values
					double s2 = 0.0;

					double vari = 0.0;
					// consider all the previous values
					for (int ii = 1; ii <= i; ii++)
					{
						// index in to sorted data array
						int i3 = i - ii + 1;
						// remember to allow for 0 index
						double val = data[i3 - 1];
						// update running totals
						s2 = s2 + (val * val);
						s1 += val;
						double s0 = ii;
						// calculate (square of) the variance
						// (http://secure.wikimedia.org/wikipedia/en/wiki/Standard_deviation#Rapid_calculation_methods)
						vari = s2 - ((s1 * s1) / s0);
						// Debug.WriteLine(s0 + " " + s1 + " " + s2);
						// Debug.WriteLine(i + "," + ii + " vari " + vari);
						int ik0 = i3 - 1;
						if (ik0 != 0)
						{
							// not the last value
							for (int j = 2; j <= k; j++)
							{
								// for each class compare current value to vari + previous value
								// Debug.WriteLine("\tis "+work[i, j]+" >= "+(vari + work[ik0, j - 1]));
								if (work[i, j] >= (vari + work[ik0, j - 1]))
								{
									// if it is greater or equal update classification
									iwork[i, j] = i3 - 1;
									// Debug.WriteLine("\t\tiwork["+i+", "+j+"] = "+i3);
									work[i, j] = vari + work[ik0, j - 1];
								}
							}
						}
					}
					// store the latest variance!
					iwork[i, 1] = 1;
					work[i, 1] = vari;
				}

				//For debug
				//for (int i = 0; i < m; i++)
				//{
				//	string tmp = i + ": " + data[i];
				//	for (int j = 2; j <= k; j++)
				//		tmp += ("\t" + iwork[i, j]);

				//	Debug.WriteLine(tmp);
				//}

				// go through matrix and extract class breaks
				int ik = m - 1;

				var min = new double[k];
				var max = new double[k];
				max[k - 1] = data[ik];
				for (int j = k; j >= 2; j--)
				{
					//Debug.WriteLine("index " + ik + ", class" + j);
					int id = iwork[ik, j] - 1; // subtract one as we want inclusive breaks on the
					// left?
					max[j - 2] = data[id];
					min[j - 1] = data[id];
					ik = iwork[ik, j] - 1;
				}
				min[0] = data[0];
				// for(int k1=0;k1<k;k1++) { Debug.WriteLine(k1+" "+localMin[k1]+" - "+localMax[k1]); }

				var ret = new List<Tuple<double, double>>();
				for (int i = 0; i < k; i++)
					ret.Add(new Tuple<double, double>(min[i], max[i]));
				return ret;
			}
		}

		public static List<Tuple<double, double>> CreateMiminalList(IList<double> data, int classes)
		{
			var max = Math.Min(classes, data.Count);
			var list = new List<Tuple<double, double>>();
			if (max == 0) return list;
			for (int id = 0; id < max; id++)
				list.Add(new Tuple<double, double>(data[id], data[id]));

			return list;
		}
	}
}

