using System;
using System.Reflection;
using System.Threading;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace GA.BDC.Core.EnterpriseComponents {
	/// <summary>
	/// Summary description for DatabaseException.
	/// </summary>
	public class DatabaseException : ApplicationException {

		public DatabaseException(string message, Exception inner, IDbConnection connection,
			IDbTransaction transaction, string commandText, 
			DataParameters[] parameters, DataProviders provider): this(message, inner, connection.ConnectionString, connection.Database, "", transaction, commandText, parameters, provider) {
		}

		public DatabaseException(string message, Exception inner, IDbConnection connection,
			CommandType commandType, IDbTransaction transaction, string commandText, 
			DataParameters[] parameters, DataProviders provider): this(message, inner, connection.ConnectionString, connection.Database, commandType.ToString(), transaction, commandText, parameters, provider) {
		}

		public DatabaseException(string message, Exception inner, string connectionConnectionString, string connectionDatabase,
									string commandType, IDbTransaction transaction, string commandText, 
									DataParameters[] parameters, DataProviders provider) {
			// collect info
			DateTime currentDate = DateTime.Now;
			string machineName = Environment.MachineName;
			string exceptionSource = inner.Source;
			string exceptionType = inner.GetType().FullName;
			string exceptionMessage = message;
			string stackTrace = inner.StackTrace;
			string callStack = Environment.StackTrace;
			string applicationDomainName = AppDomain.CurrentDomain.FriendlyName;

			string xmlException = "<?xml version=\"1.0\" encoding=\"utf-8\" ?> \r\n" +
				"<DatabaseException>\r\n" +
				"	<Date>" + currentDate + "</Date>\r\n" +
				"	<MachineName>" + machineName + "</MachineName>\r\n" +
				"	<ConnectionString>" + connectionConnectionString + "</ConnectionString>\r\n" +
				"	<DatabaseName>" + connectionDatabase + "</DatabaseName>\r\n" +
				"	<Source>" + exceptionSource + "</Source>\r\n" +
				"	<Message>" + message + "</Message>\r\n" +
				"	<CommandType>" + commandType + "</CommandType>\r\n" +
				"	<CommandLine>" + commandText + "</CommandLine>\r\n" +
				"	<Arguments>\r\n";
				
			try {
				foreach(DataParameters dparam in parameters) {
					string parameterName = "";
					string parameterValue = "";

					if(dparam.ParameterName != null) {
						parameterName = dparam.ParameterName;
					}

					if(dparam.Value != null) {
						parameterValue = dparam.Value.ToString();
					}

					xmlException += 
						"		<Argument>\r\n" +
						"			<Type>" + dparam.DataType.ToString() + "</Type>\r\n" +
						"			<Direction>" + dparam.ParamDirection.ToString() + "</Direction>\r\n" +
						"			<Name>" + parameterName + "</Name>\r\n" +
						"			<Value>" + dparam.Value.ToString() + "</Value>\r\n" +
						"		</Argument>\r\n";
				}
			} catch(System.Exception ex) {
				xmlException += "<ArgumentBuilderError>" + ex.Message + "</ArgumentBuilderError>\r\n";
			}

			xmlException +=	"	</Arguments>\r\n" +
				"	<DataProvider>" + provider + "</DataProvider>\r\n" +
				"	<FriendlyQuery>" + GenerateFriendlyQuery(commandText, parameters) + "</FriendlyQuery>\r\n" +
				"	<CallStack>" + callStack + "</CallStack>\r\n" +
				"</DatabaseException>\r\n";

			LoggingSystem.LogError(xmlException);
		}

		private string GenerateFriendlyQuery(string commandText, 
			DataParameters[] parameters) {

			string friendlyQuery = commandText + " ";
			string comment = "";

			bool isFirst = true;
			foreach(DataParameters dp in parameters) {
				bool isString = false;

				// set the para type
				switch(dp.DataType) {
					case DbType.AnsiString:
					case DbType.AnsiStringFixedLength:
					case DbType.Date:
					case DbType.DateTime:
					case DbType.Guid:
					case DbType.String:
					case DbType.StringFixedLength:
					case DbType.Time:
						isString = true;
						break;
					default:
						break;
				}

				if(isFirst) {
					isFirst = false;
				} else {
					friendlyQuery += ", ";
				}

				if(dp.ParamDirection != ParameterDirection.ReturnValue) {
					if(isString && dp.Value != DBNull.Value) {
						friendlyQuery += "'";
					}

					if(dp.Value == DBNull.Value) {
						friendlyQuery += "NULL";
					} else {
						try {
							friendlyQuery += dp.Value.ToString();
						} catch {
							friendlyQuery += "UNDEFINED";
						}
					}

					if(isString && dp.Value != DBNull.Value) {
						friendlyQuery += "'";
					}
				} else {
					comment += "   -- Return value (" + dp.ParameterName + ")";
				}

			}
			return friendlyQuery + comment;
		}
	}
}
