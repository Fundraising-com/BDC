using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Reflection;
using System.Collections;

namespace GA.BDC.Core.EnterpriseComponents
{
	public class DatabaseInterfaceException : EnterpriseException
	{
		// default constructor
		public DatabaseInterfaceException()
		{
		}

		// Constructor accepting a single string message
		public DatabaseInterfaceException (string message) : base(message)
		{
		}
   
		// Constructor accepting a string message and an 
		// inner exception which will be wrapped by this 
		// custom exception class
		public DatabaseInterfaceException(string message, 
			Exception inner) : base(message, inner)
		{
		}

		//		public DatabaseInterfaceException(string message, 
		//			Exception inner, MethodBase mBase, DataParameters[] parameters) : 
		//			base(message, inner, mBase, parameters)
		//		{
		//		}
	}

	public struct DataParameters
	{
		public string ParameterName;
		public DbType DataType;
		public int Size;
		public object Value;
		public ParameterDirection ParamDirection;
		public string SourceColumn;
	}
		
	/// <summary>
	/// Enumeration of available database providers.
	/// </summary>
	public enum DataProviders
	{
		/// <summary>
		/// For SQL Server providers.
		/// </summary>
		SqlServer, 
		/// <summary>
		/// For Oracle providers.
		/// </summary>
		Oracle, 
		/// <summary>
		/// For ODBC providers.
		/// </summary>
		ODBC, 
		/// <summary>
		/// For OleDb providers.
		/// </summary>
		OleDb,
		/// <summary>
		/// For unidentified providers.
		/// </summary>
		Undefined
	}

	/// <summary>
	/// A provider independent database interface.
	/// </summary>
	public sealed class DatabaseInterface
	{
		private IDbTransaction	currentTransaction;
		private IDbCommand		currentCommand;
		private IDbConnection	currentConnection;
		private CommandBehavior	currentCommandBehavior;
		private IDbDataAdapter	currentDataAdapter;
		private string predefinedConnectionString = "";
		private string predefinedDataProvider = "";
		
		#region Data Independant Object Creation
		
		#region Initializing connections
		
		/// <summary>
		/// Gets a connection object to the database.
		/// The database provider and connection string are picked up from the configuration file.
		/// </summary>
		/// <returns>A connection object.</returns>
		public IDbConnection GetConnection()
		{
			DataProviders provider;// variable declaration
			provider = GetDataProvider();// get the current data provider

			// check if the data provider was specified
			if(provider == DataProviders.Undefined)
				throw new Exception();// To do: change to custom exception data provider was not specified

			return GetConnection(provider);
		}

		/// <summary>
		/// Gets a connection object to the database.
		/// The connection string is picked up from the configuration file.
		/// </summary>
		/// <param name="provider"></param>
		/// <returns>A connection object.</returns>
		public  IDbConnection GetConnection(DataProviders provider)
		{
			string connectionString;// variable declaration
			connectionString = GetConnectionString();// get the connection string from the configuration file

			return GetConnection(connectionString, provider);
		}

		/// <summary>
		/// Gets a connection object to the database.
		/// The data provider is picked up from the configuration file.
		/// </summary>
		/// <param name="connectionString"></param>
		/// <returns>A connection object.</returns>
		public  IDbConnection GetConnection(string connectionString)
		{
			DataProviders provider;// variable declaration
			provider = GetDataProvider();// get the current data provider

			// check if the data provider was specified
			if(provider == DataProviders.Undefined)
				throw new Exception();// To do: change to custom exception data provider was not specified

			return GetConnection(connectionString, provider);
		}

		/// <summary>
		/// Gets a connection object to the database based on a specific connection string and provider.
		/// </summary>
		/// <param name="provider"></param>
		/// <param name="connectionString"></param>
		/// <returns>A connection object.</returns>
		public  IDbConnection GetConnection(string connectionString, 
			DataProviders provider)
		{
			// create a connection object based on the data provider
			switch(provider)
			{
				case DataProviders.ODBC:
					// currentConnection = new ODBCConnection(connectionString);
					break;
				case DataProviders.OleDb:
					currentConnection = new OleDbConnection(connectionString);
					break;
				case DataProviders.Oracle:
					// currentConnection = new OracleConnection(connectionString);
					break;
				case DataProviders.SqlServer:
					currentConnection = new SqlConnection(connectionString);
					break;
				default:
					// To do: change to custom exception
					// data provider was not specified
					throw new Exception();
			}

			return currentConnection;
		}

		#endregion Initializing connections

		#region Initializing command objects

		/// <summary>
		/// 
		/// </summary>
		/// <param name="commandText"></param>
		/// <param name="typeOfCommand"></param>
		/// <returns></returns>
		public  IDbCommand GetCommand(string commandText, 
			CommandType typeOfCommand)
		{
			DataProviders provider; 
			provider = GetDataProvider(); // get the current data provider

			// check if the data provider was specified
			if(provider == DataProviders.Undefined)
				throw new Exception();// To do: change to custom exception data provider was not specified

			return GetCommand(commandText, provider, typeOfCommand);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="commandText"></param>
		/// <param name="provider"></param>
		/// <param name="typeOfCommand"></param>
		/// <returns></returns>
		public  IDbCommand GetCommand(string commandText, 
			DataProviders provider, CommandType typeOfCommand)
		{
			// make sure the connection object was initialized
			if(currentConnection == null)
			{
				currentConnection = GetConnection();
			}
			
			return GetCommand(commandText, provider, currentConnection, 
				typeOfCommand);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="commandText"></param>
		/// <param name="provider"></param>
		/// <param name="connectionObject"></param>
		/// <param name="typeOfCommand"></param>
		/// <returns></returns>
		public  IDbCommand GetCommand(string commandText, DataProviders provider,
			IDbConnection connectionObject, CommandType typeOfCommand)
		{
			
			if(connectionObject == null)// make sure the connection object was initialized
				throw new Exception();	// to do: change to custom exception
			
			currentConnection = connectionObject;
			switch(provider)// create a command object based on the data provider
			{
				case DataProviders.ODBC:
					// currentCommand = new SqlCommand(commandText, currentConnection);
					break;
				case DataProviders.OleDb:
					currentCommand = new OleDbCommand(commandText, (OleDbConnection) currentConnection);
					break;
				case DataProviders.Oracle:
					// currentCommand = new SqlCommand(commandText, currentConnection);
					break;
				case DataProviders.SqlServer:
					currentCommand = new SqlCommand(commandText, (SqlConnection) currentConnection);
					break;
				default:
					// To do: change to custom exception
					// data provider was not specified
					throw new Exception();
			}

			// set the type of command
			currentCommand.CommandType = typeOfCommand;
			currentCommand.CommandTimeout = 200;
			return currentCommand;
		}

		public  IDbCommand GetCommand(string commandText, DataProviders provider,
			IDbConnection connectionObject, CommandType typeOfCommand, 
			DataParameters[] parameters)
		{
			// get the command object
			currentCommand = GetCommand(commandText, provider, connectionObject, typeOfCommand);

			// add the parameters to the command
			AddCommandParameters(currentCommand, parameters, provider);
			
			return currentCommand;
		}

		/// <summary>
		/// Update the Data Parameters with the values updated in the command parameters
		/// </summary>
		/// <param name="dataParams"></param>
		private void UpdateCommandParameters(DataParameters[] dataParams)
		{
			int count = 0;

			foreach(IDataParameter param in currentCommand.Parameters)
			{
				// check if the parameter is an output parameter or a return value
				if(param.Direction == ParameterDirection.Output || 
					param.Direction == ParameterDirection.InputOutput ||
					param.Direction == ParameterDirection.ReturnValue)

					dataParams[count].Value = param.Value;// update the data parameter value
				
				count++;
			}
		}

		/// <summary>
		/// Convert the DataParameters to a specific command parameter
		/// </summary>
		/// <param name="command"></param>
		/// <param name="parameters"></param>
		/// <param name="provider"></param>
		public void AddCommandParameters(IDbCommand command, 
			DataParameters[] parameters, DataProviders provider)
		{
			switch(provider)
			{
				case DataProviders.ODBC:
/*					foreach(DataParameters param in parameters)
					{
					}*/
					break;
				case DataProviders.OleDb:
					foreach(DataParameters param in parameters)
					{
						OleDbParameter tempParam = new OleDbParameter();
						
						tempParam.ParameterName = param.ParameterName;
						tempParam.DbType = param.DataType;
						tempParam.Size = param.Size;
						tempParam.Direction = param.ParamDirection;
						tempParam.SourceColumn = param.SourceColumn;
						tempParam.Value = param.Value;

						command.Parameters.Add(tempParam);
					}
					break;
				case DataProviders.Oracle:
					/* foreach(DataParameters param in parameters)
					{
					} */
					break;
				case DataProviders.SqlServer:
					foreach(DataParameters param in parameters)
					{
						SqlParameter tempParam = new SqlParameter();
						
						tempParam.ParameterName = param.ParameterName;
						tempParam.DbType = param.DataType;
						tempParam.Size = param.Size;
						tempParam.Direction = param.ParamDirection;
						tempParam.SourceColumn = param.SourceColumn;
						tempParam.Value = param.Value;

						command.Parameters.Add(tempParam);
					}
					break;
				default:
					throw new Exception();// To do: change to custom exception data provider was not specified
			}
		}
		
		public DataParameters CreateParam(DbType dbType, ParameterDirection paramDirection, string paramName, Object paramValue)
		{
			DataParameters parameter = new DataParameters();
			
			parameter.DataType = dbType;
			parameter.ParameterName = paramName;
			parameter.ParamDirection = paramDirection;
			parameter.Value = paramValue;
			
			return parameter;
		}

		#endregion Initializing command objects

		#region Initializing Data Adapter objects

		/// <summary>
		/// 
		/// </summary>
		/// <param name="commandObject"></param>
		/// <returns></returns>
		public IDbDataAdapter GetDataAdapter(IDbCommand commandObject)
		{
			DataProviders provider;// variable declaration
			provider = GetDataProvider();// get the current data provider
			if(provider == DataProviders.Undefined)// check if the data provider was specified
				throw new Exception();// To do: change to custom exception data provider was not specified

			return GetDataAdapter(commandObject, provider);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="commandObject"></param>
		/// <param name="provider"></param>
		/// <returns></returns>
		public  IDbDataAdapter GetDataAdapter(IDbCommand commandObject, 
			DataProviders provider)
		{
			// make sure the command object has been initialized
			if(commandObject == null)
			{
				// to do: change to custom exception
				throw new Exception();
			}

			// create a data adapter object based on the data provider
			switch(provider)
			{
				case DataProviders.ODBC:
					// currentDataAdapter = new OleDbDataAdapter(commandObject);
					break;
				case DataProviders.OleDb:
					currentDataAdapter = new OleDbDataAdapter((OleDbCommand) commandObject);
					break;
				case DataProviders.Oracle:
					// currentDataAdapter = new OleDbDataAdapter(commandObject);
					break;
				case DataProviders.SqlServer:
					currentDataAdapter = new SqlDataAdapter((SqlCommand)commandObject);
					break;
				default:
					// To do: change to custom exception
					// data provider was not specified
					throw new Exception();
			}

			return currentDataAdapter;
		}

		#endregion Initializing Data Adapter objects

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public  IDataParameter GetParameter()
		{
			return null;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="provider"></param>
		/// <returns></returns>
		public  IDataParameter GetParameter(DataProviders provider)
		{
			return null;
		}

		#endregion Data Independant Object Creation

		#region Settings

		/// <summary>
		/// 
		/// </summary>
		/// <returns>The connection string</returns>
		public  string GetConnectionString()
		{
			
			string temp = string.Empty;
			if(predefinedConnectionString != "") {
				temp = ConfigurationSettings.AppSettings[predefinedConnectionString];
				if(temp == null) {
					temp = predefinedConnectionString;
				}
			} else {
				temp = ConfigurationSettings.AppSettings["ConnectionString"];
			}
			
			// check if the connection string was provided
			if(temp == string.Empty)
			{
				// To do: change to custom exception
				throw new Exception();
			}

			return temp;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>The data provider name.</returns>
		public  string GetDataProviderName()
		{
			string temp = string.Empty;

			if(predefinedDataProvider != "") {
				temp = ConfigurationSettings.AppSettings[predefinedDataProvider];
				if(temp == null) {
					temp = predefinedDataProvider;
				}
			} else {
				// get the data provider name from the configuration file
				temp = ConfigurationSettings.AppSettings["DataProvider"];
			}
			
			// check if the connection string was provided
			if(temp == string.Empty)
			{
				// To do: change to custom exception
				throw new Exception();
			}

			return temp;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public  DataProviders GetDataProvider()
		{
			// initialize the data provider
			DataProviders tempDataProvider = DataProviders.Undefined;
			
			// get the data provider name
			string temp = GetDataProviderName();

			// set the data provider
			switch(temp.ToLower())
			{
				case "sql":
					tempDataProvider = DataProviders.SqlServer;
					break;
				case "oracle":
					tempDataProvider = DataProviders.Oracle;
					break;
				case "odbc":
					tempDataProvider = DataProviders.ODBC;
					break;
				case "oledb":
					tempDataProvider = DataProviders.OleDb;
					break;
				default:
					tempDataProvider = DataProviders.Undefined;
					break;
			}

			return tempDataProvider;
		}

		#endregion Settings

		#region attributes

		public string PredefinedConnectionString {
			set { predefinedConnectionString = value; }
			get { return predefinedConnectionString; }
		}

		public IDbTransaction transaction
		{
			get
			{
				return currentTransaction;
			}
		}
		
		#endregion attributes

		#region constructors and desctructors

		public DatabaseInterface()
		{
		}

		public DatabaseInterface(string _predefinedConnectionString, string _predefinedDataProvider) {
			predefinedConnectionString = _predefinedConnectionString;
			predefinedDataProvider = _predefinedDataProvider;
		}

		// clean up and dispose of any unused objects
		~DatabaseInterface()
		{
			try 
			{
				if(currentCommand != null)
					currentCommand.Dispose();// clean up the command object
			}
			catch {}
			
			try 
			{
				if(currentConnection != null)
					currentConnection.Close();// close the connection and return it to the pool
			}
			catch {}
		}

		#endregion constructors and desctructors

		#region Execute DataSet

		#region Supporting Transactions
		
		public  DataSet ExecuteFetchDataSet(IDbConnection connection,
			CommandType dataCommandType, ref IDbTransaction transaction, 
			string commandText, DataParameters[] parameters)
		{
			DataProviders provider;
			provider = GetDataProvider();// get the data provider

			return ExecuteFetchDataSet(connection, dataCommandType, ref transaction, commandText, parameters, provider);
		}
		
		public  DataSet ExecuteFetchDataSet(IDbConnection connection,
			CommandType dataCommandType, ref IDbTransaction transaction, string commandText, 
			DataParameters[] parameters, DataProviders provider)
		{
			DataSet tempDataSet = new DataSet();

			// get command
			if(parameters != null)
				currentCommand = GetCommand(commandText, provider, connection, dataCommandType, parameters);
			else
				currentCommand = GetCommand(commandText, provider, connection, dataCommandType);

			try
			{
				if(transaction == null)
					transaction = currentConnection.BeginTransaction();
				
				currentCommand.Transaction = currentTransaction = transaction;

				currentDataAdapter = GetDataAdapter(currentCommand);// initialize the data adapter using the current command object
				currentDataAdapter.Fill(tempDataSet);				// execute the command and fill the data set
				
				UpdateCommandParameters(parameters);
				//currentConnection.Close();							// close the connection
			}
			catch(Exception e)
			{
				if(currentCommand != null) {
					currentCommand.Dispose();
				}

				if(currentConnection != null) {
					currentConnection.Close();
				}

				throw new DatabaseException(e.Message, e, connection, transaction, commandText, parameters, provider);
				// throw new DatabaseInterfaceException(e.Message, e);
				//				throw new DatabaseInterfaceException(e.Message, e, 
				//					MethodBase.GetCurrentMethod(), parameters);
			}
			
			return tempDataSet;
		}

		#endregion Supporting Transactions

		#region Not Supporting Transactions

		public  DataSet ExecuteFetchDataSet(string connectionString,
			CommandType dataCommandType, string commandText, 
			DataParameters[] parameters)
		{
			currentConnection = GetConnection(connectionString);// initialize the connection
			return ExecuteFetchDataSet(currentConnection, dataCommandType, commandText, parameters);
		}


		public  DataSet ExecuteFetchDataSet(IDbConnection connection,
			CommandType dataCommandType, string commandText, 
			DataParameters[] parameters)
		{
			DataProviders provider;

			// get the data provider
			provider = GetDataProvider();

			return ExecuteFetchDataSet(connection, dataCommandType, commandText, parameters, provider);
		}
			

		public  DataSet ExecuteFetchDataSet(string connection,
			CommandType dataCommandType, string commandText, 
			DataParameters[] parameters, DataProviders provider) 
		{
			// initialize the connection
			currentConnection = GetConnection(connection);
			return ExecuteFetchDataSet(currentConnection, dataCommandType, commandText, parameters, provider);
		}


		public  DataSet ExecuteFetchDataSet(IDbConnection connection,
			CommandType dataCommandType, string commandText, 
			DataParameters[] parameters, DataProviders provider)
		{
			DataSet tempDataSet = new DataSet();

			// get command
			if(parameters != null)
				currentCommand = GetCommand(commandText, provider, connection, dataCommandType, parameters);
			else
				currentCommand = GetCommand(commandText, provider, connection, dataCommandType);

			// get the data adapter
			try
			{
				currentDataAdapter = GetDataAdapter(currentCommand);// initialize the data adapter using the current command object
				currentDataAdapter.Fill(tempDataSet);				// execute the command and fill the data set
				UpdateCommandParameters(parameters);
				currentConnection.Close();							// close the connection
			}
			catch(Exception e)
			{
				if(currentCommand != null) {
					currentCommand.Dispose();
				}

				if(currentConnection != null) {
					currentConnection.Close();
				}

				throw new DatabaseException(e.Message, e, connection, dataCommandType, transaction, commandText, parameters, provider);
				//throw new DatabaseInterfaceException(e.Message, e);
				//				throw new DatabaseInterfaceException(e.Message, e, 
				//					MethodBase.GetCurrentMethod(), parameters);
			}
			
			return tempDataSet;
		}
		
		public  DataSet ExecuteFetchDataSet(CommandType dataCommandType, 
			string commandText, DataParameters[] parameters)
		{
			currentConnection = GetConnection();// initialize the connection
			return ExecuteFetchDataSet(currentConnection, dataCommandType, commandText, parameters);
		}

		/// <summary>
		/// Default command type set to stored procedure.
		/// </summary>
		/// <param name="commandText"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public  DataSet ExecuteFetchDataSet(string commandText, DataParameters[] parameters)
		{
			return ExecuteFetchDataSet(CommandType.StoredProcedure, commandText, parameters);
		}
		
		public DataTable ExecuteFetchDataTable(string commandText, DataParameters[] parameters)
		{
			DataSet tempDS;
			tempDS = ExecuteFetchDataSet(CommandType.StoredProcedure, commandText, parameters);

			if(tempDS != null && tempDS.Tables.Count > 0)
				return tempDS.Tables[0];
			else
				return null;
		}

		public  DataTable ExecuteFetchDataTable(string commandText, CommandType commandType, DataParameters[] parameters)
		{
            try
            {
            
            
            DataSet tempDS;
			tempDS = ExecuteFetchDataSet(commandType, commandText, parameters);

			if(tempDS != null && tempDS.Tables.Count > 0)
				return tempDS.Tables[0];
			else
				return null;

        }
        catch (Exception x)
        {
            int a = 1;
             throw x;

        }
		}

		public  DataTable ExecuteFetchDataTable(string commandText)
		{
			DataSet tempDS;
			tempDS = ExecuteFetchDataSet(CommandType.StoredProcedure, commandText, null);

			if(tempDS != null && tempDS.Tables.Count > 0)
				return tempDS.Tables[0];
			else
				return null;

		}

		public  DataTable ExecuteFetchDataTable(string commandText, DataParameters[] parameters,
			string tableName)
		{
			DataSet tempDS;
			tempDS = ExecuteFetchDataSet(CommandType.StoredProcedure, commandText, parameters);

			if(tempDS != null && tempDS.Tables.IndexOf(tableName) > -1)
				return tempDS.Tables[tableName];
			else
				return null;
		}

		#endregion NotSupporting Transactions

		#endregion Execute DataSet

		#region Execute DataReader

		public IDataReader ExecuteReader()
		{
			return null;
		}

		#endregion Execute DataReader

		#region Execute Scalar
		#region Not Supporting Transactions
		/// <summary>
		/// 
		/// </summary>
		/// <param name="connectionString"></param>
		/// <param name="dataCommandType"></param>
		/// <param name="commandText"></param>
		/// <param name="parameters"></param>
		/// <param name="dataProvider"></param>
		/// <returns></returns>
		public object ExecuteScalar(string connectionString,
			CommandType dataCommandType, string commandText, 
			DataParameters[] parameters, DataProviders dataProvider)
		{
			object returnData;

			// initialize the connection
			currentConnection = GetConnection(connectionString);

			try
			{
				// create the command object
				if(parameters == null)
				{
					currentCommand = GetCommand(commandText, dataProvider,
					currentConnection, dataCommandType);
				}
				else
				{
					currentCommand = GetCommand(commandText, dataProvider,
					currentConnection, dataCommandType, parameters);
				}

				currentConnection.Open();// open the connection
				returnData = currentCommand.ExecuteScalar();// execute the query
				UpdateCommandParameters(parameters);
				currentConnection.Close();// close the connection
			}
			catch(Exception e)
			{
				currentCommand.Dispose();
				currentConnection.Close();

				throw new DatabaseException(e.Message, e, connectionString, "", dataCommandType.ToString(), transaction, commandText, parameters, dataProvider);
				//throw new DatabaseInterfaceException(e.Message, e);
				//				throw new DatabaseInterfaceException(e.Message, e, 
				//					MethodBase.GetCurrentMethod(), parameters);
			}
			finally
			{
			}

			return returnData;
		}

		public object ExecuteScalar(CommandType dataCommandType, string commandText, 
			DataParameters[] parameters, DataProviders dataProvider)
		{
			object returnData = null;
			string connectionString;

			connectionString = GetConnectionString();// get the connection string
			currentConnection = GetConnection(connectionString);// initialize the connection

			try
			{
				// create the command object
				if(parameters == null)
				{
					currentCommand = GetCommand(commandText, dataProvider,
					currentConnection, dataCommandType);
				}
				else
				{
					currentCommand = GetCommand(commandText, dataProvider,
					currentConnection, dataCommandType, parameters);
				}

				// open the connection
				currentConnection.Open();

				// execute the query
				returnData = currentCommand.ExecuteScalar();

				UpdateCommandParameters(parameters);

				// close the connection
				currentConnection.Close();
			}
			catch(Exception e)
			{
				currentCommand.Dispose();
				currentConnection.Close();

				throw new DatabaseException(e.Message, e, connectionString, "", dataCommandType.ToString(), transaction, commandText, parameters, dataProvider);
				//throw new DatabaseInterfaceException(e.Message, e);
				//				throw new DatabaseInterfaceException(e.Message, e, 
				//					MethodBase.GetCurrentMethod(), parameters);
			}
			finally
			{
			}

			return returnData;
		}

		public object ExecuteScalar(CommandType dataCommandType, string commandText, 
			DataParameters[] parameters)
		{
			object returnData;
			string connectionString;
			DataProviders provider;

			connectionString = GetConnectionString(); // get the connection string
			currentConnection = GetConnection(connectionString); // initialize the connection
			provider = GetDataProvider(); // get the data provider

			// check if the data provider was specified
			if(provider == DataProviders.Undefined)
			{
				// throw warning, default set to SQL server
				provider = DataProviders.SqlServer;
				LoggingSystem.LogWarning("Data Provider was not specified in the configuration file. Default set to Sql Server.");
			}

			try
			{
				// create the command object
				if(parameters == null)
				{
					currentCommand = GetCommand(commandText, provider,
					currentConnection, dataCommandType);
				}
				else
				{
					currentCommand = GetCommand(commandText, provider,
					currentConnection, dataCommandType, parameters);
				}

				currentConnection.Open();// open the connection
				returnData = currentCommand.ExecuteScalar();// execute the query

				UpdateCommandParameters(parameters);

				currentConnection.Close();// close the connection
			}
			catch(Exception e)
			{
				currentCommand.Dispose();
				currentConnection.Close();
	
				throw new DatabaseException(e.Message, e, connectionString, "", dataCommandType.ToString(), transaction, commandText, parameters, provider);
				//throw new DatabaseInterfaceException(e.Message, e);

				//				throw new DatabaseInterfaceException(e.Message, e, 
				//					MethodBase.GetCurrentMethod(), parameters);
			}
			finally
			{
			}

			return returnData;
		}

		/// <summary>
		/// The connection string is picked up from the config file.
		/// The provider is picked up dynamically from the config file.
		/// Default command type set to stored procedure.
		/// </summary>
		/// <param name="commandText"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public object ExecuteScalar(string commandText, DataParameters[] parameters)
		{
			string connectionString;
			DataProviders provider;

			// get the connection string
			connectionString = GetConnectionString();

			// initialize the connection
			currentConnection = GetConnection(connectionString);

			// get the data provider
			provider = GetDataProvider();

			// check if the data provider was specified
			if(provider == DataProviders.Undefined)
			{
				// To do: change to custom exception
				// data provider was not specified

				// throw warning, default set to SQL server
				provider = DataProviders.SqlServer;

				LoggingSystem.LogWarning("Data Provider was not specified in the configuration file. Default set to Sql Server.");
			}

			return ExecuteScalar(connectionString, CommandType.StoredProcedure, 
				commandText, parameters, provider);
		}
		#endregion Not Supporting Transactions

		#region Supporting Transactions
		public object ExecuteScalar(IDbConnection connection, CommandType dataCommandType, ref IDbTransaction transaction,
			string commandText, DataParameters[] parameters)
		{
			object returnData;
			DataProviders provider;

			this.currentConnection = connection; // get the connection string
			provider = GetDataProvider(); // get the data provider

			// check if the data provider was specified
			if(provider == DataProviders.Undefined)
				throw new EnterpriseException("Data provider undefined");

			try
			{
				if(parameters == null)
					currentCommand = GetCommand(commandText, provider, currentConnection, dataCommandType);
				
				else
					currentCommand = GetCommand(commandText, provider,currentConnection, dataCommandType, parameters);
				
				if(currentConnection.State != ConnectionState.Open)
					currentConnection.Open();		//open the connection
				
				if(transaction == null)
					transaction = currentConnection.BeginTransaction();
				
				currentCommand.Transaction = currentTransaction = transaction;
				returnData = currentCommand.ExecuteScalar();// execute the query
				UpdateCommandParameters(parameters);
			}
			catch(Exception e)
			{
				currentCommand.Dispose();
				currentConnection.Close();

				throw new DatabaseException(e.Message, e, connection, dataCommandType, transaction, commandText, parameters, provider);
				//throw new DatabaseInterfaceException(e.Message, e);

				//				throw new DatabaseInterfaceException(e.Message, e, 
				//					MethodBase.GetCurrentMethod(), parameters);
			}
			finally
			{
			}

			return returnData;
		}

		public object ExecuteScalar(IDbConnection connection, CommandType dataCommandType, ref IDbTransaction transaction,
			string commandText, DataParameters[] parameters, DataProviders dataProvider)
		{
			DataProviders provider;

			this.currentConnection = connection; // get the connection string
			provider = dataProvider;

			return ExecuteScalar(connection, dataCommandType, ref transaction, commandText, parameters);
		}

		#endregion Supporting Transactions

		#endregion Execute Scalar

		#region Execute Non Query

		#region Supporting Transactions

		/// <summary>
		/// 
		/// </summary>
		/// <param name="connectionString"></param>
		/// <param name="dataCommandType"></param>
		/// <param name="transaction"></param>
		/// <param name="commandText"></param>
		/// <param name="parameters"></param>
		/// <param name="dataProvider"></param>
		/// <returns></returns>

		public int ExecuteNonQuery(IDbConnection connection,
			CommandType dataCommandType, ref IDbTransaction transaction, 
			string commandText, DataParameters[] parameters, 
			DataProviders dataProvider)
		{
			int numberOfRowsAffected;
			currentConnection = connection;

			try
			{
				if(currentConnection.State == ConnectionState.Closed)
					currentConnection.Open();// open the connection

				if(transaction == null)
					transaction = currentConnection.BeginTransaction();

				currentCommand = GetCommand(commandText, dataProvider, currentConnection, dataCommandType, parameters);// create the command object
				currentCommand.Transaction = currentTransaction = transaction;
				numberOfRowsAffected = currentCommand.ExecuteNonQuery();// execute the query
				
				UpdateCommandParameters(parameters);
			}
			catch(Exception e)
			{
				throw new DatabaseException(e.Message, e, connection, dataCommandType, transaction, commandText, parameters, dataProvider);
				//throw new DatabaseInterfaceException(e.Message, e);
			}

			return numberOfRowsAffected;
		}

		public int ExecuteNonQuery(IDbConnection connection,
			CommandType dataCommandType, IDbTransaction transaction, 
			string commandText, DataParameters[] parameters)
		{
			DataProviders provider = GetDataProvider();	// get provider
			if(provider == DataProviders.Undefined)		// check if the data provider was specified
				throw new Exception();

			return ExecuteNonQuery(connection, dataCommandType, ref transaction, commandText, parameters, provider);
		}

		#endregion Supporting Transactions

		#region Not Supporting Transactions

		/// <summary>
		/// Execute non query. Generic function.
		/// </summary>
		/// <param name="connectionString"></param>
		/// <param name="dataCommandType"></param>
		/// <param name="commandText"></param>
		/// <param name="parameters"></param>
		/// <param name="dataProvider"></param>
		/// <returns></returns>
		public int ExecuteNonQuery(string connectionString,
			CommandType dataCommandType, string commandText, 
			DataParameters[] parameters, DataProviders dataProvider)
		{
			int numberOfRowsAffected;

			// initialize the connection
			currentConnection = GetConnection(connectionString);

			try
			{
				// create the command object
				if(parameters == null)
				{
					currentCommand = GetCommand(commandText, dataProvider,
						currentConnection, dataCommandType);
				}
				else
				{
					currentCommand = GetCommand(commandText, dataProvider,
						currentConnection, dataCommandType, parameters);
				}

				currentConnection.Open();// open the connection
				numberOfRowsAffected = currentCommand.ExecuteNonQuery();// execute the query
				UpdateCommandParameters(parameters);				
				currentConnection.Close();// close the connection
			}
			catch(Exception e)
			{
				currentCommand.Dispose();
				currentConnection.Close();

				throw new DatabaseException(e.Message, e, connectionString, "", dataCommandType.ToString(), transaction, commandText, parameters, dataProvider);
				//throw new DatabaseInterfaceException(e.Message, e);
				//				throw new DatabaseInterfaceException(e.Message, e, 
				//					MethodBase.GetCurrentMethod(), parameters);
			}
			finally
			{
			}

			return numberOfRowsAffected;
		}

		/// <summary>
		/// 
		/// The connection string is picked up from the config file.
		/// </summary>
		/// <param name="dataCommandType"></param>
		/// <param name="commandText"></param>
		/// <param name="parameters"></param>
		/// <param name="dataProvider"></param>
		/// <returns></returns>
		public int ExecuteNonQuery(CommandType dataCommandType, string commandText, 
			DataParameters[] parameters, DataProviders dataProvider)
		{
			string connectionString;

			// get the connection string
			connectionString = GetConnectionString();

			return ExecuteNonQuery(connectionString, dataCommandType, commandText,
				parameters, dataProvider);
		}

		/// <summary>
		/// The connection string is picked up from the config file.
		/// The provider is picked up dynamically from the config file.
		/// </summary>
		/// <param name="dataCommandType"></param>
		/// <param name="commandText"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public int ExecuteNonQuery(CommandType dataCommandType, string commandText, 
			DataParameters[] parameters)
		{
			string connectionString;
			
			// get provider
			DataProviders provider = GetDataProvider();

			// check if the data provider was specified
			if(provider == DataProviders.Undefined)
			{
				// To do: change to custom exception
				// data provider was not specified
				throw new Exception();
			}

			// get the connection string
			connectionString = GetConnectionString();

			return ExecuteNonQuery(connectionString, dataCommandType, commandText,
				parameters, provider);
		}

		/// <summary>
		/// The connection string is picked up from the config file.
		/// The provider is picked up dynamically from the config file.
		/// Default command type set to stored procedure.
		/// </summary>
		/// <param name="commandText"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public int ExecuteNonQuery(string commandText, DataParameters[] parameters)
		{
			string connectionString;
			
			// get provider
			DataProviders provider = GetDataProvider();

			// check if the data provider was specified
			if(provider == DataProviders.Undefined)
			{
				// To do: change to custom exception
				// data provider was not specified
				throw new Exception();
			}

			// get the connection string
			connectionString = GetConnectionString();

			return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, 
				commandText, parameters, provider);
		}

		#endregion Not Supporting Transactions

		#endregion Execute Non Query
	}
}