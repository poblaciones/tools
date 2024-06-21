using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace addressCoder
{
	class cache
	{
		Dictionary<string, string> dict = new Dictionary<string, string>();

		public string resolveFilename()
		{
			string folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			return Path.Combine(folder, "cache.json");
		}
		public void Put(string key, string o)
		{
			dict[key] = o;
			Save();
		}
		public string Get(string key)
		{
			if (dict.ContainsKey(key))
				return dict[key];
			else
				return null;
		}
		public void Save()
		{
			string file = this.resolveFilename();
			string output = JsonConvert.SerializeObject(dict);
			File.WriteAllText(file, output);
		}
		public void Load()
		{
			string file = this.resolveFilename();
			string data;
			if (File.Exists(file))
			{
				data = File.ReadAllText(file);
				dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
			}
			else
				dict.Clear();
		}
	}
}
