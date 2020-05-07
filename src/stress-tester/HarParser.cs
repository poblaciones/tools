using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

namespace stress_tester
{
	public class HarParser
	{
		public static List<Request> ParseRequests(string filePath)
		{
			var json = File.ReadAllText(filePath);
			var data = JObject.Parse(json);

			var entries = data["log"]["entries"];
			var ret = new List<Request>();
			foreach (var entry in entries)
			{
				var url = entry["request"]["url"].Value<string>();
				var date = entry["startedDateTime"].Value<DateTime>();
				var ellapsed = entry["time"].Value<double>();
				ret.Add(new Request(url, date, Guid.NewGuid()));
			}
			return ret;
		}
	}
}
