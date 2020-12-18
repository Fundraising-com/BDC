///////////////////////////////////////////////////////////////////////////
// Description: Data Access class for the table 'campaign'
// Generated by Jas on: Monday, November 03, 2003, 4:18:08 PM
// Because the Base Class already implements IDispose, this class doesn't.
///////////////////////////////////////////////////////////////////////////
using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
//using System.EnterpriseServices;
//using System.Runtime.InteropServices;
using dataDef = QSPForm.Common.DataDef.OrganizationTable;

namespace QSPForm.Data
{
	/// <summary>
	/// Purpose: Data Access class for the table 'Organization'.
	/// </summary>
	public class Organization : DBTableOperation 
	{
		
		//
		// Stored procedure parameter names
		// Paremater Name must match with the SP Called
		private const string PARAM_PKID				= "@iorganization_id";
		private const string PARAM_ORG_TYPE_ID		= "@iorganization_type_id";
		private const string PARAM_ORG_LEVEL_ID		= "@iorganization_level_id";
		private const string PARAM_NAME				= "@sorganization_name";
		private const string PARAM_ORG_STATUS_ID	= "@iorganization_status_id";
		private const string PARAM_FM_ID			= "@sfm_id";		
		private const string PARAM_TAX_EXEMPTION_NO	="@stax_exemption_number";
		private const string PARAM_TAX_EXEMPTION_EXP_DATE	= "@datax_exemption_expiration_date";
		private const string PARAM_MDRPID			= "@sMDRPID";
		private const string PARAM_COMMENTS			= "@scomments";				

		//
		// Stored procedure names for each operation
		private const String SQL_PROC_INSERT       = "pr_organization_Insert";
		private const String SQL_PROC_UPDATE       = "pr_organization_Update";
		private const String SQL_PROC_DELETE       = "pr_organization_Delete";
		private const String SQL_PROC_SELECT_ONE   = "pr_organization_SelectOne";
		private const String SQL_PROC_SELECT_ALL   = "pr_organization_SelectAll";

		//
		// DataSetCommand object
		//
		
		private SqlCommand insertCommand;
		private SqlCommand updateCommand;
		private SqlCommand deleteCommand;

		/// <summary>
		/// Purpose: Class constructor.
		/// </summary>
		public Organization()
		{
		
		}
		protected override string TableName
		{
			get{return dataDef.TBL_ORGANIZATION;}
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
            
				sqlParams.Add(new SqlParameter(PARAM_PKID, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(PARAM_ORG_TYPE_ID, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(PARAM_ORG_LEVEL_ID, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(PARAM_NAME, SqlDbType.VarChar, 50));
				sqlParams.Add(new SqlParameter(PARAM_FM_ID, SqlDbType.VarChar, 4));	
				sqlParams.Add(new SqlParameter(PARAM_TAX_EXEMPTION_NO, SqlDbType.VarChar, 20));
				sqlParams.Add(new SqlParameter(PARAM_TAX_EXEMPTION_EXP_DATE, SqlDbType.DateTime));
				sqlParams.Add(new SqlParameter(PARAM_MDRPID, SqlDbType.VarChar, 8));
				sqlParams.Add(new SqlParameter(PARAM_COMMENTS, SqlDbType.VarChar, 4000));
				sqlParams.Add(new SqlParameter(PARAM_ORG_STATUS_ID, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(PARAM_CREATE_USER_ID, SqlDbType.Int));
				
				sqlParams[PARAM_PKID].Direction = ParameterDirection.Output;
				//
				// Define the parameter mappings from the data table in the
				// dataset.
				//
				sqlParams[PARAM_PKID].SourceColumn = dataDef.FLD_PKID;
				sqlParams[PARAM_ORG_TYPE_ID].SourceColumn = dataDef.FLD_ORG_TYPE_ID;
				sqlParams[PARAM_ORG_STATUS_ID].SourceColumn = dataDef.FLD_ORG_STATUS_ID;
				sqlParams[PARAM_ORG_LEVEL_ID].SourceColumn = dataDef.FLD_ORG_LEVEL_ID;
				sqlParams[PARAM_NAME].SourceColumn = dataDef.FLD_NAME;
				sqlParams[PARAM_FM_ID].SourceColumn = dataDef.FLD_FM_ID;
				sqlParams[PARAM_TAX_EXEMPTION_NO].SourceColumn = dataDef.FLD_TAX_EXEMPTION_NO;
				sqlParams[PARAM_TAX_EXEMPTION_EXP_DATE].SourceColumn = dataDef.FLD_TAX_EXEMPTION_EXP_DATE;
				sqlParams[PARAM_MDRPID].SourceColumn = dataDef.FLD_MDRPID;
				sqlParams[PARAM_COMMENTS].SourceColumn = dataDef.FLD_COMMENTS;

				sqlParams[PARAM_CREATE_USER_ID].SourceColumn = dataDef.FLD_CREATE_USER_ID;
				//Don't need to map ErrorCode cause is not imply in the insert
				//Only Mapped DataColumn are imply...
				
			}
            
			return insertCommand;
		}

		//----------------------------------------------------------------
		// Sub BuildUpdateCommand:
		//   Initialize the parameterized Update command for the DataAdapter
		//----------------------------------------------------------------
		protected override SqlCommand GetUpdateCommand()
		{
			if ( updateCommand == null )
			{
				//
				// Construct the command since we don't have it already
				//
				updateCommand = new SqlCommand(SQL_PROC_UPDATE);
				updateCommand.CommandType = CommandType.StoredProcedure;
				            
				SqlParameterCollection sqlParams = updateCommand.Parameters;
            				

				sqlParams.Add(new SqlParameter(PARAM_PKID, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(PARAM_ORG_TYPE_ID, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(PARAM_ORG_LEVEL_ID, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(PARAM_NAME, SqlDbType.VarChar, 50));
				sqlParams.Add(new SqlParameter(PARAM_FM_ID, SqlDbType.VarChar, 4));	
				sqlParams.Add(new SqlParameter(PARAM_TAX_EXEMPTION_NO, SqlDbType.VarChar, 20));
				sqlParams.Add(new SqlParameter(PARAM_TAX_EXEMPTION_EXP_DATE, SqlDbType.DateTime));
				sqlParams.Add(new SqlParameter(PARAM_MDRPID, SqlDbType.VarChar, 8));
				sqlParams.Add(new SqlParameter(PARAM_COMMENTS, SqlDbType.VarChar, 4000));
				sqlParams.Add(new SqlParameter(PARAM_ORG_STATUS_ID, SqlDbType.Int));
				sqlParams.Add(new SqlParameter(PARAM_DELETED, SqlDbType.Bit));
				sqlParams.Add(new SqlParameter(PARAM_UPDATE_USER_ID, SqlDbType.Int));
				//
				// Define the parameter mappings from the data table in the
				// dataset.
				//
				sqlParams[PARAM_PKID].SourceColumn = dataDef.FLD_PKID;
				sqlParams[PARAM_ORG_TYPE_ID].SourceColumn = dataDef.FLD_ORG_TYPE_ID;
				sqlParams[PARAM_ORG_STATUS_ID].SourceColumn = dataDef.FLD_ORG_STATUS_ID;
				sqlParams[PARAM_ORG_LEVEL_ID].SourceColumn = dataDef.FLD_ORG_LEVEL_ID;
				sqlParams[PARAM_NAME].SourceColumn = dataDef.FLD_NAME;
				sqlParams[PARAM_FM_ID].SourceColumn = dataDef.FLD_FM_ID;
				sqlParams[PARAM_TAX_EXEMPTION_NO].SourceColumn = dataDef.FLD_TAX_EXEMPTION_NO;
				sqlParams[PARAM_TAX_EXEMPTION_EXP_DATE].SourceColumn = dataDef.FLD_TAX_EXEMPTION_EXP_DATE;
				sqlParams[PARAM_MDRPID].SourceColumn = dataDef.FLD_MDRPID;
				sqlParams[PARAM_COMMENTS].SourceColumn = dataDef.FLD_COMMENTS;

				sqlParams[PARAM_DELETED].SourceColumn = dataDef.FLD_DELETED;
				sqlParams[PARAM_UPDATE_USER_ID].SourceColumn = dataDef.FLD_UPDATE_USER_ID;
				//Only Mapped DataColumn are imply...
			}
            
			return updateCommand;
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
				sqlParams.Add(new SqlParameter(PARAM_UPDATE_USER_ID, SqlDbType.Int));
				
				//
				// Define the parameter mappings from the data tabl
				//
				sqlParams[PARAM_PKID].SourceColumn = dataDef.FLD_PKID;
				sqlParams[PARAM_UPDATE_USER_ID].SourceColumn = dataDef.FLD_UPDATE_USER_ID;
				//Only Mapped DataColumn are imply...
			}
            
			return deleteCommand;
		}
		
		/// <summary>
		/// Purpose: Select method. This method will Select one existing row from the database, based on the Primary Key.
		/// </summary>
		/// <returns>DataTable object if succeeded, otherwise an Exception is thrown. </returns>
		/// <remarks>
		/// Properties needed for this method: 
		/// <UL>
		///		 <LI>Campaign_id</LI>
		/// </UL> 
		/// </remarks>
		public new dataDef SelectOne(int OrgID)
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = SQL_PROC_SELECT_ONE;
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_PKID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, OrgID));
			
			dataDef toReturn = new dataDef();			
			Select(cmdToExecute,toReturn);
			return toReturn;
			
		}
		
		/// <summary>
		/// Purpose: SelectAll method. This method will Select all rows from the table.
		/// </summary>
		/// <returns>DataTable object if succeeded, otherwise an Exception is thrown. </returns>
		/// <remarks>
		/// Properties set after a succesful call of this method: 
		/// <UL>
		///		 <LI>ErrorCode</LI>
		/// </UL>
		/// </remarks>
		public new dataDef SelectAll()
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = SQL_PROC_SELECT_ALL;
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			dataDef toReturn = new dataDef();			

			Select(cmdToExecute,toReturn);
			return toReturn;
		}

		public dataDef SelectAllWcampaign_idLogic(int CampaignID)
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.pr_organization_SelectAllWcampaign_idLogic";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter("@icampaign_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CampaignID));
			
			dataDef toReturn = new dataDef();			
			Select(cmdToExecute,toReturn);
			return toReturn;
			
		}

		public dataDef SelectAllWaccount_idLogic(int AccountID)
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.pr_organization_SelectAllWaccount_idLogic";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter("@iaccount_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, AccountID));
			
			dataDef toReturn = new dataDef();			
			Select(cmdToExecute,toReturn);
			return toReturn;
			
		}


		
		public dataDef SelectAllWfm_idLogic(string FMID)
		{			
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.pr_organization_SelectAllWfm_idLogic";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_FM_ID, SqlDbType.VarChar, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, FMID));
			
			dataDef toReturn = new dataDef();	
			
			Select(cmdToExecute,toReturn);
			return toReturn;
			
		}


        public dataDef SelectAll_Search(int SearchType, String Criteria, int OrgType, string SubdivisionCode, string FMID, int FSM_DisplayMode, string FMName)
		{
			dataDef Table = new dataDef();
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.pr_organization_SelectAll_Search";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			if (OrgType > 0)
				cmdToExecute.Parameters.Add(new SqlParameter(PARAM_ORG_TYPE_ID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, OrgType));
			if (SubdivisionCode.Length > 0)
				cmdToExecute.Parameters.Add(new SqlParameter("@ssubdivision_code", SqlDbType.NVarChar, 7, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, SubdivisionCode));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_SEARCH_TYPE, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, SearchType));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_SEARCH_CRITERIA, SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, Criteria));
			if (FMID.Length > 0)
				cmdToExecute.Parameters.Add(new SqlParameter(PARAM_FM_ID, SqlDbType.VarChar, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, FMID));
			
			cmdToExecute.Parameters.Add(new SqlParameter("@idisplay_fm_mode", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, FSM_DisplayMode));

            if (FMName.Length > 0)
                cmdToExecute.Parameters.Add(new SqlParameter("@sfm_name", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, FMName));
			
			Select(cmdToExecute,Table);

			return Table;
		}

	}
}
