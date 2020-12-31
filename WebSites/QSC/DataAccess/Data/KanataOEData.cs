using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using tableRef =QSPFulfillment.DataAccess.Common.TableDef.CampaignProgramTable;


namespace QSPFulfillment.DataAccess.Data
{
	public class KanataOEData : QSPFulfillment.DataAccess.Data.DBTableOperation
	{
		#region Class Member Declarations
		internal const string PARAM_CAMPAIGNID= "@iCampaignID";
		internal const string PARAM_PROGRAMID= "@iProgramID";
		internal const string PARAM_ISPRECOLLECT= "@sIsPreCollect";
		internal const string PARAM_GROUPPROFIT= "@dcGroupProfit";
		internal const string PARAM_DELETEDTF= "@bDeletedTF";
		internal const string PARAM_SEARCH_CRITERIA = "@isearch_type";

		internal const string PARAM_SEARCH_TYPE = "@scriteria";
		

		private SqlCommand insertCommand;

		private SqlCommand deleteCommand;

		private SqlCommand updateCommand;

		#endregion


		public KanataOEData()
		{
			// Nothing for now.
		}
		
		protected override SqlCommand GetInsertCommand()
		{
			if ( insertCommand == null )
			{
				//
				// Construct the command since we don't have it already
				// 
				insertCommand = new SqlCommand("dbo.[pr_CampaignProgram_Insert]");
				insertCommand.CommandType = CommandType.StoredProcedure;
				insertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters; 
				SqlParameterCollection sqlParams = insertCommand.Parameters;

				sqlParams.Add(new SqlParameter(PARAM_CAMPAIGNID,SqlDbType.Int));
				sqlParams[PARAM_CAMPAIGNID].SourceColumn = tableRef.FLD_CAMPAIGNID;

				sqlParams.Add(new SqlParameter(PARAM_PROGRAMID,SqlDbType.Int));
				sqlParams[PARAM_PROGRAMID].SourceColumn = tableRef.FLD_PROGRAMID;

				sqlParams.Add(new SqlParameter(PARAM_ISPRECOLLECT,SqlDbType.VarChar));
				sqlParams[PARAM_ISPRECOLLECT].SourceColumn = tableRef.FLD_ISPRECOLLECT;

				sqlParams.Add(new SqlParameter(PARAM_GROUPPROFIT,SqlDbType.Decimal));
				sqlParams[PARAM_GROUPPROFIT].SourceColumn = tableRef.FLD_GROUPPROFIT;

				sqlParams.Add(new SqlParameter(PARAM_DELETEDTF,SqlDbType.Bit));
				sqlParams[PARAM_DELETEDTF].SourceColumn = tableRef.FLD_DELETEDTF;
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
				deleteCommand = new SqlCommand("dbo.[pr_CampaignProgram_Delete]");
				deleteCommand.CommandType = CommandType.StoredProcedure;
				SqlParameterCollection sqlParams = deleteCommand.Parameters;

				sqlParams.Add(new SqlParameter(PARAM_CAMPAIGNID,SqlDbType.Int));
				sqlParams[PARAM_CAMPAIGNID].SourceColumn = tableRef.FLD_CAMPAIGNID;

				sqlParams.Add(new SqlParameter(PARAM_PROGRAMID,SqlDbType.Int));
				sqlParams[PARAM_PROGRAMID].SourceColumn = tableRef.FLD_PROGRAMID;
			}
			return deleteCommand;
		}
	
		protected override SqlCommand GetUpdateCommand()
		{
			if ( updateCommand == null )
			{
				//
				// Construct the command since we don't have it already
				//
				updateCommand = new SqlCommand("dbo.[pr_CampaignProgram_Update]");
				updateCommand.CommandType = CommandType.StoredProcedure;
				updateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
				SqlParameterCollection sqlParams = updateCommand.Parameters;

				sqlParams.Add(new SqlParameter(PARAM_CAMPAIGNID,SqlDbType.Int));
				sqlParams[PARAM_CAMPAIGNID].SourceColumn = tableRef.FLD_CAMPAIGNID;

				sqlParams.Add(new SqlParameter(PARAM_PROGRAMID,SqlDbType.Int));
				sqlParams[PARAM_PROGRAMID].SourceColumn = tableRef.FLD_PROGRAMID;

				sqlParams.Add(new SqlParameter(PARAM_ISPRECOLLECT,SqlDbType.VarChar));
				sqlParams[PARAM_ISPRECOLLECT].SourceColumn = tableRef.FLD_ISPRECOLLECT;

				sqlParams.Add(new SqlParameter(PARAM_GROUPPROFIT,SqlDbType.Decimal));
				sqlParams[PARAM_GROUPPROFIT].SourceColumn = tableRef.FLD_GROUPPROFIT;

				sqlParams.Add(new SqlParameter(PARAM_DELETEDTF,SqlDbType.Bit));
				sqlParams[PARAM_DELETEDTF].SourceColumn = tableRef.FLD_DELETEDTF;
			}
			return updateCommand;
		}
		
		protected override string TableName
		{
			get
			{
				return tableRef.TBL_CAMPAIGNPROGRAM;
			}
		}
		
		public void SelectAllCatalogForKanata(DataTable Table, int CampaignID, int IsFMAccount)
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.Parameters.Add(new SqlParameter("@iCampaignID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CampaignID));
			cmdToExecute.CommandText = "QSPCanadaProduct.dbo.pr_Kanata_Catalog_SelectAll";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			Select(cmdToExecute,Table);
		}

		public int AddNewItemForKanataOrder(int customerOrderHeaderInstance, int magPriceInstance, int ShipToInstance, string Fname, string Lname, int quantity, float enteredPrice, int overrideCode) 
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[pr_Kanata_OrderItem_Create]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.Parameters.Add(new SqlParameter("@iCustomerOrderHeaderInstance", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, customerOrderHeaderInstance));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@iMagPriceInstance", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, magPriceInstance));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@iShipToInstance", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, ShipToInstance));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@zShipToFirstName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, Fname));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@zShipToLastName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, Lname));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@iQuantity", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, quantity));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@fPrice", SqlDbType.Float, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, enteredPrice));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@iOverrideCode", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, overrideCode));

			return ExecuteCmd(scmCmdToExecute);
		}

		public void KanataSelectItemsByCatalogAndAccountType(DataTable Table,string ProgramType, string Product_Code,int CampaignID, int IsFMAccount)
		{
			SqlCommand	scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "[pr_Kanata_GetItemsByCatalogAndAccountType]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.Parameters.Add(new SqlParameter("@ProgramType", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed,ProgramType));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@ProductCode", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed,Product_Code));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@CampaignId", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed,CampaignID));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@IsFmAccount", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed,IsFMAccount));
			Select(scmCmdToExecute,Table);
		}
	}
}

