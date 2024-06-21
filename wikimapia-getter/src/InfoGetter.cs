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
	class InfoGetter
	{
		const int sleepSeconds = 8;
		const string urlPreffix = "http://api.wikimapia.org/?key={key}&function=box&coordsby=bbox&count=100&format=json&";
		const string outputPath = @"D:\Pablo\Sociologia\Mapa Social\tools\src\wikimapia\wiki-getter\output";

		int keyIndex = 0;
		
		static string[] keys = new string[] {
			"8DD1B337-A264B9D0-5B50DF15-321A57CD-AA15AF06-673B79FA-21C4179B-86E55B1C",
			"8DD1B337-71A3AF64-CC00EBA6-1B4A588C-4B6C08D5-EE70FB0D-2C837E4B-B1AC3319",
			"14CA5564-508408E9-50841BBB-912F2A69-F9EA03A9-EDDAE3EC-58B6337E-74BE5D1C",
"14CA5564-49FB217D-B51B42B2-C1950A28-EBE4A2EC-9B37D780-D8FB3F51-814E553C",
"14CA5564-FD2D35F0-A89B8944-25C72849-D1607DEC-0134A0C3-00C0294C-4F5438D4",
"14CA5564-A8DB684A-F1A5F0C6-7B247D41-F1B89024-0C51CD3A-4EAF545E-F9F044FC",
"14CA5564-A407CFF3-00EE5F61-C79BCA8E-0840312F-33D0C17C-43CDF0AC-84304902",

"14CA5564-F647AED1-27D790A3-C0BC827E-E8155CD5-8281A073-EF52B319-847314D7",
"8DD1B337-B00ACBE8-35C35C3D-7C0302FE-E2A13065-B29BB577-F5F76DEE-2D8B8596",
"8DD1B337-EF59120-F503A190-0B574F03-B36207EC-2FF90AA5-8998A472-A490F9A6",
"8DD1B337-4DEDF202-B035EBBA-95B63D7C-CF9DD657-F7C2E092-2BFB4D0C-C979A5F6",
"8DD1B337-FE8D6D25-59BBD499-C9B4A4C6-19AA9A87-0B8BA53B-2A956E41-C967BD83",

"8DD1B337-FB7BE204-38E28261-A78EA979-3902C888-0AF5729A-4A282F87-D2077F",
"8DD1B337-22A04AB2-CED3BA30-75961144-F1ACA6E8-F1C88D8C-EC48AFBD-9FCC0CF2",
"8DD1B337-DD45F845-BA6D27FA-EC30D42E-78DDEB6E-FD70CD92-87B3F5F0-7CAF3A92",
"14CA5564-AE50ABEA-A793376A-56072551-38E7BEB7-74B1CDC0-9116C189-E0E4BDC6",
			"8DD1B337-8577E917-3F53BA51-30D8A061-F98FB2A3-06AB06BF-69D53A61-85280D02",
			"8DD1B337-BC521531-81BA30E8-8F25BF6F-E9066D74-7B4F5144-66DC9C6D-90760EAD",
			"8DD1B337-3B67660D-42678DE1-593C9828-8B04048C-29CEFDA1-01380AB2-222F756F",
			"8DD1B337-2F45E8F1-DF3E6F24-6D152920-9369A184-4B2AB47C-9A56272E-225D0F13",
			"8DD1B337-BB227416-ADA9DD34-A8249012-2080EC57-5807E0E4-8F8C51A0-1DE1893C",
			"8DD1B337-17B2437C-F9DD4854-2B76990B-DA370608-9BE2777F-79B89278-A368B4BB",
			"8DD1B337-A41058FE-7F1E15B3-9399213B-091B0CFD-08256C61-89DEF411-7C202050",

					"8DD1B337-45F5CA4D-24F92773-6BFAC502-28C60C06-104FC53F-86DED57E-7EE3EF05",
					"14CA5564-C518456E-30306D7B-C3E2F498-EFE96D4D-06ED8E45-486C0F42-8111250",
					"14CA5564-9121A8CE-BC51C164-E51FB77E-51C351B8-3EFF2FAC-BDB195D8-7F978FA4",
					"14CA5564-D2DE58A2-57B42DB1-17EBCE14-BD4E5C88-C227B879-5A39D900-CCC21C0E",
					"14CA5564-9E477616-DFF340C4-4C40937C-92ABF800-DE125713-572D8AE3-2238CAF",
					"14CA5564-8F050799-F7862596-D17CDE90-522A8470-BC58AB9C-29B216B6-DEDD8A87",
					"14CA5564-2C10E3DB-51B9DD15-7FB2A0D5-861FDF5B-531220D4-B779E6C7-BED477B7",
					"14CA5564-1B403553-2A92F12F-BAF079A9-E5C1145B-AA0091B8-A8D316FC-7D704FBE",
					"14CA5564-BF58092D-B7E5045C-BC57F076-68B3CBE2-9428FE17-D2F577A2-583919D0",
					"14CA5564-30355FDA-0D867EED-EA73C42E-E865D2BD-4B968E1C-7466063D-FB921A48",
					"14CA5564-A899C04-C2742BEE-1C12BCAE-99AD3A9B-88547EEF-C1AB2752-BDDE5BB5",
					"14CA5564-8B8C45EF-A2FE6FDC-77D1FBF4-65DDEC6D-84CCFE76-AD18FF86-60425DDC",
					"14CA5564-43118720-81BFB7C0-5122E79B-F1B25583-9A4821E1-8DF94564-D841404C",
					"14CA5564-F971E75E-A513FAEE-764D5085-E581B5C5-D30FBD70-76891622-D58AE5EC" };

		private int failed = 0;

		public void Go()
		{
			// Nor oeste: -21.780778,-73.570804
			// Latitud sur: -55.055620,-53.637548.


			var lat_min = -55.055620;
			var lon_min = -73.570804;
			var lat_max = -21.780778;
			var lon_max = -53.637548;
			var category = 55191;
	//		var lat_interval = .5;
//			var lon_interval = 1;

		var lat_interval = .25;
		var lon_interval = .5;
			failed = 0;

			var country = Argentina.Get();
			List<Tile> items = CalculateRectangles(lat_min, lon_min, lat_max, lon_max,
							lat_interval, lon_interval, country);
			var n = 1;
			foreach (Tile t in items)
			{
				Console.WriteLine("Obteniendo " + n + " de " + items.Count + " pendientes.");
				while (!GetRectangle(category, t))
				{
					Console.WriteLine("Key limit has been reached " + GetKey());
					keyIndex++;
				}
				n++;
			}
		
			Console.WriteLine("Done. Failed: " + failed);
		}

		private List<Tile> CalculateRectangles(double lat_min, double lon_min, double lat_max,
			double lon_max, double lat_interval, double lon_interval, Geometry country)
		{
			List<Tile> items = new List<Tile>();

			for (var lat = lat_max; lat >= lat_min; lat -= lat_interval)
		//	for (var lat = lat_min; lat < lat_max; lat += lat_interval)
				for (var lon = lon_min; lon < lon_max; lon += lon_interval)
				{
					var lat_end = lat + lat_interval;
					var lon_end = lon + lon_interval;
					Tile t = new Tile() { lat_min = lat, lat_max = lat_end, lon_min = lon, lon_max = lon_end };
					string target = resolveFilename(t);
					if (Directory.GetFiles(outputPath, Path.GetFileName(target) + "-Count_*.*").Length == 0)
					{
						Coordinate[] points = new Coordinate[] {   new Coordinate(lon, lat),
																	new Coordinate(lon, lat_end),
																	new Coordinate(lon_end, lat_end),
																	new Coordinate(lon_end, lat),
																	new Coordinate(lon, lat)};

						GeometryFactory geomFactory = new GeometryFactory();
						Polygon p = geomFactory.CreatePolygon(points);
						if (country.Contains(p) || country.Intersects(p))
						{
							items.Add(t);
						}
					}
				}
			return items;
		}

		private bool GetRectangle(int category, Tile t)
		{
			string target = resolveFilename(t);
			var url = urlPreffix + "bbox=" + t.lon_min.ToString().Replace(",", ".") + "%2C" + t.lat_min.ToString().Replace(",", ".") + "%2C" + t.lon_max.ToString().Replace(",", ".") + "%2C" + t.lat_max.ToString().Replace(",", ".") + "&category=" + category;
			string res = Get(url);
			Dictionary<string, string> filesToSave = new Dictionary<string, string>();
			if (res == null)
			{
				failed++;
				return true;
			}
			dynamic results = JsonConvert.DeserializeObject<dynamic>(res);
			if (results.debug != null && results.debug.message == "Key limit has been reached")
				return false;
			int resultsCount = (int)results.found;
			filesToSave.Add(target + "-Count_" + resultsCount + ".json", res);
			if (results.found == null)
			{
				failed++;
				Console.WriteLine("Error inesperado");
				Application.Exit();
			}
			if (resultsCount > 100)
			{
				// trae el resto
				for (var n = 2; n <= (( resultsCount - 1) / 100) + 1; n++)
				{
					string resPaged = Get(url + "&page=" + n);
					if (resPaged == null)
					{
						failed++;
						return true;
					}
					dynamic results2 = JsonConvert.DeserializeObject<dynamic>(res);
					if (results2.found == null)
						{
							failed++;
							Console.WriteLine("Error inesperado");
							Application.Exit();
						}
			
					filesToSave.Add(target + "-Count_" + results2.found + "-Page_" + n + ".json", resPaged);
				}
			}
			foreach (var items in filesToSave)
				File.WriteAllText(items.Key, items.Value);

			return true;
		}

		private static string resolveFilename(Tile t)
		{
			return Path.Combine(outputPath, "lat" + t.lat_min.ToString() + "-lon" + t.lon_min.ToString());
		}

		public string Get(string uri)
		{
			Console.WriteLine(uri.Replace(urlPreffix, ""));
			uri = uri.Replace("{key}", GetKey());

			Thread.Sleep(sleepSeconds * 1000);

			for (var n = 1; n < 4; n++)
			{
				try
				{
					HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
					request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
			//		request.Proxy = new WebProxy ("80.48.119.28", 8080);
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
				catch (Exception ex)
				{
					Console.WriteLine("ERR:" + ex.Message);
				}
				Thread.Sleep(15 * 1000);
			}
			Console.WriteLine("Too much failed");
			throw new Exception();
		}

		private string GetKey()
		{
			return keys[keyIndex];
		}
	}
}
