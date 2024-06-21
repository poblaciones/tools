using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wiki_getter
{
	class InfoSummarize
	{
		const string outputPath = @"D:\Pablo\Sociologia\Mapa Social\tools\src\wikimapia\wiki-getter\output";

		public void Go()
		{
			var country = Argentina.GetPrecise();
			List<Barrio> barrios = ConsolidaBarrios(country);
			// Los graba...
			string csv = Path.Combine(outputPath, "out.csv");
			List<string> lines = new List<string>();
			lines.Add("id\tname\turl\tWKT");
			foreach(Barrio b in barrios)
			{
				string text = b.id + "\t";
				text += b.name.Replace("\t", " ") + "\t";
				text += b.url.Replace("\t", " ") + "\t";
				string polygon = b.geometry.ToText();
				text += polygon;
				lines.Add(text);				
			}
			File.WriteAllLines(csv, lines.ToArray());
			Console.Write("Listo! " + (lines.Count - 1) + " agregados.");
		}

		private static List<Barrio> ConsolidaBarrios(Geometry country)
		{
			List<Barrio> barrios = new List<Barrio>();
			Dictionary<string, bool> done = new Dictionary<string, bool>();
			foreach (string file in Directory.GetFiles(outputPath, "*.json"))
			{
				string info = File.ReadAllText(file);
				dynamic results = JsonConvert.DeserializeObject<dynamic>(info);
				if (results.found > 0)
				{
					foreach (var barrio in results.folder)
					{
						if (!done.ContainsKey(barrio.id.ToString()))
						{
							Barrio b = new Barrio();
							b.name = barrio.name;
							b.name = b.name.Replace("  ", " ").Replace("  ", " ").Replace(" (es)", "");
							b.id = barrio.id;
							b.url = barrio.url;
							b.polygon = barrio.polygon;
							List<Coordinate> points = new List<Coordinate>();
							foreach (var item in b.polygon)
							{
								points.Add(new Coordinate((double) item.x, (double) item.y));
							}
							if (points[0].X != points[points.Count - 1].X || points[0].Y != points[points.Count - 1].Y)
								points.Add(new Coordinate(points[0].X, points[0].Y));

							GeometryFactory geomFactory = new GeometryFactory();
							b.geometry = geomFactory.CreatePolygon(points.ToArray());
							
							if (country.Contains(b.geometry))
							{
								barrios.Add(b);
								Console.WriteLine("Agregado " + b.name);
							}
							done.Add(b.id, true);
						}
					}
				}
			}
			return barrios;
		}
	}
}
