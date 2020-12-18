
using System;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;



namespace QSPForm.Data
{
	
	
	[Serializable()]
	public abstract class DBTableOperation: DBInteractionBase 
	{
		public const string PARAM_DELETED ="@bdeleted";
		public const string PARAM_CREATE_USER_ID = "@icreate_user_id";
		public const string PARAM_CREATE_DATE= "@dacreate_date";
		public const string PARAM_UPDATE_USER_ID="@iupdate_user_id";
		public const string PARAM_UPDATE_DATE="@daupdate_date";
		
		
		protected SqlDataAdapter adapter;
		protected abstract SqlCommand GetInsertCommand();
		protected abstract SqlCommand GetDeleteCommand();
		protected abstract SqlCommand GetUpdateCommand();
		protected abstract string TableName	{get;}
        protected bool acceptChangesDuringUpdate = true;

		#region Insert Update Delete table

        public bool AcceptChangesDuringUpdate
        { 
            get
            {
                return acceptChangesDuringUpdate;
            }
            set
            {
                acceptChangesDuringUpdate = value;
            }
        }
        
		public override int Insert(DataTable Table)
		{
			return Insert(GetInsertCommand(),Table);
		}
		public override int Update(DataTable Table)
		{
			return Update(GetUpdateCommand(),Table);
		}
		public override int Delete(DataTable Table)
		{
			return Delete(GetDeleteCommand(),Table);
		}

		public int UpdateBatch(DataTable Table)
		{
			adapter = new SqlDataAdapter();
            adapter.AcceptChangesDuringUpdate = acceptChangesDuringUpdate;
			adapter.InsertCommand = GetInsertCommand();
			adapter.UpdateCommand = GetUpdateCommand();
			adapter.DeleteCommand = GetDeleteCommand(); 
			return base.UpdateBatch(adapter,Table);
		}

		public int UpdateBatch(DataRow[] dataRows)
		{
			adapter = new SqlDataAdapter();
            adapter.AcceptChangesDuringUpdate = acceptChangesDuringUpdate;
			adapter.InsertCommand = GetInsertCommand();
			adapter.UpdateCommand = GetUpdateCommand();
			adapter.DeleteCommand = GetDeleteCommand();
			return base.UpdateBatch(adapter, dataRows);
		}

		public int Insert(DataSet dts)
		{
            return Insert(GetInsertCommand(), dts,TableName);			
		}
		#endregion

		
	}
}
