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
	public class CustomerOrderDetailRemitHistoryTable: CommonTable
	{
		public const string TBL_CUSTOMERORDERDETAILREMITHISTORY= "CustomerOrderDetailRemitHistory";
		public const string FLD_CUSTOMERORDERHEADERINSTANCE= "CustomerOrderHeaderInstance";
		public const string FLD_TRANSID= "TransID";
		public const string FLD_REMITBATCHID= "RemitBatchID";
		public const string FLD_COUNTRYCODE= "CountryCode";
		public const int FLD_COUNTRYCODE_LENGTH= 2;
		public const string FLD_CUSTOMERREMITHISTORYINSTANCE= "CustomerRemitHistoryInstance";
		public const string FLD_STATUS= "Status";
		public const string FLD_QUANTITY= "Quantity";
		public const string FLD_REMITRATE= "RemitRate";
		public const string FLD_BASEPRICE= "BasePrice";
		public const string FLD_CURRENCYID= "CurrencyID";
		public const string FLD_LANG= "Lang";
		public const int FLD_LANG_LENGTH= 10;
		public const string FLD_PREMIUMINDICATOR= "PremiumIndicator";
		public const string FLD_PREMIUMCODE= "PremiumCode";
		public const int FLD_PREMIUMCODE_LENGTH= 20;
		public const string FLD_PREMIUMDESCRIPTION= "PremiumDescription";
		public const int FLD_PREMIUMDESCRIPTION_LENGTH= 50;
		public const string FLD_ABCCODE= "ABCCode";
		public const int FLD_ABCCODE_LENGTH= 20;
		public const string FLD_RENEWAL= "Renewal";
		public const int FLD_RENEWAL_LENGTH= 1;
		public const string FLD_TITLECODE= "TitleCode";
		public const int FLD_TITLECODE_LENGTH= 4;
		public const string FLD_MAGAZINETITLE= "Title";
		public const int FLD_MAGAZINETITLE_LENGTH= 55;
		public const string FLD_CATALOGPRICE= "CatalogPrice";
		public const string FLD_ITEMPRICETOTAL= "ItemPriceTotal";
		public const string FLD_NUMBEROFISSUES= "NumberOfIssues";
		public const string FLD_DEFAULTGROSSVALUE= "DefaultGrossValue";
		public const string FLD_COMMENT= "Comment";
		public const int FLD_COMMENT_LENGTH= 500;
		public const string FLD_SWITCHLETTERBATCHID= "SwitchLetterBatchID";
		public const string FLD_GIFTORDERTYPE= "GiftOrderType";
		public const int FLD_GIFTORDERTYPE_LENGTH= 1;
		public const string FLD_GIFTORDERSTATUS= "GiftOrderStatus";
		public const string FLD_GIFTCARDDATEGENERATED= "GiftCardDateGenerated";
		public const string FLD_SUPPORTERNAME= "SupporterName";
		public const int FLD_SUPPORTERNAME_LENGTH= 80;
		public const string FLD_DATECHANGED= "DateChanged";
		public const string FLD_USERIDCHANGED= "UserIDChanged";
		public const int FLD_USERIDCHANGED_LENGTH= 4;
		public const string FLD_EFFORTKEY= "EffortKey";
		public const int FLD_EFFORTKEY_LENGTH= 40;
		/// <summary>
		///     Constructor for CampaignPrizeData.  
		///     <remarks>Initialize a CampaignPrizeData instance by building the table schema.</remarks> 
		/// </summary>
		public CustomerOrderDetailRemitHistoryTable()
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
		public CustomerOrderDetailRemitHistoryTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{
		}	
		//----------------------------------------------------------------
		// Sub BuildDataTable:
		//   Creates the following datatable:CustomerOrderDetailRemitHistory//----------------------------------------------------------------
		private void BuildDataTable()
		{
			//
			// Create the CustomerOrderDetailRemitHistory
			//
			this.TableName =TBL_CUSTOMERORDERDETAILREMITHISTORY;
			DataColumnCollection columns = this.Columns;
			DataColumn Column = columns.Add(FLD_CUSTOMERORDERHEADERINSTANCE, typeof(Int32));
			Column.AllowDBNull = false;
			Column.DefaultValue = 0;
			Column = columns.Add(FLD_TRANSID, typeof(Int32));
			Column.AllowDBNull = false;
			Column.DefaultValue = 0;
			Column = columns.Add(FLD_REMITBATCHID, typeof(Int32));
			Column.AllowDBNull = false;
			Column.DefaultValue = 0;
			columns.Add(FLD_COUNTRYCODE, typeof(string));
			columns.Add(FLD_CUSTOMERREMITHISTORYINSTANCE, typeof(Int32));
			columns.Add(FLD_STATUS, typeof(string));
			columns.Add(FLD_QUANTITY, typeof(Int32));
			columns.Add(FLD_REMITRATE, typeof(Decimal));
			columns.Add(FLD_BASEPRICE, typeof(Decimal));
			columns.Add(FLD_CURRENCYID, typeof(Int32));
			columns.Add(FLD_LANG, typeof(string));
			columns.Add(FLD_PREMIUMINDICATOR, typeof(Int32));
			columns.Add(FLD_PREMIUMCODE, typeof(string));
			columns.Add(FLD_PREMIUMDESCRIPTION, typeof(string));
			columns.Add(FLD_ABCCODE, typeof(string));
			columns.Add(FLD_RENEWAL, typeof(string));
			columns.Add(FLD_TITLECODE, typeof(string));
			columns.Add(FLD_MAGAZINETITLE, typeof(string));
			columns.Add(FLD_CATALOGPRICE, typeof(Decimal));
			columns.Add(FLD_ITEMPRICETOTAL, typeof(Decimal));
			columns.Add(FLD_NUMBEROFISSUES, typeof(Int32));
			columns.Add(FLD_DEFAULTGROSSVALUE, typeof(Decimal));
			columns.Add(FLD_COMMENT, typeof(string));
			columns.Add(FLD_SWITCHLETTERBATCHID, typeof(Int32));
			columns.Add(FLD_GIFTORDERTYPE, typeof(string));
			columns.Add(FLD_GIFTORDERSTATUS, typeof(Int32));
			columns.Add(FLD_GIFTCARDDATEGENERATED, typeof(DateTime));
			columns.Add(FLD_SUPPORTERNAME, typeof(string));
			columns.Add(FLD_DATECHANGED, typeof(DateTime));
			columns.Add(FLD_USERIDCHANGED, typeof(string));
			columns.Add(FLD_EFFORTKEY, typeof(string));
			columns.Add("MagazineStatus", typeof(string));
			columns.Add("Ful_Name", typeof(string));
			columns.Add("Ful_Tel", typeof(string));
			columns.Add("PriceEntered", typeof(Decimal));
			columns.Add("CustomerLastName", typeof(string));
			columns.Add("CustomerFirstName", typeof(string));
			columns.Add("DateCardSent", typeof(DateTime));
			columns.Add("DonorName", typeof(string));
			columns.Add("RecipientLastName", typeof(string));
			columns.Add("RecipientFirstName", typeof(string));
			SetConstraint();
		}
		private void SetConstraint()
		{
			this.PrimaryKey = new DataColumn[]{Columns[FLD_CUSTOMERORDERHEADERINSTANCE],Columns[FLD_TRANSID],Columns[FLD_REMITBATCHID]};
		}
	}
}