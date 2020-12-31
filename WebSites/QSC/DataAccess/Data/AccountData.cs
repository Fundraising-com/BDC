///////////////////////////////////////////////////////////////////////////
// Description: Data Access class for the table 'Account'
// Generated by LLBLGen v1.2.1594.24829 Final
// on: Thursday, May 13, 2004, 2:49:17 PM
// Because the Base Class already implements IDispose, this class doesn't.
///////////////////////////////////////////////////////////////////////////
using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using tableRef =QSPFulfillment.DataAccess.Common.TableDef.AccountTable;


namespace QSPFulfillment.DataAccess.Data
{
	/// <summary>
	/// Purpose: Data Access class for the table 'Account'.
	/// </summary>
	public class AccountData : QSPFulfillment.DataAccess.Data.DBTableOperation
	{
		#region Class Member Declarations
		internal const string PARAM_ID= "@iID";
		internal const string PARAM_NAME= "@sName";
		internal const string PARAM_ADDRESS1= "@sAddress1";
		internal const string PARAM_ADDRESS2= "@sAddress2";
		internal const string PARAM_CITY= "@sCity";
		internal const string PARAM_STATE= "@sState";
		internal const string PARAM_ZIP= "@sZip";
		internal const string PARAM_ZIPPLUSFOUR= "@sZipPlusFour";
		internal const string PARAM_ATTNLINE= "@sAttnLine";
		internal const string PARAM_FIELDMANAGERNO= "@sFieldManagerNo";
		internal const string PARAM_FIELDMANAGERREGION= "@sFieldManagerRegion";
		internal const string PARAM_COUNTY= "@sCounty";
		internal const string PARAM_COUNTYCODE= "@sCountyCode";
		internal const string PARAM_SCHOOLTYPE= "@sSchoolType";
		internal const string PARAM_PUBLICCATHOLIC= "@sPublicCatholic";
		internal const string PARAM_TAXEXEMPTNUMBER= "@sTaxExemptNumber";
		internal const string PARAM_CAMPAIGNSTART= "@daCampaignStart";
		internal const string PARAM_CAMPAIGNEND= "@daCampaignEnd";
		internal const string PARAM_ISNATIONAL= "@bIsNational";
		internal const string PARAM_UNITTYPE= "@sUnitType";
		internal const string PARAM_NATIONALDISTRICT= "@sNationalDistrict";
		internal const string PARAM_NATIONALFIELDMANAGER= "@sNationalFieldManager";
		internal const string PARAM_SCHOOLDISTRICTNAME= "@sSchoolDistrictName";
		internal const string PARAM_NUMBEROFCLASSROOMS= "@iNumberOfClassrooms";
		internal const string PARAM_NUMBEROFSTUDENTS= "@iNumberOfStudents";
		internal const string PARAM_SHIPTOACCTORFM= "@sShipToAcctOrFM";
		internal const string PARAM_AMFMIND= "@sAMFMInd";
		internal const string PARAM_COMMISSION= "@fCommission";
		internal const string PARAM_SEARCH_CRITERIA = "@isearch_type";
		internal const string PARAM_ORDER_ID = "@OrderID";
		internal const string PARAM_CAMPAIGNID = "@iCampaignID";
		internal const string PARAM_SEARCH_TYPE = "@scriteria";
		internal const string PARAM_SUBSCRIPTION_ID = "@SubscriptionID";
		//

		// DataSetCommand object

		//

		private SqlCommand insertCommand;

		private SqlCommand deleteCommand;

		private SqlCommand updateCommand;

		#endregion


		/// <summary>
		/// Purpose: Class constructor.
		/// </summary>
		public AccountData()
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
				insertCommand = new SqlCommand("dbo.[pr_Account_Insert]");
				insertCommand.CommandType = CommandType.StoredProcedure;
				insertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters; 
				SqlParameterCollection sqlParams = insertCommand.Parameters;

				sqlParams.Add(new SqlParameter(PARAM_ID,SqlDbType.Int));
				sqlParams[PARAM_ID].SourceColumn = tableRef.FLD_ID;

				sqlParams.Add(new SqlParameter(PARAM_NAME,SqlDbType.VarChar));
				sqlParams[PARAM_NAME].SourceColumn = tableRef.FLD_NAME;

				sqlParams.Add(new SqlParameter(PARAM_ADDRESS1,SqlDbType.VarChar));
				sqlParams[PARAM_ADDRESS1].SourceColumn = tableRef.FLD_ADDRESS1;

				sqlParams.Add(new SqlParameter(PARAM_ADDRESS2,SqlDbType.VarChar));
				sqlParams[PARAM_ADDRESS2].SourceColumn = tableRef.FLD_ADDRESS2;

				sqlParams.Add(new SqlParameter(PARAM_CITY,SqlDbType.VarChar));
				sqlParams[PARAM_CITY].SourceColumn = tableRef.FLD_CITY;

				sqlParams.Add(new SqlParameter(PARAM_STATE,SqlDbType.VarChar));
				sqlParams[PARAM_STATE].SourceColumn = tableRef.FLD_STATE;

				sqlParams.Add(new SqlParameter(PARAM_ZIP,SqlDbType.VarChar));
				sqlParams[PARAM_ZIP].SourceColumn = tableRef.FLD_ZIP;

				sqlParams.Add(new SqlParameter(PARAM_ZIPPLUSFOUR,SqlDbType.VarChar));
				sqlParams[PARAM_ZIPPLUSFOUR].SourceColumn = tableRef.FLD_ZIPPLUSFOUR;

				sqlParams.Add(new SqlParameter(PARAM_ATTNLINE,SqlDbType.VarChar));
				sqlParams[PARAM_ATTNLINE].SourceColumn = tableRef.FLD_ATTNLINE;

				sqlParams.Add(new SqlParameter(PARAM_FIELDMANAGERNO,SqlDbType.VarChar));
				sqlParams[PARAM_FIELDMANAGERNO].SourceColumn = tableRef.FLD_FIELDMANAGERNO;

				sqlParams.Add(new SqlParameter(PARAM_FIELDMANAGERREGION,SqlDbType.VarChar));
				sqlParams[PARAM_FIELDMANAGERREGION].SourceColumn = tableRef.FLD_FIELDMANAGERREGION;

				sqlParams.Add(new SqlParameter(PARAM_COUNTY,SqlDbType.VarChar));
				sqlParams[PARAM_COUNTY].SourceColumn = tableRef.FLD_COUNTY;

				sqlParams.Add(new SqlParameter(PARAM_COUNTYCODE,SqlDbType.VarChar));
				sqlParams[PARAM_COUNTYCODE].SourceColumn = tableRef.FLD_COUNTYCODE;

				sqlParams.Add(new SqlParameter(PARAM_SCHOOLTYPE,SqlDbType.VarChar));
				sqlParams[PARAM_SCHOOLTYPE].SourceColumn = tableRef.FLD_SCHOOLTYPE;

				sqlParams.Add(new SqlParameter(PARAM_PUBLICCATHOLIC,SqlDbType.VarChar));
				sqlParams[PARAM_PUBLICCATHOLIC].SourceColumn = tableRef.FLD_PUBLICCATHOLIC;

				sqlParams.Add(new SqlParameter(PARAM_TAXEXEMPTNUMBER,SqlDbType.VarChar));
				sqlParams[PARAM_TAXEXEMPTNUMBER].SourceColumn = tableRef.FLD_TAXEXEMPTNUMBER;

				sqlParams.Add(new SqlParameter(PARAM_CAMPAIGNSTART,SqlDbType.DateTime));
				sqlParams[PARAM_CAMPAIGNSTART].SourceColumn = tableRef.FLD_CAMPAIGNSTART;

				sqlParams.Add(new SqlParameter(PARAM_CAMPAIGNEND,SqlDbType.DateTime));
				sqlParams[PARAM_CAMPAIGNEND].SourceColumn = tableRef.FLD_CAMPAIGNEND;

				sqlParams.Add(new SqlParameter(PARAM_ISNATIONAL,SqlDbType.Bit));
				sqlParams[PARAM_ISNATIONAL].SourceColumn = tableRef.FLD_ISNATIONAL;

				sqlParams.Add(new SqlParameter(PARAM_UNITTYPE,SqlDbType.VarChar));
				sqlParams[PARAM_UNITTYPE].SourceColumn = tableRef.FLD_UNITTYPE;

				sqlParams.Add(new SqlParameter(PARAM_NATIONALDISTRICT,SqlDbType.VarChar));
				sqlParams[PARAM_NATIONALDISTRICT].SourceColumn = tableRef.FLD_NATIONALDISTRICT;

				sqlParams.Add(new SqlParameter(PARAM_NATIONALFIELDMANAGER,SqlDbType.VarChar));
				sqlParams[PARAM_NATIONALFIELDMANAGER].SourceColumn = tableRef.FLD_NATIONALFIELDMANAGER;

				sqlParams.Add(new SqlParameter(PARAM_SCHOOLDISTRICTNAME,SqlDbType.VarChar));
				sqlParams[PARAM_SCHOOLDISTRICTNAME].SourceColumn = tableRef.FLD_SCHOOLDISTRICTNAME;

				sqlParams.Add(new SqlParameter(PARAM_NUMBEROFCLASSROOMS,SqlDbType.Int));
				sqlParams[PARAM_NUMBEROFCLASSROOMS].SourceColumn = tableRef.FLD_NUMBEROFCLASSROOMS;

				sqlParams.Add(new SqlParameter(PARAM_NUMBEROFSTUDENTS,SqlDbType.Int));
				sqlParams[PARAM_NUMBEROFSTUDENTS].SourceColumn = tableRef.FLD_NUMBEROFSTUDENTS;

				sqlParams.Add(new SqlParameter(PARAM_SHIPTOACCTORFM,SqlDbType.VarChar));
				sqlParams[PARAM_SHIPTOACCTORFM].SourceColumn = tableRef.FLD_SHIPTOACCTORFM;

				sqlParams.Add(new SqlParameter(PARAM_AMFMIND,SqlDbType.VarChar));
				sqlParams[PARAM_AMFMIND].SourceColumn = tableRef.FLD_AMFMIND;

				sqlParams.Add(new SqlParameter(PARAM_COMMISSION,SqlDbType.Float));
				sqlParams[PARAM_COMMISSION].SourceColumn = tableRef.FLD_COMMISSION;
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
				deleteCommand = new SqlCommand("dbo.[pr_Account_Delete]");
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
				updateCommand = new SqlCommand("dbo.[pr_Account_Update]");
				updateCommand.CommandType = CommandType.StoredProcedure;
				updateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
				SqlParameterCollection sqlParams = updateCommand.Parameters;

				sqlParams.Add(new SqlParameter(PARAM_ID,SqlDbType.Int));
				sqlParams[PARAM_ID].SourceColumn = tableRef.FLD_ID;

				sqlParams.Add(new SqlParameter(PARAM_NAME,SqlDbType.VarChar));
				sqlParams[PARAM_NAME].SourceColumn = tableRef.FLD_NAME;

				sqlParams.Add(new SqlParameter(PARAM_ADDRESS1,SqlDbType.VarChar));
				sqlParams[PARAM_ADDRESS1].SourceColumn = tableRef.FLD_ADDRESS1;

				sqlParams.Add(new SqlParameter(PARAM_ADDRESS2,SqlDbType.VarChar));
				sqlParams[PARAM_ADDRESS2].SourceColumn = tableRef.FLD_ADDRESS2;

				sqlParams.Add(new SqlParameter(PARAM_CITY,SqlDbType.VarChar));
				sqlParams[PARAM_CITY].SourceColumn = tableRef.FLD_CITY;

				sqlParams.Add(new SqlParameter(PARAM_STATE,SqlDbType.VarChar));
				sqlParams[PARAM_STATE].SourceColumn = tableRef.FLD_STATE;

				sqlParams.Add(new SqlParameter(PARAM_ZIP,SqlDbType.VarChar));
				sqlParams[PARAM_ZIP].SourceColumn = tableRef.FLD_ZIP;

				sqlParams.Add(new SqlParameter(PARAM_ZIPPLUSFOUR,SqlDbType.VarChar));
				sqlParams[PARAM_ZIPPLUSFOUR].SourceColumn = tableRef.FLD_ZIPPLUSFOUR;

				sqlParams.Add(new SqlParameter(PARAM_ATTNLINE,SqlDbType.VarChar));
				sqlParams[PARAM_ATTNLINE].SourceColumn = tableRef.FLD_ATTNLINE;

				sqlParams.Add(new SqlParameter(PARAM_FIELDMANAGERNO,SqlDbType.VarChar));
				sqlParams[PARAM_FIELDMANAGERNO].SourceColumn = tableRef.FLD_FIELDMANAGERNO;

				sqlParams.Add(new SqlParameter(PARAM_FIELDMANAGERREGION,SqlDbType.VarChar));
				sqlParams[PARAM_FIELDMANAGERREGION].SourceColumn = tableRef.FLD_FIELDMANAGERREGION;

				sqlParams.Add(new SqlParameter(PARAM_COUNTY,SqlDbType.VarChar));
				sqlParams[PARAM_COUNTY].SourceColumn = tableRef.FLD_COUNTY;

				sqlParams.Add(new SqlParameter(PARAM_COUNTYCODE,SqlDbType.VarChar));
				sqlParams[PARAM_COUNTYCODE].SourceColumn = tableRef.FLD_COUNTYCODE;

				sqlParams.Add(new SqlParameter(PARAM_SCHOOLTYPE,SqlDbType.VarChar));
				sqlParams[PARAM_SCHOOLTYPE].SourceColumn = tableRef.FLD_SCHOOLTYPE;

				sqlParams.Add(new SqlParameter(PARAM_PUBLICCATHOLIC,SqlDbType.VarChar));
				sqlParams[PARAM_PUBLICCATHOLIC].SourceColumn = tableRef.FLD_PUBLICCATHOLIC;

				sqlParams.Add(new SqlParameter(PARAM_TAXEXEMPTNUMBER,SqlDbType.VarChar));
				sqlParams[PARAM_TAXEXEMPTNUMBER].SourceColumn = tableRef.FLD_TAXEXEMPTNUMBER;

				sqlParams.Add(new SqlParameter(PARAM_CAMPAIGNSTART,SqlDbType.DateTime));
				sqlParams[PARAM_CAMPAIGNSTART].SourceColumn = tableRef.FLD_CAMPAIGNSTART;

				sqlParams.Add(new SqlParameter(PARAM_CAMPAIGNEND,SqlDbType.DateTime));
				sqlParams[PARAM_CAMPAIGNEND].SourceColumn = tableRef.FLD_CAMPAIGNEND;

				sqlParams.Add(new SqlParameter(PARAM_ISNATIONAL,SqlDbType.Bit));
				sqlParams[PARAM_ISNATIONAL].SourceColumn = tableRef.FLD_ISNATIONAL;

				sqlParams.Add(new SqlParameter(PARAM_UNITTYPE,SqlDbType.VarChar));
				sqlParams[PARAM_UNITTYPE].SourceColumn = tableRef.FLD_UNITTYPE;

				sqlParams.Add(new SqlParameter(PARAM_NATIONALDISTRICT,SqlDbType.VarChar));
				sqlParams[PARAM_NATIONALDISTRICT].SourceColumn = tableRef.FLD_NATIONALDISTRICT;

				sqlParams.Add(new SqlParameter(PARAM_NATIONALFIELDMANAGER,SqlDbType.VarChar));
				sqlParams[PARAM_NATIONALFIELDMANAGER].SourceColumn = tableRef.FLD_NATIONALFIELDMANAGER;

				sqlParams.Add(new SqlParameter(PARAM_SCHOOLDISTRICTNAME,SqlDbType.VarChar));
				sqlParams[PARAM_SCHOOLDISTRICTNAME].SourceColumn = tableRef.FLD_SCHOOLDISTRICTNAME;

				sqlParams.Add(new SqlParameter(PARAM_NUMBEROFCLASSROOMS,SqlDbType.Int));
				sqlParams[PARAM_NUMBEROFCLASSROOMS].SourceColumn = tableRef.FLD_NUMBEROFCLASSROOMS;

				sqlParams.Add(new SqlParameter(PARAM_NUMBEROFSTUDENTS,SqlDbType.Int));
				sqlParams[PARAM_NUMBEROFSTUDENTS].SourceColumn = tableRef.FLD_NUMBEROFSTUDENTS;

				sqlParams.Add(new SqlParameter(PARAM_SHIPTOACCTORFM,SqlDbType.VarChar));
				sqlParams[PARAM_SHIPTOACCTORFM].SourceColumn = tableRef.FLD_SHIPTOACCTORFM;

				sqlParams.Add(new SqlParameter(PARAM_AMFMIND,SqlDbType.VarChar));
				sqlParams[PARAM_AMFMIND].SourceColumn = tableRef.FLD_AMFMIND;

				sqlParams.Add(new SqlParameter(PARAM_COMMISSION,SqlDbType.Float));
				sqlParams[PARAM_COMMISSION].SourceColumn = tableRef.FLD_COMMISSION;
			}
			return updateCommand;
		}
		protected override string TableName
		{
			get
			{
				return tableRef.TBL_ACCOUNT;
			}
		}
		public void SelectSearch(DataTable Table,int SearchType,string SearchCriteria)
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.pr_Account_Search";
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
		///		 <LI>sName</LI>
		///		 <LI>sAddress1</LI>
		///		 <LI>sAddress2</LI>
		///		 <LI>sCity</LI>
		///		 <LI>sState</LI>
		///		 <LI>sZip</LI>
		///		 <LI>sZipPlusFour</LI>
		///		 <LI>sAttnLine</LI>
		///		 <LI>sFieldManagerNo</LI>
		///		 <LI>sFieldManagerRegion</LI>
		///		 <LI>sCounty</LI>
		///		 <LI>sCountyCode</LI>
		///		 <LI>sSchoolType</LI>
		///		 <LI>sPublicCatholic</LI>
		///		 <LI>sTaxExemptNumber</LI>
		///		 <LI>daCampaignStart</LI>
		///		 <LI>daCampaignEnd</LI>
		///		 <LI>bIsNational</LI>
		///		 <LI>sUnitType</LI>
		///		 <LI>sNationalDistrict</LI>
		///		 <LI>sNationalFieldManager</LI>
		///		 <LI>sSchoolDistrictName</LI>
		///		 <LI>iNumberOfClassrooms</LI>
		///		 <LI>iNumberOfStudents</LI>
		///		 <LI>sShipToAcctOrFM</LI>
		///		 <LI>sAMFMInd</LI>
		///		 <LI>fCommission</LI>
		/// Will fill all properties corresponding with a field in the table with the value of the row selected.
		/// </remarks>
		public  void SelectOne(DataTable Table, Int32 ID)		
		{
			SqlCommand	scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[pr_Account_SelectOne]";
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
			scmCmdToExecute.CommandText = "dbo.[pr_Account_SelectAll]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			Select(scmCmdToExecute,Table);
		}
		public void SelectFieldManager(DataTable Table,int CampaingID)
		{
			SqlCommand	scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[pr_FieldManager_ByCampaignID]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.Parameters.Add(new SqlParameter("@iCampaignID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CampaingID));
			Select(scmCmdToExecute,Table);
		}
		internal void SelectAllFulfillmentHouse(DataTable Table)
		{
			SqlCommand	scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "qspcanadaproduct..GetAllFulfillmentHouse";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			
			Select(scmCmdToExecute,Table);
		}
		
		internal void SelectAllIncludInIncident(DataTable Table)
		{
				SqlCommand	scmCmdToExecute = new SqlCommand();
				scmCmdToExecute.CommandText = "pr_SelectAllIncludInIncident";
				scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			
				Select(scmCmdToExecute,Table);
		}
		internal void SelectAllPublisher(DataTable Table,int FulfillmentHouseID)
		{
			SqlCommand	scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "pr_publisher_SelectAll";
			scmCmdToExecute.Parameters.Add(new SqlParameter("@iFulfillmentHouseID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, FulfillmentHouseID));
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			
			Select(scmCmdToExecute,Table);
		}

		internal void SelectAllFieldManager(DataTable Table) 
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "pr_FieldManager_SelectAll";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;

			Select(scmCmdToExecute, Table);
		}

		internal void SelectAllProgram(DataTable Table) 
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "pr_Program_SelectAll";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;

			Select(scmCmdToExecute, Table);
		}

		internal void SelectAllProgram(DataTable Table, int programTypeID) 
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "pr_Program_SelectByProgramTypeID";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.Parameters.Add(new SqlParameter("@iProgramTypeID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, programTypeID));

			Select(scmCmdToExecute, Table);
		}

		internal void SelectAllCatalogCode(DataTable Table) 
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "pr_CatalogCode_SelectAll";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;

			Select(scmCmdToExecute, Table);
		}
	}
}
