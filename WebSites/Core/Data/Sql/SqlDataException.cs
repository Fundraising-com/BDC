//
// 2005-07-12 - Stephen Lim - New class.
//


using System;
using System.Data;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace GA.BDC.Core.Data.Sql
{
	/// <summary>
	/// SqlDataException.
	/// </summary>
	public class SqlDataException : Exception
	{
		public SqlDataException()
		{
		}

		public SqlDataException(string message) : base(message)
		{		
		}

		public SqlDataException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public SqlDataException(string message, System.Exception innerException) : base(message, innerException)
		{
		}

		public SqlDataException(string message, SqlProviderType provider, string connectionString, string connectionDatabase,
			CommandType commandType, string commandText, SqlDataParameterCollection parameters, System.Exception innerException) : 
			base(message + Environment.NewLine + SqlDataException.CreateMessage(SqlProviderFactory.GetSqlProviderType(provider), connectionString, connectionDatabase,
			commandType, commandText, parameters), innerException)
		{
		}

		public SqlDataException(string message, string provider, string connectionString, string connectionDatabase,
			CommandType commandType, string commandText, SqlDataParameterCollection parameters, System.Exception innerException) : 
			base(message + Environment.NewLine + SqlDataException.CreateMessage(provider, connectionString, connectionDatabase,
			commandType, commandText, parameters), innerException)
		{
		}

		public SqlDataException(string message, SqlProviderType provider, string connectionString, string connectionDatabase,
			string commandType, string commandText, SqlDataParameterCollection parameters, System.Exception innerException) : 
			base(message + Environment.NewLine + SqlDataException.CreateMessage(SqlProviderFactory.GetSqlProviderType(provider), connectionString, connectionDatabase,
			commandType, commandText, parameters), innerException)
		{
		}

		public SqlDataException(string message, string provider, string connectionString, string connectionDatabase,
			string commandType, string commandText, SqlDataParameterCollection parameters, System.Exception innerException) : 
			base(message + Environment.NewLine + SqlDataException.CreateMessage(provider, connectionString, connectionDatabase,
			commandType, commandText, parameters), innerException)
		{
		}

		public SqlDataException(string message, SqlProviderType provider, IDbConnection connection, IDbCommand command, System.Exception innerException) : 
			base(message + Environment.NewLine + SqlDataException.CreateMessage(SqlProviderFactory.GetSqlProviderType(provider), connection, command), innerException)
		{
		}

		public SqlDataException(string message, string provider, IDbConnection connection, IDbCommand command, System.Exception innerException) : 
			base(message + Environment.NewLine + SqlDataException.CreateMessage(provider, connection, command), innerException)
		{
		}

		public SqlDataException(string message, SqlProviderType provider, IDbConnection connection, IDbCommand command, IDataParameterCollection parameters, System.Exception innerException) : 
			base(message + Environment.NewLine + SqlDataException.CreateMessage(SqlProviderFactory.GetSqlProviderType(provider), connection, command, parameters), innerException)
		{
		}

		public SqlDataException(string message, string provider, IDbConnection connection, IDbCommand command, IDataParameterCollection parameters, System.Exception innerException) : 
			base(message + Environment.NewLine + SqlDataException.CreateMessage(provider, connection, command, parameters), innerException)
		{
		}

		/// <summary>
		/// Create SQL error message.
		/// </summary>
		/// <param name="provider"></param>
		/// <param name="connectionString"></param>
		/// <param name="connectionDatabase"></param>
		/// <param name="commandType"></param>
		/// <param name="commandText"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static string CreateMessage(string provider, string connectionString, string connectionDatabase,
			CommandType commandType, string commandText, SqlDataParameterCollection parameters)
		{			
			return CreateMessage(provider, connectionString, connectionDatabase, commandType.ToString(), commandText, parameters);  
		}

		/// <summary>
		/// Create SQL error message.
		/// </summary>
		/// <param name="provider"></param>
		/// <param name="connectionString"></param>
		/// <param name="database"></param>
		/// <param name="commandType"></param>
		/// <param name="commandText"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static string CreateMessage(string provider, string connectionString, string database, string commandType, string commandText,
			IDataParameterCollection parameters)
		{
			return CreateMessage(provider, connectionString, database, commandType, commandText, MapSqlDataParameterCollection(parameters));
		}

		/// <summary>
		/// Create SQL error message.
		/// </summary>
		/// <param name="provider"></param>
		/// <param name="connection"></param>
		/// <param name="command"></param>
		/// <returns></returns>
		public static string CreateMessage(string provider, IDbConnection connection, IDbCommand command)
		{
			if (command != null)
				return CreateMessage(provider, connection, command, command.Parameters);
			else
				return CreateMessage(provider, connection, command, null);
		}


		/// <summary>
		/// Create SQL error message.
		/// </summary>
		/// <param name="provider"></param>
		/// <param name="connection"></param>
		/// <param name="command"></param>
		/// <returns></returns>
		public static string CreateMessage(string provider, IDbConnection connection, IDbCommand command, IDataParameterCollection parameters)
		{
			string connectionString = "";
			string database = "";
			string commandType = "";
			string commandText = "";

			if (connection != null)
			{
				connectionString = connection.ConnectionString;
				database = connection.Database;
			}

			SqlDataParameterCollection sqlParameters = null;
			if (command != null)
			{
				commandType = command.CommandType.ToString();
				commandText = command.CommandText;
				sqlParameters = MapSqlDataParameterCollection(command.Parameters);
			}			
			
			return CreateMessage(provider, connectionString, database, commandType, commandText, parameters);
		}

		/// <summary>
		/// Create SQL error message.
		/// </summary>
		/// <param name="provider"></param>
		/// <param name="connectionString"></param>
		/// <param name="database"></param>
		/// <param name="commandType"></param>
		/// <param name="commandText"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static string CreateMessage(string provider, string connectionString, string database, string commandType, string commandText,
			SqlDataParameterCollection parameters)
		{
			// Collect info
			string msg = Environment.NewLine;

			// Strip off password for security reasons
			connectionString = Regex.Replace(connectionString, @"password\s*=.*?(;|$)", "", RegexOptions.IgnoreCase);
			connectionString = Regex.Replace(connectionString, @"passwd\s*=.*?(;|$)", "", RegexOptions.IgnoreCase);
			connectionString = Regex.Replace(connectionString, @"pwd\s*=.*?(;|$)", "", RegexOptions.IgnoreCase);

			msg += "Connection String: " + connectionString + Environment.NewLine;
			msg += "Database: " + database + Environment.NewLine;
			msg += "Command Type: " + commandType + Environment.NewLine;
			msg += "Command Text: " + commandText + Environment.NewLine;
			msg += "Provider: " + provider + Environment.NewLine;

			if (parameters != null)
			{
				try 
				{
					for (int i = 0; i < parameters.Count; i++)
					{
						msg += Environment.NewLine;
						msg += "Parameter Index: " + i + Environment.NewLine;
						try {msg += "Name: " + parameters[i].ParameterName + Environment.NewLine;} 
						catch {}
						try {msg += "DbType: " + parameters[i].DbType.ToString() + Environment.NewLine;} 
						catch {}
						try {msg += "Direction: " + parameters[i].Direction.ToString() + Environment.NewLine;} 
						catch {}
						try {msg += "Source Column: " + parameters[i].SourceColumn + Environment.NewLine;} 
						catch {}
						try {msg += "Value: " + parameters[i].Value.ToString() + Environment.NewLine;} 
						catch {}
					}
				}
				catch {}
			}
			return msg;	
		}

		/// <summary>
		/// Map IDataParameterCollection to SqlDataParameterCollection.
		/// </summary>
		/// <param name="iParams"></param>
		/// <returns></returns>
		private static SqlDataParameterCollection MapSqlDataParameterCollection(IDataParameterCollection iParams)
		{
			// Map IDataParameterCollection to SqlDataParameterCollection
			SqlDataParameterCollection parameters = new SqlDataParameterCollection();
			if (iParams != null)
			{
				for (int i = 0; i < iParams.Count; i++)
				{
					try 
					{
						IDataParameter param = (IDataParameter) iParams[i];
						parameters.Add(new SqlDataParameter(param.ParameterName, param.DbType, param.Direction, param.Value, param.SourceColumn));
					}
					catch {}
				}
			}
			return parameters;	
		}
	}
}
