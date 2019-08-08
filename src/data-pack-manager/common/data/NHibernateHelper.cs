/*
*    Poblaciones - Plataforma abierta de datos espaciales de poblaciÃ³n.
*    Copyright (C) 2018-2019. Consejo Nacional de Investigaciones CientÃ­ficas y TÃ©cnicas (CONICET)
*		 y Universidad CatÃ³lica Argentina (UCA).
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
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Web;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Spatial.Mapping;

namespace medea.Data
{
	/// <summary>
	/// Modulo de manejo de sesión de NHibernate
	/// </summary>
	internal class NHibernateHelper
	{
		internal const string KEY_NHIBERNATE_CONFIG = "NHibernateSessionConfig";
		internal static ConnectionSettings ConnectionSettings;
		protected static Configuration _config;

		// this is only used if not running in HttpModule mode
		protected static ISessionFactory _factory;

		private const string KEY_DB_NAME = "CON_DB_NAME";
		private const string KEY_DB_PORT = "CON_DB_PORT";
		private const string KEY_DB_SERVER = "CON_DB_SERVER";

		// this is only used if not running in HttpModule mode
		//private static ISession session;
		//private static int activetransactions;
		//
		private const string KEY_DB_USER = "CON_DB_USER";
		private const string KEY_ENTITIES = "Entities";
		private const string KEY_NHIBERNATE_ASSEMBLY_ENTITIES = "hibernate.assembly.entities";
		private const string KEY_NHIBERNATE_CONNECTION_STRING = "hibernate.connection.connection_string";
		private const string KEY_NHIBERNATE_DIALECT = "hibernate.dialect";
		private const string KEY_NHIBERNATE_FACTORY = "NHibernateSessionFactory";

		private NHibernate.Caches.SysCache2.SysCacheProvider refeference1;
		private NHibernate.Caches.MemCache.MemCacheProvider refeference2;
		private NHibernate.Caches.EntLibCache.EntLibCacheProvider refeference3;
		private Microsoft.Practices.ObjectBuilder.DependencyAttribute refeference4;
		private Microsoft.Practices.EnterpriseLibrary.Common.Configuration.IObjectWithName refeference5;
		private Microsoft.Practices.EnterpriseLibrary.Caching.CacheItemPriority refeference6;

		private NHibernateHelper()
		{
		}
		private static bool IsWeb
		{
			get
			{
				return (HttpContext.Current != null);
			}
		}

		private static Configuration CurrentConfiguration
		{
			get
			{
				if (IsWeb == false)
				{
					return GetOrCreateMemoryConfiguration(ref _config);
				}
				else
				{
					// running inside of an HttpContext (web mode)
					// the nhibernate session is a singleton to the http request
					Dictionary<string, Configuration> config = GetStaticWebDictionary<Configuration>(KEY_NHIBERNATE_CONFIG);
					string currentDb = "";

					Configuration ret;
					if (config.TryGetValue(currentDb, out ret) == false)
					{
						ret = CreateConfiguration();
						config.Add(currentDb, ret);
						HttpContext.Current.Application[KEY_NHIBERNATE_CONFIG] = config;
					}
					return ret;
				}
			}
		}

		private static Configuration GetOrCreateMemoryConfiguration(ref Configuration configuration)
		{
			if (configuration == null) configuration = CreateConfiguration();
			return configuration;
		}

		private static Dictionary<string, T> GetStaticWebDictionary<T>(string key)
		{
			HttpContext currentContext = HttpContext.Current;
			Dictionary<string, T> dictionary = currentContext.Application[key] as Dictionary<string, T>;
			if (dictionary == null)
			{
				dictionary = new Dictionary<string, T>();
				currentContext.Application[key] = dictionary;
			}
			return dictionary;
		}

		internal static ISessionFactory CurrentFactory
		{
			get
			{
				if (IsWeb == false)
				{
					// running without an HttpContext (non-web mode)
					// the nhibernate session is a singleton in the app domain
					return GetOrCreateFactory(ref _factory);
				}
				else
				{
					// running inside of an HttpContext (web mode)
					// the nhibernate session is a singleton to the http request
					HttpContext currentContext = HttpContext.Current;
					Dictionary<string, ISessionFactory> factories = GetStaticWebDictionary<ISessionFactory>(KEY_NHIBERNATE_FACTORY);
					ISessionFactory ret;
					string currentDb = "current";
					if (factories.TryGetValue(currentDb, out ret) == false)
					{
						ret = CreateSessionFactory();
						string cacheRegion = GetCacheRegion(CurrentConfiguration);
						if (string.IsNullOrEmpty(cacheRegion))
							ClearCache(ret);
						else
							ClearCache(ret, cacheRegion);
						factories.Add(currentDb, ret);
						currentContext.Application[KEY_NHIBERNATE_FACTORY] = factories;
					}
					return ret;
				}
			}
		}

		private static ISessionFactory GetOrCreateFactory(ref ISessionFactory factory)
		{
			if (factory == null)
			{
				factory = CreateSessionFactory();
				ClearCache(factory, GetCacheRegion(CurrentConfiguration));
			}
			return factory;
		}

		private static bool UseHibernateConfigurationSection
		{
			get
			{
				object config = GetConfigSection();
				if (config == null)
					return false;
				else
					return IsHibernateConfigurationSection(config);
			}
		}
		public static void ClearCache(ISessionFactory factory)
		{
			ClearCache(factory, null);
		}
		public static void ClearCache(ISessionFactory factory, string region)
		{
			// Libera los tres bloques...
			// TODO: MIGRAR
			/*
            ICollection types = factory.GetAllClassMetadata().Keys;
            foreach (Type type in types)
                factory.Evict(type);*/
			foreach (string role in factory.GetAllCollectionMetadata().Keys)
				factory.EvictCollection(role);
			if (string.IsNullOrEmpty(region))
				factory.EvictQueries();
			else
				factory.EvictQueries(region);
		}

		public static NHibernateSession CreateNHibernateSession()
		{
			return CreateNHibernateSession(false);
		}
		private static NHibernateSession CreateNHibernateSession(bool stateless)
		{
			ISessionFactory factory;
			NHibernateSession session;
			// Accede al factory de nhibernate
			factory = CurrentFactory;

			// Crea la sessión de nhibernate
			if (stateless == false)
				session = new NHibernateSession(factory.OpenSession(), null);
			else
				session = new NHibernateSession(null, factory.OpenStatelessSession());

			if (session == null)
			{
				string msg = ("NHibernateCouldNotCreateSessionException");
				throw new Exception(msg);
			}
			else
				return session;
		}

		public static NHibernateSession CreateSession()
		{
			// Crea la sesión de nhibernate
			NHibernateSession nHibernateSession = CreateNHibernateSession(false);
			// Crea la enfokeSession
			Configuration config = CurrentConfiguration;
			nHibernateSession.Configuration = config;
			NHibernateSession retSession = nHibernateSession;
			retSession.DatabaseType = GetDatabaseType(config);
			retSession.CommandTimeout = resolveTimeout();
			retSession.SessionFactory = CurrentFactory;
			// Listo
			return retSession;
		}

		public static string GetSQLConnString()
		{
			return CurrentConfiguration.Properties[
						NHibernate.Cfg.Environment.ConnectionString] as string;
		}

		protected static Configuration CreateConfiguration()
		{
			//
			Configuration config;
			config = new Configuration();
			config.AddAuxiliaryDatabaseObject(new SpatialAuxiliaryDatabaseObject(config));

			if (UseHibernateConfigurationSection)
				config.Configure();
			// Se fija si tiene que agregar seteos extra
			SetDefaultConfigSettings(config);

			// Listo
			foreach (string assembly in GetEntityAssemblies())
			{
				Assembly ass = Assembly.Load(assembly);
				if (UseHibernateConfigurationSection == false)
					config.AddAssembly(assembly);
			}
			config.AddAssembly("medea.data");

			return config;
		}

		protected static ISessionFactory CreateSessionFactory()
		{
			ISessionFactory factory = CurrentConfiguration.BuildSessionFactory();
			if (factory == null)
			{
				string msg = "NHibernateCouldNotCreateFactoryException";
				throw new Exception(msg);
			}
			return factory;
		}

		private static ConnectionSettings BuildConnectionString(DatabaseTypeEnum databaseType)
		{
			string connectionString;
			string server = ConfigurationGet("ServerName", "");
			if (server == "") server = ConfigurationGet("Server", "");

			string databaseOrService = ConfigurationGet("DatabaseName", "");
			if (databaseOrService == "") databaseOrService = ConfigurationGet("Database", "");
			if (databaseOrService == "") databaseOrService = ConfigurationGet("Service", "");
			if (databaseOrService == "") databaseOrService = ConfigurationGet("ServiceName", "");
			if (server == "" || databaseOrService == "")
				return null;
			string user = ConfigurationGet("User", "");
			if (user == "")
				user = ConfigurationGet("DBUser", "");

			string password = ConfigurationGet("PasswordPlain", null);
			if (password == null)
				password = ConfigurationGet("DBPasswordPlain", null);
			if (password == null) password = "";

			string port = ConfigurationGet("DatabasePort", "1521");
			string ssl = ConfigurationGet("SslMode", "Required");

			if (databaseType == DatabaseTypeEnum.SqlServer)
			// sql
			{
				if (string.IsNullOrEmpty(user))
				{
					connectionString = string.Format("Server={0};initial catalog={1};Trusted_Connection=True;",
						server, databaseOrService);
				}
				else
				{
					connectionString = string.Format("Server={0};initial catalog={1};User Id={2};Password={3};",
						server, databaseOrService, user, password);
				}
			}
			else if (databaseType == DatabaseTypeEnum.MySql)
			{
							connectionString = string.Format("Data Source={0};Database={1};User Id={2};Password={3};SslMode={4};UseCompression=True",
						server, databaseOrService, user, password, ssl);
}
			else if (databaseType == DatabaseTypeEnum.Oracle) // oracle
			{
				connectionString = string.Format("Data Source = (DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = {0})(PORT = {4})))(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = {1}))); User Id = {2}; Password = {3};",
						server, databaseOrService, user, password, port);
			}
			else if (databaseType == DatabaseTypeEnum.SQLite)
			{
				connectionString = string.Format("Data Source={0}", databaseOrService);
				// Configura
				databaseOrService = connectionString;
				server = "SQLite";
				user = "anonymous";
				port = "";
			}
			else
				throw new Exception("Unsupported database type '" + databaseType.ToString() + "'.");

			ConnectionSettings ret = new ConnectionSettings();
			ret.Database = databaseOrService;
			ret.User = user;
			ret.Server = server;
			ret.ConnectionString = connectionString;
			ret.Port = port;
			return ret;
		}

		private static string GetConfig(string key)
		{
			object config = GetConfigSection();
			if (config == null)
				return null;
			if (config is NameValueCollection)
			{
				NameValueCollection properties = config as NameValueCollection;
				if (properties == null)
				{
					string msg = "ConfigurationSectionException";
					throw new Exception(msg);
				}
				string ret = properties[key];
				if (string.IsNullOrEmpty(ret))
					return null;
				else
					return ret;
			}
			else if (config is System.Xml.XmlElement)
			{
				System.Xml.XmlElement section = (System.Xml.XmlElement)config;
				System.Xml.XmlNode sessionfactory = section["session-factory"];
				foreach (System.Xml.XmlNode child in sessionfactory.ChildNodes)
				{
					if (child.Attributes != null && child.Attributes["name"] != null)
					{
						if (child.Attributes["name"].Value == key ||
							"nhibernate." + child.Attributes["name"].Value == key)
						{
							if (child.Attributes["value"] == null)
								return child.Value;
							else
								return child.Attributes["value"].Value;
						}
					}
				}
				return null;
			}
			else
				return null;
		}

		private static object GetConfigSection()
		{
			object config = System.Configuration.ConfigurationManager.GetSection("nhibernate");
			if (config == null)
				config = System.Configuration.ConfigurationManager.GetSection("hibernate-configuration");
			if (config == null)
				return null;
			else
				return config;
		}

		private static DatabaseTypeEnum GetDatabaseType(Configuration config)
		{
			// valores... MsSql2005Dialect, MsSql2000Dialect
			string ret = config.Properties[NHibernate.Cfg.Environment.Dialect] as string;
			if (string.IsNullOrEmpty(ret))
				return DatabaseTypeEnum.Undefined;
			if (ret.IndexOf("MsSql", StringComparison.OrdinalIgnoreCase) != -1)
				return DatabaseTypeEnum.SqlServer;
			else if (ret.IndexOf("Oracle", StringComparison.OrdinalIgnoreCase) != -1)
				return DatabaseTypeEnum.Oracle;
			else if (ret.IndexOf("SQLite", StringComparison.OrdinalIgnoreCase) != -1)
				return DatabaseTypeEnum.SQLite;
			else
				return DatabaseTypeEnum.Undefined;
		}

		private static string[] GetEntityAssemblies()
		{
			string ret;
			if (UseHibernateConfigurationSection)
				ret = GetMapping();
			else
				ret = GetConfig(KEY_NHIBERNATE_ASSEMBLY_ENTITIES);
			if (string.IsNullOrEmpty(ret))
				ret = ConfigurationGet(KEY_NHIBERNATE_ASSEMBLY_ENTITIES);
			if (string.IsNullOrEmpty(ret))
				ret = ConfigurationGet(KEY_ENTITIES);

			if (string.IsNullOrEmpty(ret) || context.Data.DisableMappingLoad == true)
				return new string[0];
			else
				return ret.Split(',');
		}

		private static string GetHibernateConfigurationConnectionString()
		{
			object config = GetConfigSection();
			if (config == null)
				return null;
			System.Xml.XmlElement section = (System.Xml.XmlElement)config;
			System.Xml.XmlNode sessionfactory = section["session-factory"];
			string ret = "";
			foreach (System.Xml.XmlNode child in sessionfactory.ChildNodes)
			{
				if (child.Name == "property")
				{
					if (child.Attributes["name"] != null && child.Attributes["name"].Value == "connection.connection_string")
						return child.InnerText;
				}
			}
			return ret;
		}

		private static string GetMapping()
		{
			object config = GetConfigSection();
			if (config == null)
				return null;
			System.Xml.XmlElement section = (System.Xml.XmlElement)config;
			System.Xml.XmlNode sessionfactory = section["session-factory"];
			string ret = "";
			foreach (System.Xml.XmlNode child in sessionfactory.ChildNodes)
			{
				if (child.Name == "mapping")
				{
					if (ret != "") ret += ",";
					if (child.Attributes["assembly"] == null)
						ret += child.Value;
					else
						ret += child.Attributes["assembly"].Value;
				}
			}
			return ret;
		}

		private static bool IsHibernateConfigurationSection(object config)
		{
			if (config is NameValueCollection)
				return false;
			else if (config is System.Xml.XmlElement)
				return true;
			else
			{
				string msg = "ConfigurationSectionException";
				throw new Exception(msg);
			}
		}

		private static void SetDefault(Configuration config,
			string key, object value)
		{
			if (config.Properties.ContainsKey(key) == false ||
				config.Properties[key] == null)
			{
				config.Properties[key] = value.ToString();
			}
		}

		private static void SetDefaultConfigSettings(Configuration config)
		{
			// Determina el dialect y driver_class
			string dbType;
			DatabaseTypeEnum databaseType;
			string driverClass;
			string dialect;

			dbType = ConfigurationGet("DatabaseType", "Sql2000");
			if (dbType.Equals("SQLite", StringComparison.OrdinalIgnoreCase))
			{
				driverClass = "NHibernate.Driver.SQLite20Driver";
				dialect = "NHibernate.Dialect.SQLiteDialect";
				databaseType = DatabaseTypeEnum.SQLite;
			}
			else if (dbType.Equals("Sql2000", StringComparison.OrdinalIgnoreCase))
			{
				driverClass = "NHibernate.Driver.SqlClientDriver";
				dialect = "NHibernate.Dialect.MsSql2000Dialect";
				databaseType = DatabaseTypeEnum.SqlServer;
			}
			else if (dbType.Equals("Sql2005", StringComparison.OrdinalIgnoreCase))
			{
				driverClass = "NHibernate.Driver.SqlClientDriver";
				dialect = "NHibernate.Dialect.MsSql2005Dialect";
				databaseType = DatabaseTypeEnum.SqlServer;
			}
			else if (dbType.Equals("Sql2008", StringComparison.OrdinalIgnoreCase))
			{
				driverClass = "NHibernate.Driver.SqlClientDriver";
				dialect = "NHibernate.Dialect.MsSql2008Dialect";
				databaseType = DatabaseTypeEnum.SqlServer;
			}
			else if (dbType.Equals("Oracle", StringComparison.OrdinalIgnoreCase))
			{
				// MS
				driverClass = "NHibernate.Driver.OracleClientDriver";
				// ORACLE
				dialect = "NHibernate.Dialect.Oracle9iDialect";
				databaseType = DatabaseTypeEnum.Oracle;
			}
			else if (dbType.Equals("MySql", StringComparison.OrdinalIgnoreCase))
			{
				driverClass = "NHibernate.Driver.MySqlDataDriver";
				dialect = "NHibernate.Dialect.MySQLDialect";
				dialect = "NHibernate.Spatial.Dialect.MySQLSpatialDialect,NHibernate.Spatial.MySQL";
				databaseType = DatabaseTypeEnum.MySql;
			}
			else throw new Exception("Valor incorrecto en DatabaseType.");

			SetDefault(config, NHibernate.Cfg.Environment.ConnectionDriver,
								driverClass);
			SetDefault(config, NHibernate.Cfg.Environment.Dialect, dialect);
			// Determina el ConnectionString
			ConnectionSettings connectionStringSettings = BuildConnectionString(databaseType);
			if (connectionStringSettings != null &&
				connectionStringSettings.ConnectionString != null)
			{
				SetDefault(config, NHibernate.Cfg.Environment.ConnectionString, connectionStringSettings.ConnectionString);
				SetDefault(config, KEY_DB_USER, connectionStringSettings.User);
				SetDefault(config, KEY_DB_NAME, connectionStringSettings.Database);
				SetDefault(config, KEY_DB_PORT, connectionStringSettings.Port);
				SetDefault(config, KEY_DB_SERVER, connectionStringSettings.Server);
				ConnectionSettings = connectionStringSettings;
			}
			// <!-- General configuration -->
			SetDefault(config, NHibernate.Cfg.Environment.ShowSql, "true");
			SetDefault(config, NHibernate.Cfg.Environment.BatchSize, 100);
			//SetDefault(config, NHibernate.Cfg.Environment.ReleaseConnections, "on_close");

			string timeout = resolveTimeout().ToString();
			SetDefault(config, NHibernate.Cfg.Environment.CommandTimeout, timeout);
			// Sustitutions
			string substitutions = "true 1, false 0";
			SetDefault(config, NHibernate.Cfg.Environment.QuerySubstitutions, substitutions);
			// <!-- Cache configuration -->

			if (ConfigurationGetBool("DisableCache", false) == false &&
				context.Data.DisableCache == false)
			{
				string cacheProvider;
				if (HttpContext.Current == null)
				{
					bool cacheInProcess = ConfigurationGetBool("CacheInProcess", true);
					if (cacheInProcess)
						// https://github.com/diegose/NHibernate.Diegose/blob/master/Caches/RtMemoryCache.Tests/App.config
						cacheProvider = "NHibernate.Caches.RtMemoryCache.RtMemoryCacheProvider,NHibernate.Caches.RtMemoryCache";
					else
						cacheProvider = "NHibernate.Caches.MemCache.MemCacheProvider,NHibernate.Caches.MemCache";
				}
				else
					cacheProvider = "NHibernate.Caches.SysCache2.SysCacheProvider, NHibernate.Caches.SysCache2";
				SetDefault(config, NHibernate.Cfg.Environment.CacheProvider, cacheProvider);
				SetDefault(config, NHibernate.Cfg.Environment.UseQueryCache, true);
				string expiration = ConfigurationGet("expiration", "3600");
				SetDefault(config, "expiration", expiration);
			}
		}

		private static int resolveTimeout()
		{
			return ConfigurationGetInt("CommandTimeout", 600);
		}
		private static int ConfigurationGetInt(string key, int defaultValue = 0)
		{
			string cad = ConfigurationGet(key, null);
			if (cad == null) return defaultValue;
			return int.Parse(cad);
		}
		private static bool ConfigurationGetBool(string key, bool defaultValue = false)
		{
			string cad = ConfigurationGet(key, null);
			if (cad == null) return defaultValue;
			return (cad == "true" || cad == "enabled" || cad == "1");
		}
		internal static string ConfigurationGet(string key, string defaultValue = null)
		{
			if (NHibernateSession.DbSettings.ContainsKey(key) == false)
				return defaultValue;
			string ret = NHibernateSession.DbSettings[key];
			if (string.IsNullOrEmpty(ret))
				return defaultValue;
			else
				return ret;
		}

		private static bool existsAssembly(string assemblyName)
		{
			string filePath = typeof(NHibernateHelper).Assembly.CodeBase;
			if (filePath == null) filePath = typeof(NHibernateHelper).Assembly.Location;
			if (filePath.StartsWith("file:///")) filePath = filePath.Substring(8);
			filePath = filePath.Replace("/", "\\");
			string path = Path.GetDirectoryName(filePath);
			return System.IO.File.Exists(path + "\\" + assemblyName + ".dll");
		}

		private static string format(DateTime start)
		{
			return start.ToString("dd/mm/yyyy hh:mm:ss.fff");
		}

		private void useReferecesMethod()
		{
			refeference1 = refeference1 = null; refeference2 = refeference2 = null; refeference3 = refeference3 = null;
			refeference4 = refeference4 = null; refeference5 = refeference5 = null; refeference6 = refeference6 = Microsoft.Practices.EnterpriseLibrary.Caching.CacheItemPriority.None;
		}

		private static string GetCacheRegion(Configuration config)
		{
			return "R:" + GetStr(config.Properties[KEY_DB_SERVER]) + "@" +
				GetStr(config.Properties[KEY_DB_NAME]) + "//" +
				GetStr(config.Properties[KEY_DB_USER]) + "@" +
				GetStr(config.Properties[KEY_DB_PORT]);
		}

		private static object GetStr(object p)
		{
			string s = p as string;
			if (s == null) return "";
			else
				return s;
		}
	}
}
