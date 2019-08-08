/*
*    Poblaciones - Plataforma abierta de datos espaciales de población.
*    Copyright (C) 2018-2019. Consejo Nacional de Investigaciones Científicas y Técnicas (CONICET)
*		 y Universidad Católica Argentina (UCA).
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
using System.Data;
using System;
using NHibernate;
using System.Data.OleDb;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Collections.Generic;
using medea.common;
using System.Diagnostics;
using MySql.Data.MySqlClient;

namespace medea.Data
{
	/// <summary>
	/// This enum is used to indicate whether the connection was provided by the caller, or created by SqlHelper, so that
	/// we can set the appropriate CommandBehavior when calling ExecuteReader()
	/// </summary>
	public enum connectionOwnership
	{
		/// <summary>Connection is owned and managed by SqlHelper</summary>
		Internal,
		/// <summary>Connection is owned and managed by the caller</summary>
		External
	}

	public class SqlActions
	{
		readonly NHibernateSession _session;

		public SqlActions(NHibernateSession session)
		{
			_session = session;
		}

		private bool ShowSql
		{
			get
			{
				return bool.Parse(context.Data.Session.Configuration.Properties[
					NHibernate.Cfg.Environment.ShowSql]);
			}
		}

		/// <summary>
		/// This method assigns an array of values to an array of SqlParameters
		/// </summary>
		/// <param name="commandParameters">Array of SqlParameters to be assigned values</param>
		/// <param name="parameterValues">Array of objects holding the values to be assigned</param>
		private static void AssignParameterValues(SqlParameter[] commandParameters, object[] parameterValues)
		{
			// Do nothing if we get no data
			if ((commandParameters == null) || (parameterValues == null))
				return;

			// We must have the same number of values as we pave parameters to put them in
			if (commandParameters.Length != parameterValues.Length)
				throw new ArgumentException("Parameter count does not match Parameter Value count.");

			// Iterate through the SqlParameters, assigning the values from the corresponding position in the
			// value array
			for (int i = 0, j = commandParameters.Length; i < j; i++)
			{
				// If the current array value derives from IDbDataParameter, then assign its Value property
				if (parameterValues[i] is IDbDataParameter)
				{
					IDbDataParameter paramInstance = (IDbDataParameter)parameterValues[i];
					if (paramInstance.Value == null)
						commandParameters[i].Value = DBNull.Value;
					else
						commandParameters[i].Value = paramInstance.Value;
				}
				else if (parameterValues[i] == null)
					commandParameters[i].Value = DBNull.Value;
				else
					commandParameters[i].Value = parameterValues[i];
			}
		}

		/// <summary>
		/// This method is used to attach array of SqlParameters to a SqlCommand.
		///
		/// This method will assign a value of DbNull to any parameter with a direction of
		/// InputOutput and a value of null.
		///
		/// This behavior will prevent default values from being used, but
		/// this will be the less common case than an intended pure output parameter (derived as InputOutput)
		/// where the user provided no input value.
		/// </summary>
		/// <param name="command">The command to which the parameters will be added</param>
		/// <param name="commandParameters">An array of SqlParameters to be added to command</param>
		private static void AttachParameters(IDbCommand command, IDbDataParameter[] commandParameters)
		{
			if (command == null) throw new ArgumentNullException("command");
			if (commandParameters != null)
			{
				foreach (IDbDataParameter p in commandParameters)
				{
					if (p != null)
					{
						// Check for derived output value with no value assigned
						if ((p.Direction == ParameterDirection.InputOutput ||
								p.Direction == ParameterDirection.Input) &&
								(p.Value == null))
						{
							p.Value = DBNull.Value;
						}
						command.Parameters.Add(p);
					}
				}
			}
		}

		public IDbDataParameter[] DiscoverParameters(string spName, object[] parameterValues)
		{
			if ((_session.Connection is SqlConnection) == false)
				throw new Exception("Only SQL Connection can discover stored procedure parameters automatically.");

			// Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
			SqlParameter[] commandParameters = null;
			//SqlHelperParameterCache.GetSpParameterSet((SqlConnection)_session.Connection, spName);

			// Assign the provided values to these parameters based on parameter order
			AssignParameterValues(commandParameters, parameterValues);
			return commandParameters;
		}


		/// <summary>
		/// This method opens (if necessary) and assigns a connection, transaction, command type and parameters
		/// to the provided command
		/// </summary>
		/// <param name="command">The SqlCommand to be prepared</param>
		/// <param name="connection">A valid SqlConnection, on which to execute this command</param>
		/// <param name="transaction">A valid SqlTransaction, or 'null'</param>
		/// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
		/// <param name="commandText">The stored procedure name or T-SQL command</param>
		/// <param name="commandParameters">An array of SqlParameters to be associated with the command or 'null' if no parameters are required</param>
		/// <param name="mustCloseConnection"><c>true</c> if the connection was opened by the method, otherwose is false.</param>
		internal void PrepareCommand(IDbCommand command, IDbConnection connection, ITransaction transaction, CommandType commandType, string commandText, IDbDataParameter[] commandParameters, out bool mustCloseConnection)
		{
			if (command == null) throw new ArgumentNullException("command");
			if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

			context.Data.CheckSession();

			// If the provided connection is not open, we will open it
			if (connection.State != ConnectionState.Open)
			{
				mustCloseConnection = true;
				connection.Open();
			}
			else
				mustCloseConnection = false;

			// Associate the connection with the command
			command.Connection = connection;

			command.CommandTimeout = _session.CommandTimeout;
			// Set the command text (stored procedure name or SQL statement)
			command.CommandText = commandText;

			// If we were provided a transaction, assign it
			if (transaction != null)
				transaction.Enlist(command);

			// Set the command type
			command.CommandType = commandType;

			// Attach the command parameters if they are provided
			if (commandParameters != null)
				AttachParameters(command, commandParameters);

			return;
		}

		public DataSet ExecuteDataSet(string commandText, params IDbDataParameter[] commandParameters)
		{
			return ExecuteDataSet(commandText, false, commandParameters);

		}

		public List<T> ExecuteList<T>(string commandText, bool doNotFlush = true, params IDbDataParameter[] commandParameters)
		{
			var ret = new List<T>();
			var dt = context.Data.Session.SqlActions.ExecuteDataSet(commandText, true, commandParameters).Tables[0];
			foreach (DataRow row in dt.Rows)
				ret.Add((T) row[0]);
			return ret;
		}
		public Dictionary<T1, T2> ExecuteDictionary<T1, T2>(string commandText, bool doNotFlush)
		{
			var ret = new Dictionary<T1, T2>();
			var dt = context.Data.Session.SqlActions.ExecuteDataSet(commandText, true).Tables[0];
			foreach (DataRow row in dt.Rows)
				ret.Add((T1) row[0], (T2) row[1]);
			return ret;
		}
		public DataSet ExecuteDataSet(string commandText, bool doNotFlush, params IDbDataParameter[] commandParameters)
		{
			CommandType commandType = CommandType.Text;

			if (_session.FlushMode != FlushMode.Never && doNotFlush == false)
				_session.Flush();

			if (_session.Connection == null) throw new ArgumentNullException("connection");
			// Create a command and prepare it for execution
			using (IDbCommand cmd = _session.Connection.CreateCommand())
			{
				bool mustCloseConnection = false;
				PrepareCommand(cmd, _session.Connection, _session.Transaction, commandType, commandText, commandParameters, out mustCloseConnection);

				if (ShowSql) //TODO: Agregar print de commandParameters etc...
					Debug.WriteLine("ExecuteDataSet: " + commandText);

				// Create the DataAdapter & DataSet
				IDbDataAdapter da = CreateAdapter(cmd);

				// Fill the DataSet using default values for DataTable names, etc
				DataSet ds = new DataSet();
				da.Fill(ds);

				// Detach the SqlParameters from the command object, so they can be used again
				cmd.Parameters.Clear();

				if (mustCloseConnection)
					_session.Connection.Close();

				context.Data.MarkSessionUsage();

				return ds;
			}
		}

		private IDbDataAdapter CreateAdapter(IDbCommand cmd)
		{
			if (cmd is SqlCommand)
				return new SqlDataAdapter((SqlCommand)cmd);
			else if (cmd is OleDbCommand)
				return new OleDbDataAdapter((OleDbCommand)cmd);
			else if (cmd is OdbcCommand)
				return new OdbcDataAdapter((OdbcCommand)cmd);
			else if (cmd is MySqlCommand)
				return new MySqlDataAdapter((MySqlCommand)cmd);
			else throw new Exception("Connection type not supported: " + cmd.GetType().Name + ".");
		}

		public void TruncateTable(string tablaName)
		{
			ExecuteNonQuery(CommandType.Text, "truncate table " + tablaName, true);
		}

		public int ExecuteNonQuery(string commandText, params IDbDataParameter[] commandParameters)
		{
			return ExecuteNonQuery(CommandType.Text, commandText, true, commandParameters);
		}
		public int ExecuteNonQuery(string commandText, bool doNotFlush, params IDbDataParameter[] commandParameters)
		{
			return ExecuteNonQuery(CommandType.Text, commandText, doNotFlush, commandParameters);
		}

		public int ExecuteNonQuery(CommandType commandType, string commandText, bool doNotFlush, params IDbDataParameter[] commandParameters)
		{
			if (_session.FlushMode != FlushMode.Never && doNotFlush == false)
				_session.Flush();

			if (_session.Connection == null) throw new ArgumentNullException("connection");
			// Create a command and prepare it for execution
			using (IDbCommand cmd = _session.Connection.CreateCommand())
			{
				bool mustCloseConnection = false;

				PrepareCommand(cmd, _session.Connection, _session.Transaction, commandType, commandText, commandParameters, out mustCloseConnection);

				if (ShowSql) //TODO: Agregar print de commandParameters etc...
					Debug.WriteLine("ExecuteNonQuery: " + commandText);

				// Finally, execute the command
				int retval = cmd.ExecuteNonQuery();

				// Detach the SqlParameters from the command object, so they can be used again
				cmd.Parameters.Clear();
				if (mustCloseConnection)
					_session.Connection.Close();

				context.Data.MarkSessionUsage();

				return retval;
			}
		}

		public IDataReader ExecuteReader(string commandText, bool doNotFlush, IDbDataParameter[] commandParameters = null, IDbTransaction transaction = null, connectionOwnership connectionOwnership = connectionOwnership.External)
		{
			CommandType commandType = CommandType.Text;

			if (_session.FlushMode != FlushMode.Never && doNotFlush == false )
				_session.Flush();

			if (_session.Connection == null) throw new ArgumentNullException("connection");
			bool mustCloseConnection = false;
			try
			{
				// Create a command and prepare it for execution
				using (IDbCommand cmd = _session.Connection.CreateCommand())
				{
					PrepareCommand(cmd, _session.Connection, _session.Transaction, commandType, commandText, commandParameters, out mustCloseConnection);

					if (ShowSql) //TODO: Agregar print de commandParameters etc...
						Debug.WriteLine("ExecuteReader: " + commandText);

					// Create a reader
					IDataReader dataReader;
					// Call ExecuteReader with the appropriate CommandBehavior
					if (connectionOwnership == connectionOwnership.External)
						dataReader = cmd.ExecuteReader();
					else
						dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

					// Detach the SqlParameters from the command object, so they can be used again.
					// HACK: There is a problem here, the output parameter values are fletched
					// when the reader is closed, so if the parameters are detached from the command
					// then the SqlReader can�t set its values.
					// When this happen, the parameters can�t be used again in other command.
					bool canClear = true;
					foreach (IDbDataParameter commandParameter in cmd.Parameters)
					{
						if (commandParameter.Direction != ParameterDirection.Input)
							canClear = false;
					}

					if (canClear)
						cmd.Parameters.Clear();

					return dataReader;
				}
			}
			catch
			{
				if (mustCloseConnection)
					_session.Connection.Close();
				throw;
			}
			finally
			{
				context.Data.MarkSessionUsage();
			}
		}

		public int ExecuteScalarIntNoFlush(string commandText, params IDbDataParameter[] commandParameters)
		{
			return (int)(long) ExecuteScalar(commandText, true, commandParameters);
		}
		public int ExecuteScalarInt(string commandText, params IDbDataParameter[] commandParameters)
		{
			return (int)(long) ExecuteScalar(commandText, false, commandParameters);
		}

		public object ExecuteScalar(string commandText, params IDbDataParameter[] commandParameters)
		{
			return ExecuteScalar(commandText, false, commandParameters);
		}

		public object ExecuteScalar(string commandText, bool doNotFlush, params IDbDataParameter[] commandParameters)
		{
			CommandType commandType = CommandType.Text;
			if (_session.FlushMode != FlushMode.Never &&
							doNotFlush == false)
				_session.Flush();

			if (_session.Connection == null) throw new ArgumentNullException("connection");
			// Create a command and prepare it for execution
			using (IDbCommand cmd = _session.Connection.CreateCommand())
			{
				if (ShowSql) //TODO: Agregar print de commandParameters etc...
					Debug.WriteLine("ExecuteScalar: " + commandText);

				bool mustCloseConnection = false;
				PrepareCommand(cmd, _session.Connection, _session.Transaction, commandType, commandText, commandParameters, out mustCloseConnection);

				// Execute the command & return the results
				var retval = cmd.ExecuteScalar();

				// Detach the SqlParameters from the command object, so they can be used again
				cmd.Parameters.Clear();

				if (mustCloseConnection)
					_session.Connection.Close();

				context.Data.MarkSessionUsage();

				return retval;
			}
		}

		public void BulkInsert(string sql, Progress progress = null, int batchCount = 0)
		{
			if (string.IsNullOrEmpty(sql))
				return;

			if (batchCount < 1)
				batchCount = Settings.BulkInsertBatchSize / Settings.InsertGeneratorFieldsEvery;

			var batch = SplitBatch(sql, batchCount);

			if (progress != null)
				progress.Total = batch.Count;

			foreach (var item in batch)
			{
				if (progress != null)
					progress.Increment();
				ExecuteNonQuery(item);
			}
		}

		private List<string> SplitBatch(string str, int count)
		{
			if (count < 1)
				count = 500;

			var ret = new List<string>();

			int n = 0;
			int start = 0;
			for (int i = 0; i < str.Length; i++)
			{
				if (str[i] == '\n')
					n++;
				if (count == n)
				{
					ret.Add(str.Substring(start, i - start + 1));
					start = i + 1;
					n = 0;
				}
			}
			if (start < str.Length)
				ret.Add(str.Substring(start));
			return ret;
		}
	}
}
