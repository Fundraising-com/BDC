///////////////////////////////////////////////////////////////////////////
// Description: Data Access class for the table 'product'
// Generated by Jas on: Monday, November 03, 2003, 4:18:07 PM
// Because the Base Class already implements IDispose, this class doesn't.
///////////////////////////////////////////////////////////////////////////
using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.Favorite_LogoTable;

namespace QSPForm.Data
{
	/// <summary>
	/// Purpose: Data Access class for the table 'product'.
	/// </summary>
	public class Favorite_Logo : DBTableOperation
	{

		#region Parameter
		//Stored procedure parameter names
		public const string PARAM_PKID = "@ifavorite_logo_id";
		//public const string PARAM_USER_ID = "@iuserid";
        public const string PARAM_FM_ID = "@sfm_id";
        public const string PARAM_FIELD_SALES_MANAGER_ID = "@ifield_sales_manager_id";
		public const string PARAM_LOGO_ID = "@ilogoid";
        public const string SQL_PROC_INSERT = "pr_favorite_logo_insert";
        public const string SQL_PROC_DELETE = "pr_favorite_logo_delete";
		#endregion

		//
		// DataSetCommand object
		//
		
		private SqlCommand insertCommand;
		private SqlCommand updateCommand;
		private SqlCommand deleteCommand;

		/// <summary>
		/// Purpose: Class constructor.
		/// </summary>
        public Favorite_Logo()
		{
			// Nothing for now.
		}

		//----------------------------------------------------------------
		// Sub GetInsertCommand:
		//   Initialize the parameterized Insert command for the DataAdapter
		//----------------------------------------------------------------
		protected override SqlCommand GetInsertCommand()
		{
			if ( insertCommand == null )
			{
				//
				// Construct the command since we don't have it already
				// 
				insertCommand = new SqlCommand(SQL_PROC_INSERT);
				insertCommand.CommandType = CommandType.StoredProcedure;
				insertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters; 
            
				SqlParameterCollection sqlParams = insertCommand.Parameters;
            
				//Fill the SqlParameterCollection
				FillParams(sqlParams);				
								
				//Adjust paramters direction
				sqlParams[PARAM_PKID].Direction = ParameterDirection.Output;				
				
				//Map the source column
				MapColumn(sqlParams);	
			}
            
			return insertCommand;
		}

		//----------------------------------------------------------------
		// Sub BuildUpdateCommand:
		//   Initialize the parameterized Update command for the DataAdapter
		//----------------------------------------------------------------
		protected override SqlCommand GetUpdateCommand()
		{
            //if ( updateCommand == null )
            //{
				
            //    updateCommand = new SqlCommand(SQL_PROC_UPDATE);
            //    updateCommand.CommandType = CommandType.StoredProcedure;
				
            
            //    SqlParameterCollection sqlParams = updateCommand.Parameters;
            
            //    //Fill the SqlParameterCollection
            //    FillParams(sqlParams);

            //    //Map the source column
            //    MapColumn(sqlParams);
            //}
            
            //return updateCommand;
            return null;
		}
		//-----------------------
		// Set Mapping and Params in 2 differents method
		//-----------------------

		private void FillParams(SqlParameterCollection param)
		{
			param.Add(new SqlParameter(PARAM_PKID, SqlDbType.Int));
			param.Add(new SqlParameter(PARAM_FIELD_SALES_MANAGER_ID, SqlDbType.Int));
            param.Add(new SqlParameter(PARAM_LOGO_ID, SqlDbType.Int));			
		}

		private void MapColumn(SqlParameterCollection param)
		{
			param[PARAM_PKID].SourceColumn = dataDef.FLD_PKID;
			param[PARAM_FIELD_SALES_MANAGER_ID].SourceColumn = dataDef.FLD_FIELD_SALES_MANAGER_ID;
			param[PARAM_LOGO_ID].SourceColumn = dataDef.FLD_LOGO_ID;
		}

		protected override string TableName
		{
			get{return dataDef.TBL_LOGO;}
		}

		
		//----------------------------------------------------------------
		// Sub BuildUpdateCommand:
		//   Initialize the parameterized Update command for the DataAdapter
		//----------------------------------------------------------------
		protected override SqlCommand GetDeleteCommand()
		{
			if ( deleteCommand == null )
			{
				//
				// Construct the command since we don't have it already
				//
				deleteCommand = new SqlCommand(SQL_PROC_DELETE);
				deleteCommand.CommandType = CommandType.StoredProcedure;
				
				SqlParameterCollection sqlParams = deleteCommand.Parameters;
            
				sqlParams.Add(new SqlParameter(PARAM_PKID, SqlDbType.Int));
			}
            
			return deleteCommand;
		}
        
        //public dataDef SelectAll_SearchByUserID(int field_sales_manager_id)
        //{
        //    dataDef Table = new dataDef();
        //    SqlCommand	cmdToExecute = new SqlCommand();
        //    cmdToExecute.CommandText = "dbo.pr_favorite_logo_SelectAllByUserID";
        //    cmdToExecute.CommandType = CommandType.StoredProcedure;
        //    cmdToExecute.Parameters.Add(new SqlParameter(PARAM_FM_ID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, field_sales_manager_id));
        //    Select(cmdToExecute,Table);
        //    return Table;
        //}
        public dataDef SelectAllDefault()
        {
            dataDef Table = new dataDef();
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.pr_favorite_logo_SelectAllDefault";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            Select(cmdToExecute, Table);
            return Table;
        }

        public dataDef SelectLogoByFMID(string FM_ID)
        {
            dataDef Table = new dataDef();
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.pr_favorite_logo_SelectByFMID";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            cmdToExecute.Parameters.Add(new SqlParameter(PARAM_FM_ID, SqlDbType.VarChar, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, FM_ID));
            Select(cmdToExecute, Table);
            return Table;
        }
        public dataDef SelectAll_SearchByFM_ID(string FM_ID)
        {
            dataDef Table = new dataDef();
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.pr_favorite_logo_SelectAllByFMID";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            cmdToExecute.Parameters.Add(new SqlParameter(PARAM_FM_ID, SqlDbType.VarChar, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, FM_ID));
            Select(cmdToExecute, Table);
            return Table;
        }
		public new dataDef SelectOne(int Favorite_Logo_ID)
		{
			dataDef Table = new dataDef();
			SqlCommand	cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.pr_favorite_logo_SelectOne";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
            cmdToExecute.Parameters.Add(new SqlParameter(PARAM_PKID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, Favorite_Logo_ID));
			Select(cmdToExecute,Table);
			return Table;
		}
        public new dataDef SelectOne(int FieldSalesManagerID, int LogoID)
		{
			dataDef Table = new dataDef();
			SqlCommand	cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.pr_favorite_logo_SelectByFieldSalesManagerIDLogoID";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
            cmdToExecute.Parameters.Add(new SqlParameter(PARAM_FIELD_SALES_MANAGER_ID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, FieldSalesManagerID));
            cmdToExecute.Parameters.Add(new SqlParameter(PARAM_LOGO_ID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, LogoID));
			Select(cmdToExecute,Table);
			return Table;
		}
        public new dataDef SelectOne(string FMID, int LogoID)
        {
            dataDef Table = new dataDef();
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.pr_favorite_logo_SelectByFMIDLogoID";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            cmdToExecute.Parameters.Add(new SqlParameter(PARAM_FM_ID, SqlDbType.VarChar, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, FMID));
            cmdToExecute.Parameters.Add(new SqlParameter(PARAM_LOGO_ID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, LogoID));
            Select(cmdToExecute, Table);
            return Table;
        }
        public new dataDef Insert(int FieldSalesManagerID, int LogoID)
        {
            dataDef Table = new dataDef();
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.pr_favorite_logo_insert";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            cmdToExecute.Parameters.Add(new SqlParameter(PARAM_FIELD_SALES_MANAGER_ID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, FieldSalesManagerID));
            cmdToExecute.Parameters.Add(new SqlParameter(PARAM_LOGO_ID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, LogoID));
            Select(cmdToExecute, Table);
            return Table;
        }
        public new dataDef Insert(string FM_ID, int LogoID)
        {
            dataDef Table = new dataDef();
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.pr_favorite_logo_insertByFMID";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            cmdToExecute.Parameters.Add(new SqlParameter(PARAM_FM_ID, SqlDbType.VarChar, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, FM_ID));
            cmdToExecute.Parameters.Add(new SqlParameter(PARAM_LOGO_ID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, LogoID));
            Select(cmdToExecute, Table);
            return Table;
        }
        
        public void Delete(int ID)
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.pr_favorite_logo_delete";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            cmdToExecute.Parameters.Add(new SqlParameter(PARAM_PKID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, ID));
            ExecuteCmd(cmdToExecute);
        }
	}
}
