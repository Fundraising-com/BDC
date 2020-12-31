using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Runtime.InteropServices;
using System.Reflection;
using System.ComponentModel;
using QSPFulfillment.DataAccess.Common;
namespace QSPFulfillment.DataAccess.Data
{

	internal enum DataBaseName {QSPCanadaOrderManagement,QSPCanadaFinance,QSPCanadaCommon,QSPCanadaOriginalData,QSPCanadaProduct};
	/// <summary>
	/// Purpose: Abstract base class for Database Interaction classes.
	/// </summary>
	[Serializable()]
	public abstract class DBInteractionBase 
	{
		//This parameter is in almost SP odf the system
		#region Class Member Declarations
		public		SqlConnection			_mainConnection;
		protected	Int32				    _errorCode;
		protected  	bool					_mainConnectionIsCreatedLocal;
		protected  	ConnectionProvider	    _mainConnectionProvider;
		protected	bool					_isDisposed;
		#endregion
		public static string ConnStrWithNoDataBase = DataAccess.Common.ApplicationConfiguration.ConnectionString;
		public static String ConnStr ="";
		public static int CommandTimeout;
		public static string ConnnectionString
		{
			set{ConnStr=value;}
		}
		public DBInteractionBase()
		{
			int iTimeOutIndex;
			int iTimeOutEndIndex;

			_mainConnection = new SqlConnection();
			_mainConnectionIsCreatedLocal = true;
			_mainConnectionProvider = null;
			DataBase = DataBaseName.QSPCanadaOrderManagement;
			_mainConnection.ConnectionString = ConnStr;
			_errorCode = 0;
			_isDisposed = false;

			//grab TimeOut setting from Web.Config
			iTimeOutIndex = DataAccess.Common.ApplicationConfiguration.ConnectionString.IndexOf("Timeout=") + 8;
			iTimeOutEndIndex = DataAccess.Common.ApplicationConfiguration.ConnectionString.IndexOf(";", iTimeOutIndex);
			CommandTimeout = Convert.ToInt32(DataAccess.Common.ApplicationConfiguration.ConnectionString.Substring(iTimeOutIndex, iTimeOutEndIndex - iTimeOutIndex));
		}
		/// <summary>
		/// Execute Update, Insert, Delete Action into BD
		/// </summary>
		/// <param name="Command">command with parameter already set with values</param>
		protected int ExecuteCmd(SqlCommand Command)
		{
			int NbRecAff = 0;	
			try
			{	
				if(_mainConnectionIsCreatedLocal)
				{
					Command.Connection = _mainConnection;
					_mainConnection.Open();
				}
				else
				{
					if(_mainConnectionProvider.IsTransactionPending)
					{
						Command.Connection = _mainConnectionProvider.DBConnection;
						Command.Transaction = _mainConnectionProvider.CurrentTransaction;
					}
				}
				Command.CommandTimeout = CommandTimeout;

				NbRecAff = Command.ExecuteNonQuery();	
			}
			catch(SqlException ex)
			{
				_errorCode = ex.Number;
				throw ex;
			}
			finally
			{
				if(_mainConnectionIsCreatedLocal)
				{
					// Close connection.
					if (_mainConnection.State != ConnectionState.Closed)
						_mainConnection.Close();
				}	
			}
			return NbRecAff;
		}
		/// <summary>
		/// Execute Update, Insert, Delete Action into BD
		/// </summary>
		/// <param name="Command">command with parameter already set with values</param>
		protected object ExecuteScalar(SqlCommand Command)
		{
			//int NbRecAff = 0;	
			object valuereturned;
			try
			{	
				if(_mainConnectionIsCreatedLocal)
				{
					Command.Connection = _mainConnection;
					_mainConnection.Open();
				}
				else
				{
					if(_mainConnectionProvider.IsTransactionPending)
					{
						Command.Connection = _mainConnectionProvider.DBConnection;
						Command.Transaction = _mainConnectionProvider.CurrentTransaction;
					}
				}
				Command.CommandTimeout = CommandTimeout;

				valuereturned= Command.ExecuteScalar();	
			}
			catch(SqlException ex)
			{
				_errorCode = ex.Number;
				throw ex;
			}
			finally
			{
				if(_mainConnectionIsCreatedLocal)
				{
					// Close connection.
					if (_mainConnection.State != ConnectionState.Closed)
						_mainConnection.Close();
				}	
			}
			return valuereturned;
		}
		/// <summary>
		/// Insert row row in the database
		/// </summary>
		/// /// <param name="Table">Table containing row to be Inserted</param>
		/// <param name="Command">command with value and param</param>
		/// <returns>nb row affected</returns>
		protected int Insert(SqlCommand Command, DataTable Table)
		{
			int NbRecAff = 0;
			SqlDataAdapter adapter = new SqlDataAdapter();
			adapter.InsertCommand = Command;
			try
			{	
				if(_mainConnectionIsCreatedLocal)
				{
					// Open connection.
					adapter.InsertCommand.Connection = _mainConnection;
					_mainConnection.Open();
				}
				else
				{
					if(_mainConnectionProvider.IsTransactionPending)
					{
						adapter.InsertCommand.Connection = _mainConnectionProvider.DBConnection;
						adapter.InsertCommand.Transaction = _mainConnectionProvider.CurrentTransaction;
					}
				}
				NbRecAff = adapter.Update(Table);
			}
			catch(SqlException ex)
			{
				_errorCode = ex.Number;
				throw ex;
			}
			finally
			{
				if(_mainConnectionIsCreatedLocal)
				{
					// Close connection.
					if (_mainConnection.State != ConnectionState.Closed)
						_mainConnection.Close();
				}
			}
			return NbRecAff;
		}
		/// <summary>
		/// Update row corresponding of row updated in the table in the database
		/// </summary>
		/// <param name="Command">Command to execute for Update row with param</param>
		/// <param name="Table">Table containing row to be Updated</param>
		/// <returns>nb row affected</returns>
		protected int Update(SqlCommand Command,DataTable Table)
		{
			int NbRecAff = 0;
			SqlDataAdapter adapter = new SqlDataAdapter();
			adapter.UpdateCommand = Command;
			try
			{	
				if(_mainConnectionIsCreatedLocal)
				{
					// Open connection.
					adapter.UpdateCommand.Connection = _mainConnection;
					_mainConnection.Open();
				}
				else
				{
					if(_mainConnectionProvider.IsTransactionPending)
					{
						adapter.UpdateCommand.Connection = _mainConnectionProvider.DBConnection;
						adapter.UpdateCommand.Transaction = _mainConnectionProvider.CurrentTransaction;
					}
				}
				// Execute query.
				NbRecAff = adapter.Update(Table);
			}
			catch(SqlException ex)
			{				
				_errorCode = ex.Number;
				throw ex;
			}
			finally
			{
				if(_mainConnectionIsCreatedLocal)
				{
					// Close connection.
					if (_mainConnection.State != ConnectionState.Closed)
						_mainConnection.Close();
				}
			}
			return NbRecAff;
		}
		/// <summary>
		/// Deleted row corresponding of row deleted in the table in the database
		/// </summary>
		/// <param name="Command">Command to execute for delete row with param</param>
		/// <param name="Table">Table containing row to be deleted</param>
		/// <returns>nb row affected</returns>
		protected int Delete(SqlCommand Command,DataTable Table)
		{
			int NbRecAff = 0;
			SqlDataAdapter adapter = new SqlDataAdapter();
			adapter.DeleteCommand = Command;
			try
			{	
				if(_mainConnectionIsCreatedLocal)
				{
					// Open connection.
					adapter.DeleteCommand.Connection = _mainConnection;
					_mainConnection.Open();
				}
				else
				{
					if(_mainConnectionProvider.IsTransactionPending)
					{
						adapter.DeleteCommand.Connection = _mainConnectionProvider.DBConnection;
						adapter.DeleteCommand.Transaction = _mainConnectionProvider.CurrentTransaction;
					}
				}
				// Execute query.
				NbRecAff = adapter.Update(Table.GetChanges(DataRowState.Deleted));	
			}
			catch(SqlException ex)
			{
				_errorCode = ex.Number;
				throw ex;
			}
			finally
			{
				if(_mainConnectionIsCreatedLocal)
				{
					// Close connection.
					if (_mainConnection.State != ConnectionState.Closed)
						_mainConnection.Close();
				}	
			}
			return NbRecAff;
		}
		/// <summary>
		/// Fill a table
		/// </summary>
		/// <param name="Command">Command to fill the table</param>
		/// <param name="Table">Table to be filled</param>
		protected void Select(SqlCommand Command,DataTable Table)
		{
			try
			{	
				if(_mainConnectionIsCreatedLocal)
				{
					// Open connection.
					Command.Connection = _mainConnection;
					_mainConnection.Open();
				}
				else
				{
					if(_mainConnectionProvider.IsTransactionPending)
					{
						Command.Connection = _mainConnectionProvider.DBConnection;
						Command.Transaction = _mainConnectionProvider.CurrentTransaction;
					}
				}
			
				Command.CommandTimeout = CommandTimeout;
				
				SqlDataAdapter sda = new SqlDataAdapter(Command);
				sda.Fill(Table);
			}
			catch(SqlException ex)
			{		
				_errorCode = ex.Number;
				throw ex;
			}
			finally
			{
				if(_mainConnectionIsCreatedLocal)
				{
					// Close connection.
					if (_mainConnection.State != ConnectionState.Closed)
						_mainConnection.Close();
					//TODO:REMOVE
					string ss;
					if(Command.Parameters.Contains("@Query") && Command.Parameters["@Query"].Value != null)
						ss = Command.Parameters["@Query"].Value.ToString();
				}
			}
		}
		/// <summary>
		/// Fill a DataSet
		/// </summary>
		/// <param name="Command">Command to fill the dataset</param>
		/// <param name="dts">DataSet to be filled</param>
		protected void Select(SqlCommand Command, DataSet dts)
		{
			try
			{	
				if(_mainConnectionIsCreatedLocal)
				{
					// Open connection.
					Command.Connection = _mainConnection;
					_mainConnection.Open();
				}
				else
				{
					if(_mainConnectionProvider.IsTransactionPending)
					{
						Command.Connection = _mainConnectionProvider.DBConnection;
						Command.Transaction = _mainConnectionProvider.CurrentTransaction;
					}
				}

				Command.CommandTimeout = CommandTimeout;

				SqlDataAdapter sda = new SqlDataAdapter(Command);
				sda.Fill(dts);
			}
			catch(SqlException ex)
			{	
				_errorCode = ex.Number;
				throw ex;
			}
			finally
			{
				if(_mainConnectionIsCreatedLocal)
				{
					// Close connection.
					if (_mainConnection.State != ConnectionState.Closed)
						_mainConnection.Close();
				}		
			}
		}
		/// <summary>
		/// Update table
		/// </summary>
		/// <param name="adapter">Adapter with Update, Delete and insert command</param>
		/// <param name="Table">Table to update</param>
		/// <returns>nb row affected</returns>
		protected int UpdateBatch(SqlDataAdapter adapter,DataTable Table)
		{
			int NbRecAff = 0;
			try
			{	
				if(_mainConnectionIsCreatedLocal)
				{
					// Open connection.
					adapter.InsertCommand.Connection = _mainConnection;
					adapter.UpdateCommand.Connection = _mainConnection;
					adapter.DeleteCommand.Connection = _mainConnection;
					_mainConnection.Open();
				}
				else
				{
					if(_mainConnectionProvider.IsTransactionPending)
					{
						adapter.InsertCommand.Connection = _mainConnectionProvider.DBConnection;
						adapter.InsertCommand.Transaction = _mainConnectionProvider.CurrentTransaction;
						adapter.UpdateCommand.Connection = _mainConnectionProvider.DBConnection;
						adapter.UpdateCommand.Transaction = _mainConnectionProvider.CurrentTransaction;
						adapter.DeleteCommand.Connection = _mainConnectionProvider.DBConnection;
						adapter.DeleteCommand.Transaction = _mainConnectionProvider.CurrentTransaction;
					}
				}
				// Execute query.
				NbRecAff = adapter.Update(Table);
			}
			catch(SqlException ex)
			{
				_errorCode = ex.Number;
				throw ex; 
			}
			finally
			{
				if(_mainConnectionIsCreatedLocal)
				{
					// Close connection.
					if (_mainConnection.State != ConnectionState.Closed)
						_mainConnection.Close();
				}	
			}
			return NbRecAff;
		}
		private void SetConnection(SqlCommand Command)
		{
			Command.Connection = _mainConnection;
		}
		/// <summary>
		/// 
		/// </summary>
		internal ConnectionProvider MainConnectionProvider
		{
			get 
			{
				ConnectionProvider connectionProvider = null;

				if(!_mainConnectionIsCreatedLocal) 
				{
					connectionProvider = _mainConnectionProvider;
				}

				return connectionProvider;
			}
			set
			{
				if(value==null)
				{
					// Reset the connection
					_mainConnection = new SqlConnection();
					_mainConnectionIsCreatedLocal = true;
					_mainConnectionProvider = null;
					_mainConnection.ConnectionString = ConnStr;
				} 
				else 
				{
					// A connection provider object is passed to this class.
					// Retrieve the SqlConnection object, if present and create a
					// reference to it. If there is already a MainConnection object
					// referenced by the membervar, destroy that one or simply 
					// remove the reference, based on the flag.
					if(_mainConnection!=null)
					{
						// First get rid of current connection object. Caller is responsible
						if(_mainConnectionIsCreatedLocal)
						{
							// Is local created object, close it and dispose it.
							_mainConnection.Close();
							_mainConnection.Dispose();
						}
						// Remove reference.
						_mainConnection = null;
					}
					_mainConnectionProvider = (ConnectionProvider)value;
					_mainConnection = _mainConnectionProvider.DBConnection;
					_mainConnectionIsCreatedLocal = false;
				}
			}
		}
		public Int32 ErrorCode
		{
			get
			{
				return _errorCode;
			}
		}
		internal DataBaseName DataBase
		{
			set
			{
				ConnStr = ConnStrWithNoDataBase.Replace("[DataBase]",value.ToString());
			
			}
			get
			{
				return DataBaseName.QSPCanadaOrderManagement; //do a switch statement(DataBaseName)_mainConnection.Database;
			
			}
		}
		protected void AddParameters(SqlCommand Command,ParameterValueList List)
		{
			foreach(ParameterValue pv in List)
			{			
				Command.Parameters.Add("@"+pv.Parameter,pv.Value);
			}
		}
	}
}