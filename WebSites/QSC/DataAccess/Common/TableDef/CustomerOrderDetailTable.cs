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
	public class CustomerOrderDetailTable: CommonTable
	{
		public const string TBL_CUSTOMERORDERDETAIL= "CustomerOrderDetail";
		public const string FLD_CUSTOMERORDERHEADERINSTANCE= "CustomerOrderHeaderInstance";
		public const string FLD_TRANSID= "TransID";
		public const string FLD_CUSTOMERSHIPTOINSTANCE= "CustomerShipToInstance";
		public const string FLD_PRODUCTCODE= "ProductCode";
		public const int FLD_PRODUCTCODE_LENGTH= 20;
		public const string FLD_PRODUCTNAME= "ProductName";
		public const int FLD_PRODUCTNAME_LENGTH= 50;
		public const string FLD_QUANTITY= "Quantity";
		public const string FLD_PRICE= "Price";
		public const string FLD_PRICEA= "PriceA";
		public const string FLD_TAX= "Tax";
		public const string FLD_TAXA= "TaxA";
		public const string FLD_STATUSINSTANCE= "StatusInstance";
		public const string FLD_DELFLAG= "DelFlag";
		public const string FLD_RENEWAL= "Renewal";
		public const int FLD_RENEWAL_LENGTH= 1;
		public const string FLD_RECIPIENT= "Recipient";
		public const int FLD_RECIPIENT_LENGTH= 81;
		public const string FLD_OVERRIDEPRODUCT= "OverrideProduct";
		public const string FLD_CREATIONDATE= "CreationDate";
		public const string FLD_CROSSEDBRIDGEDATE= "CrossedBridgeDate";
		public const string FLD_CHANGEUSERID= "ChangeUserID";
		public const int FLD_CHANGEUSERID_LENGTH= 4;
		public const string FLD_CHANGEDATE= "ChangeDate";
		public const string FLD_INVOICENUMBER= "InvoiceNumber";
		public const string FLD_ALPHAPRODUCTCODE= "AlphaProductCode";
		public const int FLD_ALPHAPRODUCTCODE_LENGTH= 4;
		public const string FLD_COUPONPAGE= "CouponPage";
		public const int FLD_COUPONPAGE_LENGTH= 2;
		public const string FLD_FDINDICATOR= "FDIndicator";
		public const int FLD_FDINDICATOR_LENGTH= 1;
		public const string FLD_MKTGINDICATOR= "MktgIndicator";
		public const int FLD_MKTGINDICATOR_LENGTH= 10;
		public const string FLD_TOTEINSTANCE= "ToteInstance";
		public const string FLD_GIFTCD= "GiftCD";
		public const int FLD_GIFTCD_LENGTH= 3;
		public const string FLD_ISGIFT= "IsGift";
		public const string FLD_ISGIFTCARDSENT= "IsGiftCardSent";
		public const string FLD_SENDGIFTCARDBEFOREDATE= "SendGiftCardBeforeDate";
		public const string FLD_PROGRAMSECTIONID= "ProgramSectionID";
		public const string FLD_CATALOGPRICE= "CatalogPrice";
		public const string FLD_QUANTITYRESERVED= "QuantityReserved";
		public const string FLD_PRICEOVERRIDEID= "PriceOverrideID";
		public const string FLD_PRODUCTTYPE= "ProductType";
		public const string FLD_PRICINGDETAILSID= "PricingDetailsID";
		public const string FLD_TAX2= "Tax2";
		public const string FLD_TAX2A= "Tax2A";
		public const string FLD_NET= "Net";
		public const string FLD_GROSS= "Gross";
		public const string FLD_SUPPORTERNAME= "SupporterName";
		public const int FLD_SUPPORTERNAME_LENGTH= 50;
		public const string FLD_SENDGIFTCARD= "SendGiftCard";
		/// <summary>
		///     Constructor for CampaignPrizeData.  
		///     <remarks>Initialize a CampaignPrizeData instance by building the table schema.</remarks> 
		/// </summary>
		public CustomerOrderDetailTable()
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
		public CustomerOrderDetailTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{
		}	
		//----------------------------------------------------------------
		// Sub BuildDataTable:
		//   Creates the following datatable:CustomerOrderDetail//----------------------------------------------------------------
		private void BuildDataTable()
		{
			//
			// Create the CustomerOrderDetail
			//
			this.TableName =TBL_CUSTOMERORDERDETAIL;
			DataColumnCollection columns = this.Columns;
			DataColumn Column = columns.Add(FLD_CUSTOMERORDERHEADERINSTANCE, typeof(Int32));
			Column.AllowDBNull = false;
			Column.DefaultValue = 0;
			Column = columns.Add(FLD_TRANSID, typeof(Int32));
			Column.AllowDBNull = false;
			Column.DefaultValue = 0;
			columns.Add(FLD_CUSTOMERSHIPTOINSTANCE, typeof(Int32));
			columns.Add(FLD_PRODUCTCODE, typeof(string));
			columns.Add(FLD_PRODUCTNAME, typeof(string));
			columns.Add(FLD_QUANTITY, typeof(Int32));
			columns.Add(FLD_PRICE, typeof(Decimal));
			columns.Add(FLD_PRICEA, typeof(Decimal));
			columns.Add(FLD_TAX, typeof(Decimal));
			columns.Add(FLD_TAXA, typeof(Decimal));
			columns.Add(FLD_STATUSINSTANCE, typeof(Int32));
			columns.Add(FLD_DELFLAG, typeof(bool));
			columns.Add(FLD_RENEWAL, typeof(string));
			columns.Add(FLD_RECIPIENT, typeof(string));
			columns.Add(FLD_OVERRIDEPRODUCT, typeof(bool));
			columns.Add(FLD_CREATIONDATE, typeof(DateTime));
			columns.Add(FLD_CROSSEDBRIDGEDATE, typeof(DateTime));
			columns.Add(FLD_CHANGEUSERID, typeof(string));
			columns.Add(FLD_CHANGEDATE, typeof(DateTime));
			columns.Add(FLD_INVOICENUMBER, typeof(Int32));
			columns.Add(FLD_ALPHAPRODUCTCODE, typeof(string));
			columns.Add(FLD_COUPONPAGE, typeof(string));
			columns.Add(FLD_FDINDICATOR, typeof(string));
			columns.Add(FLD_MKTGINDICATOR, typeof(string));
			columns.Add(FLD_TOTEINSTANCE, typeof(Int32));
			columns.Add(FLD_GIFTCD, typeof(string));
			columns.Add(FLD_ISGIFT, typeof(bool));
			columns.Add(FLD_ISGIFTCARDSENT, typeof(bool));
			columns.Add(FLD_SENDGIFTCARDBEFOREDATE, typeof(DateTime));
			columns.Add(FLD_PROGRAMSECTIONID, typeof(Int32));
			columns.Add(FLD_CATALOGPRICE, typeof(Decimal));
			columns.Add(FLD_QUANTITYRESERVED, typeof(Int32));
			columns.Add(FLD_PRICEOVERRIDEID, typeof(Int32));
			columns.Add(FLD_PRODUCTTYPE, typeof(Int32));
			columns.Add(FLD_PRICINGDETAILSID, typeof(Int32));
			columns.Add(FLD_TAX2, typeof(Decimal));
			columns.Add(FLD_TAX2A, typeof(Decimal));
			columns.Add(FLD_NET, typeof(Decimal));
			columns.Add(FLD_GROSS, typeof(Decimal));
			columns.Add(FLD_SUPPORTERNAME, typeof(string));
			columns.Add(FLD_SENDGIFTCARD, typeof(bool));
			SetConstraint();
		}
		private void SetConstraint()
		{
			this.PrimaryKey = new DataColumn[]{Columns[FLD_CUSTOMERORDERHEADERINSTANCE],Columns[FLD_TRANSID]};
		}
	}
}