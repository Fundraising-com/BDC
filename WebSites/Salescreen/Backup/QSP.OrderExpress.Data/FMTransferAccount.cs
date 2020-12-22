using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using QSPForm.Common.DataDef;
using QSPForm.Common;
//using dataDef = QSPForm.Common.DataDef.AccountTransferAccountTable;
using dataDef = QSPForm.Common.DataDef.AccountTransferAccountTable;
using dataDefOrg = QSPForm.Common.DataDef.AccountTransferOrganizationTable;
using dataDefAccount = QSPForm.Common.DataDef.AccountTable;

namespace QSPForm.Data
{
    public class FMTransferAccount : DBInteractionBase
    {
        
        
        #region Parameter
        //Stored procedure parameter names
        public const string FM_ID = "@sfm_id";
        public const string RESULTS_BY = "@iResultsBy";
        public const string PARAM_FM_ID = "@sfm_id";		
        //
        // Stored procedure names for each operation
        private const string SQL_PROC_SELECT_ACCOUNT = "pr_Account_Transfers_Get_Accounts_ByFM";
        private const string SQL_PROC_SELECT_ORG = "pr_Account_Transfers_Get_Flagpole_ByFM";
       
        #endregion


      	/// <summary>
		/// Purpose: Class constructor.
		/// </summary>
        public FMTransferAccount()
		{
			// Nothing for now.
		}

        public FMTransferAccount (string TransactionName) : base()
        {
            
        }
        

        public dataDef SelectAccount(string fm_Id)
		{
            dataDef Table = new dataDef();
			SqlCommand	cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = SQL_PROC_SELECT_ACCOUNT;
			cmdToExecute.CommandType = CommandType.StoredProcedure;

            cmdToExecute.Parameters.Add(new SqlParameter(FM_ID, SqlDbType.NVarChar, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, fm_Id));
            
 
			Select(cmdToExecute,Table);
			//AssignInnerProperty(Table);

			return Table;
				
		}


        public dataDefOrg SelectOrg(string fm_Id)
        {
            dataDefOrg Table = new dataDefOrg();
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = SQL_PROC_SELECT_ORG;
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            cmdToExecute.Parameters.Add(new SqlParameter(FM_ID, SqlDbType.NVarChar, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, fm_Id));

            Select(cmdToExecute, Table);
            //AssignInnerProperty(Table);

            return Table;
        }


        public bool UpdateAccountsByFMID(string accountid,string fromfmid, string tofmid, string salestofmid, DateTime effectiveDate, string reason, int icreateuserId)
        {

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_Account_Transfers_Insert_ByAccount]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            cmdToExecute.Parameters.Add(new SqlParameter("@iaccount_id", accountid));
            cmdToExecute.Parameters.Add(new SqlParameter("@sfrom_fmid", fromfmid));
            cmdToExecute.Parameters.Add(new SqlParameter("@sto_fmid", tofmid));
            cmdToExecute.Parameters.Add(new SqlParameter("@ssalesto_fmid", salestofmid));
            cmdToExecute.Parameters.Add(new SqlParameter("@seffective_date", effectiveDate));
            cmdToExecute.Parameters.Add(new SqlParameter("@sreason", reason));
            cmdToExecute.Parameters.Add(new SqlParameter("@icreate_user_id", icreateuserId));

            return ExecuteCmd(cmdToExecute);

        }

        public dataDef SelectAll_Search(int SearchType, String Criteria, int ProgramType, string SubdivisionCode, string FMID, int FSM_DisplayMode, int StatusCategoryID, string FMName)
        {
            dataDef Table = new dataDef();
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.pr_Account_Transfers_Search";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            if (ProgramType > 0)
                cmdToExecute.Parameters.Add(new SqlParameter("@iprogram_type_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, ProgramType));
            if (SubdivisionCode.Length > 0)
                cmdToExecute.Parameters.Add(new SqlParameter("@ssubdivision_code", SqlDbType.NVarChar, 7, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, SubdivisionCode));
            cmdToExecute.Parameters.Add(new SqlParameter(PARAM_SEARCH_TYPE, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, SearchType));
            cmdToExecute.Parameters.Add(new SqlParameter(PARAM_SEARCH_CRITERIA, SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, Criteria));

            if (FMID.Length > 0)
                cmdToExecute.Parameters.Add(new SqlParameter(PARAM_FM_ID, SqlDbType.VarChar, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, FMID));

            cmdToExecute.Parameters.Add(new SqlParameter("@idisplay_fm_mode", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, FSM_DisplayMode));

            if (FMName.Length > 0)
                cmdToExecute.Parameters.Add(new SqlParameter("@sfm_name", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, FMName));

            if (StatusCategoryID > 0)
                cmdToExecute.Parameters.Add(new SqlParameter("@istatus_category_id", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, StatusCategoryID));

            Select(cmdToExecute, Table);

            return Table;
        }

    }
}
