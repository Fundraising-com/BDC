///////////////////////////////////////////////////////////////////////////
// Description: Data Access class for the table 'RemitBatch'
// Generated by LLBLGen v1.2.1603.19903 Final
// on: Saturday, May 22, 2004, 12:05:21 PM
// Because the Base Class already implements IDispose, this class doesn't.
///////////////////////////////////////////////////////////////////////////
using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using tableRef =QSPFulfillment.DataAccess.Common.TableDef.RemitBatchTable;


namespace QSPFulfillment.DataAccess.Data
{
	/// <summary>
	/// Purpose: Data Access class for the table 'RemitBatch'.
	/// </summary>
	public class RemitBatchData : QSPFulfillment.DataAccess.Data.DBTableOperation
	{
		#region Class Member Declarations
		internal const string PARAM_ID= "@iID";
		internal const string PARAM_DATE= "@daDate";
		internal const string PARAM_COUNTRYCODE= "@sCountryCode";
		internal const string PARAM_STATUS= "@iStatus";
		internal const string PARAM_FILENAME= "@sFilename";
		internal const string PARAM_FULFILLMENTHOUSENBR= "@sFulfillmentHouseNbr";
		internal const string PARAM_TOTALBASEPRICE= "@dcTotalBasePrice";
		internal const string PARAM_TOTALUNITS= "@iTotalUnits";
		internal const string PARAM_TOTALCHADD= "@iTotalCHADD";
		internal const string PARAM_TOTALCANCELLED= "@iTotalCancelled";
		internal const string PARAM_DATECHANGED= "@daDateChanged";
		internal const string PARAM_USERIDCHANGED= "@sUserIDChanged";
		internal const string PARAM_TOTALCATALOGPRICE= "@dcTotalCatalogPrice";
		internal const string PARAM_TOTALITEMPRICE= "@dcTotalItemPrice";
		internal const string PARAM_SEARCH_CRITERIA = "@isearch_type";
		internal const string PARAM_SEARCH_TYPE = "@scriteria";
		internal const string PARAM_DATE_FROM = "@daDateFrom";
		internal const string PARAM_DATE_TO = "@daDateTo";

		#region StoredProcedure Name
		internal const string SP_SELECT_REMITBATCH_STATUS_DATE = "pr_SelectRemitBatchByStatusByDate";
		internal const string SP_REMITBATCHBYDATE = "pr_RemitBatchByStatusByDateNestedGridHeader";
		#endregion
		private SqlCommand insertCommand;

		private SqlCommand deleteCommand;

		private SqlCommand updateCommand;

		#endregion


		/// <summary>
		/// Purpose: Class constructor.
		/// </summary>
		public RemitBatchData()
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
				insertCommand = new SqlCommand("dbo.[pr_RemitBatch_Insert]");
				insertCommand.CommandType = CommandType.StoredProcedure;
				insertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters; 
				SqlParameterCollection sqlParams = insertCommand.Parameters;

				sqlParams.Add(new SqlParameter(PARAM_ID,SqlDbType.Int));
				sqlParams[PARAM_ID].SourceColumn = tableRef.FLD_ID;

				sqlParams.Add(new SqlParameter(PARAM_DATE,SqlDbType.DateTime));
				sqlParams[PARAM_DATE].SourceColumn = tableRef.FLD_DATE;

				sqlParams.Add(new SqlParameter(PARAM_COUNTRYCODE,SqlDbType.VarChar));
				sqlParams[PARAM_COUNTRYCODE].SourceColumn = tableRef.FLD_COUNTRYCODE;

				sqlParams.Add(new SqlParameter(PARAM_STATUS,SqlDbType.Int));
				sqlParams[PARAM_STATUS].SourceColumn = tableRef.FLD_STATUS;

				sqlParams.Add(new SqlParameter(PARAM_FILENAME,SqlDbType.VarChar));
				sqlParams[PARAM_FILENAME].SourceColumn = tableRef.FLD_FILENAME;

				sqlParams.Add(new SqlParameter(PARAM_FULFILLMENTHOUSENBR,SqlDbType.VarChar));
				sqlParams[PARAM_FULFILLMENTHOUSENBR].SourceColumn = tableRef.FLD_FULFILLMENTHOUSENBR;

				sqlParams.Add(new SqlParameter(PARAM_TOTALBASEPRICE,SqlDbType.Decimal));
				sqlParams[PARAM_TOTALBASEPRICE].SourceColumn = tableRef.FLD_TOTALBASEPRICE;

				sqlParams.Add(new SqlParameter(PARAM_TOTALUNITS,SqlDbType.Int));
				sqlParams[PARAM_TOTALUNITS].SourceColumn = tableRef.FLD_TOTALUNITS;

				sqlParams.Add(new SqlParameter(PARAM_TOTALCHADD,SqlDbType.Int));
				sqlParams[PARAM_TOTALCHADD].SourceColumn = tableRef.FLD_TOTALCHADD;

				sqlParams.Add(new SqlParameter(PARAM_TOTALCANCELLED,SqlDbType.Int));
				sqlParams[PARAM_TOTALCANCELLED].SourceColumn = tableRef.FLD_TOTALCANCELLED;

				sqlParams.Add(new SqlParameter(PARAM_DATECHANGED,SqlDbType.DateTime));
				sqlParams[PARAM_DATECHANGED].SourceColumn = tableRef.FLD_DATECHANGED;

				sqlParams.Add(new SqlParameter(PARAM_USERIDCHANGED,SqlDbType.VarChar));
				sqlParams[PARAM_USERIDCHANGED].SourceColumn = tableRef.FLD_USERIDCHANGED;

				sqlParams.Add(new SqlParameter(PARAM_TOTALCATALOGPRICE,SqlDbType.Decimal));
				sqlParams[PARAM_TOTALCATALOGPRICE].SourceColumn = tableRef.FLD_TOTALCATALOGPRICE;

				sqlParams.Add(new SqlParameter(PARAM_TOTALITEMPRICE,SqlDbType.Decimal));
				sqlParams[PARAM_TOTALITEMPRICE].SourceColumn = tableRef.FLD_TOTALITEMPRICE;
			}
			return insertCommand;
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
				deleteCommand = new SqlCommand("dbo.[pr_RemitBatch_Delete]");
				deleteCommand.CommandType = CommandType.StoredProcedure;
				SqlParameterCollection sqlParams = deleteCommand.Parameters;

				sqlParams.Add(new SqlParameter(PARAM_ID,SqlDbType.Int));
				sqlParams[PARAM_ID].SourceColumn = tableRef.FLD_ID;
			}
			return deleteCommand;
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
				updateCommand = new SqlCommand("dbo.[pr_RemitBatch_Update]");
				updateCommand.CommandType = CommandType.StoredProcedure;
				updateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
				SqlParameterCollection sqlParams = updateCommand.Parameters;

				sqlParams.Add(new SqlParameter(PARAM_ID,SqlDbType.Int));
				sqlParams[PARAM_ID].SourceColumn = tableRef.FLD_ID;

				sqlParams.Add(new SqlParameter(PARAM_DATE,SqlDbType.DateTime));
				sqlParams[PARAM_DATE].SourceColumn = tableRef.FLD_DATE;

				sqlParams.Add(new SqlParameter(PARAM_COUNTRYCODE,SqlDbType.VarChar));
				sqlParams[PARAM_COUNTRYCODE].SourceColumn = tableRef.FLD_COUNTRYCODE;

				sqlParams.Add(new SqlParameter(PARAM_STATUS,SqlDbType.Int));
				sqlParams[PARAM_STATUS].SourceColumn = tableRef.FLD_STATUS;

				sqlParams.Add(new SqlParameter(PARAM_FILENAME,SqlDbType.VarChar));
				sqlParams[PARAM_FILENAME].SourceColumn = tableRef.FLD_FILENAME;

				sqlParams.Add(new SqlParameter(PARAM_FULFILLMENTHOUSENBR,SqlDbType.VarChar));
				sqlParams[PARAM_FULFILLMENTHOUSENBR].SourceColumn = tableRef.FLD_FULFILLMENTHOUSENBR;

				sqlParams.Add(new SqlParameter(PARAM_TOTALBASEPRICE,SqlDbType.Decimal));
				sqlParams[PARAM_TOTALBASEPRICE].SourceColumn = tableRef.FLD_TOTALBASEPRICE;

				sqlParams.Add(new SqlParameter(PARAM_TOTALUNITS,SqlDbType.Int));
				sqlParams[PARAM_TOTALUNITS].SourceColumn = tableRef.FLD_TOTALUNITS;

				sqlParams.Add(new SqlParameter(PARAM_TOTALCHADD,SqlDbType.Int));
				sqlParams[PARAM_TOTALCHADD].SourceColumn = tableRef.FLD_TOTALCHADD;

				sqlParams.Add(new SqlParameter(PARAM_TOTALCANCELLED,SqlDbType.Int));
				sqlParams[PARAM_TOTALCANCELLED].SourceColumn = tableRef.FLD_TOTALCANCELLED;

				sqlParams.Add(new SqlParameter(PARAM_DATECHANGED,SqlDbType.DateTime));
				sqlParams[PARAM_DATECHANGED].SourceColumn = tableRef.FLD_DATECHANGED;

				sqlParams.Add(new SqlParameter(PARAM_USERIDCHANGED,SqlDbType.VarChar));
				sqlParams[PARAM_USERIDCHANGED].SourceColumn = tableRef.FLD_USERIDCHANGED;

				sqlParams.Add(new SqlParameter(PARAM_TOTALCATALOGPRICE,SqlDbType.Decimal));
				sqlParams[PARAM_TOTALCATALOGPRICE].SourceColumn = tableRef.FLD_TOTALCATALOGPRICE;

				sqlParams.Add(new SqlParameter(PARAM_TOTALITEMPRICE,SqlDbType.Decimal));
				sqlParams[PARAM_TOTALITEMPRICE].SourceColumn = tableRef.FLD_TOTALITEMPRICE;
			}
			return updateCommand;
		}
		protected override string TableName
		{
			get
			{
				return tableRef.TBL_REMITBATCH;
			}
		}
		public void SelectSearch(DataTable Table,int SearchType,string SearchCriteria)
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.pr_RemitBatch_Search";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_SEARCH_TYPE, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, SearchType));
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_SEARCH_CRITERIA, SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, SearchCriteria));
			Select(cmdToExecute,Table);
		}


		/// <summary>
		/// Purpose: Select method. This method will Select one existing row from the database, based on the Primary Key.
		/// </summary>
		/// <returns>DataTable object if succeeded, otherwise an Exception is thrown. </returns>
		/// <remarks>
		/// Properties needed for this method: 
		/// <UL>
		///		 <LI>iID</LI>
		/// </UL>
		///		 <LI>iID</LI>
		///		 <LI>daDate</LI>
		///		 <LI>sCountryCode</LI>
		///		 <LI>iStatus</LI>
		///		 <LI>sFilename</LI>
		///		 <LI>sFulfillmentHouseNbr</LI>
		///		 <LI>dcTotalBasePrice</LI>
		///		 <LI>iTotalUnits</LI>
		///		 <LI>iTotalCHADD</LI>
		///		 <LI>iTotalCancelled</LI>
		///		 <LI>daDateChanged</LI>
		///		 <LI>sUserIDChanged</LI>
		///		 <LI>dcTotalCatalogPrice</LI>
		///		 <LI>dcTotalItemPrice</LI>
		/// Will fill all properties corresponding with a field in the table with the value of the row selected.
		/// </remarks>
		public  void SelectOne(DataTable Table, Int32 ID)		
		{
			SqlCommand	scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[pr_RemitBatch_SelectOne]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.Parameters.Add(new SqlParameter(PARAM_ID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, ID));
			Select(scmCmdToExecute,Table);
		}


		/// <summary>
		/// Purpose: SelectAll method. This method will Select all rows from the table.
		/// </summary>
		/// <returns>DataTable object if succeeded, otherwise an Exception is thrown. </returns>
		/// <remarks>
		/// </remarks>
		public  void SelectAll(DataTable Table)		
		{
			SqlCommand	scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[pr_RemitBatch_SelectAll]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			Select(scmCmdToExecute,Table);
		}
		public void SelectByDate(DataTable Table,int Status,DateTime From,DateTime To)
		{
			SqlCommand	scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = SP_SELECT_REMITBATCH_STATUS_DATE;
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.Parameters.Add(new SqlParameter(PARAM_STATUS, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, Status));
			AddParameterDate(PARAM_DATE_FROM,From,scmCmdToExecute,true);
			AddParameterDate(PARAM_DATE_TO,To,scmCmdToExecute,true);
			/*scmCmdToExecute.Parameters.Add(new SqlParameter(PARAM_DATE_FROM, SqlDbType.DateTime, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, From));
			scmCmdToExecute.Parameters.Add(new SqlParameter(PARAM_DATE_TO, SqlDbType.DateTime, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, To));*/
			Select(scmCmdToExecute,Table);
		}

		public void SelectByDateNestedGridHeader(DataTable Table,int Status,DateTime From,DateTime To)
		{
			SqlCommand	scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = SP_REMITBATCHBYDATE;
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.Parameters.Add(new SqlParameter(PARAM_STATUS, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, Status));
			AddParameterDate(PARAM_DATE_FROM,From,scmCmdToExecute,true);
			AddParameterDate(PARAM_DATE_TO,To,scmCmdToExecute,true);
			/*scmCmdToExecute.Parameters.Add(new SqlParameter(PARAM_DATE_FROM, SqlDbType.DateTime, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, From));
			scmCmdToExecute.Parameters.Add(new SqlParameter(PARAM_DATE_TO, SqlDbType.DateTime, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, To));*/
			Select(scmCmdToExecute,Table);
		}
		public void SelectByDateNestedGridGiftCardOutputSecondLevel(DataTable Table,int Status,DateTime From,DateTime To)
		{
			SqlCommand	scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "pr_RemitBatchByStatusByDateNestedGridSecondLevel";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.Parameters.Add(new SqlParameter(PARAM_STATUS, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, Status));
			AddParameterDate(PARAM_DATE_FROM,From,scmCmdToExecute,true);
			AddParameterDate(PARAM_DATE_TO,To,scmCmdToExecute,true);
			/*scmCmdToExecute.Parameters.Add(new SqlParameter(PARAM_DATE_FROM, SqlDbType.DateTime, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, From));
			scmCmdToExecute.Parameters.Add(new SqlParameter(PARAM_DATE_TO, SqlDbType.DateTime, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, To));*/
			Select(scmCmdToExecute,Table);
		}
	}
}
