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
using System.Globalization;

namespace medea.common
{
	/// <summary>
	/// Migrated from: https://github.com/geotools/geotools/blob/0e6fc022f395194e7670b63d2de785123a178cdf/modules/library/main/src/test/java/org/geotools/filter/function/JenksFunctionTest.java
	/// </summary>
	public class JenksTest
	{
		private string ToPair(Tuple<double, double> item)
		{
			return item.Item1.ToString(CultureInfo.InvariantCulture) 
				+ ".." + item.Item2.ToString(CultureInfo.InvariantCulture);
		}

		public void assertEquals(string msg, object a, object b)
		{
			if ((a == null && b != null)
				|| (b == null && a != null)
				|| a.ToString().Equals(b.ToString()) == false)
			{
				throw new Exception(msg);
			}
		}

		public void assertEquals(object a, object b)
		{
			assertEquals("", a, b);
		}

		public static void TestAll()
		{
			var t = new JenksTest();
			t.testEvaluateRealData();
			t.testEvaluateWithExpressions();
			t.testSingleBin();
		}

		// Rework to test with Jenks71 data
		// Answer (from R) is [15.57,41.2] (41.2,60.66] (60.66,77.29] (77.29,100.1] (100.1,155.3]
		public void testEvaluateRealData()
		{
			double[] jenks71 = new double[] { 50.12, 83.9, 76.43, 71.61, 79.66, 84.84, 87.87, 92.45, 119.9,
				155.3, 131.5, 111.8, 96.78, 86.75, 62.41, 96.37, 75.51, 77.29, 85.41, 116.4, 58.5, 75.29,
				66.32, 62.65, 80.45, 72.76, 63.67, 60.27, 68.45, 100.1, 55.3, 54.07, 57.49, 73.52,
				68.25, 64.28, 50.64, 52.47, 68.19, 57.4, 39.72, 60.66, 57.59, 38.22, 57.22, 67.04,
				47.29, 71.05, 50.53, 34.63, 59.65, 62.06, 52.89, 56.35, 57.26, 53.77, 59.89, 55.44,
				45.4, 52.21, 49.38, 51.15, 54.27, 54.32, 41.2, 34.58, 50.11, 52.05, 33.82, 39.88,
				36.24, 41.02, 46.13, 51.15, 32.28, 33.26, 31.78, 31.28, 50.52, 47.21, 32.69, 38.3,
				33.83, 40.3, 40.62, 32.14, 31.66, 26.09, 39.84, 24.83, 28.2, 31.19, 37.57, 27.16,
				23.42, 18.57, 30.97, 17.82, 15.57, 15.93, 28.71, 32.22 };

			var jenks = JenksNaturalBreaks.Calculate(jenks71, 5);
			assertEquals(5, jenks.Count);

			assertEquals("15.57..41.2", ToPair(jenks[0]));
			assertEquals("41.2..60.66", ToPair(jenks[1]));
			assertEquals("60.66..77.29", ToPair(jenks[2]));
			assertEquals("77.29..100.1", ToPair(jenks[3]));
			assertEquals("100.1..155.3", ToPair(jenks[4]));
		}

		public void testEvaluateWithExpressions()
		{
			double[] data = new double[] { 4, 90, 20, 43, 29, 61, 8, 12 };
			var jenks = JenksNaturalBreaks.Calculate(data, 2);

			// the values being quantiled are
			// {4,90,20,43,29,61,8,12};
			// so there should be two groups:
			// {4, 8, 12, 20} 4 <= x < 29
			// {29, 43, 61, 90} 29 <= x <= 90
			assertEquals(2, jenks.Count);
			assertEquals("4..29", ToPair(jenks[0]));
			assertEquals("29..90", ToPair(jenks[1]));
		}


		/// <summary>
		/// Test a feature collection where each feature will be in it's own bin.
		///
		/// Creates a feature collection with five features 1-5. Then uses the quantile function to put
		/// these features in 5 bins. Each bin should have a single feature.
		/// </summary>
		public void testSingleBin()
		{
			// create a feature collection with five features values 1-5
			var data = new double[] { 1, 2, 3, 4, 5 };
			// run the quantile function
			var jenks = JenksNaturalBreaks.Calculate(data, 5);

			assertEquals(5, jenks.Count);
			for (int i = 0; i < 5; i++)
			{
				assertEquals(i + 1, jenks[i].Item1);
				if (i != 4)
				{
					assertEquals("wrong value for max", i + 2, (int)jenks[i].Item2);
					assertEquals("bad title", (i + 1) + ".." + (i + 2), ToPair(jenks[i]));
				}
				else
				{
					assertEquals("wrong value for max", i + 1, (int)jenks[i].Item2);
					assertEquals("bad title", (i + 1) + ".." + (i + 1), ToPair(jenks[i]));
				}
			}
		}
	}
}
