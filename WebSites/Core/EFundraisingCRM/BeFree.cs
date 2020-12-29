using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class BeFree: EFundraisingCRMDataObject {

		private string merchantID;
		private string recordType;
		private DateTime dateInsert;
		private string sourceID;
		private string transactionID;
		private string productKey;
		private float qtyProduct;
		private float unitPrice;
		private string currencyType;
		private string merchandiseType;


		public BeFree() : this(null) { }
		public BeFree(string merchantID) : this(merchantID, null) { }
		public BeFree(string merchantID, string recordType) : this(merchantID, recordType, DateTime.MinValue) { }
		public BeFree(string merchantID, string recordType, DateTime dateInsert) : this(merchantID, recordType, dateInsert, null) { }
		public BeFree(string merchantID, string recordType, DateTime dateInsert, string sourceID) : this(merchantID, recordType, dateInsert, sourceID, null) { }
		public BeFree(string merchantID, string recordType, DateTime dateInsert, string sourceID, string transactionID) : this(merchantID, recordType, dateInsert, sourceID, transactionID, null) { }
		public BeFree(string merchantID, string recordType, DateTime dateInsert, string sourceID, string transactionID, string productKey) : this(merchantID, recordType, dateInsert, sourceID, transactionID, productKey, float.MinValue) { }
		public BeFree(string merchantID, string recordType, DateTime dateInsert, string sourceID, string transactionID, string productKey, float qtyProduct) : this(merchantID, recordType, dateInsert, sourceID, transactionID, productKey, qtyProduct, float.MinValue) { }
		public BeFree(string merchantID, string recordType, DateTime dateInsert, string sourceID, string transactionID, string productKey, float qtyProduct, float unitPrice) : this(merchantID, recordType, dateInsert, sourceID, transactionID, productKey, qtyProduct, unitPrice, null) { }
		public BeFree(string merchantID, string recordType, DateTime dateInsert, string sourceID, string transactionID, string productKey, float qtyProduct, float unitPrice, string currencyType) : this(merchantID, recordType, dateInsert, sourceID, transactionID, productKey, qtyProduct, unitPrice, currencyType, null) { }
		public BeFree(string merchantID, string recordType, DateTime dateInsert, string sourceID, string transactionID, string productKey, float qtyProduct, float unitPrice, string currencyType, string merchandiseType) {
			this.merchantID = merchantID;
			this.recordType = recordType;
			this.dateInsert = dateInsert;
			this.sourceID = sourceID;
			this.transactionID = transactionID;
			this.productKey = productKey;
			this.qtyProduct = qtyProduct;
			this.unitPrice = unitPrice;
			this.currencyType = currencyType;
			this.merchandiseType = merchandiseType;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<BeFree>\r\n" +
			"	<MerchantID>" + System.Web.HttpUtility.HtmlEncode(merchantID) + "</MerchantID>\r\n" +
			"	<RecordType>" + System.Web.HttpUtility.HtmlEncode(recordType) + "</RecordType>\r\n" +
			"	<DateInsert>" + dateInsert + "</DateInsert>\r\n" +
			"	<SourceID>" + System.Web.HttpUtility.HtmlEncode(sourceID) + "</SourceID>\r\n" +
			"	<TransactionID>" + System.Web.HttpUtility.HtmlEncode(transactionID) + "</TransactionID>\r\n" +
			"	<ProductKey>" + System.Web.HttpUtility.HtmlEncode(productKey) + "</ProductKey>\r\n" +
			"	<QtyProduct>" + qtyProduct + "</QtyProduct>\r\n" +
			"	<UnitPrice>" + unitPrice + "</UnitPrice>\r\n" +
			"	<CurrencyType>" + System.Web.HttpUtility.HtmlEncode(currencyType) + "</CurrencyType>\r\n" +
			"	<MerchandiseType>" + System.Web.HttpUtility.HtmlEncode(merchandiseType) + "</MerchandiseType>\r\n" +
			"</BeFree>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("merchantId")) {
					SetXmlValue(ref merchantID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("recordType")) {
					SetXmlValue(ref recordType, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("dateInsert")) {
					SetXmlValue(ref dateInsert, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("sourceId")) {
					SetXmlValue(ref sourceID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("transactionId")) {
					SetXmlValue(ref transactionID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("productKey")) {
					SetXmlValue(ref productKey, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("qtyProduct")) {
					SetXmlValue(ref qtyProduct, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("unitPrice")) {
					SetXmlValue(ref unitPrice, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("currencyType")) {
					SetXmlValue(ref currencyType, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("merchandiseType")) {
					SetXmlValue(ref merchandiseType, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static BeFree[] GetBeFrees() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetBeFrees();
		}

		/*
		public static BeFree GetBeFreeByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetBeFreeByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertBeFree(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateBeFree(this);
		}*/
		#endregion

		#region Properties
		public string MerchantID {
			set { merchantID = value; }
			get { return merchantID; }
		}

		public string RecordType {
			set { recordType = value; }
			get { return recordType; }
		}

		public DateTime DateInsert {
			set { dateInsert = value; }
			get { return dateInsert; }
		}

		public string SourceID {
			set { sourceID = value; }
			get { return sourceID; }
		}

		public string TransactionID {
			set { transactionID = value; }
			get { return transactionID; }
		}

		public string ProductKey {
			set { productKey = value; }
			get { return productKey; }
		}

		public float QtyProduct {
			set { qtyProduct = value; }
			get { return qtyProduct; }
		}

		public float UnitPrice {
			set { unitPrice = value; }
			get { return unitPrice; }
		}

		public string CurrencyType {
			set { currencyType = value; }
			get { return currencyType; }
		}

		public string MerchandiseType {
			set { merchandiseType = value; }
			get { return merchandiseType; }
		}

		#endregion
	}
}
