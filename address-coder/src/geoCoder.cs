using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace addressCoder
{
	class geoCoder 
	{
		public string API_KEY = "";
		cache cache = new cache();
		public geoCoder()
		{
			cache.Load();
		}
		string url = "https://maps.googleapis.com/maps/api/geocode/json?address=DIREC&key=YOUR_API_KEY";
		// ej. 1600+Amphitheatre+Parkway,+Mountain+View,+CA
		public dynamic Get(string add)
		{
			string fromCache = cache.Get(add);
			if (fromCache != null && !fromCache.Contains("exceeded your rate-limit for this"))
				return JsonConvert.DeserializeObject(fromCache);
			System.Threading.Thread.Sleep(200);
			// hace el get
			string uri = url.Replace("DIREC", add);
			uri = uri.Replace("YOUR_API_KEY", API_KEY);

			string ret = HttpGet(uri);
			dynamic decoded = JsonConvert.DeserializeObject(ret);
			object status = decoded.status;
			if (decoded.status.ToString() == "REQUEST_DENIED" || ret.Contains("exceeded your rate-limit for this"))
				throw new Exception(decoded.error_message.ToString());
			cache.Put(add, ret);
			return decoded;
		}


		public string HttpGet(string uri)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
			request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
			request.Referer = "https://mapas.poblaciones.org/";
			using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
			using (Stream stream = response.GetResponseStream())
			using (StreamReader reader = new StreamReader(stream))
			{
				return reader.ReadToEnd();
			}
		}
	}
}
