///////////////////////////////////////////////////////////////////////////
// Description: Data Access class for the table 'CAccount'
// Generated by GenerationClass v1.2.1949.28361 Final
// on: 4 mai, 2005, 12:05:29
// Because the Base Class already implements IDispose, this class doesn't.
///////////////////////////////////////////////////////////////////////////
using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using Common;
using Common.TableDef;
using dataSetRef = Common.TableDef.FieldSuppliesOrderListDataSet;


namespace DAL
{
	/// <summary>
	/// Purpose: Data Access class for the table 'CAccount'.
	/// </summary>
	public class FieldSuppliesData : DBTableOperation
	{
		#region Class Member Declarations
		internal const string PARAM_CAMPAIGNID= "@iCampaignID";
		//

		// DataSetCommand object

		//

		private SqlCommand insertCommand = null;

		private SqlCommand deleteCommand = null;

		private SqlCommand updateCommand = null;

		#endregion

		dataSetRef dataSet = new dataSetRef();



		/// <summary>
		/// Purpose: Class constructor.
		/// </summary>
		public FieldSuppliesData() : base(DataBaseName.QSPCanadaCommon) { }
		//----------------------------------------------------------------
		// Sub GetInsertCommand:
		//   Initialize the parameterized Insert command for the DataAdapter
		//----------------------------------------------------------------
		protected override SqlCommand GetInsertCommand()
		{
			/*if ( insertCommand == null )
			{
				//
				// Construct the command since we don't have it already
				// 
				insertCommand = new SqlCommand("dbo.[pr_CAccount_Insert]");
				insertCommand.CommandType = CommandType.StoredProcedure;
				insertCommand.UpdatedRowSource = UpdateRowSource.FirstReturnedRecord; 
				SqlParameterCollection sqlParams = insertCommand.Parameters;

				sqlParams.Add(new SqlParameter(PARAM_NAME,SqlDbType.VarChar));
				sqlParams[PARAM_NAME].SourceColumn = dataSet.CAccount.NameColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_COUNTRY,SqlDbType.VarChar));
				sqlParams[PARAM_COUNTRY].SourceColumn = dataSet.CAccount.CountryColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_LANG,SqlDbType.VarChar));
				sqlParams[PARAM_LANG].SourceColumn = dataSet.CAccount.LangColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_CACCOUNTCODECLASS,SqlDbType.VarChar));
				sqlParams[PARAM_CACCOUNTCODECLASS].SourceColumn = dataSet.CAccount.CAccountCodeClassColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_CACCOUNTCODEGROUP,SqlDbType.VarChar));
				sqlParams[PARAM_CACCOUNTCODEGROUP].SourceColumn = dataSet.CAccount.CAccountCodeGroupColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_PHONELISTID,SqlDbType.Int));
				sqlParams[PARAM_PHONELISTID].SourceColumn = dataSet.CAccount.PhoneListIDColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_ADDRESSLISTID,SqlDbType.Int));
				sqlParams[PARAM_ADDRESSLISTID].SourceColumn = dataSet.CAccount.AddressListIDColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_ADDRESS1,SqlDbType.VarChar));
				sqlParams[PARAM_ADDRESS1].SourceColumn = dataSet.CAccount.Address1Column.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_ADDRESS2,SqlDbType.VarChar));
				sqlParams[PARAM_ADDRESS2].SourceColumn = dataSet.CAccount.Address2Column.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_CITY,SqlDbType.VarChar));
				sqlParams[PARAM_CITY].SourceColumn = dataSet.CAccount.CityColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_STATE,SqlDbType.Char));
				sqlParams[PARAM_STATE].SourceColumn = dataSet.CAccount.StateColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_ZIP,SqlDbType.VarChar));
				sqlParams[PARAM_ZIP].SourceColumn = dataSet.CAccount.ZipColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_ZIP4,SqlDbType.VarChar));
				sqlParams[PARAM_ZIP4].SourceColumn = dataSet.CAccount.Zip4Column.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_COUNTY,SqlDbType.VarChar));
				sqlParams[PARAM_COUNTY].SourceColumn = dataSet.CAccount.CountyColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_STATUSID,SqlDbType.Int));
				sqlParams[PARAM_STATUSID].SourceColumn = dataSet.CAccount.StatusIDColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_ENROLLMENT,SqlDbType.Int));
				sqlParams[PARAM_ENROLLMENT].SourceColumn = dataSet.CAccount.EnrollmentColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_COMMENT,SqlDbType.VarChar));
				sqlParams[PARAM_COMMENT].SourceColumn = dataSet.CAccount.CommentColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_EMAIL,SqlDbType.VarChar));
				sqlParams[PARAM_EMAIL].SourceColumn = dataSet.CAccount.EMailColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_ISPRIVATEORG,SqlDbType.Bit));
				sqlParams[PARAM_ISPRIVATEORG].SourceColumn = dataSet.CAccount.IsPrivateOrgColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_ISADULTGROUP,SqlDbType.Bit));
				sqlParams[PARAM_ISADULTGROUP].SourceColumn = dataSet.CAccount.IsAdultGroupColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_PARENTID,SqlDbType.Int));
				sqlParams[PARAM_PARENTID].SourceColumn = dataSet.CAccount.ParentIDColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_SALESREGIONID,SqlDbType.Int));
				sqlParams[PARAM_SALESREGIONID].SourceColumn = dataSet.CAccount.SalesRegionIDColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_STATEMENTPRINTCYCLEID,SqlDbType.Int));
				sqlParams[PARAM_STATEMENTPRINTCYCLEID].SourceColumn = dataSet.CAccount.StatementPrintCycleIDColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_STATEMENTPRINTSLOT,SqlDbType.Int));
				sqlParams[PARAM_STATEMENTPRINTSLOT].SourceColumn = dataSet.CAccount.StatementPrintSlotColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_DATECREATEDTOSSTHIS,SqlDbType.DateTime));
				sqlParams[PARAM_DATECREATEDTOSSTHIS].SourceColumn = dataSet.CAccount.DateCreatedTOSSthisColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_DATEUPDATED,SqlDbType.DateTime));
				sqlParams[PARAM_DATEUPDATED].SourceColumn = dataSet.CAccount.DateUpdatedColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_USERIDMODIFIED,SqlDbType.Int));
				sqlParams[PARAM_USERIDMODIFIED].SourceColumn = dataSet.CAccount.UserIDModifiedColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_VENDORNUMBER,SqlDbType.VarChar));
				sqlParams[PARAM_VENDORNUMBER].SourceColumn = dataSet.CAccount.VendorNumberColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_VENDORSITENAME,SqlDbType.VarChar));
				sqlParams[PARAM_VENDORSITENAME].SourceColumn = dataSet.CAccount.VendorSiteNameColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_VENDORPAYGROUP,SqlDbType.VarChar));
				sqlParams[PARAM_VENDORPAYGROUP].SourceColumn = dataSet.CAccount.VendorPayGroupColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_ORIGINALADDRESS1,SqlDbType.VarChar));
				sqlParams[PARAM_ORIGINALADDRESS1].SourceColumn = dataSet.CAccount.OriginalAddress1Column.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_ORIGINALADDRESS2,SqlDbType.VarChar));
				sqlParams[PARAM_ORIGINALADDRESS2].SourceColumn = dataSet.CAccount.OriginalAddress2Column.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_ORIGINALCITY,SqlDbType.VarChar));
				sqlParams[PARAM_ORIGINALCITY].SourceColumn = dataSet.CAccount.OriginalCityColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_ORIGINALSTATE,SqlDbType.Char));
				sqlParams[PARAM_ORIGINALSTATE].SourceColumn = dataSet.CAccount.OriginalStateColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_ORIGINALZIP,SqlDbType.VarChar));
				sqlParams[PARAM_ORIGINALZIP].SourceColumn = dataSet.CAccount.OriginalZipColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_ORIGINALZIP4,SqlDbType.VarChar));
				sqlParams[PARAM_ORIGINALZIP4].SourceColumn = dataSet.CAccount.OriginalZip4Column.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_SHIPTOADDRESS1,SqlDbType.VarChar));
				sqlParams[PARAM_SHIPTOADDRESS1].SourceColumn = dataSet.CAccount.ShipToAddress1Column.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_SHIPTOADDRESS2,SqlDbType.VarChar));
				sqlParams[PARAM_SHIPTOADDRESS2].SourceColumn = dataSet.CAccount.ShipToAddress2Column.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_SHIPTOCITY,SqlDbType.VarChar));
				sqlParams[PARAM_SHIPTOCITY].SourceColumn = dataSet.CAccount.ShipToCityColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_SHIPTOSTATE,SqlDbType.Char));
				sqlParams[PARAM_SHIPTOSTATE].SourceColumn = dataSet.CAccount.ShipToStateColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_SHIPTOZIP,SqlDbType.VarChar));
				sqlParams[PARAM_SHIPTOZIP].SourceColumn = dataSet.CAccount.ShipToZipColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_SHIPTOZIP4,SqlDbType.VarChar));
				sqlParams[PARAM_SHIPTOZIP4].SourceColumn = dataSet.CAccount.ShipToZip4Column.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_SPONSOR,SqlDbType.VarChar));
				sqlParams[PARAM_SPONSOR].SourceColumn = dataSet.CAccount.SponsorColumn.ColumnName;
			}*/
			return insertCommand;
		}
		//----------------------------------------------------------------
		// Sub BuildUpdateCommand:
		//   Initialize the parameterized Update command for the DataAdapter
		//----------------------------------------------------------------
		protected override SqlCommand GetDeleteCommand()
		{
			/*if ( deleteCommand == null )
			{
				//
				// Construct the command since we don't have it already
				//
				deleteCommand = new SqlCommand("dbo.[pr_CAccount_Delete]");
				deleteCommand.CommandType = CommandType.StoredProcedure;
				SqlParameterCollection sqlParams = deleteCommand.Parameters;

				sqlParams.Add(new SqlParameter(PARAM_ID,SqlDbType.Int));
				sqlParams[PARAM_ID].SourceColumn = dataSet.CAccount.IdColumn.ColumnName;
			}*/
			return deleteCommand;
		}
		//----------------------------------------------------------------
		// Sub BuildUpdateCommand:
		//   Initialize the parameterized Update command for the DataAdapter
		//----------------------------------------------------------------
		protected override SqlCommand GetUpdateCommand()
		{
			/*if ( updateCommand == null )
			{
				//
				// Construct the command since we don't have it already
				//
				updateCommand = new SqlCommand("dbo.[pr_CAccount_Update]");
				updateCommand.CommandType = CommandType.StoredProcedure;
				updateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
				SqlParameterCollection sqlParams = updateCommand.Parameters;

				sqlParams.Add(new SqlParameter(PARAM_ID,SqlDbType.Int));
				sqlParams[PARAM_ID].SourceColumn = dataSet.CAccount.IdColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_NAME,SqlDbType.VarChar));
				sqlParams[PARAM_NAME].SourceColumn = dataSet.CAccount.NameColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_COUNTRY,SqlDbType.VarChar));
				sqlParams[PARAM_COUNTRY].SourceColumn = dataSet.CAccount.CountryColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_LANG,SqlDbType.VarChar));
				sqlParams[PARAM_LANG].SourceColumn = dataSet.CAccount.LangColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_CACCOUNTCODECLASS,SqlDbType.VarChar));
				sqlParams[PARAM_CACCOUNTCODECLASS].SourceColumn = dataSet.CAccount.CAccountCodeClassColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_CACCOUNTCODEGROUP,SqlDbType.VarChar));
				sqlParams[PARAM_CACCOUNTCODEGROUP].SourceColumn = dataSet.CAccount.CAccountCodeGroupColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_PHONELISTID,SqlDbType.Int));
				sqlParams[PARAM_PHONELISTID].SourceColumn = dataSet.CAccount.PhoneListIDColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_ADDRESSLISTID,SqlDbType.Int));
				sqlParams[PARAM_ADDRESSLISTID].SourceColumn = dataSet.CAccount.AddressListIDColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_ADDRESS1,SqlDbType.VarChar));
				sqlParams[PARAM_ADDRESS1].SourceColumn = dataSet.CAccount.Address1Column.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_ADDRESS2,SqlDbType.VarChar));
				sqlParams[PARAM_ADDRESS2].SourceColumn = dataSet.CAccount.Address2Column.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_CITY,SqlDbType.VarChar));
				sqlParams[PARAM_CITY].SourceColumn = dataSet.CAccount.CityColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_STATE,SqlDbType.Char));
				sqlParams[PARAM_STATE].SourceColumn = dataSet.CAccount.StateColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_ZIP,SqlDbType.VarChar));
				sqlParams[PARAM_ZIP].SourceColumn = dataSet.CAccount.ZipColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_ZIP4,SqlDbType.VarChar));
				sqlParams[PARAM_ZIP4].SourceColumn = dataSet.CAccount.Zip4Column.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_COUNTY,SqlDbType.VarChar));
				sqlParams[PARAM_COUNTY].SourceColumn = dataSet.CAccount.CountyColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_STATUSID,SqlDbType.Int));
				sqlParams[PARAM_STATUSID].SourceColumn = dataSet.CAccount.StatusIDColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_ENROLLMENT,SqlDbType.Int));
				sqlParams[PARAM_ENROLLMENT].SourceColumn = dataSet.CAccount.EnrollmentColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_COMMENT,SqlDbType.VarChar));
				sqlParams[PARAM_COMMENT].SourceColumn = dataSet.CAccount.CommentColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_EMAIL,SqlDbType.VarChar));
				sqlParams[PARAM_EMAIL].SourceColumn = dataSet.CAccount.EMailColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_ISPRIVATEORG,SqlDbType.Bit));
				sqlParams[PARAM_ISPRIVATEORG].SourceColumn = dataSet.CAccount.IsPrivateOrgColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_ISADULTGROUP,SqlDbType.Bit));
				sqlParams[PARAM_ISADULTGROUP].SourceColumn = dataSet.CAccount.IsAdultGroupColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_PARENTID,SqlDbType.Int));
				sqlParams[PARAM_PARENTID].SourceColumn = dataSet.CAccount.ParentIDColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_SALESREGIONID,SqlDbType.Int));
				sqlParams[PARAM_SALESREGIONID].SourceColumn = dataSet.CAccount.SalesRegionIDColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_STATEMENTPRINTCYCLEID,SqlDbType.Int));
				sqlParams[PARAM_STATEMENTPRINTCYCLEID].SourceColumn = dataSet.CAccount.StatementPrintCycleIDColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_STATEMENTPRINTSLOT,SqlDbType.Int));
				sqlParams[PARAM_STATEMENTPRINTSLOT].SourceColumn = dataSet.CAccount.StatementPrintSlotColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_DATECREATEDTOSSTHIS,SqlDbType.DateTime));
				sqlParams[PARAM_DATECREATEDTOSSTHIS].SourceColumn = dataSet.CAccount.DateCreatedTOSSthisColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_DATEUPDATED,SqlDbType.DateTime));
				sqlParams[PARAM_DATEUPDATED].SourceColumn = dataSet.CAccount.DateUpdatedColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_USERIDMODIFIED,SqlDbType.Int));
				sqlParams[PARAM_USERIDMODIFIED].SourceColumn = dataSet.CAccount.UserIDModifiedColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_VENDORNUMBER,SqlDbType.VarChar));
				sqlParams[PARAM_VENDORNUMBER].SourceColumn = dataSet.CAccount.VendorNumberColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_VENDORSITENAME,SqlDbType.VarChar));
				sqlParams[PARAM_VENDORSITENAME].SourceColumn = dataSet.CAccount.VendorSiteNameColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_VENDORPAYGROUP,SqlDbType.VarChar));
				sqlParams[PARAM_VENDORPAYGROUP].SourceColumn = dataSet.CAccount.VendorPayGroupColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_ORIGINALADDRESS1,SqlDbType.VarChar));
				sqlParams[PARAM_ORIGINALADDRESS1].SourceColumn = dataSet.CAccount.OriginalAddress1Column.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_ORIGINALADDRESS2,SqlDbType.VarChar));
				sqlParams[PARAM_ORIGINALADDRESS2].SourceColumn = dataSet.CAccount.OriginalAddress2Column.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_ORIGINALCITY,SqlDbType.VarChar));
				sqlParams[PARAM_ORIGINALCITY].SourceColumn = dataSet.CAccount.OriginalCityColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_ORIGINALSTATE,SqlDbType.Char));
				sqlParams[PARAM_ORIGINALSTATE].SourceColumn = dataSet.CAccount.OriginalStateColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_ORIGINALZIP,SqlDbType.VarChar));
				sqlParams[PARAM_ORIGINALZIP].SourceColumn = dataSet.CAccount.OriginalZipColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_ORIGINALZIP4,SqlDbType.VarChar));
				sqlParams[PARAM_ORIGINALZIP4].SourceColumn = dataSet.CAccount.OriginalZip4Column.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_SHIPTOADDRESS1,SqlDbType.VarChar));
				sqlParams[PARAM_SHIPTOADDRESS1].SourceColumn = dataSet.CAccount.ShipToAddress1Column.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_SHIPTOADDRESS2,SqlDbType.VarChar));
				sqlParams[PARAM_SHIPTOADDRESS2].SourceColumn = dataSet.CAccount.ShipToAddress2Column.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_SHIPTOCITY,SqlDbType.VarChar));
				sqlParams[PARAM_SHIPTOCITY].SourceColumn = dataSet.CAccount.ShipToCityColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_SHIPTOSTATE,SqlDbType.Char));
				sqlParams[PARAM_SHIPTOSTATE].SourceColumn = dataSet.CAccount.ShipToStateColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_SHIPTOZIP,SqlDbType.VarChar));
				sqlParams[PARAM_SHIPTOZIP].SourceColumn = dataSet.CAccount.ShipToZipColumn.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_SHIPTOZIP4,SqlDbType.VarChar));
				sqlParams[PARAM_SHIPTOZIP4].SourceColumn = dataSet.CAccount.ShipToZip4Column.ColumnName;

				sqlParams.Add(new SqlParameter(PARAM_SPONSOR,SqlDbType.VarChar));
				sqlParams[PARAM_SPONSOR].SourceColumn = dataSet.CAccount.SponsorColumn.ColumnName;
			}*/
			return updateCommand;
		}

		public void SelectSearch(DataSet dtsDataSet, string tableName, int CampaignID) 
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.[pr_FieldSuppliesOrder_SelectSearch]";
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.Parameters.Add(new SqlParameter(PARAM_CAMPAIGNID, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CampaignID));
			Select(cmdToExecute,dtsDataSet, tableName);
		}
	}
}