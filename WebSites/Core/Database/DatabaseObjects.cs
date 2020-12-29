//
// Nov 30, 2004. Stephen Lim - New class.
// Dec 6, 2004. Stephen Lim - Add remarks to encourage using derived class.
// Dec 16, 2004. Jean-Francois Buist - Add abstract GetConnectionStringConfigKey() & GetDataProviderStringConfigKey().
// Dec 17, 2004. Fixed DatabaseInterface init.
//

using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using GA.BDC.Core.EnterpriseComponents;

namespace GA.BDC.Core.Database
{

	/// <summary>
	/// Easily access various database related tasks with transaction support.
	/// </summary>
	/// <remarks>Instance members are not thread-safe. 
	/// Transaction support is only safe for connections bound to the same physical server.
	/// Nested transaction is not supported.
	/// </remarks>
	/// <example>
	/// The following example uses transaction to insert into the email massmailer.
	/// A new instance of the class is first created.
	/// <code>
	/// DatabaseObjects dbo = new DatabaseObjects();
	/// dbo.Open();
	/// dbo.Begin();
	/// try 
	/// {
	///		dbo.InsertTouchSent(1, 1, 1); 
	///
	///		dbo.InsertTouchSent(2, 2, 2); 
	///		
	///		dbo.Commit();
	///	}
	///	catch (Exception e)
	///	{
	///		try {
	///			dbo.Rollback();
	///		}
	///		catch 
	///		{
	///			Console.WriteLine("Rollback failed.");
	///		}
	///	}
	///	dbo.Close();
	/// </code>
	/// The following example does not use transaction. Note the call to Begin is omitted.
	/// <code>
	/// DatabaseObjects dbto = new DatabaseObjects();
	/// dbo.Open();
	/// try 
	/// {
	///		dbo.InsertTouchSent(1, 1, 1); 
	///
	///		dbo.InsertTouchSent(2, 2, 2);
	///	}
	///	catch {}
	///	dbo.Close();
	/// </code>
	/// </example>
	/// <remarks>
	/// The connection string and provider will be loaded from the application config file
	/// if you do not specify the connection string and provider when calling the Open method.
	/// This class is not intended to be used directly. You should use derived classes instead.
	/// </remarks>
	public abstract class DatabaseObjects
	{
		#region Fields
		protected IDbTransaction currentTransaction;
		protected IDbConnection currentConnection;
		protected DatabaseInterface dbi;
		protected bool enableTransaction;
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor for the class.
		/// </summary>
		public DatabaseObjects()
		{
			dbi = null;
			currentConnection = null;
			currentTransaction = null;
			enableTransaction = false;
		}

		// Destructor
		~DatabaseObjects() 
		{
			// Rollback any pending transactions.
			try 
			{
				if (currentTransaction != null)
					currentTransaction.Rollback();
			}
			catch {}

			// Close connections.
			try 
			{
				if (currentConnection != null)
					currentConnection.Close();
			}
			catch {}
		}
		#endregion

		#region Abstract Methods
		protected abstract string GetConnectionStringConfigKey();
		protected abstract string GetDataProviderStringConfigKey();
		#endregion

		#region Methods

		protected void ExecuteScalarFromQuery(string query, string connectionString, string dataProvider, params string[] list) {
			if(list != null) {
				for(int i=0;i<list.Length;i++) {
					query = query.Replace("{" + i + "}", list[i]);
				}
			}

			try {
				DatabaseInterface dbi = new DatabaseInterface(connectionString, dataProvider);
				dbi.ExecuteScalar(CommandType.Text, query, null);
			} catch(Exception ex) {
				throw ex;
			}
		}

		protected void ExecuteNonQueryFromQuery(string query, string connectionString, string dataProvider, params string[] list) {
			if(list != null) {
				for(int i=0;i<list.Length;i++) {
					query = query.Replace("{" + i + "}", list[i]);
				}
			}

			try {
				DatabaseInterface dbi = new DatabaseInterface(connectionString, dataProvider);
				dbi.ExecuteNonQuery(CommandType.Text, query, null);
			} catch(Exception ex) {
				throw ex;
			}
		}

		protected DataTable GetDataTableFromQuery(string query, string connectionString, string dataProvider, params string[] list) {
			DataSet ds = GetDataSetFromQuery(query, connectionString, dataProvider, list);
			return ds.Tables[0];
		}

		protected DataSet GetDataSetFromQuery(string query, string connectionString, string dataProvider, params string[] list) {
			DataSet ds = null;
			
			if(list != null) {
				for(int i=0;i<list.Length;i++) {
					query = query.Replace("{" + i + "}", list[i]);
				}
			}

			try {
				DatabaseInterface dbi = new DatabaseInterface(connectionString, dataProvider);
				ds = dbi.ExecuteFetchDataSet(CommandType.Text, query, null);
			} catch(Exception ex) {
				throw ex;
			}
			return ds;
		}

		/// <summary>
		/// Create a new database connection for this instance.
		/// </summary>
		/// <exception cref="EnterpriseException">
		/// An unknown error occured.
		/// </exception>
		public void Open()
		{
			try 
			{
				dbi = new DatabaseInterface(GetConnectionStringConfigKey(), GetDataProviderStringConfigKey());
				currentConnection = dbi.GetConnection();
				currentConnection.Open();
			}
			catch (Exception e) 
			{
				throw new EnterpriseException(e.Message, e);
			}
		}

		/// <summary>
		/// Create a new database connection for this instance.
		/// </summary>
		/// <param name="provider">Database provider type for this connection.</param>
		/// <exception cref="EnterpriseException">
		/// An unknown error occured.
		/// </exception>
		public void Open(EnterpriseComponents.DataProviders provider)
		{
			try 
			{
				dbi = new DatabaseInterface(GetConnectionStringConfigKey(), "");
				currentConnection = dbi.GetConnection(provider);
				currentConnection.Open();
			}
			catch (Exception e) 
			{
				throw new EnterpriseException(e.Message, e);
			}
		}

		/// <summary>
		/// Create a new database connection for this instance.
		/// </summary>
		/// <param name="connectionString">Database connection string.</param>
		/// <param name="provider">Database provider type for this connection.</param>
		/// <exception cref="EnterpriseException">
		/// An unknown error occured.
		/// </exception>
		public void Open(string connectionString, EnterpriseComponents.DataProviders provider)
		{
			try 
			{
				dbi = new DatabaseInterface();
				currentConnection = dbi.GetConnection(connectionString, provider);
				currentConnection.Open();
			}
			catch (Exception e) 
			{
				throw new EnterpriseException(e.Message, e);
			}
		}

		/// <summary>
		/// Create a new database connection for this instance.
		/// </summary>
		/// <param name="connectionString">Database connection string.</param>
		/// <exception cref="EnterpriseException">
		/// An unknown error occured.
		/// </exception>
		public void Open(string connectionString)
		{
			try 
			{
				dbi = new DatabaseInterface("", GetDataProviderStringConfigKey());
				currentConnection = dbi.GetConnection(connectionString);
				currentConnection.Open();
			}
			catch (Exception e) 
			{
				throw new EnterpriseException(e.Message, e);
			}
		}

		/// <summary>
		/// Closes the existing database connection for this instance.
		/// </summary>
		/// <exception cref="EnterpriseException">
		/// An unknown error occured.
		/// </exception>
		public void Close()
		{
			// Always rollback transaction.
			try 
			{
				if (enableTransaction && currentTransaction != null)
					currentTransaction.Rollback();				
			}
			catch {}

			// Close connection.
			try 
			{
				currentConnection.Close();
			}
			catch {}

			enableTransaction = false;
		}

		/// <summary>
		/// Start transaction.
		/// </summary>
		public void Begin() 
		{
			enableTransaction = true;
		}

		/// <summary>
		/// Commit the current active transaction for this instance.
		/// </summary>
		/// <remarks>Nested transactions are not supported within the same instance.</remarks>
		/// <exception cref="EnterpriseException">
		/// An unknown error occured.
		/// </exception>
		public void Commit() 
		{
			try 
			{
				if (enableTransaction && currentTransaction != null)
					currentTransaction.Commit();
			}
			catch (Exception e) 
			{
				throw new EnterpriseException(e.Message, e);
			}
		}

		/// <summary>
		/// Rollback the current active transaction for this instance.
		/// </summary>
		/// <remarks>Nested transactions are not supported within the same instance.</remarks>
		/// <exception cref="EnterpriseException">
		/// An unknown error occured.
		/// </exception>
		public void Rollback() 
		{
			try 
			{
				if (enableTransaction && currentTransaction != null) 
					currentTransaction.Rollback();
			}
			catch (Exception e) 
			{
				throw new EnterpriseException(e.Message, e);
			}
		}

		#endregion Methods

		#region Properties

		/// <summary>
		/// Get the connection object for this instance.
		/// </summary>
		/// <value>The connection object that is used by this instance.</value>
		/// <remarks>The connection object may be null if no connection has been opened.</remarks>
		public IDbConnection Connection 
		{
			get 
			{
				return currentConnection;
			}
		}

		/// <summary>
		/// Get the transaction object for this instance.
		/// </summary>
		/// <value>The transaction object used by this instance.</value>
		/// <remarks>The transaction object may be null if there is no active transaction.</remarks>
		public IDbTransaction Transaction
		{
			get 
			{
				return currentTransaction;
			}
		}
		#endregion Properties
	}
}
