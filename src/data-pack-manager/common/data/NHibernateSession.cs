/*
*    Poblaciones - Plataforma abierta de datos espaciales de poblaci√≥n.
*    Copyright (C) 2018-2019. Consejo Nacional de Investigaciones Cient√≠ficas y T√©cnicas (CONICET)
*		 y Universidad Cat√≥lica Argentina (UCA).
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
using System.Collections;
using System.Data;

using NHibernate;
using NHibernate.Linq;
using System.Linq;

using medea.common;
using System.Collections.Generic;
using System.Linq.Expressions;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

namespace medea.Data
{
	public class NHibernateSession : IDisposable
	{
		public static Dictionary<string, string> DbSettings = new Dictionary<string, string>();
		ISession _wrap;
		IStatelessSession _wrapStateless;
		bool _isStateless;
		public DatabaseTypeEnum DatabaseType;
		public NHibernate.Cfg.Configuration Configuration;
		public ISessionFactory SessionFactory;
		private SqlActions _sessionSqlActions;
		public SqlActions SqlActions { get { return _sessionSqlActions; } }

		public NHibernateSession(ISession wrap, IStatelessSession wrapStateless)
		{
			_wrap = wrap;
			_sessionSqlActions = new SqlActions(this);
			_wrapStateless = wrapStateless;
			if (_wrapStateless != null)
				_isStateless = true;
		}
		public ConnectionSettings ConnectionSettings
		{
			get
			{
				return NHibernateHelper.ConnectionSettings;
			}
		}
		public ITransaction Transaction
		{
			get
			{
				if (_isStateless)
					return _wrapStateless.Transaction;
				else
					return _wrap.Transaction;
			}
		}
		public IDbConnection Connection
		{
			get
			{
				if (_isStateless)
					return _wrapStateless.Connection;
				else
					return _wrap.Connection;
			}
		}

		public void Delete(object obj)
		{
			if (_isStateless)
				_wrapStateless.Delete(obj);
			else
				_wrap.Delete(obj);
		}

		public void Flush()
		{
			if (_isStateless == false)
				_wrap.Flush();
		}

		public object Get(Type theType, object id, LockMode lockMode)
		{
			if (_isStateless)
				return _wrapStateless.Get(theType.FullName, id, lockMode);
			else
				return _wrap.Get(theType, id, lockMode);
		}


		public T Load<T>(int id) where T : IIdentifiable
		{
			if (_isStateless)
				return _wrapStateless.Get<T>(id);
			else
				return _wrap.Load<T>(id);
		}

		public object Load(Type theType, object id)
		{
			if (_isStateless)
				return _wrapStateless.Get(theType.FullName, id);
			else
				return _wrap.Load(theType, id);
		}

		public void Lock(object obj, LockMode lockMode)
		{
			if (_isStateless == false)
				_wrap.Lock(obj, lockMode);
		}
		private ITransaction _transaction;
		public void BeginTransaction()
		{
			if (_transaction != null)
				throw new Exception("Transaction pending.");

			if (_isStateless)
				_transaction = _wrapStateless.BeginTransaction();
			else
			{
				if (_wrap.Connection.State == ConnectionState.Closed)
					_wrap.Connection.Open();

				_transaction = _wrap.BeginTransaction();
			}
		}
		public void Commit()
		{
			if (_transaction == null)
				throw new Exception("No transaction.");
			_transaction.Commit();
			_transaction = null;
			context.Data.MarkSessionUsage();
		}
		public void Rollback()
		{
			if (_transaction == null)
				throw new Exception("No transaction.");
			var tr = _transaction;
			_transaction = null;
			context.Data.MarkSessionUsage();
			tr.Rollback();
		}
		public bool IsInTransaction
		{
			get
			{
				return (_transaction != null);
			}
		}

		public void SaveOrUpdate(object obj)
		{
			if (obj == null) return;
			if (_isStateless)
				throw new NotSupportedException("Stateless no soporta SaveOrUpdate.");
			else
				_wrap.SaveOrUpdate(obj);
		}

		public IQuery CreateQuery(string query)
		{
			if (_isStateless)
				return _wrapStateless.CreateQuery(query);
			else
				return _wrap.CreateQuery(query);
		}

		public FlushMode FlushMode
		{
			get
			{
				if (_isStateless)
					return FlushMode.Always;
				else
					return _wrap.FlushMode;
			}
			set
			{
				if (_isStateless == false)
					_wrap.FlushMode = value;
			}
		}

		public ISQLQuery CreateSQLQuery(string query)
		{
			if (_isStateless)
				return _wrapStateless.CreateSQLQuery(query);
			else
				return _wrap.CreateSQLQuery(query);
		}

		public bool IsConnected
		{
			get
			{
				if (_isStateless)
					return true;
				else
					return _wrap.IsConnected;
			}
		}

		public bool Contains(object obj)
		{
			if (_isStateless)
				return false;
			else
				return _wrap.Contains(obj);
		}

		public ICriteria CreateCriteria(Type persistentClass, string alias)
		{
			if (_isStateless)
				return _wrapStateless.CreateCriteria(persistentClass, alias);
			else
				return _wrap.CreateCriteria(persistentClass, alias);
		}

		public void DisableFilter(string filterName)
		{
			if (_isStateless == false)
				_wrap.DisableFilter(filterName);
		}

		public void Evict(object obj)
		{
			if (_isStateless == false)
				_wrap.Evict(obj);
		}

		public IQuery GetNamedQuery(string name)
		{
			if (_isStateless)
				return _wrapStateless.GetNamedQuery(name);
			else
				return _wrap.GetNamedQuery(name);
		}

		public void Refresh(object obj)
		{
			if (_isStateless)
				_wrapStateless.Refresh(obj);
			else
				_wrap.Refresh(obj);
		}

		public object Load(Type theType, object id, LockMode lockMode)
		{
			if (_isStateless)
				return _wrapStateless.Get(theType.FullName, id, lockMode);
			else
				return _wrap.Load(theType, id, lockMode);
		}

		public object Save(object obj)
		{
			if (_isStateless)
				return _wrapStateless.Insert(obj);
			else
				return _wrap.Save(obj);
		}

		public void SaveOrUpdateCopy(object obj)
		{
			if (_isStateless)
				_wrapStateless.Insert(obj);
			else
				_wrap.Merge(obj);
		}

		public void Update(object obj)
		{
			if (_isStateless)
				_wrapStateless.Update(obj);
			else
				_wrap.Update(obj);
		}

		public System.Linq.IQueryable<T> Query<T>()
		{
			if (_isStateless)
				throw new NotImplementedException();
			else
				return _wrap.Query<T>();
		}

		public IList Query(Type t)
		{
			if (_isStateless)
				throw new NotImplementedException();
			else
			{
				var q = _wrap.CreateQuery("from " + t.Name + " c");
				return q.List();
			}
		}

		public IList NullQuery(Type t)
		{
			if (_isStateless)
				throw new NotImplementedException();
			else
			{
				var q = _wrap.CreateQuery("from " + t.Name + " c where 1 = 0");
				return q.List();
			}
		}

		public void Clear()
		{
			if (_isStateless == false)
				_wrap.Clear();
		}

		public IFilter EnableFilter(string filterName)
		{
			throw new NotImplementedException();
		}

		public object GetIdentifier(object obj)
		{
			if (_isStateless)
				return null;
			else
				return _wrap.GetIdentifier(obj);
		}

		public void Close()
		{
			if (_isStateless)
				_wrapStateless.Connection.Close();
			else
				_wrap.Close();
		}

		public bool IsDirty()
		{
			if (_isStateless == false)
				return _wrap.IsDirty();
			else
				return false;
		}

		public LockMode GetCurrentLockMode(object obj)
		{
			if (_isStateless)
				return LockMode.None;
			else
				return GetCurrentLockMode(obj);
		}

		public void Dispose()
		{
			if (_isStateless)
				_wrapStateless.Dispose();
			else
				_wrap.Dispose();
		}

		public IQuery CreateFilter(object collection, string query)
		{
			if (_isStateless)
				throw new NotImplementedException();
			else
				return _wrap.CreateFilter(collection, query);
		}

		public ICriteria CreateCriteria(Type persistentClass)
		{
			if (_isStateless)
				return _wrapStateless.CreateCriteria(persistentClass);
			else
				return _wrap.CreateCriteria(persistentClass);
		}

		public T GetById<T>(int id) where T : IIdentifiable
		{
			T ret = Get<T>(id);
			if (ret == null)
				return Query<T>().Where(x => x.Id == id).FirstOrDefault();
			else
				return ret;
		}
		public T Get<T>(int id) where T : IIdentifiable
		{
			if (_isStateless)
				return _wrapStateless.Get<T>(id);
			else
				return _wrap.Get<T>(id);
		}

		/// <summary>
		/// Trae los datos de campos Geometry de mysql tipados como Ìdem en un diccionario.
		/// </summary>
		/// <typeparam name="T">El tipo a consultar.</typeparam>
		/// <param name="ids">Lista de ids de los campos a buscar.</param>
		/// <param name="selectField">Opcional. El campo a consultar. Si no se pasa el par·metro trae el campo tipo Geometry de la tabla, si es que hay uno.</param>
		/// <returns>Diccionario con ids y Geometries de NetTopology.</returns>
		public Dictionary<int, Geometry> GetGeometries<T>(List<int> ids, Expression<Func<T, object>> selectField = null)
			where T : ActiveBaseEntity<T>, IIdentifiable
		{
			var fieldName = "";
			if (selectField == null)
				fieldName = context.Metadata<T>.GetGeometryColumn();
			else
				fieldName = context.Metadata<T>.GetColumn(selectField);

			//TODO: Si ids.Count es muy grande hacer un loop de queries.
			var sql = "SELECT " + context.Metadata<T>.GetColumn(x => x.Id)
				+ ", " + fieldName
				+ " FROM " + context.Metadata<T>.TableName
				+ " WHERE " + context.Metadata<T>.KeyColumn + " IN (" + string.Join(",", ids).TrimEnd(',') + ")";

			return GetGeometries<T>(sql);
		}

		/// <summary>
		/// Trae los datos de campos Geometry de mysql tipados como Ìdem en un diccionario.
		/// </summary>
		/// <typeparam name="T">El tipo a consultar.</typeparam>
		/// <param name="whereField">El campo para hacer el where.</param>
		/// <param name="whereValue">El valor del where.</param>
		/// <param name="selectField">Opcional. El campo a consultar. Si no se pasa el par·metro trae el campo tipo Geometry de la tabla, si es que hay uno.</param>
		/// <returns>Diccionario con ids y Geometries de NetTopology.</returns>
		public Dictionary<int, Geometry> GetGeometries<T>(Expression<Func<T, object>> whereField, object whereValue, Expression<Func<T, object>> selectField, bool skipNullValues = false)
			where T : ActiveBaseEntity<T>, IIdentifiable
		{
			var fieldName = "";
			if (selectField == null)
				fieldName = context.Metadata<T>.GetGeometryColumn();
			else
				fieldName = context.Metadata<T>.GetColumn(selectField);

			var sql = "SELECT " + context.Metadata<T>.GetColumn(x => x.Id)
				+ ", " + fieldName
				+ " FROM " + context.Metadata<T>.TableName + " WHERE 1 ";
			if (whereField != null)
				sql += " AND " + context.Metadata<T>.GetColumn(whereField) + " = " + whereValue.ToString();
			if (skipNullValues)
				sql += " AND " + fieldName + " IS NOT NULL";
			return GetGeometries<T>(sql);
		}

		private Dictionary<int, Geometry> GetGeometries<T>(string sql)
				where T : ActiveBaseEntity<T>, IIdentifiable
		{
			var ds = SqlActions.ExecuteDataSet(sql, true);
			var geometries = new Dictionary<int, Geometry>();

			WKBReader reader = new WKBReader();
			foreach(DataRow dr in ds.Tables[0].Rows)
			{
				var bytes = dr[1] as byte[];
				var trimmed = new byte[bytes.Length - 4];
				Array.Copy(bytes, 4, trimmed, 0, trimmed.Length);

				geometries.Add((int) dr[0], (Geometry)reader.Read(trimmed));
			}
			return geometries;

			/*
			using (var rows = SqlActions.ExecuteReader(sql, true))
			{
				var geometries = new Dictionary<int, Geometry>();

				WKBReader reader = new WKBReader();
				while (rows.Read())
				{
					var bytes = rows.GetValue(1) as byte[];
					var trimmed = new byte[bytes.Length - 4];
					Array.Copy(bytes, 4, trimmed, 0, trimmed.Length);

					geometries.Add(rows.GetInt32(0), (Geometry)reader.Read(trimmed));
				}
				return geometries;
			}*/
		}
		public Geometry GetGeometry<T>(int id, Expression<Func<T, object>> field = null)
			where T : ActiveBaseEntity<T>, IIdentifiable
		{
			List<int> ids = new List<int>();
			ids.Add(id);
			var res = GetGeometries(ids, field);
			if (res.Count == 1)
				return res.First().Value;
			return null;
		}


		public Point GetPoint<T>(int id, Expression<Func<T, object>> field = null)
			where T : ActiveBaseEntity<T>, IIdentifiable
		{
			using (new WaitCursor())
			{
				List<int> ids = new List<int>();
				ids.Add(id);
				var res = GetPoints(ids, field);
				if (res.Count == 1)
					return res.First().Value;
				return null;
			}
		}

		/// <summary>
		/// Trae los datos de campos Point de mysql tipados como Ìdem en un diccionario.
		/// </summary>
		/// <typeparam name="T">El tipo a consultar.</typeparam>
		/// <param name="ids">Lista de ids de los campos a buscar.</param>
		/// <param name="selectField">Opcional. El campo a consultar. Si no se pasa el par·metro trae el campo tipo Point de la tabla, si es que hay uno.</param>
		/// <returns>Diccionario con ids y Points de NetTopology.</returns>
		public Dictionary<int, Point> GetPoints<T>(List<int> ids, Expression<Func<T, object>> selectField = null)
			where T : ActiveBaseEntity<T>, IIdentifiable
		{
			var fieldName = "";
			if (selectField == null)
				fieldName = context.Metadata<T>.GetPointColumn();
			else
				fieldName = context.Metadata<T>.GetColumn(selectField);

			//TODO: Si ids.Count es muy grande hacer un loop de queries.
			var sql = "SELECT " + context.Metadata<T>.GetColumn(x => x.Id)
				+ ", " + fieldName
				+ " FROM " + context.Metadata<T>.TableName
				+ " WHERE " + context.Metadata<T>.KeyColumn + " IN (" + string.Join(",", ids).TrimEnd(',') + ")";

			return GetPoints<T>(sql);
		}

		/// <summary>
		/// Trae los datos de campos Point de mysql tipados como Ìdem en un diccionario.
		/// </summary>
		/// <typeparam name="T">El tipo a consultar.</typeparam>
		/// <param name="whereField">El campo para hacer el where.</param>
		/// <param name="whereValue">El valor del where.</param>
		/// <param name="selectField">Opcional. El campo a consultar. Si no se pasa el par·metro trae el campo tipo Point de la tabla, si es que hay uno.</param>
		/// <returns>Diccionario con ids y Points de NetTopology.</returns>
		public Dictionary<int, Point> GetPoints<T>(Expression<Func<T, object>> whereField, object whereValue, Expression<Func<T, object>> selectField = null)
			where T : ActiveBaseEntity<T>, IIdentifiable
		{
			var fieldName = "";
			if (selectField == null)
				fieldName = context.Metadata<T>.GetPointColumn();
			else
				fieldName = context.Metadata<T>.GetColumn(selectField);

			var sql = "SELECT " + context.Metadata<T>.GetColumn(x => x.Id)
				+ ", " + fieldName
				+ " FROM " + context.Metadata<T>.TableName
				+ " WHERE " + context.Metadata<T>.GetColumn(whereField) + " = " + whereValue.ToString();

			return GetPoints<T>(sql);
		}

		/// <summary>
		/// Trae los datos de campos Point de mysql tipados como Ìdem en un diccionario.
		/// </summary>
		/// <typeparam name="T">El tipo a consultar.</typeparam>
		/// <param name="whereField">El campo para hacer el where.</param>
		/// <param name="whereValue">El valor del where.</param>
		/// <param name="selectField">Opcional. El campo a consultar. Si no se pasa el par·metro trae el campo tipo Point de la tabla, si es que hay uno.</param>
		/// <returns>Diccionario con ids y Points de NetTopology.</returns>
		private Dictionary<int, Point> GetPoints<T>(string sql)
			where T : ActiveBaseEntity<T>, IIdentifiable
		{
			using (var rows = SqlActions.ExecuteReader(sql, true))
			{
				var points = new Dictionary<int, Point>();

				WKBReader reader = new WKBReader();
				while (rows.Read())
				{
					var bytes = rows.GetValue(1) as byte[];
					var trimmed = new byte[bytes.Length - 4];
					Array.Copy(bytes, 4, trimmed, 0, trimmed.Length);

					points.Add(rows.GetInt32(0), (Point)reader.Read(trimmed));
				}
				return points;
			}
		}

		public object Get(Type theType, object id)
		{
			if (_isStateless)
				return _wrapStateless.Get(theType.FullName, id);
			else
				return _wrap.Get(theType, id);
		}


		public static string DbSid { get; set; }
		private int timeout = -1;
		public int CommandTimeout
		{
			get { return timeout; }
			set
			{
				if (timeout != -1) throw new Exception("timeout cannot be modified at runtime");
				timeout = value;
			}
		}

		public void Ping()
		{
			SqlActions.ExecuteNonQuery("select 1");
		}
	}
}