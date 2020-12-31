using System;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using Common;
namespace DAL
{
	[Serializable()]
	public abstract class DBTableOperation: DBInteractionBase
	{
		private int iNbRowAffected  =0;
		protected SqlDataAdapter adapter;
		protected abstract SqlCommand GetInsertCommand();
		protected abstract SqlCommand GetDeleteCommand();
		protected abstract SqlCommand GetUpdateCommand();

		public DBTableOperation() : base() { }

		public DBTableOperation(DataBaseName DBName) : base(DBName) { }

		public bool Delete(DataSet dtsDataSet, string tableName)
		{
			iNbRowAffected = Delete(GetDeleteCommand(), dtsDataSet, tableName);
			return iNbRowAffected > 0;
		}
		public bool Insert(DataSet dtsDataSet, string tableName)
		{
			iNbRowAffected = Insert(GetInsertCommand(), dtsDataSet, tableName);
			return iNbRowAffected > 0;
		}
		public bool Update(DataSet dtsDataSet, string tableName)
		{
			iNbRowAffected = Update(GetUpdateCommand(), dtsDataSet, tableName);
			return iNbRowAffected > 0;
		}
		public bool UpdateBatch(DataSet dtsDataSet, string tableName)
		{
			adapter = new SqlDataAdapter();		
			adapter.InsertCommand = GetInsertCommand();
			adapter.UpdateCommand = GetUpdateCommand();
			adapter.DeleteCommand = GetDeleteCommand();
			iNbRowAffected = UpdateBatch(adapter, dtsDataSet, tableName);
			return iNbRowAffected > 0;
		}
		public int NbRowAffected
		{
			get{return iNbRowAffected;}
			set{iNbRowAffected = value;}
		}
	}
}