///////////////////////////////////////////////////////////////////////////
// Description: Data Access class for the table 'Customer'
// Generated by LLBLGen v1.2.1594.24829 Final
// on: Thursday, May 13, 2004, 2:49:44 PM
// Because the Base Class already implements IDispose, this class doesn't.
///////////////////////////////////////////////////////////////////////////
using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using tableRef =QSPFulfillment.DataAccess.Common.TableDef.CustomerTable;
using QSPFulfillment.DataAccess.Common.ActionObject;


namespace QSPFulfillment.DataAccess.Data
{
	/// <summary>
	/// Purpose: Data Access class for the table 'Customer'.
	/// </summary>
	public class CustomerData : QSPFulfillment.DataAccess.Data.DBTableOperation
	{
		#region Class Member Declarations
		internal const string PARAM_INSTANCE= "@iInstance";
		internal const string PARAM_STATUSINSTANCE= "@iStatusInstance";
		internal const string PARAM_LASTNAME= "@sLastName";
		internal const string PARAM_FIRSTNAME= "@sFirstName";
		internal const string PARAM_ADDRESS1= "@sAddress1";
		internal const string PARAM_ADDRESS2= "@sAddress2";
		internal const string PARAM_CITY= "@sCity";
		internal const string PARAM_COUNTY= "@sCounty";
		internal const string PARAM_STATE= "@sState";
		internal const string PARAM_ZIP= "@sZip";
		internal const string PARAM_ZIPPLUSFOUR= "@sZipPlusFour";
		internal const string PARAM_OVERRIDEADDRESS= "@bOverrideAddress";
		internal const string PARAM_CHANGEUSERID= "@sChangeUserID";
		internal const string PARAM_CHANGEDATE= "@daChangeDate";
		internal const string PARAM_EMAIL= "@sEmail";
		internal const string PARAM_PHONE= "@sPhone";
		internal const string PARAM_TYPE= "@iType";
		internal const string PARAM_SEARCH_CRITERIA = "@isearch_type";

		internal const string PARAM_SEARCH_TYPE = "@scriteria";

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
		public CustomerData()
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
				insertCommand = new SqlCommand("dbo.[pr_Customer_Insert]");
				insertCommand.CommandType = CommandType.StoredProcedure;
				insertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters; 
				SqlParameterCollection sqlParams = insertCommand.Parameters;

				//sqlParams.Add(new SqlParameter(PARAM_INSTANCE,SqlDbType.Int));
				//sqlParams[PARAM_INSTANCE].SourceColumn = tableRef.FLD_INSTANCE;

				sqlParams.Add(new SqlParameter(PARAM_STATUSINSTANCE,SqlDbType.Int));
				sqlParams[PARAM_STATUSINSTANCE].SourceColumn = tableRef.FLD_STATUSINSTANCE;

				sqlParams.Add(new SqlParameter(PARAM_LASTNAME,SqlDbType.VarChar));
				sqlParams[PARAM_LASTNAME].SourceColumn = tableRef.FLD_LASTNAME;

				sqlParams.Add(new SqlParameter(PARAM_FIRSTNAME,SqlDbType.VarChar));
				sqlParams[PARAM_FIRSTNAME].SourceColumn = tableRef.FLD_FIRSTNAME;

				sqlParams.Add(new SqlParameter(PARAM_ADDRESS1,SqlDbType.VarChar));
				sqlParams[PARAM_ADDRESS1].SourceColumn = tableRef.FLD_ADDRESS1;

				sqlParams.Add(new SqlParameter(PARAM_ADDRESS2,SqlDbType.VarChar));
				sqlParams[PARAM_ADDRESS2].SourceColumn = tableRef.FLD_ADDRESS2;

				sqlParams.Add(new SqlParameter(PARAM_CITY,SqlDbType.VarChar));
				sqlParams[PARAM_CITY].SourceColumn = tableRef.FLD_CITY;

				sqlParams.Add(new SqlParameter(PARAM_COUNTY,SqlDbType.VarChar));
				sqlParams[PARAM_COUNTY].SourceColumn = tableRef.FLD_COUNTY;

				sqlParams.Add(new SqlParameter(PARAM_STATE,SqlDbType.VarChar));
				sqlParams[PARAM_STATE].SourceColumn = tableRef.FLD_STATE;

				sqlParams.Add(new SqlParameter(PARAM_ZIP,SqlDbType.VarChar));
				sqlParams[PARAM_ZIP].SourceColumn = tableRef.FLD_ZIP;

				//sqlParams.Add(new SqlParameter(PARAM_ZIPPLUSFOUR,SqlDbType.VarChar));
				//sqlParams[PARAM_ZIPPLUSFOUR].SourceColumn = tableRef.FLD_ZIPPLUSFOUR;

				sqlParams.Add(new SqlParameter(PARAM_OVERRIDEADDRESS,SqlDbType.Bit));
				sqlParams[PARAM_OVERRIDEADDRESS].SourceColumn = tableRef.FLD_OVERRIDEADDRESS;

				sqlParams.Add(new SqlParameter(PARAM_CHANGEUSERID,SqlDbType.VarChar));
				sqlParams[PARAM_CHANGEUSERID].SourceColumn = tableRef.FLD_CHANGEUSERID;

				sqlParams.Add(new SqlParameter(PARAM_CHANGEDATE,SqlDbType.DateTime));
				sqlParams[PARAM_CHANGEDATE].SourceColumn = tableRef.FLD_CHANGEDATE;

				sqlParams.Add(new SqlParameter(PARAM_EMAIL,SqlDbType.VarChar));
				sqlParams[PARAM_EMAIL].SourceColumn = tableRef.FLD_EMAIL;

				sqlParams.Add(new SqlParameter(PARAM_PHONE,SqlDbType.VarChar));
				sqlParams[PARAM_PHONE].SourceColumn = tableRef.FLD_PHONE;

				sqlParams.Add(new SqlParameter(PARAM_TYPE,SqlDbType.Int));
				sqlParams[PARAM_TYPE].SourceColumn = tableRef.FLD_TYPE;
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
				deleteCommand = new SqlCommand("dbo.[pr_Customer_Delete]");
				deleteCommand.CommandType = CommandType.StoredProcedure;
				SqlParameterCollection sqlParams = deleteCommand.Parameters;

				sqlParams.Add(new SqlParameter(PARAM_INSTANCE,SqlDbType.Int));
				sqlParams[PARAM_INSTANCE].SourceColumn = tableRef.FLD_INSTANCE;
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
				updateCommand = new SqlCommand("dbo.[pr_Customer_Update]");
				updateCommand.CommandType = CommandType.StoredProcedure;
				updateCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
				SqlParameterCollection sqlParams = updateCommand.Parameters;

				sqlParams.Add(new SqlParameter(PARAM_INSTANCE,SqlDbType.Int));
				sqlParams[PARAM_INSTANCE].SourceColumn = tableRef.FLD_INSTANCE;

				sqlParams.Add(new SqlParameter(PARAM_STATUSINSTANCE,SqlDbType.Int));
				sqlParams[PARAM_STATUSINSTANCE].SourceColumn = tableRef.FLD_STATUSINSTANCE;

				sqlParams.Add(new SqlParameter(PARAM_LASTNAME,SqlDbType.VarChar));
				sqlParams[PARAM_LASTNAME].SourceColumn = tableRef.FLD_LASTNAME;

				sqlParams.Add(new SqlParameter(PARAM_FIRSTNAME,SqlDbType.VarChar));
				sqlParams[PARAM_FIRSTNAME].SourceColumn = tableRef.FLD_FIRSTNAME;

				sqlParams.Add(new SqlParameter(PARAM_ADDRESS1,SqlDbType.VarChar));
				sqlParams[PARAM_ADDRESS1].SourceColumn = tableRef.FLD_ADDRESS1;

				sqlParams.Add(new SqlParameter(PARAM_ADDRESS2,SqlDbType.VarChar));
				sqlParams[PARAM_ADDRESS2].SourceColumn = tableRef.FLD_ADDRESS2;

				sqlParams.Add(new SqlParameter(PARAM_CITY,SqlDbType.VarChar));
				sqlParams[PARAM_CITY].SourceColumn = tableRef.FLD_CITY;

				sqlParams.Add(new SqlParameter(PARAM_COUNTY,SqlDbType.VarChar));
				sqlParams[PARAM_COUNTY].SourceColumn = tableRef.FLD_COUNTY;

				sqlParams.Add(new SqlParameter(PARAM_STATE,SqlDbType.VarChar));
				sqlParams[PARAM_STATE].SourceColumn = tableRef.FLD_STATE;

				sqlParams.Add(new SqlParameter(PARAM_ZIP,SqlDbType.VarChar));
				sqlParams[PARAM_ZIP].SourceColumn = tableRef.FLD_ZIP;

				sqlParams.Add(new SqlParameter(PARAM_ZIPPLUSFOUR,SqlDbType.VarChar));
				sqlParams[PARAM_ZIPPLUSFOUR].SourceColumn = tableRef.FLD_ZIPPLUSFOUR;

				sqlParams.Add(new SqlParameter(PARAM_OVERRIDEADDRESS,SqlDbType.Bit));
				sqlParams[PARAM_OVERRIDEADDRESS].SourceColumn = tableRef.FLD_OVERRIDEADDRESS;

				sqlParams.Add(new SqlParameter(PARAM_CHANGEUSERID,SqlDbType.VarChar));
				sqlParams[PARAM_CHANGEUSERID].SourceColumn = tableRef.FLD_CHANGEUSERID;

				sqlParams.Add(new SqlParameter(PARAM_CHANGEDATE,SqlDbType.DateTime));
				sqlParams[PARAM_CHANGEDATE].SourceColumn = tableRef.FLD_CHANGEDATE;

				sqlParams.Add(new SqlParameter(PARAM_EMAIL,SqlDbType.VarChar));
				sqlParams[PARAM_EMAIL].SourceColumn = tableRef.FLD_EMAIL;

				sqlParams.Add(new SqlParameter(PARAM_PHONE,SqlDbType.VarChar));
				sqlParams[PARAM_PHONE].SourceColumn = tableRef.FLD_PHONE;

				sqlParams.Add(new SqlParameter(PARAM_TYPE,SqlDbType.Int));
				sqlParams[PARAM_TYPE].SourceColumn = tableRef.FLD_TYPE;
			}
			return updateCommand;
		}
		protected override string TableName
		{
			get
			{
				return tableRef.TBL_CUSTOMER;
			}
		}
		public void SelectSearch(DataTable Table,int SearchType,string SearchCriteria)
		{
			SqlCommand	cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = "dbo.pr_Customer_Search";
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
		///		 <LI>iInstance</LI>
		/// </UL>
		///		 <LI>iInstance</LI>
		///		 <LI>iStatusInstance</LI>
		///		 <LI>sLastName</LI>
		///		 <LI>sFirstName</LI>
		///		 <LI>sAddress1</LI>
		///		 <LI>sAddress2</LI>
		///		 <LI>sCity</LI>
		///		 <LI>sCounty</LI>
		///		 <LI>sState</LI>
		///		 <LI>sZip</LI>
		///		 <LI>sZipPlusFour</LI>
		///		 <LI>bOverrideAddress</LI>
		///		 <LI>sChangeUserID</LI>
		///		 <LI>daChangeDate</LI>
		///		 <LI>sEmail</LI>
		///		 <LI>sPhone</LI>
		///		 <LI>iType</LI>
		/// Will fill all properties corresponding with a field in the table with the value of the row selected.
		/// </remarks>
		public  void SelectOne(DataTable Table, Int32 Instance)		
		{
			SqlCommand	scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[pr_Customer_SelectOne]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.Parameters.Add(new SqlParameter(PARAM_INSTANCE, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, Instance));
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
			scmCmdToExecute.CommandText = "dbo.[pr_Customer_SelectAll]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			Select(scmCmdToExecute,Table);
		}

		public void SelectCustomerByCOH(DataTable Table, int CustomerOrderHeaderInstance)		
		{
			SqlCommand	scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[pr_SelectCustomerByCOH]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.Parameters.Add("@iCustomerOrderHeaderInstance", CustomerOrderHeaderInstance);
			Select(scmCmdToExecute,Table);
		}

		public void SelectCustomerByCOD(DataTable Table, int CustomerOrderHeaderInstance, int TransID)		
		{
			SqlCommand	scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[pr_Customer_SelectByCOD]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.Parameters.Add("@iCustomerOrderHeaderInstance", CustomerOrderHeaderInstance);
			scmCmdToExecute.Parameters.Add("@iTransID", TransID);
			Select(scmCmdToExecute,Table);
		}

        public void UpdateCustomerEmailByCOD(int CustomerOrderHeaderInstance, int TransID, string Email)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_UpdateCustomerEmailByCOD]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.Parameters.Add("@iCustomerOrderHeaderInstance", CustomerOrderHeaderInstance);
            scmCmdToExecute.Parameters.Add("@iTransID", TransID);
            scmCmdToExecute.Parameters.Add("@Email", Email);
            ExecuteCmd(scmCmdToExecute);
        }

		public int InsertForCHADD(Customer CustomerInfo, string UserID) 
		{
			SqlCommand	scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[pr_CreateCustomerForCHADD]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.Parameters.Add(new SqlParameter(PARAM_LASTNAME,SqlDbType.VarChar, 40, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CustomerInfo.LastName));
			scmCmdToExecute.Parameters.Add(new SqlParameter(PARAM_FIRSTNAME,SqlDbType.VarChar, 40, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CustomerInfo.FirstName));
			scmCmdToExecute.Parameters.Add(new SqlParameter(PARAM_ADDRESS1,SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CustomerInfo.CustomerAddress.Street1));
			scmCmdToExecute.Parameters.Add(new SqlParameter(PARAM_ADDRESS2,SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CustomerInfo.CustomerAddress.Street2));
			scmCmdToExecute.Parameters.Add(new SqlParameter(PARAM_CITY,SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CustomerInfo.CustomerAddress.City));
			scmCmdToExecute.Parameters.Add(new SqlParameter(PARAM_STATE,SqlDbType.VarChar, 10, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CustomerInfo.CustomerAddress.StateProvinceCode));
			scmCmdToExecute.Parameters.Add(new SqlParameter(PARAM_ZIP,SqlDbType.VarChar, 10, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CustomerInfo.CustomerAddress.PostalCode));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@iUserID",SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, Convert.ToInt32(UserID)));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@dChangeDate",SqlDbType.DateTime, 8, ParameterDirection.Input, false, 23, 3, "", DataRowVersion.Proposed, DateTime.Now));
			scmCmdToExecute.Parameters.Add(new SqlParameter(PARAM_EMAIL,SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CustomerInfo.Email));
			scmCmdToExecute.Parameters.Add(new SqlParameter(PARAM_PHONE,SqlDbType.VarChar, 25, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CustomerInfo.PhoneNumber));
			scmCmdToExecute.Parameters.Add(new SqlParameter(PARAM_TYPE, SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CustomerInfo.Type));

			return Convert.ToInt32(ExecuteScalar(scmCmdToExecute));
		}
		public int CreateOrderHeaderForKanataOrder(int orderID,int FmAcc, string billTo ,string CustFname, string CustLname,  string email, string address1, string address2, string city, string province, string postal, int userID) 
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[pr_Kanata_OrderHeader_Create]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.Parameters.Add(new SqlParameter("@iOrderID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, orderID));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@iIsFMAccount", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, FmAcc));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@zBillTo", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, billTo));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@zBillToFirstname", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CustFname));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@zBillToLastname", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CustLname));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@zBillToEmail", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, email));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@zBillToAddress1", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, address1));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@zBillToAddress2", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, address2));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@zBillToCity", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, city));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@zBillToProvince", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, province));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@zBillToPostal", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, postal));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@iUserProfileID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, userID));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@iCustomerOrderHeaderInstance", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, null));

			ExecuteCmd(scmCmdToExecute);

			return Convert.ToInt32(scmCmdToExecute.Parameters["@iCustomerOrderHeaderInstance"].Value);
		}

		public int CreateCustomer(string CustFname, string CustLname,  string email, string address1, string address2, string county, string city, string province, string postal, string postal2, string userID) 
		{
			SqlCommand	scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[CreateCustomer]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.Parameters.Add(new SqlParameter("@zFirstname", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CustFname));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@zLastname", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CustLname));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@zAddress1", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, address1));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@zAddress2", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, address2));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@zCity", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, city));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@zCounty", SqlDbType.VarChar, 31, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, county));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@zProvince", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, province));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@zPostal", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, postal));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@zPostal2", SqlDbType.VarChar, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, postal2));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@zEmail", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, email));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@zChangeUserID", SqlDbType.VarChar, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, userID));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@iCustomerInstance", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, null));

			ExecuteCmd(scmCmdToExecute);

			return Convert.ToInt32(scmCmdToExecute.Parameters["@iCustomerInstance"].Value);
		}

		public int CreateCustomerAccount(int AccountID, string UserID) 
		{
			SqlCommand	scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[CreateCustomerAccount]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.Parameters.Add(new SqlParameter("@accountID",SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, AccountID));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@ChangeUserID",SqlDbType.VarChar, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, UserID));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@customerinstance", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, null));

			ExecuteCmd(scmCmdToExecute);

			return Convert.ToInt32(scmCmdToExecute.Parameters["@customerinstance"].Value);
		}

		public int CreateCustomerFM(string FMID, string UserID, int AddressTypeID) 
		{
			SqlCommand scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[CreateCustomerFM]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.Parameters.Add(new SqlParameter("@FMID",SqlDbType.VarChar, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, FMID));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@ChangeUserID",SqlDbType.VarChar, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, UserID));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@AddressTypeID",SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, AddressTypeID));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@customerinstance", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, null));

			return Convert.ToInt32(ExecuteScalar(scmCmdToExecute));
		}

		public int RefundCustomer(RefundCustomer RefundCustomerInfo)
		{
			
			SqlCommand	scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "QSPCanadaFinance..AP_Refund_DoCustomerRefund";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.Parameters.Add(new SqlParameter("@CustomerOrderHeaderInstance", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, RefundCustomerInfo.CustomerOrderHeaderInstance));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@TransID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, RefundCustomerInfo.TransID));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.VarChar,50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, RefundCustomerInfo.CustomerInfo.FirstName));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@LastName", SqlDbType.VarChar,50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, RefundCustomerInfo.CustomerInfo.LastName));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@Address1",SqlDbType.VarChar,50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, RefundCustomerInfo.CustomerInfo.CustomerAddress.Street1));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@Address2", SqlDbType.VarChar,50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, RefundCustomerInfo.CustomerInfo.CustomerAddress.Street2));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@City", SqlDbType.VarChar,50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, RefundCustomerInfo.CustomerInfo.CustomerAddress.City));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@ProvinceCode", SqlDbType.VarChar,2, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, RefundCustomerInfo.CustomerInfo.CustomerAddress.StateProvinceCode));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@PostalCode", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, RefundCustomerInfo.CustomerInfo.CustomerAddress.PostalCode));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@Amount", SqlDbType.Float, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, RefundCustomerInfo.RefundAmount));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@Reason", SqlDbType.VarChar,255, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, RefundCustomerInfo.RefundReason));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@IncidentId", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, RefundCustomerInfo.IncidentID));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@iUserID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, RefundCustomerInfo.UserID));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@ErrorMessage", SqlDbType.VarChar, 200, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, ""));
	
			return ExecuteCmd(scmCmdToExecute);
		}

		public void SelectAllResolveCreditCardRefunds(DataTable Table)		
		{
			SqlCommand	scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[pr_ResolveCreditCardRefund_SelectAll]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			Select(scmCmdToExecute,Table);
		}

		public int UpdateResolveCreditCardRefund(string creditCardNumber, int refundStatus) 
		{
			SqlCommand	scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[pr_ResolveCreditCardRefund_UpdateStatus]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			scmCmdToExecute.Parameters.Add(new SqlParameter("@zCreditCardNumber", SqlDbType.VarChar, 25, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, creditCardNumber));
			scmCmdToExecute.Parameters.Add(new SqlParameter("@iRefundStatus", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, refundStatus));

			return ExecuteCmd(scmCmdToExecute);
		}
	}
}
