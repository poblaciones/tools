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
	public partial class Form1 : Form
	{
		const int sleepSeconds = 5;
		private int failed = 0;
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			// Nor oeste: -21.780778,-73.570804
			// Latitud sur: -55.055620,-53.637548.


			var lat_min = -55.055620;
			var lon_min = -73.570804;
			var lat_max = -21.780778;
			var lon_max = -53.637548;
			var category = 55191;

			var urlPreffix = "http://api.wikimapia.org/?key=8DD1B337-45F5CA4D-24F92773-6BFAC502-28C60C06-104FC53F-86DED57E-7EE3EF05&function=box&coordsby=bbox&";
			var lat_interval = .5;
			var lon_interval = 1;
			failed = 0;

			var outputPath = @"D:\Pablo\Sociologia\Mapa Social\tools\src\wikimapia\wiki-getter\output";
			var country = Argentina.Get();
			for (var lat = lat_min; lat < lat_max; lat += lat_interval)
				for (var lon = lon_min; lon < lon_max; lon += lon_interval)
				{
					var target = Path.Combine(outputPath, "lat" + lat.ToString() + "-lon" + lon.ToString());
					if (!File.Exists(target))
					{
						var lat_end = lat + lat_interval;
						var lon_end = lon + lon_interval;
						Coordinate[] points = new Coordinate[] {   new Coordinate(lon, lat),
																	new Coordinate(lon, lat_end),
																	new Coordinate(lon_end, lat_end),
																	new Coordinate(lon_end, lat),
																	new Coordinate(lon, lat)};

						GeometryFactory geomFactory = new GeometryFactory();
						Polygon p = geomFactory.CreatePolygon(points);
						if (country.Contains(p))
						{
							GetRectangle(category, urlPreffix, sleepSeconds, lat, lon, target, lat_end, lon_end);
						}
					}
				}

			MessageBox.Show("Done. Failed: " + failed);
		}

		public string Get(string uri)
		{
			Thread.Sleep(sleepSeconds);

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
			request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

			using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
			{
				using (Stream stream = response.GetResponseStream())
				using (StreamReader reader = new StreamReader(stream))
				{
					if (response.StatusCode != HttpStatusCode.OK)
						return null;
					else
						return reader.ReadToEnd();
				}
			}
		}


		private void GetRectangle(int category, string urlPreffix, int sleepSeconds, double lat, double lon, string target, double lat_end, double lon_end)
		{
			var url = urlPreffix + "bbox=" + lon.ToString().Replace(",", ".") + "%2C" + lat.ToString().Replace(",", ".") + "%2C" + lon_end.ToString().Replace(",", ".") + "%2C" + lat_end.ToString().Replace(",", ".") + "&category=" + category + "&count=100&format=json";
			string res = Get(url);
			Dictionary<string, string> filesToSave = new Dictionary<string, string>();
			if (res == null)
			{
				failed++;
				return;
			}
			dynamic results = JsonConvert.DeserializeObject<dynamic>(res);
			filesToSave.Add(target + ".json", res);
			if (results.found > 100)
			{
				// trae el resto
				for (var n = 2; n <= ((results.found - 1) / 100) + 1; n++)
				{
					string resPaged = Get(url + "&page=" + n);
					if (res == null)
					{
						failed++;
						return;
					}
					filesToSave.Add(target + "-" + n + ".json", resPaged);
				}
			}
			foreach (var items in filesToSave)
				File.WriteAllText(items.Key, items.Value);
		}
	}
}