//
// 2005-07-06 - Stephen Lim - New class.
//

using System;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Npgsql;
using Finisar.SQLite;

namespace GA.BDC.Core.Data.Sql
{
	#region Enums
	/// <summary>
	/// Sql provider type.
	/// </summary>
	public enum SqlProviderType
	{
		/// <summary>
		/// IBM DB2.
		/// </summary>
		IbmDb2 = 1,

		/// <summary>
		/// Microsft Access.
		/// </summary>
		MsAccess = 2,

		/// <summary>
		/// Microsoft Sql server.
		/// </summary>
		MsSql = 3,        

		/// <summary>
		/// MySql.
		/// </summary>
		MySql = 4,

		/// <summary>
		/// ODBC.
		/// </summary>
		Odbc = 5,

		/// <summary>
		/// Oledb.
		/// </summary>
		OleDb = 6,

		/// <summary>
		/// Oracle.
		/// </summary>
		Oracle = 7,

		/// <summary>
		/// PostgreSql.
		/// </summary>
		PostgreSql = 8,

		/// <summary>
		/// SQLite.
		/// </summary>
		SQLite = 9,

        /// <summary>
        /// Microsoft Sql server.
        /// </summary>
        SqlClient = 10,
	}
	#endregion

	/// <summary>
	/// Generic data access provider factory for IBM DB2, Microsoft Access, 
	/// Microsoft Sql Server, MySql, ODBC, OleDb, Oracle, PostgreSql and Sqlite.
	/// </summary>
	/// <remarks>IBM DB2 provider uses System.Data.OleDb internally. 
	/// Microsoft Access provider uses System.Data.OleDb internally. Microsoft Sql Server provider uses System.Data.SqlClient internally.
	/// MySql provider is obtained from http://www.mysql.com. ODBC provider uses System.Data.Odbc internally. 
	/// OleDb provider uses System.Data.OleDb internally. Oracle provider uses System.Data.OracleClient internally.
	/// PostgreSql provider uses Npgsql connector from http://gborg.postgresql.org/project/npgsql/projdisplay.php.
	/// Sqlite ADO.NET provider is obtained from http://sourceforge.net/projects/adodotnetsqlite.
	/// For Sqlite, you must include the Sqlite.dll or Sqlite3.dll assemblies. </remarks>
	/// <example>
	/// // Create connection and command object.
	///	IDbConnection conn = SqlProviderFactory.CreateConnection("mssql", "connectionstring");
	///	IDbCommand cmd = conn.CreateCommand();
	///	cmd.CommandText = "UPDATE TABLE SET gid = 1 WHERE id = @id";
	///	
	///	// Set parameters
	///	IDbDataParameter idbDataParam = cmd.CreateParameter();
	/// idbDataParam.DbType = DbType.Int32;
	/// idbDataParam.ParameterName = "@id";
	/// idbDataParam.Value = 1;
	///	cmd.Parameters.Add(idbDataParam);
	///	
	///	try {
	///		conn.Open();
	///		int ret = cmd.ExecuteNonQuery();
	///	}
	///	finally {
	///		conn.Close();
	///	}
	/// </example>
	public sealed class SqlProviderFactory
	{
		#region Constructors
		/// <summary>
		/// Cannot instantiate this class.
		/// </summary>
		private SqlProviderFactory()
		{
		
		}
		#endregion

		#region Methods

		#region SqlProviderType methods
		/// <summary>
		/// Get the corresponding SqlProviderType from string.
		/// </summary>
		/// <param name="provider">A provider type string.</param>
		/// <returns>SqlProviderType.</returns>
		public static SqlProviderType GetSqlProviderType(string provider)
		{
			switch (provider)
			{
				case "ibmdb2":
					return SqlProviderType.IbmDb2;
				case "msaccess":
					return SqlProviderType.MsAccess;
				case "mssql":
					return SqlProviderType.MsSql;
                case "System.Data.SqlClient":
                    return SqlProviderType.MsSql;
				case "mysql":
					return SqlProviderType.MySql;
				case "odbc":
					return SqlProviderType.Odbc;
				case "oledb":
					return SqlProviderType.OleDb;
				case "oracle":
					return SqlProviderType.Oracle;
				case "postgresql":
					return SqlProviderType.PostgreSql;
				case "sqlite":
					return SqlProviderType.SQLite;
				default:
					throw new ArgumentException("Invalid Sql provider. Provider received: " + provider);
			}
		}

		/// <summary>
		/// Get the corresponding SqlProviderType from string.
		/// </summary>
		/// <param name="provider">A provider type string.</param>
		/// <returns>SqlProviderType.</returns>
		public static string GetSqlProviderType(SqlProviderType provider)
		{
			switch (provider)
			{
				case SqlProviderType.IbmDb2:
					return "ibmdb2";
				case SqlProviderType.MsAccess:
					return "msaccess";
				case SqlProviderType.MsSql:
					return "mssql";
                case SqlProviderType.SqlClient:
                    return "System.Data.SqlClient";
				case SqlProviderType.MySql:
					return "mysql";
				case SqlProviderType.Odbc:
					return "odbc";
				case SqlProviderType.OleDb:
					return "oledb";
				case SqlProviderType.Oracle:
					return "oracle";
				case SqlProviderType.PostgreSql:
					return "postgresql";
				case SqlProviderType.SQLite:
					return "sqlite";
				default:
                    throw new NotSupportedException("Invalid Sql provider. Provider received: " + provider);
			}
		}
		#endregion

		#region IDbCommand methods
		/// <summary>
		/// Create a new IDbCommand object.
		/// </summary>
		/// <param name="provider">Sql provider type string.</param>
		/// <returns>IDbCommand object.</returns>
		public static IDbCommand CreateCommand(string provider)
		{
			return CreateCommand(GetSqlProviderType(provider));
		}

		/// <summary>
		/// Create a new IDbCommand object.
		/// </summary>
		/// <param name="provider">SqlProviderType.</param>
		/// <returns>IDbCommand object.</returns>
		public static IDbCommand CreateCommand(SqlProviderType provider)
		{
			switch (provider)
			{
				case SqlProviderType.IbmDb2:
					return new OleDbCommand();
				case SqlProviderType.MsAccess:
					return new OleDbCommand();
				case SqlProviderType.MsSql:
					return new SqlCommand();
                case SqlProviderType.SqlClient:
                    return new SqlCommand();
				case SqlProviderType.MySql:
					return new MySqlCommand();
				case SqlProviderType.Odbc:
					return new OdbcCommand();
				case SqlProviderType.OleDb:
					return new OleDbCommand();
				case SqlProviderType.Oracle:
					return new OracleCommand();
				case SqlProviderType.PostgreSql:
					return new NpgsqlCommand();
				case SqlProviderType.SQLite:
					return new SQLiteCommand();
				default:
                    throw new NotSupportedException("Invalid Sql provider. Provider received: " + provider);
			}
		}

		/// <summary>
		/// Create a new IDbCommand object.
		/// </summary>
		/// <param name="provider">Sql provider type string.</param>
		/// <param name="cmdText">Command text.</param>
		/// <returns>IDbCommand object.</returns>
		public static IDbCommand CreateCommand(string provider, string cmdText)
		{
			return CreateCommand(GetSqlProviderType(provider), cmdText);
		}

		/// <summary>
		/// Create a new IDbCommand object.
		/// </summary>
		/// <param name="provider">SqlProviderType.</param>
		/// <param name="cmdText">Command text.</param>
		/// <returns>IDbCommand object.</returns>
		public static IDbCommand CreateCommand(SqlProviderType provider, string cmdText)
		{
			return CreateCommand(provider, cmdText, null, null);
		}

		/// <summary>
		/// Create a new IDbCommand object.
		/// </summary>
		/// <param name="provider">Sql provider type string.</param>
		/// <param name="cmdText">Command text.</param>
		/// <param name="connection">IDbConnection object.</param>
		/// <returns>IDbCommand object.</returns>
		public static IDbCommand CreateCommand(string provider, string cmdText, IDbConnection connection)
		{
			return CreateCommand(GetSqlProviderType(provider), cmdText, connection);
		}


		/// <summary>
		/// Create a new IDbCommand object.
		/// </summary>
		/// <param name="provider">SqlProviderType.</param>
		/// <param name="cmdText">Command text.</param>
		/// <param name="connection">IDbConnection object.</param>
		/// <returns>IDbCommand object.</returns>
		public static IDbCommand CreateCommand(SqlProviderType provider, string cmdText, IDbConnection connection)
		{
			return CreateCommand(provider, cmdText, connection, null);
		}

		/// <summary>
		/// Create a new IDbCommand object.
		/// </summary>
		/// <param name="provider">Sql provider type string.</param>
		/// <param name="cmdText">Command text.</param>
		/// <param name="connection">IDbConnection object.</param>
		/// <param name="transaction">IDbTransaction object.</param>
		/// <returns>IDbCommand object.</returns>
		public static IDbCommand CreateCommand(string provider, string cmdText, IDbConnection connection, IDbTransaction transaction)
		{
			return CreateCommand(GetSqlProviderType(provider), cmdText, connection, transaction);
		}


		/// <summary>
		/// Create a new IDbCommand object.
		/// </summary>
		/// <param name="provider">SqlProviderType.</param>
		/// <param name="cmdText">Command text.</param>
		/// <param name="connection">IDbConnection object.</param>
		/// <param name="transaction">IDbTransaction object.</param>
		/// <returns>IDbCommand object.</returns>
		public static IDbCommand CreateCommand(SqlProviderType provider, string cmdText, IDbConnection connection, IDbTransaction transaction)
		{
			IDbCommand cmd;

			switch (provider)
			{
				case SqlProviderType.IbmDb2:
					cmd = new OleDbCommand();
					break;
				case SqlProviderType.MsAccess:
					cmd = new OleDbCommand();
					break;
				case SqlProviderType.MsSql:
					cmd = new SqlCommand();
					break;
                case SqlProviderType.SqlClient:
                    cmd = new SqlCommand();
                    break;
				case SqlProviderType.MySql:
					cmd = new MySqlCommand();
					break;
				case SqlProviderType.Odbc:
					cmd = new OdbcCommand();
					break;
				case SqlProviderType.OleDb:
					cmd = new OleDbCommand();
					break;
				case SqlProviderType.Oracle:
					cmd = new OracleCommand();
					break;
				case SqlProviderType.PostgreSql:
					cmd = new NpgsqlCommand();
					break;
				case SqlProviderType.SQLite:
					cmd = new SQLiteCommand();
					break;
				default:
                    throw new NotSupportedException("Invalid Sql provider. Provider received: " + provider);
			}

			cmd.Transaction = transaction;
			cmd.Connection = connection;
			cmd.CommandText = cmdText;
			return cmd;
		}
		#endregion

		#region IDbConnection methods
		/// <summary>
		/// Create a new IDbConnection object.
		/// </summary>
		/// <param name="provider">Sql provider type string.</param>
		/// <returns>IDbConnection object.</returns>
		public static IDbConnection CreateConnection(string provider)
		{
			return CreateConnection(GetSqlProviderType(provider));
		}

		/// <summary>
		/// Create a new IDbConnection object.
		/// </summary>
		/// <param name="provider">SqlProviderType.</param>
		/// <returns>IDbConnection object.</returns>
		public static IDbConnection CreateConnection(SqlProviderType provider)
		{
			return CreateConnection(provider, "");
		}

		/// <summary>
		/// Create a new IDbConnection object.
		/// </summary>
		/// <param name="provider">Sql provider type string.</param>
		/// <returns>IDbConnection object.</returns>
		public static IDbConnection CreateConnection(string provider, string connectionString)
		{
			return CreateConnection(GetSqlProviderType(provider), connectionString);
		}


		/// <summary>
		/// Create a new IDbConnection object.
		/// </summary>
		/// <param name="provider">SqlProviderType.</param>
		/// <param name="connectionString">Connection string.</param>
		/// <returns>IDbConnection object.</returns>
		public static IDbConnection CreateConnection(SqlProviderType provider, string connectionString)
		{
			switch (provider)
			{
				case SqlProviderType.IbmDb2:
					return new OleDbConnection(connectionString);
				case SqlProviderType.MsAccess:
					return new OleDbConnection(connectionString);
				case SqlProviderType.MsSql:
					return new SqlConnection(connectionString);
                case SqlProviderType.SqlClient:
                    return new SqlConnection(connectionString);
				case SqlProviderType.MySql:
					return new MySqlConnection(connectionString);
				case SqlProviderType.Odbc:
					return new OdbcConnection(connectionString);
				case SqlProviderType.OleDb:
					return new OleDbConnection(connectionString);
				case SqlProviderType.Oracle:
					return new OracleConnection(connectionString);
				case SqlProviderType.PostgreSql:
					return new NpgsqlConnection(connectionString);
				case SqlProviderType.SQLite:
					return new SQLiteConnection(connectionString);
				default:
                    throw new NotSupportedException("Invalid Sql provider. Provider received: " + provider);
			}
		}
		#endregion

		#region IDataParameter methods
		/// <summary>
		/// Create a new IDataParameter object.
		/// </summary>
		/// <param name="provider">Sql provider type string.</param>
		/// <returns>IDataParameter object.</returns>
		public static IDataParameter CreateParameter(string provider)
		{
			return CreateParameter(GetSqlProviderType(provider));
		}

		/// <summary>
		/// Create a new IDataParameter object.
		/// </summary>
		/// <param name="provider">SqlProviderType.</param>
		/// <returns>IDataParameter object.</returns>
		public static IDataParameter CreateParameter(SqlProviderType provider)
		{
			return CreateParameter(provider, "", DbType.String, ParameterDirection.Input, null);
		}

		/// <summary>
		/// Create a new IDataParameter object.
		/// </summary>
		/// <param name="provider">Sql provider type string.</param>
		/// <param name="paramName">Parameter name.</param>
		/// <param name="paramType">DbType.</param>
		/// <param name="paramValue">Parameter value.</param>
		/// <returns>IDataParameter object.</returns>
		public static IDataParameter CreateParameter(string provider, string paramName, DbType paramType, object paramValue)
		{
			return CreateParameter(GetSqlProviderType(provider), paramName, paramType, ParameterDirection.Input, paramValue);
		}

		/// <summary>
		/// Create a new IDataParameter object.
		/// </summary>
		/// <param name="provider">SqlProviderType.</param>
		/// <param name="paramName">Parameter name.</param>
		/// <param name="paramType">DbType.</param>
		/// <param name="paramValue">Parameter value.</param>
		/// <returns>IDataParameter object.</returns>
		public static IDataParameter CreateParameter(SqlProviderType provider, string paramName, DbType paramType, object paramValue)
		{
			return CreateParameter(GetSqlProviderType(provider), paramName, paramType, ParameterDirection.Input, paramValue);
		}

		/// <summary>
		/// Create a new IDataParameter object.
		/// </summary>
		/// <param name="provider">Sql provider type string.</param>
		/// <param name="paramName">Parameter name.</param>
		/// <param name="paramType">DbType.</param>
		/// <param name="paramDirection">ParameterDirection.</param>
		/// <param name="paramValue">Parameter value.</param>
		/// <returns>IDataParameter object.</returns>
		public static IDataParameter CreateParameter(string provider, string paramName, DbType paramType, ParameterDirection paramDirection, object paramValue)
		{
			return CreateParameter(GetSqlProviderType(provider), paramName, paramType, paramDirection, paramValue);
		}


		/// <summary>
		/// Create a new IDataParameter object.
		/// </summary>
		/// <param name="provider">SqlProviderType.</param>
		/// <param name="paramName">Parameter name.</param>
		/// <param name="paramType">DbType.</param>
		/// <param name="paramDirection">ParameterDirection.</param>
		/// <param name="paramValue">Parameter value.</param>
		/// <returns>IDataParameter object.</returns>
		public static IDataParameter CreateParameter(SqlProviderType provider, string paramName, DbType paramType, ParameterDirection paramDirection, object paramValue)
		{
			IDataParameter param;

			switch (provider)
			{
				case SqlProviderType.IbmDb2:
					param = new OleDbParameter();
					break;
				case SqlProviderType.MsAccess:
					param = new OleDbParameter();
					break;
				case SqlProviderType.MsSql:
					param = new SqlParameter();
					break;
                case SqlProviderType.SqlClient:
                    param = new SqlParameter();
                    break;
				case SqlProviderType.MySql:
					param = new MySqlParameter();
					break;
				case SqlProviderType.Odbc:
					param = new OdbcParameter();
					break;
				case SqlProviderType.OleDb:
					param = new OleDbParameter();
					break;
				case SqlProviderType.Oracle:
					param = new OracleParameter();
					break;
				case SqlProviderType.PostgreSql:
					param = new NpgsqlParameter();
					break;
				case SqlProviderType.SQLite:
					param = new SQLiteParameter();
					break;
				default:
                    throw new NotSupportedException("Invalid Sql provider. Provider received: " + provider);
			}

			param.ParameterName = paramName;
			param.DbType = paramType;
			param.Direction = paramDirection;
			param.Value = paramValue;

			return param;
		}
		#endregion

		#region IDbDataAdapter methods
		/// <summary>
		/// Create a new IDbDataAdapter object.
		/// </summary>
		/// <param name="provider">Sql provider type string.</param>
		/// <returns>IDbDataAdapter object.</returns>
		public static IDbDataAdapter CreateDataAdapter(string provider)
		{
			return CreateDataAdapter(GetSqlProviderType(provider));
		}

		/// <summary>
		/// Create a new IDbDataAdapter object.
		/// </summary>
		/// <param name="provider">SqlProviderType.</param>
		/// <returns>IDbDataAdapter object.</returns>
		public static IDbDataAdapter CreateDataAdapter(SqlProviderType provider)
		{
			switch (provider)
			{
				case SqlProviderType.IbmDb2:
					return new OleDbDataAdapter();
				case SqlProviderType.MsAccess:
					return new OleDbDataAdapter();
				case SqlProviderType.MsSql:
					return new SqlDataAdapter();
                case SqlProviderType.SqlClient:
                    return new SqlDataAdapter();
				case SqlProviderType.MySql:
					return new MySqlDataAdapter();
				case SqlProviderType.Odbc:
					return new OdbcDataAdapter();
				case SqlProviderType.OleDb:
					return new OleDbDataAdapter();
				case SqlProviderType.Oracle:
					return new OracleDataAdapter();
				case SqlProviderType.PostgreSql:
					return new NpgsqlDataAdapter();
				case SqlProviderType.SQLite:
					return new SQLiteDataAdapter();
				default:
                    throw new NotSupportedException("Invalid Sql provider. Provider received: " + provider);
			}
		}
		#endregion

		#endregion
	}
}
