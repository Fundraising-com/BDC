//
// 2005-07-11 - Stephen Lim - New class.
// 2005-08-02 - Stephen Lim - Fix return and output values not mapped back to parameters.
//

using System;
using System.Data;

namespace GA.BDC.Core.Data.Sql
{
	/// <summary>
	/// A simplified interface to perform common SQL commands.
	/// </summary>
	/// <example>
	/// SqlInterface si = new SqlInterface("mssql", "connectionstring");
	/// try {
	///		// Package our parameters
	///		SqlDataParameterCollection paramCol = new SqlDataParameterCollection();
	///		paramCol.Add(new SqlDataParameter("@id", DbType.Int32, 123456));
	///		si.Open();
	///		
	///		// Optionally use transaction
	///		si.BeginTransaction();
	///		
	///		// Fetch and store into database.
	///		DataTable dt1 = si.ExecuteFetchDataTable("SELECT * FROM TABLE1 WHERE id = @id", CommandType.Text, paramCol);
	///		int ret = si.ExecuteNonQuery("UPDATE TABLE1 SET gid = 1 WHERE id = @id", CommandType.Text, paramCol);
	///		
	///		// Commit our transaction.
	///		si.Commit();
	/// }
	/// catch 
	/// {
	///		// Rollback on error.
	///		si.Rollback();
	/// }
	/// finally 
	/// {
	///		// Always close connection.
	///		si.Close();
	/// }
	/// </example>
	public class SqlInterface
	{
		#region Fields
		private SqlProviderType _provider = SqlProviderType.MsSql;
		private string _connectionString = "";
		private IDbConnection _connection = null;
		private IDbTransaction _transaction = null;
		private int _commandTimeout = 30;
		#endregion

		#region Constructors
		/// <summary>
		/// Create a new SqlInterface instance.
		/// </summary>
		/// <param name="provider">Provider.</param>
		/// <param name="connectionString">Connection string.</param>
		/// <param name="commandTimeout">Number of seconds to timeout a command.</param>
		public SqlInterface(SqlProviderType provider, string connectionString, int commandTimeout)
		{
			_provider = provider;
			_connectionString = connectionString;
			_connection = SqlProviderFactory.CreateConnection(provider);
			_connection.ConnectionString = _connectionString;
			_commandTimeout = commandTimeout;
		}

		/// <summary>
		/// Create a new SqlInterface instance.
		/// </summary>
		/// <param name="provider">Provider.</param>
		/// <param name="connectionString">Connection string.</param>
		public SqlInterface(SqlProviderType provider, string connectionString) : this (provider, connectionString, 30)
		{
		}

		/// <summary>
		/// Create a new SqlInterface instance.
		/// </summary>
		/// <param name="provider">Provider string.</param>
		/// <param name="connectionString">Connection string.</param>
		/// <param name="commandTimeout">Number of seconds to timeout an executing command.</param>
		public SqlInterface(string provider, string connectionString, int commandTimeout) : this(SqlProviderFactory.GetSqlProviderType(provider), connectionString, commandTimeout)
		{
		}

		/// <summary>
		/// Create a new SqlInterface instance.
		/// </summary>
		/// <param name="provider">Provider string.</param>
		/// <param name="connectionString">Connection string.</param>
		public SqlInterface(string provider, string connectionString) : this(provider, connectionString, 30)
		{
		}


		/// <summary>
		/// Finalizer frees connections.
		/// </summary>
		~SqlInterface()
		{
			try 
			{
				// Dispose transaction
				if (_transaction != null)
				{
					_transaction.Dispose();
				}
			}
			catch {}

			try 
			{
				// Disconnect database on finalize in case client forgets.
				if (_connection != null && _connection.State != ConnectionState.Broken && _connection.State != ConnectionState.Closed)
				{
					_connection.Close();
					_connection.Dispose();
				}
			}
			catch {}
		}
		#endregion

		#region Methods

		#region Connection methods
		/// <summary>
		/// Open a new database connection.
		/// </summary>
		public void Open()
		{
			_connection.Open();
		}

		/// <summary>
		/// Close database connection.
		/// </summary>
		public void Close()
		{
			// We ignore errors because programmers normally
			// try to close connnection inside their finally statement
			// and we want to make sure we don't throw an unexpected exception there.
			try 
			{
				_connection.Close();
			}
			catch {}
		}
		#endregion

		#region Transaction methods
		/// <summary>
		/// Start a new transaction.
		/// </summary>
		public void BeginTransaction()
		{
			_transaction = _connection.BeginTransaction();
		}

		/// <summary>
		/// Commit the current transaction.
		/// </summary>
		public void Commit()
		{
			_transaction.Commit();
		}

		/// <summary>
		/// Rollback the current transaction.
		/// </summary>
		public void Rollback()
		{
			_transaction.Rollback();
		}
		#endregion

		#region DataSet methods
		/// <summary>
		/// Execute and fetch a dataset.
		/// </summary>
		/// <param name="cmdText">Command text.</param>
		/// <param name="cmdType">Command type.</param>
		/// <param name="parameters">Parameter collection.</param>
		/// <returns>Dataset object.</returns>
		public virtual DataSet ExecuteFetchDataSet(string cmdText, CommandType cmdType, SqlDataParameterCollection parameters)
		{
			DataSet ds = new DataSet();
			IDbCommand cmd = _connection.CreateCommand();

			try 
			{
				// Create IDbCommand	
				cmd.CommandText = cmdText;
				cmd.CommandType = cmdType;
				cmd.Transaction = _transaction;
				cmd.CommandTimeout = 15000;//_commandTimeout;

				// Map parameters
				SetIDbDataParameters(cmd, parameters);

				// Use DataAdapter to fill dataset
				IDbDataAdapter adapter = SqlProviderFactory.CreateDataAdapter(_provider);
				adapter.SelectCommand = cmd;
				adapter.Fill(ds);

				// Copy back return and output values
				SetSqlDataParameters(cmd, parameters);

				return ds;
			}
			catch (Exception ex)
			{				
				throw new SqlDataException(ex.Message, _provider,
					_connectionString, _connection.Database, cmd.CommandType, cmd.CommandText, parameters, ex);
			}
		}
		#endregion

		#region DataTable methods
		/// <summary>
		/// Execute and fetch a datatable.
		/// </summary>
		/// <param name="cmdText">Command text.</param>
		/// <param name="cmdType">Command type.</param>
		/// <param name="parameters">Parameter collection.</param>
		/// <returns>Datatable object.</returns>
		public virtual DataTable ExecuteFetchDataTable(string cmdText, CommandType cmdType, SqlDataParameterCollection parameters)
		{
			// A datatable is simply part of a dataset.
			DataSet ds = ExecuteFetchDataSet(cmdText, cmdType, parameters);
			if (ds.Tables.Count > 0)
				return ds.Tables[0];
			else
				return null;
		}
		#endregion

		#region Reader methods
		/// <summary>
		/// Execute reader.
		/// </summary>
		/// <param name="cmdText">Command text.</param>
		/// <param name="cmdType">Command type.</param>
		/// <param name="parameters">Parameter collection.</param>
		/// <returns>IDataReader object.</returns>
		public IDataReader ExecuteReader(string cmdText, CommandType cmdType, SqlDataParameterCollection parameters)
		{
			IDbCommand cmd = _connection.CreateCommand();

			try 
			{
				// Create IDbCommand
				cmd.CommandText = cmdText;
				cmd.CommandType = cmdType;
				cmd.Transaction = _transaction;
				cmd.CommandTimeout = _commandTimeout;

				// Map parameters
				SetIDbDataParameters(cmd, parameters);
			
				// Execute query
				IDataReader reader = cmd.ExecuteReader();

				// Copy back return and output values
				SetSqlDataParameters(cmd, parameters);

				return reader;
			}
			catch (Exception ex)
			{				
				throw new SqlDataException(ex.Message, _provider,
					_connectionString, _connection.Database, cmd.CommandType, cmd.CommandText, parameters, ex);
			}
		}
		#endregion

		#region Scalar methods
		/// <summary>
		/// Execute and fetch the value contained in the first column of the first row.
		/// </summary>
		/// <param name="cmdText">Command text.</param>
		/// <param name="cmdType">Command type.</param>
		/// <param name="parameters">Parameter collection.</param>
		/// <returns>The value contained in the first column of the first row.</returns>
		public object ExecuteScalar(string cmdText, CommandType cmdType, SqlDataParameterCollection parameters)
		{
			IDbCommand cmd = _connection.CreateCommand();

			try 
			{
				// Create IDbCommand
				cmd.CommandText = cmdText;
				cmd.CommandType = cmdType;
				cmd.Transaction = _transaction;
				cmd.CommandTimeout = _commandTimeout;

				// Map parameters
				SetIDbDataParameters(cmd, parameters);

				// Execute query
				object ret = cmd.ExecuteScalar();

				// Copy back return and output values
				SetSqlDataParameters(cmd, parameters);

				return ret;
			}
			catch (Exception ex)
			{				
				throw new SqlDataException(ex.Message, _provider,
					_connectionString, _connection.Database, cmd.CommandType, cmd.CommandText, parameters, ex);
			}
		}
		#endregion

		#region NonQuery methods
		/// <summary>
		/// Execute a non-query command.
		/// </summary>
		/// <param name="cmdText">Command text.</param>
		/// <param name="cmdType">Command type.</param>
		/// <param name="parameters">Parameter collection.</param>
		/// <returns>The number of rows affected.</returns>
		public int ExecuteNonQuery(string cmdText, CommandType cmdType, SqlDataParameterCollection parameters)
		{
			IDbCommand cmd = _connection.CreateCommand();

			try 
			{
				// Create IDbCommand
				cmd.CommandText = cmdText;
				cmd.CommandType = cmdType;
				cmd.Transaction = _transaction;
				cmd.CommandTimeout = _commandTimeout;

				// Map parameters
				SetIDbDataParameters(cmd, parameters);

				// Execute query
				int ret = cmd.ExecuteNonQuery();

				// Copy back return and output values
				SetSqlDataParameters(cmd, parameters);
			
				return ret;
			}
			catch (Exception ex)
			{				
				throw new SqlDataException(ex.Message, _provider,
					_connectionString, _connection.Database, cmd.CommandType, cmd.CommandText, parameters, ex);
			}
		}
		#endregion

		#region Helper methods
		/// <summary>
		/// Helper method to stored SqlDataParameterCollection into IDbCommand.
		/// </summary>
		/// <param name="cmd">IDbCommand object.</param>
		/// <param name="parameters">SqlDataParameterCollection.</param>
		private void SetIDbDataParameters(IDbCommand cmd, SqlDataParameterCollection parameters)
		{
			if (parameters != null)
			{
				for (int i = 0; i < parameters.Count; i++)
				{
					SetIDbDataParameter(cmd, parameters[i]);
				}
			}
		}

		/// <summary>
		/// Helper method to map SqlDataParameter to IDbDataParameter.
		/// </summary>
		/// <param name="cmd">IDbCommand object.</param>
		/// <param name="parameter">SqlDataParameter object.</param>
		private void SetIDbDataParameter(IDbCommand cmd, SqlDataParameter parameter)
		{
			if (parameter != null)
			{
				IDbDataParameter idbDataParam = cmd.CreateParameter();
				idbDataParam.DbType = parameter.DbType;
				idbDataParam.Direction = parameter.Direction;
				idbDataParam.ParameterName = parameter.ParameterName;
				idbDataParam.Value = parameter.Value;
				idbDataParam.SourceColumn = parameter.SourceColumn;
				cmd.Parameters.Add(idbDataParam);
			}
		}

		/// <summary>
		/// Helper method to stored IDbCommand parameters to SqlDataParameterCollection.
		/// </summary>
		/// <param name="cmd">IDbCommand object.</param>
		/// <param name="parameters">SqlDataParameterCollection.</param>
		private void SetSqlDataParameters(IDbCommand cmd, SqlDataParameterCollection parameters)
		{
			if (parameters != null)
			{
				for (int i = 0; i < cmd.Parameters.Count; i++)
				{
					IDataParameter idParam = (IDataParameter) cmd.Parameters[i];
					SetSqlDataParameter(idParam, parameters[idParam.ParameterName]);
				}
			}
		}


		/// <summary>
		/// Helper method to map IDbDataParameter to SqlDataParameter.
		/// </summary>
		/// <param name="idParam">IDataParameter object.</param>
		/// <param name="parameter">SqlDataParameter object.</param>
		private void SetSqlDataParameter(IDataParameter idParam, SqlDataParameter parameter)
		{
			// Copy back return and output values from IDbDataParameter
			if (idParam.Direction == ParameterDirection.InputOutput ||
				idParam.Direction == ParameterDirection.Output ||
				idParam.Direction == ParameterDirection.ReturnValue)
			{
				parameter.Value = idParam.Value;
			}
		}
		#endregion

		#endregion

		#region Help Methods
		public void SetTimeOut(int timeout) {
			_commandTimeout = timeout;
		}
		#endregion
	}
}
