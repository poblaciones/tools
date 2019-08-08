using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace NHibernate.Caches.FileCache
{
	class FsCache
	{
		private string path;
		public FsCache(string path)
		{
			this.path = path;
		}

		internal void Add(string cacheKey, object entry)
		{
			string file = resolveFile(cacheKey);
			serialize(file, entry);
		}

		private void serialize(string file, object entry)
		{
			FileStream fs = new FileStream(file, FileMode.Create);
			// Construct a BinaryFormatter and use it to serialize the data to the stream.
			BinaryFormatter formatter = new BinaryFormatter();
			try
			{
				formatter.Serialize(fs, entry);
				fs.Close();
			}
			catch (SerializationException e)
			{
				fs.Close();
				File.Delete(file);
				Console.WriteLine("Failed to serialize. Reason: " + e.Message);
				throw;
			}
		}
		private object deserialize(string file)
		{
			FileStream fs = new FileStream(file, FileMode.Open);
			// Construct a BinaryFormatter and use it to serialize the data to the stream.
			BinaryFormatter formatter = new BinaryFormatter();
			try
			{
				return formatter.Deserialize(fs);
			}
			catch (SerializationException e)
			{
				Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
				throw;
			}
			finally
			{
				fs.Close();
			}
		}

		private string resolveFile(string cacheKey)
		{
			return Path.Combine(path, cacheKey + ".dat");
		}

		internal object Get(string cacheKey)
		{
			string file = resolveFile(cacheKey);
			if (File.Exists(file))
			{
			
			if (new FileInfo(file).Length == 0)
			{
				File.Delete(file);
				return null;
			}
				else
					return deserialize(file);
			}
			else
				return null;
		}

		internal void Remove(string cacheKey)
		{
			string file = resolveFile(cacheKey);
			if (File.Exists(file))
				File.Delete(file);
		}

		internal bool ContainsKey(string cacheKey)
		{
			string file = resolveFile(cacheKey);
			if (
				!File.Exists(file)) return false;

			if (new FileInfo(file).Length == 0)
			{
				File.Delete(file);
				return false;
			}
			return true;
		}
	}
}
