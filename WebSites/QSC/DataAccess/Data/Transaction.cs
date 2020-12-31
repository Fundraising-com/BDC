using System;
using System.Data;

namespace QSPFulfillment.DataAccess.Data
{
	/// <summary>
	/// Summary description for Transaction.
	/// </summary>
	public class Transaction
	{
		private TransactionItems _Items;
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="Items">TransactionItems</param>
		public Transaction(TransactionItems Items)
		{
			_Items = Items;
		}
		/// <summary>
		/// Make change on every table by starting by the first one in the list and as fallow
		/// Update, Insert or delete are going to be done
		/// </summary>
		/// <returns>True when all queries as affected one or more row</returns>
		/// <remarks>Transaction is rollback when querie don't modifie row on one of the table</remarks>
		public bool Execute()
		{
			bool result = false;
			QSPFulfillment.DataAccess.Data.ConnectionProvider connProvider = new QSPFulfillment.DataAccess.Data.ConnectionProvider();			
			try
			{		
				connProvider.OpenConnection();
				connProvider.BeginTransaction("Transactions");
				foreach(TransactionItem item in _Items)
				{
					int NbRecAff = 0;
					item.TableOperation.MainConnectionProvider= connProvider;
					NbRecAff = item.TableOperation.UpdateBatch(item.Table);
					if(NbRecAff == 0)
					{
						result = false;
						throw new Exception("Transaction abort");
					}
					else
					{
						result = true;
					}
				}
				if(result)
					connProvider.CommitTransaction();
			}
			catch
			{
				if (connProvider.DBConnection.State != ConnectionState.Closed)
					connProvider.RollbackTransaction("Transactions");
				result = false;
			}
			finally
			{
				if (connProvider.DBConnection.State != ConnectionState.Closed)
					connProvider.CloseConnection(false);
			}
			return result;
		}
	}
}