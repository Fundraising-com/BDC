using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using Common;
using Common.TableDef;


namespace DAL
{
	
	public class StatementData : DBTableOperation
	{
		#region Class Member Declarations

		internal const string PARAM_ACCTNAME= "@AcctName";
		internal const string PARAM_ACCTID= "@AccountID";
		internal const string PARAM_CAID= "@iCampaignID";
		internal const string PARAM_FROMDATE= "@dFrom";
		internal const string PARAM_TODATE= "@dTo";
		internal const string PARAM_FMID= "@zFMID";
		internal const string PARAM_OVER100= "@iOver100";

		#endregion

		/// <summary>
		/// Purpose: Class constructor.
		/// </summary>
		public StatementData() : base(DataBaseName.QSPCanadaOrderManagement) { }

		//----------------------------------------------------------------
		// Sub GetInsertCommand:
		//   Initialize the parameterized Insert command for the DataAdapter
		//----------------------------------------------------------------
		protected override SqlCommand GetInsertCommand()
		{
			return null;
		}
		//----------------------------------------------------------------
		// Sub BuildUpdateCommand:
		//   Initialize the parameterized Update command for the DataAdapter
		//----------------------------------------------------------------
		protected override SqlCommand GetDeleteCommand()
		{
			return null;
		}
		//----------------------------------------------------------------
		// Sub BuildUpdateCommand:
		//   Initialize the parameterized Update command for the DataAdapter
		//----------------------------------------------------------------
		protected override SqlCommand GetUpdateCommand()
		{
			return null;
		}

		public  void SelectALLCAStatementToPrint (DataSet dtsDataSet, string tableName,  DateTime fromDate, DateTime toDate)		
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "QSPCanadaFinance.dbo.[GetAllStatementsByCampaignToPrint]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_FROMDATE, SqlDbType.DateTime, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, fromDate));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_TODATE, SqlDbType.DateTime, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, toDate));
			
			Select(cmdToExecute,dtsDataSet, tableName);
		}

		public  void SelectCAStatementToPrint (DataSet dtsDataSet, string tableName, int accountID,int campaignID, DateTime fromDate, DateTime toDate )//,int over100 )		
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.[GetAllStatementsByAcctCampaignToPrint]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_FROMDATE, SqlDbType.DateTime, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, fromDate));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_TODATE, SqlDbType.DateTime, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, toDate));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_CAID, SqlDbType.Int, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, campaignID));
			
			Select(cmdToExecute,dtsDataSet, tableName);
		}

		public  void SelectOnlineStatementToPrint (DataSet dtsDataSet, string tableName,  DateTime fromDate, DateTime toDate, int campaignID,int over100, string fmID)		
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.[pr_OnlineProgramProfitStatementReportSummary2]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_FROMDATE, SqlDbType.DateTime, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, fromDate));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_TODATE, SqlDbType.DateTime, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, toDate));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_CAID, SqlDbType.Int, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, campaignID));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_OVER100, SqlDbType.Int, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, over100));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_FMID, SqlDbType.Char, 4, ParameterDirection.Input, false, 4, 0, "", DataRowVersion.Proposed, fmID));
			Select(cmdToExecute,dtsDataSet, tableName);
		}

		public  void SelectCustServStatementToPrint (DataSet dtsDataSet, string tableName,  DateTime fromDate, DateTime toDate, int campaignID,int over100, string fmID)		
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.[pr_CustomerServiceOrdersStatementReport]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_FROMDATE, SqlDbType.DateTime, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, fromDate));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_TODATE, SqlDbType.DateTime, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, toDate));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_CAID, SqlDbType.Int, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, campaignID));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_OVER100, SqlDbType.Int, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, over100));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_FMID, SqlDbType.Char, 4, ParameterDirection.Input, false, 4, 0, "", DataRowVersion.Proposed, fmID));
			Select(cmdToExecute,dtsDataSet, tableName);
		}
		
		


	}
}