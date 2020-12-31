using System;
using System.Data;
using System.Runtime.Serialization;
namespace QSPFulfillment.DataAccess.Common.TableDef
{
	/// <summary>
	///     A custom serializable dataset containing user information.
	///     <remarks>
	///         This class is used to define the shape of CampaignPrizeData.
	///     </remarks>
	///     <remarks>
	///         The serializale constructor allows objects of type CampaignPrizeData to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class BatchTable: CommonTable
	{
		public const string TBL_BATCH= "Batch";
		public const string FLD_DATE= "Date";
		public const string FLD_ID= "ID";
		public const string FLD_ACCOUNTID= "AccountID";
		public const string FLD_ENTERREDCOUNT= "EnterredCount";
		public const string FLD_ENTERREDAMOUNT= "EnterredAmount";
		public const string FLD_CALCULATEDAMOUNT= "CalculatedAmount";
		public const string FLD_STATUSINSTANCE= "StatusInstance";
		public const string FLD_KE3FILENAME= "KE3FileName";
		public const int FLD_KE3FILENAME_LENGTH= 200;
		public const string FLD_CHANGEUSERID= "ChangeUserID";
		public const int FLD_CHANGEUSERID_LENGTH= 4;
		public const string FLD_CHANGEDATE= "ChangeDate";
		public const string FLD_TEACHERCOUNT= "TeacherCount";
		public const string FLD_STUDENTCOUNT= "StudentCount";
		public const string FLD_CUSTOMERCOUNT= "CustomerCount";
		public const string FLD_ORDERCOUNT= "OrderCount";
		public const string FLD_ORDERCOUNTACCEPT= "OrderCountAccept";
		public const string FLD_ORDERDETAILCOUNT= "OrderDetailCount";
		public const string FLD_ORDERDETAILCOUNTERROR= "OrderDetailCountError";
		public const string FLD_STARTIMPORTTIME= "StartImportTime";
		public const string FLD_ENDIMPORTTIME= "EndImportTime";
		public const string FLD_IMPORTTIMESECONDS= "ImportTimeSeconds";
		public const string FLD_CLERK= "Clerk";
		public const int FLD_CLERK_LENGTH= 4;
		public const string FLD_DATECREATED= "DateCreated";
		public const string FLD_USERIDCREATED= "UserIDCreated";
		public const int FLD_USERIDCREATED_LENGTH= 4;
		public const string FLD_DATEKEYED= "DateKeyed";
		public const string FLD_DATEBATCHCOMPLETED= "DateBatchCompleted";
		public const string FLD_OVERRIDEPCTSTATE= "OverridePctState";
		public const string FLD_PCTSTATE= "PctState";
		public const string FLD_ORIGINALSTATUSINSTANCE= "OriginalStatusInstance";
		public const string FLD_ORDERTYPECODE= "OrderTypeCode";
		public const string FLD_CAMPAIGNID= "CampaignID";
		public const string FLD_BILLTOADDRESSID= "BillToAddressID";
		public const string FLD_SHIPTOADDRESSID= "ShipToAddressID";
		public const string FLD_SHIPTOACCOUNTID= "ShipToAccountID";
		public const string FLD_BILLTOFMID= "BillToFMID";
		public const int FLD_BILLTOFMID_LENGTH= 4;
		public const string FLD_SHIPTOFMID= "ShipToFMID";
		public const int FLD_SHIPTOFMID_LENGTH= 4;
		public const string FLD_REPORTEDENVELOPES= "ReportedEnvelopes";
		public const string FLD_PAYMENTSEND= "PaymentSend";
		public const string FLD_SALESBEFORETAX= "SalesBeforeTax";
		public const string FLD_DATESENT= "DateSent";
		public const string FLD_DATERECEIVED= "DateReceived";
		public const string FLD_CONTACTFIRSTNAME= "ContactFirstName";
		public const int FLD_CONTACTFIRSTNAME_LENGTH= 50;
		public const string FLD_CONTACTLASTNAME= "ContactLastName";
		public const int FLD_CONTACTLASTNAME_LENGTH= 50;
		public const string FLD_CONTACTEMAIL= "ContactEmail";
		public const int FLD_CONTACTEMAIL_LENGTH= 50;
		public const string FLD_CONTACTPHONE= "ContactPhone";
		public const int FLD_CONTACTPHONE_LENGTH= 50;
		public const string FLD_COMMENT= "Comment";
		public const int FLD_COMMENT_LENGTH= 300;
		public const string FLD_INCENTIVECALCULATIONSTATUS= "IncentiveCalculationStatus";
		public const string FLD_MAGNETBOOKLETCOUNT= "MagnetBookletCount";
		public const string FLD_MAGNETCARDCOUNT= "MagnetCardCount";
		public const string FLD_MAGNETGOODCARDCOUNT= "MagnetGoodCardCount";
		public const string FLD_MAGNETCARDSMAILED= "MagnetCardsMailed";
		public const string FLD_MAGNETMAILDATE= "MagnetMailDate";
		public const string FLD_PICKDATE= "PickDate";
		public const string FLD_ISDMAPPROVED= "IsDMApproved";
		public const string FLD_COUNTRYCODE= "CountryCode";
		public const int FLD_COUNTRYCODE_LENGTH= 10;
		public const string FLD_PICKLINE= "PickLine";
		public const string FLD_ORDERQUALIFIERID= "OrderQualifierID";
		public const string FLD_CHECKPAYABLETOQSPAMOUNT= "CheckPayableToQSPAmount";
		public const string FLD_ISINCENTIVE= "IsIncentive";
		public const string FLD_ORDERDELIVERYDATE= "OrderDeliveryDate";
		public const string FLD_REFNUMBER= "RefNumber";
		public const string FLD_PAYMENTBATCHDATE= "PaymentBatchDate";
		public const string FLD_PAYMENTBATCHID= "PaymentBatchID";
		public const string FLD_ISSTAFFORDER= "IsStaffOrder";
		public const string FLD_INQUIREUPONCOMPLETE= "InquireUponComplete";
		public const string FLD_GROUPPROFIT= "GroupProfit";
		public const string FLD_ORDERID= "OrderID";
		public const string FLD_ORDERAMNTDUE= "OrderAmntDue";
		public const string FLD_MAGNETPOSTAGE= "MagnetPostage";
		public const string FLD_ORDERIDINCENTIVE= "OrderIDIncentive";
		public const string FLD_ISINVOICED= "IsInvoiced";
		public const string FLD_CAMPAIGNNETTOTAL= "CampaignNetTotal";
		/// <summary>
		///     Constructor for CampaignPrizeData.  
		///     <remarks>Initialize a CampaignPrizeData instance by building the table schema.</remarks> 
		/// </summary>
		public BatchTable()
		{
			//
			// Create the tables in the dataset
			//
			BuildDataTable();
		}
		/// <summary>
		///     Constructor to support serialization.
		///     <remarks>Constructor that supports serialization.</remarks> 
		///     <param name="info">The SerializationInfo object to read from.</param>
		///     <param name="context">Information on who is calling this method.</param>
		/// </summary>
		public BatchTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{
		}	
		//----------------------------------------------------------------
		// Sub BuildDataTable:
		//   Creates the following datatable:Batch//----------------------------------------------------------------
		private void BuildDataTable()
		{
			//
			// Create the Batch
			//
			this.TableName =TBL_BATCH;
			DataColumnCollection columns = this.Columns;
			DataColumn Column = columns.Add(FLD_DATE, typeof(DateTime));
			Column.AllowDBNull = false;
			Column.DefaultValue = 0;
			columns.Add(FLD_ID, typeof(Int32));
			Column.AllowDBNull = false;
			Column.DefaultValue = 0;
			columns.Add(FLD_ACCOUNTID, typeof(Int32));
			columns.Add(FLD_ENTERREDCOUNT, typeof(Int32));
			columns.Add(FLD_ENTERREDAMOUNT, typeof(Decimal));
			columns.Add(FLD_CALCULATEDAMOUNT, typeof(Decimal));
			columns.Add(FLD_STATUSINSTANCE, typeof(Int32));
			columns.Add(FLD_KE3FILENAME, typeof(string));
			columns.Add(FLD_CHANGEUSERID, typeof(string));
			columns.Add(FLD_CHANGEDATE, typeof(DateTime));
			columns.Add(FLD_TEACHERCOUNT, typeof(Int32));
			columns.Add(FLD_STUDENTCOUNT, typeof(Int32));
			columns.Add(FLD_CUSTOMERCOUNT, typeof(Int32));
			columns.Add(FLD_ORDERCOUNT, typeof(Int32));
			columns.Add(FLD_ORDERCOUNTACCEPT, typeof(Int32));
			columns.Add(FLD_ORDERDETAILCOUNT, typeof(Int32));
			columns.Add(FLD_ORDERDETAILCOUNTERROR, typeof(Int32));
			columns.Add(FLD_STARTIMPORTTIME, typeof(DateTime));
			columns.Add(FLD_ENDIMPORTTIME, typeof(DateTime));
			columns.Add(FLD_IMPORTTIMESECONDS, typeof(Int32));
			columns.Add(FLD_CLERK, typeof(string));
			columns.Add(FLD_DATECREATED, typeof(DateTime));
			columns.Add(FLD_USERIDCREATED, typeof(string));
			columns.Add(FLD_DATEKEYED, typeof(DateTime));
			columns.Add(FLD_DATEBATCHCOMPLETED, typeof(DateTime));
			columns.Add(FLD_OVERRIDEPCTSTATE, typeof(bool));
			columns.Add(FLD_PCTSTATE, typeof(Decimal));
			columns.Add(FLD_ORIGINALSTATUSINSTANCE, typeof(Int32));
			columns.Add(FLD_ORDERTYPECODE, typeof(Int32));
			columns.Add(FLD_CAMPAIGNID, typeof(Int32));
			columns.Add(FLD_BILLTOADDRESSID, typeof(Int32));
			columns.Add(FLD_SHIPTOADDRESSID, typeof(Int32));
			columns.Add(FLD_SHIPTOACCOUNTID, typeof(Int32));
			columns.Add(FLD_BILLTOFMID, typeof(string));
			columns.Add(FLD_SHIPTOFMID, typeof(string));
			columns.Add(FLD_REPORTEDENVELOPES, typeof(Int32));
			columns.Add(FLD_PAYMENTSEND, typeof(Decimal));
			columns.Add(FLD_SALESBEFORETAX, typeof(Decimal));
			columns.Add(FLD_DATESENT, typeof(DateTime));
			columns.Add(FLD_DATERECEIVED, typeof(DateTime));
			columns.Add(FLD_CONTACTFIRSTNAME, typeof(string));
			columns.Add(FLD_CONTACTLASTNAME, typeof(string));
			columns.Add(FLD_CONTACTEMAIL, typeof(string));
			columns.Add(FLD_CONTACTPHONE, typeof(string));
			columns.Add(FLD_COMMENT, typeof(string));
			columns.Add(FLD_INCENTIVECALCULATIONSTATUS, typeof(Int32));
			columns.Add(FLD_MAGNETBOOKLETCOUNT, typeof(Int32));
			columns.Add(FLD_MAGNETCARDCOUNT, typeof(Int32));
			columns.Add(FLD_MAGNETGOODCARDCOUNT, typeof(Int32));
			columns.Add(FLD_MAGNETCARDSMAILED, typeof(Int32));
			columns.Add(FLD_MAGNETMAILDATE, typeof(DateTime));
			columns.Add(FLD_PICKDATE, typeof(DateTime));
			columns.Add(FLD_ISDMAPPROVED, typeof(bool));
			columns.Add(FLD_COUNTRYCODE, typeof(string));
			columns.Add(FLD_PICKLINE, typeof(Int32));
			columns.Add(FLD_ORDERQUALIFIERID, typeof(Int32));
			columns.Add(FLD_CHECKPAYABLETOQSPAMOUNT, typeof(Decimal));
			columns.Add(FLD_ISINCENTIVE, typeof(bool));
			columns.Add(FLD_ORDERDELIVERYDATE, typeof(DateTime));
			columns.Add(FLD_REFNUMBER, typeof(Int32));
			columns.Add(FLD_PAYMENTBATCHDATE, typeof(DateTime));
			columns.Add(FLD_PAYMENTBATCHID, typeof(Int32));
			columns.Add(FLD_ISSTAFFORDER, typeof(bool));
			columns.Add(FLD_INQUIREUPONCOMPLETE, typeof(bool));
			columns.Add(FLD_GROUPPROFIT, typeof(Decimal));
			columns.Add(FLD_ORDERID, typeof(Int32));
			columns.Add(FLD_ORDERAMNTDUE, typeof(Decimal));
			columns.Add(FLD_MAGNETPOSTAGE, typeof(Decimal));
			columns.Add(FLD_ORDERIDINCENTIVE, typeof(Int32));
			columns.Add(FLD_ISINVOICED, typeof(bool));
			columns.Add(FLD_CAMPAIGNNETTOTAL, typeof(Decimal));
			SetConstraint();
		}
		private void SetConstraint()
		{
			this.PrimaryKey = new DataColumn[]{Columns[FLD_DATE],Columns[FLD_ID]};
		}
	}
}