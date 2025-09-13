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
using System.Web;

using medea.Data;

namespace medea.context
{
	/// <summary>
	/// Permite acceder a elementos del contexto de ejecución referido a la persistencia y acceso a bases de datos (NHibernateSessiones de conexión, transacionalidad, etc).
	/// </summary>
	public class Data
	{
		const string KEY_HTTP_MODULE_TAG = "NHibernateNHibernateSessionTag";
		internal const string KEY_HTTP_MODULE_DBNAME = "NHibernateNHibernateSessionDb";
		private static bool IsStarted
		{
			get
			{
				HttpContext context = HttpContext.Current;
				if (context != null)
				{
					if (context.Items[KEY_HTTP_MODULE_TAG] == null)
						return false;
				}
				else if (ThreadModuleStarted == false)
					return false;
				return true;
			}
		}
		public static bool DisableCache = false;
		public static bool DisableMappingLoad = false;

		private static NHibernateSession nHibernateSession;

		[ThreadStatic]
		private static bool ThreadModuleStarted;

		private Data()
		{
		}

		private const string KEY_CURRENT_DATABASE_NHibernateSession = "dbsIn";

		/// <summary>
		/// Informa si existe una transacción activa.
		/// </summary>

		/// <summary>
		/// Da acceso a la interfaz hacia los origenes de datos relacionales y de persistencia de objetos de la aplicación.
		/// </summary>

		public static NHibernateSession Session
		{
			get
			{
				if (nHibernateSession == null)
					nHibernateSession = NHibernateHelper.CreateSession();

				CheckSession();

				return nHibernateSession;
			}
		}

		/// <summary>
		/// Finaliza el contexto de ejecución. Al recibir el pedido de finalización, el contexto de persistencia almancena la información de performance y cierra las conexiones y tranascciones que puedan estar abiertas.
		/// </summary>
		public static void End()
		{
			// Cierra contexto
			FinalizeDataContext();
		}

		public static int GetSettingInt(string key, int defaultValue = 0)
		{
			return int.Parse(GetSetting(key, defaultValue.ToString()));
		}
		public static string GetSetting(string key, string defaultValue = null)
		{
			return NHibernateHelper.ConfigurationGet(key, defaultValue);
		}
		internal static void FinalizeDataContext()
		{
			if (!IsStarted)
				throw new Exception("The database context is not started.");

			HttpContext context = HttpContext.Current;
			if (context != null)
			{
				context.Items[NHibernateHelper.KEY_NHIBERNATE_CONFIG] = null;
				context.Items[KEY_HTTP_MODULE_TAG] = null;
			}
			else
				ThreadModuleStarted = false;
		}


		/// <summary>
		/// Inicia el contexto de ejecución. Al hacerse Start
		/// en el contexto de ejecución, se inicializan los medidores
		/// de performance.
		/// </summary>
		public static void Start()
		{
			BeginDataContext(null);
		}
		/// <summary>
		/// Inicia el contexto de ejecución. Al hacerse Start
		/// en el contexto de ejecución, se inicializan los medidores
		/// de performance.
		/// </summary>
		public static void Start(string databaseName)
		{
			BeginDataContext(databaseName);
		}

		internal static void BeginDataContext(string databaseName)
		{
			if (IsStarted)
				FinalizeDataContext();
			// Marca la sesión
			HttpContext context = HttpContext.Current;
			if (context != null)
			{
				context.Items[KEY_HTTP_MODULE_TAG] = true;
				if (databaseName != null)
					context.Items[KEY_HTTP_MODULE_DBNAME] = databaseName;
			}
			else
				ThreadModuleStarted = true;
		}
		private static DateTime LastSessionUsage = DateTime.MinValue;

		public static void MarkSessionUsage()
		{
			LastSessionUsage = DateTime.Now;

			try
			{
				// Session.SqlActions.ExecuteScalar("SET GLOBAL wait_timeout = 28800;");
			}
			catch { }
		}
		public static void CheckSession()
		{
			try
			{
				if ((DateTime.Now - LastSessionUsage).TotalSeconds >= 120)
				{
					MarkSessionUsage();
					MySql.Data.MySqlClient.MySqlConnection c = Session.Connection as MySql.Data.MySqlClient.MySqlConnection;
					if (c != null)
						c.ClearAllPoolsAsync();
					Session.SqlActions.ExecuteNonQuery("select 1");
				}
			}
			catch
			{ }
			finally
			{
				MarkSessionUsage();
			}
		}

		public static void SafeEvict(object o)
		{
			if (Session.Contains(o))
				try
				{
					Session.Evict(o);
				}
				catch { }
		}
	}
}
