#region License

//
//  FileCache - A cache provider for NHibernate using System.Runtime.Caching.MemoryCache.
//
//  This library is free software; you can redistribute it and/or
//  modify it under the terms of the GNU Lesser General Public
//  License as published by the Free Software Foundation; either
//  version 2.1 of the License, or (at your option) any later version.
//
//  This library is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//  Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public
//  License along with this library; if not, write to the Free Software
//  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//

#endregion

using System;
using System.Collections;
using System.Runtime.Caching;
using NHibernate.Cache;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

namespace NHibernate.Caches.FileCache
{
	/// <summary>
	/// Pluggable cache implementation using the System.Runtime.Caching classes
	/// </summary>
	public class FileCache : ICache
	{
		private static readonly IInternalLogger log = LoggerProvider.LoggerFor(typeof(FileCache));
		private readonly string region;
		private string regionPrefix;
		private TimeSpan expiration;
		private CacheItemPriority priority;

		private static readonly TimeSpan DefaultExpiration = TimeSpan.FromSeconds(300);
		private static readonly string DefauktRegionPrefix = string.Empty;
		private string path;
		private FsCache cache;
		/// <summary>
		/// default constructor
		/// </summary>
		public FileCache()
			: this("nhibernate", (IDictionary<string, string>)null)
		{
		}
		public object this[object key]
		{
			get
			{
				return Get(key);
			}
			set
			{
				Put(key, value);
			}
		}
		/// <summary>
		/// constructor with no properties
		/// </summary>
		/// <param name="region"></param>
		public FileCache(string region)
			: this(region, (IDictionary<string, string>)null)
		{
		}

		/// <summary>
		/// full constructor
		/// </summary>
		/// <param name="region"></param>
		/// <param name="properties">cache configuration properties</param>
		/// <remarks>
		/// There are two (2) configurable parameters:
		/// <ul>
		///		<li>expiration = number of seconds to wait before expiring each item</li>
		///		<li>priority = a numeric cost of expiring each item, where 1 is a low cost, 5 is the highest, and 3 is normal. Only values 1 through 5 are valid.</li>
		/// </ul>
		/// All parameters are optional. The defaults are an expiration of 300 seconds and the default priority of 3.
		/// </remarks>
		/// <exception cref="IndexOutOfRangeException">The "priority" property is not between 1 and 5</exception>
		/// <exception cref="ArgumentException">The "expiration" property could not be parsed.</exception>
		public FileCache(string region, IDictionary<string, string> properties)
		{
			this.region = region;
			Configure(properties);

			//rootCacheKey = GenerateRootCacheKey();
			path = resolvePath();
			if (Directory.Exists(path) == false)
			{
				try
				{
					Directory.CreateDirectory(path);
				}
				catch (Exception ex)
				{
					throw new Exception("Could not create folder " + path, ex);
				}
			}
			cache = new FsCache(path);

			//StoreRootCacheKey();
		}

		private string resolvePath()
		{
			string basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			return Path.Combine(basePath, "Caches", region);

		}

		public string Region
		{
			get { return region; }
		}

		public TimeSpan Expiration
		{
			get { return expiration; }
		}

		public CacheItemPriority Priority
		{
			get { return priority; }
		}

		private void Configure(IDictionary<string, string> props)
		{
			if (props == null)
			{
				if (log.IsWarnEnabled)
				{
					log.Warn("configuring cache with default values");
				}
				expiration = DefaultExpiration;
				priority = CacheItemPriority.Default;
				regionPrefix = DefauktRegionPrefix;
			}
			else
			{
				priority = GetPriority(props);
				expiration = GetExpiration(props);
				regionPrefix = GetRegionPrefix(props);
			}
		}

		public void SetRegionPrefix(string preffix)
		{
			regionPrefix = preffix;
		}
		private static string GetRegionPrefix(IDictionary<string, string> props)
		{
			string result;
			
			if (props.TryGetValue("regionPrefix", out result))
			{
				log.DebugFormat("new regionPrefix :{0}", result);
			}
			else
			{
				result = DefauktRegionPrefix;
				log.Debug("no regionPrefix value given, using defaults");
			}
			return result;
		}

		private static TimeSpan GetExpiration(IDictionary<string, string> props)
		{
			TimeSpan result = DefaultExpiration;
			string expirationString;
			if (!props.TryGetValue("expiration", out expirationString))
			{
				props.TryGetValue(Cfg.Environment.CacheDefaultExpiration, out expirationString);
			}

			if (expirationString != null)
			{
				try
				{
					int seconds = Convert.ToInt32(expirationString);
					result = TimeSpan.FromSeconds(seconds);
					log.Debug("new expiration value: " + seconds);
				}
				catch (Exception ex)
				{
					log.Error("error parsing expiration value");
					throw new ArgumentException("could not parse 'expiration' as a number of seconds", ex);
				}
			}
			else
			{
				if (log.IsDebugEnabled)
				{
					log.Debug("no expiration value given, using defaults");
				}
			}
			return result;
		}

		private static CacheItemPriority GetPriority(IDictionary<string, string> props)
		{
			CacheItemPriority result = CacheItemPriority.Default;
			string priorityString;
			if (props.TryGetValue("priority", out priorityString))
			{
				result = ConvertCacheItemPriorityFromXmlString(priorityString);
				if (log.IsDebugEnabled)
				{
					log.Debug("new priority: " + result);
				}
			}
			return result;
		}


		private static CacheItemPriority ConvertCacheItemPriorityFromXmlString(string priorityString)
		{
			if (string.IsNullOrEmpty(priorityString))
			{
				return CacheItemPriority.Default;
			}
			var ps = priorityString.Trim().ToLowerInvariant();
			if (ps.Length == 1 && char.IsDigit(priorityString, 0))
			{
				// the priority is specified as a number
				int priorityAsInt = int.Parse(ps);
				if (priorityAsInt >= 1 && priorityAsInt <= 6)
				{
					return (CacheItemPriority)priorityAsInt;
				}
			}
			else
			{
				switch (ps)
				{
					case "default":
						return CacheItemPriority.Default;
					case "notremovable":
						return CacheItemPriority.NotRemovable;
				}
			}
			log.Error("priority value out of range: " + priorityString);
			throw new IndexOutOfRangeException("Priority must be a valid System.Runtime.Caching.CacheItemPriority; was: " + priorityString);
		}

		private string GetCacheKey(object key)
		{
			return String.Concat(regionPrefix, "@", key.ToString(), "@", key.GetHashCode());
		}

		public object Get(object key)
		{
			if (key == null)
			{
				return null;
			}
			string cacheKey = GetCacheKey(key);
			if (log.IsDebugEnabled)
			{
				log.Debug(String.Format("Fetching object '{0}' from the cache.", cacheKey));
			}

			object obj = cache.Get(cacheKey);
			if (obj == null)
			{
				return null;
			}

			var de = (DictionaryEntry)obj;
			if (key.Equals(de.Key))
			{
				return de.Value;
			}
			else
			{
				return null;
			}
		}

		public void Put(object key, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", "null key not allowed");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value", "null value not allowed");
			}
			string cacheKey = GetCacheKey(key);
			if (cache.ContainsKey(cacheKey) != null)
			{
				if (log.IsDebugEnabled)
				{
					log.Debug(String.Format("updating value of key '{0}' to '{1}'.", cacheKey, value));
				}

				// Remove the key to re-add it again below
				cache.Remove(cacheKey);
			}
			else
			{
				if (log.IsDebugEnabled)
				{
					log.Debug(String.Format("adding new data: key={0}&value={1}", cacheKey, value));
				}
			}

			//			if (!rootCacheKeyStored)
			//	{
			//			StoreRootCacheKey();
			//	}

			cache.Add(cacheKey, new DictionaryEntry(key, value));
		}
		public bool ContainsKey(object key)
		{
			string cacheKey = GetCacheKey(key);
			if (log.IsDebugEnabled)
			{
				log.Debug("checking contains key: " + cacheKey);
			}
			return cache.ContainsKey(cacheKey);
		}

		public void Remove(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			string cacheKey = GetCacheKey(key);
			if (log.IsDebugEnabled)
			{
				log.Debug("removing item with key: " + cacheKey);
			}
			cache.Remove(cacheKey);
		}

		public void Clear()
		{
		}


		public void Destroy()
		{
			Clear();
		}

		public void Lock(object key)
		{
			// Do nothing
		}

		public void Unlock(object key)
		{
			// Do nothing
		}

		public long NextTimestamp()
		{
			return Timestamper.Next();
		}

		public int Timeout
		{
			get { return Timestamper.OneMs * 60000; } // 60 seconds
		}

		public string RegionName
		{
			get { return region; }
		}
	}
}