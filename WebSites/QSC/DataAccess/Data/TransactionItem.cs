using System;
using System.Data;

namespace QSPFulfillment.DataAccess.Data
{
	/// <summary>
	/// Summary description for TransactionItem.
	/// </summary>
	public class TransactionItem
	{
		private DataTable dtbTable;
		private DBTableOperation dtoTableOperation;
		public TransactionItem(DataTable Table,DBTableOperation TableOperation)
		{
			dtbTable = Table;
			dtoTableOperation = TableOperation;
		}
		public  DataTable Table
		{
			get{return dtbTable;}
		}
		public DBTableOperation TableOperation
		{
			get{return dtoTableOperation;}
		}
	}
}