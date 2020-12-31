 using System;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
 namespace QSPFulfillment.DataAccess.Data
 {
	 [Serializable()]
	 public abstract class DBTableOperation: DBInteractionBase 
	 {
		 protected SqlDataAdapter adapter;
		 protected abstract SqlCommand GetInsertCommand();
		 protected abstract SqlCommand GetDeleteCommand();
		 protected abstract SqlCommand GetUpdateCommand();
		 protected abstract string TableName	{get;}
		 public  int Delete(DataTable Table)
		 {
			 return Delete(GetDeleteCommand(),Table);
		 }
		 public  int Insert(DataTable Table)
		 {
			 return Insert(GetInsertCommand(),Table);
		 }
		 public  int Update(DataTable Table)
		 {
			 return Update(GetUpdateCommand(),Table);
		 }
		 public int UpdateBatch(DataTable Table)
		 {
			 adapter = new SqlDataAdapter();		
			 adapter.InsertCommand = GetInsertCommand();
			 adapter.UpdateCommand = GetUpdateCommand();
			 adapter.DeleteCommand = GetDeleteCommand();
			 return UpdateBatch(adapter,Table);
		 }
		 /// <summary>
		 /// 
		 /// </summary>
		 /// <param name="ParameterName"></param>
		 /// <param name="Value"></param>
		 /// <param name="Command"></param>
		 /// <param name="IsNullable">if true and the value reiceved is Datetime.MinValue it will be added value as System.DbNull.Value</param>
		 public void AddParameterDate(string ParameterName,DateTime Value,SqlCommand Command,bool IsNullable)
		 {
			 if(Value == DateTime.MinValue && IsNullable)
				Command.Parameters.Add(new SqlParameter(ParameterName, SqlDbType.DateTime, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, System.DBNull.Value));
			 else
				Command.Parameters.Add(new SqlParameter(ParameterName, SqlDbType.DateTime, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, Value));
		 }
		 
	 }
 }