using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json.Linq;

namespace stress_tester
{
	public class Serializator
	{
		public static T Clone<T>(T obj)
		{
			 using (MemoryStream memory_stream = new MemoryStream())
			{
					// Serialize the object into the memory stream.
					BinaryFormatter formatter = new BinaryFormatter();
					formatter.Serialize(memory_stream, obj);

					// Rewind the stream and use it to create a new object.
					memory_stream.Position = 0;
					return (T)formatter.Deserialize(memory_stream);
			}
		}

		public static T FromFile<T>(string filename) where T : new()
		{
			T ret;
			if (new FileInfo(filename).Length == 0)
				return new T();
			using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
			{
				BinaryFormatter formatter = new BinaryFormatter();
				ret = (T)formatter.Deserialize(fs);
			}
			return ret;
		}

		public static void ToFile(string filename, object obj)
		{
			using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
			{
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(fs, obj);
			}
		}
	}
}
