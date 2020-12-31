namespace Business.Objects
{
	using System;
	using System.Data;
	using Common;
	using DAL;
	/// <summary>
	///     
	/// </summary>
	public class Transaction
	{
		private ConnectionProvider oConnectionProvider;
		private string sTransactionName = "";
		private DataBaseName oDataBaseName = DataBaseName.QSPCanadaOrderManagement;

		public ConnectionProvider MainConnectionProvider 
		{
			get 
			{
				return oConnectionProvider;
			}
		}

		public string TransactionName 
		{
			get 
			{
				return sTransactionName;
			}
		}

		public DataBaseName DataBase 
		{
			get 
			{
				return oDataBaseName;
			}
			set 
			{
				oDataBaseName = value;
			}
		}

		public Transaction(string sTransactionName, DataBaseName DBName) 
		{
			this.sTransactionName = sTransactionName;
			this.DataBase = DBName;
		}

		public void Open() 
		{
			this.oConnectionProvider = new ConnectionProvider(DataBase);
			MainConnectionProvider.OpenConnection();
			MainConnectionProvider.BeginTransaction(sTransactionName);
		}

		public void Save() 
		{
			if(MainConnectionProvider.DBConnection.State != ConnectionState.Closed) 
			{
				MainConnectionProvider.CommitTransaction();
				MainConnectionProvider.CloseConnection(false);
			}
		}

		public void Cancel() 
		{
			if(MainConnectionProvider.DBConnection.State != ConnectionState.Closed) 
			{
				MainConnectionProvider.RollbackTransaction(TransactionName);
				MainConnectionProvider.CloseConnection(false);
			}
		}

		public void Close() 
		{
			if(MainConnectionProvider.DBConnection.State != ConnectionState.Closed) 
			{
				MainConnectionProvider.CloseConnection(false);
			}
		}
	}
}