using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class CommissionEarning: EFundraisingCRMDataObject {

		private int commissionEarningID;
		private int salesID;
		private string productDescription;
		private float paymentAmount;
		private DateTime paymentEntryDate;
		private string commissionAmount;
		private float commissionRate;
		private int paymentNo;
		private int consultantID;
		private DateTime recordEntryDate;
		private int associateID;
		private float salesAmount;
		private string currencyCode;
		private float exchangeRate;
		private string commissionAmountCa;
		private int leadID;
		private DateTime saleDate;


		public CommissionEarning() : this(int.MinValue) { }
		public CommissionEarning(int commissionEarningID) : this(commissionEarningID, int.MinValue) { }
		public CommissionEarning(int commissionEarningID, int salesID) : this(commissionEarningID, salesID, null) { }
		public CommissionEarning(int commissionEarningID, int salesID, string productDescription) : this(commissionEarningID, salesID, productDescription, float.MinValue) { }
		public CommissionEarning(int commissionEarningID, int salesID, string productDescription, float paymentAmount) : this(commissionEarningID, salesID, productDescription, paymentAmount, DateTime.MinValue) { }
		public CommissionEarning(int commissionEarningID, int salesID, string productDescription, float paymentAmount, DateTime paymentEntryDate) : this(commissionEarningID, salesID, productDescription, paymentAmount, paymentEntryDate, null) { }
		public CommissionEarning(int commissionEarningID, int salesID, string productDescription, float paymentAmount, DateTime paymentEntryDate, string commissionAmount) : this(commissionEarningID, salesID, productDescription, paymentAmount, paymentEntryDate, commissionAmount, float.MinValue) { }
		public CommissionEarning(int commissionEarningID, int salesID, string productDescription, float paymentAmount, DateTime paymentEntryDate, string commissionAmount, float commissionRate) : this(commissionEarningID, salesID, productDescription, paymentAmount, paymentEntryDate, commissionAmount, commissionRate, int.MinValue) { }
		public CommissionEarning(int commissionEarningID, int salesID, string productDescription, float paymentAmount, DateTime paymentEntryDate, string commissionAmount, float commissionRate, int paymentNo) : this(commissionEarningID, salesID, productDescription, paymentAmount, paymentEntryDate, commissionAmount, commissionRate, paymentNo, int.MinValue) { }
		public CommissionEarning(int commissionEarningID, int salesID, string productDescription, float paymentAmount, DateTime paymentEntryDate, string commissionAmount, float commissionRate, int paymentNo, int consultantID) : this(commissionEarningID, salesID, productDescription, paymentAmount, paymentEntryDate, commissionAmount, commissionRate, paymentNo, consultantID, DateTime.MinValue) { }
		public CommissionEarning(int commissionEarningID, int salesID, string productDescription, float paymentAmount, DateTime paymentEntryDate, string commissionAmount, float commissionRate, int paymentNo, int consultantID, DateTime recordEntryDate) : this(commissionEarningID, salesID, productDescription, paymentAmount, paymentEntryDate, commissionAmount, commissionRate, paymentNo, consultantID, recordEntryDate, int.MinValue) { }
		public CommissionEarning(int commissionEarningID, int salesID, string productDescription, float paymentAmount, DateTime paymentEntryDate, string commissionAmount, float commissionRate, int paymentNo, int consultantID, DateTime recordEntryDate, int associateID) : this(commissionEarningID, salesID, productDescription, paymentAmount, paymentEntryDate, commissionAmount, commissionRate, paymentNo, consultantID, recordEntryDate, associateID, float.MinValue) { }
		public CommissionEarning(int commissionEarningID, int salesID, string productDescription, float paymentAmount, DateTime paymentEntryDate, string commissionAmount, float commissionRate, int paymentNo, int consultantID, DateTime recordEntryDate, int associateID, float salesAmount) : this(commissionEarningID, salesID, productDescription, paymentAmount, paymentEntryDate, commissionAmount, commissionRate, paymentNo, consultantID, recordEntryDate, associateID, salesAmount, null) { }
		public CommissionEarning(int commissionEarningID, int salesID, string productDescription, float paymentAmount, DateTime paymentEntryDate, string commissionAmount, float commissionRate, int paymentNo, int consultantID, DateTime recordEntryDate, int associateID, float salesAmount, string currencyCode) : this(commissionEarningID, salesID, productDescription, paymentAmount, paymentEntryDate, commissionAmount, commissionRate, paymentNo, consultantID, recordEntryDate, associateID, salesAmount, currencyCode, float.MinValue) { }
		public CommissionEarning(int commissionEarningID, int salesID, string productDescription, float paymentAmount, DateTime paymentEntryDate, string commissionAmount, float commissionRate, int paymentNo, int consultantID, DateTime recordEntryDate, int associateID, float salesAmount, string currencyCode, float exchangeRate) : this(commissionEarningID, salesID, productDescription, paymentAmount, paymentEntryDate, commissionAmount, commissionRate, paymentNo, consultantID, recordEntryDate, associateID, salesAmount, currencyCode, exchangeRate, null) { }
		public CommissionEarning(int commissionEarningID, int salesID, string productDescription, float paymentAmount, DateTime paymentEntryDate, string commissionAmount, float commissionRate, int paymentNo, int consultantID, DateTime recordEntryDate, int associateID, float salesAmount, string currencyCode, float exchangeRate, string commissionAmountCa) : this(commissionEarningID, salesID, productDescription, paymentAmount, paymentEntryDate, commissionAmount, commissionRate, paymentNo, consultantID, recordEntryDate, associateID, salesAmount, currencyCode, exchangeRate, commissionAmountCa, int.MinValue) { }
		public CommissionEarning(int commissionEarningID, int salesID, string productDescription, float paymentAmount, DateTime paymentEntryDate, string commissionAmount, float commissionRate, int paymentNo, int consultantID, DateTime recordEntryDate, int associateID, float salesAmount, string currencyCode, float exchangeRate, string commissionAmountCa, int leadID) : this(commissionEarningID, salesID, productDescription, paymentAmount, paymentEntryDate, commissionAmount, commissionRate, paymentNo, consultantID, recordEntryDate, associateID, salesAmount, currencyCode, exchangeRate, commissionAmountCa, leadID, DateTime.MinValue) { }
		public CommissionEarning(int commissionEarningID, int salesID, string productDescription, float paymentAmount, DateTime paymentEntryDate, string commissionAmount, float commissionRate, int paymentNo, int consultantID, DateTime recordEntryDate, int associateID, float salesAmount, string currencyCode, float exchangeRate, string commissionAmountCa, int leadID, DateTime saleDate) {
			this.commissionEarningID = commissionEarningID;
			this.salesID = salesID;
			this.productDescription = productDescription;
			this.paymentAmount = paymentAmount;
			this.paymentEntryDate = paymentEntryDate;
			this.commissionAmount = commissionAmount;
			this.commissionRate = commissionRate;
			this.paymentNo = paymentNo;
			this.consultantID = consultantID;
			this.recordEntryDate = recordEntryDate;
			this.associateID = associateID;
			this.salesAmount = salesAmount;
			this.currencyCode = currencyCode;
			this.exchangeRate = exchangeRate;
			this.commissionAmountCa = commissionAmountCa;
			this.leadID = leadID;
			this.saleDate = saleDate;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<CommissionEarning>\r\n" +
			"	<CommissionEarningID>" + commissionEarningID + "</CommissionEarningID>\r\n" +
			"	<SalesID>" + salesID + "</SalesID>\r\n" +
			"	<ProductDescription>" + System.Web.HttpUtility.HtmlEncode(productDescription) + "</ProductDescription>\r\n" +
			"	<PaymentAmount>" + paymentAmount + "</PaymentAmount>\r\n" +
			"	<PaymentEntryDate>" + paymentEntryDate + "</PaymentEntryDate>\r\n" +
			"	<CommissionAmount>" + System.Web.HttpUtility.HtmlEncode(commissionAmount) + "</CommissionAmount>\r\n" +
			"	<CommissionRate>" + commissionRate + "</CommissionRate>\r\n" +
			"	<PaymentNo>" + paymentNo + "</PaymentNo>\r\n" +
			"	<ConsultantID>" + consultantID + "</ConsultantID>\r\n" +
			"	<RecordEntryDate>" + recordEntryDate + "</RecordEntryDate>\r\n" +
			"	<AssociateID>" + associateID + "</AssociateID>\r\n" +
			"	<SalesAmount>" + salesAmount + "</SalesAmount>\r\n" +
			"	<CurrencyCode>" + System.Web.HttpUtility.HtmlEncode(currencyCode) + "</CurrencyCode>\r\n" +
			"	<ExchangeRate>" + exchangeRate + "</ExchangeRate>\r\n" +
			"	<CommissionAmountCa>" + System.Web.HttpUtility.HtmlEncode(commissionAmountCa) + "</CommissionAmountCa>\r\n" +
			"	<LeadID>" + leadID + "</LeadID>\r\n" +
			"	<SaleDate>" + saleDate + "</SaleDate>\r\n" +
			"</CommissionEarning>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("commissionEarningId")) {
					SetXmlValue(ref commissionEarningID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("salesId")) {
					SetXmlValue(ref salesID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("productDescription")) {
					SetXmlValue(ref productDescription, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("paymentAmount")) {
					SetXmlValue(ref paymentAmount, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("paymentEntryDate")) {
					SetXmlValue(ref paymentEntryDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("commissionAmount")) {
					SetXmlValue(ref commissionAmount, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("commissionRate")) {
					SetXmlValue(ref commissionRate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("paymentNo")) {
					SetXmlValue(ref paymentNo, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("consultantId")) {
					SetXmlValue(ref consultantID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("recordEntryDate")) {
					SetXmlValue(ref recordEntryDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("associateId")) {
					SetXmlValue(ref associateID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("salesAmount")) {
					SetXmlValue(ref salesAmount, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("currencyCode")) {
					SetXmlValue(ref currencyCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("exchangeRate")) {
					SetXmlValue(ref exchangeRate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("commissionAmountCa")) {
					SetXmlValue(ref commissionAmountCa, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("leadId")) {
					SetXmlValue(ref leadID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("saleDate")) {
					SetXmlValue(ref saleDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static CommissionEarning[] GetCommissionEarnings() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCommissionEarnings();
		}

		public static CommissionEarning GetCommissionEarningByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCommissionEarningByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertCommissionEarning(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateCommissionEarning(this);
		}
		#endregion

		#region Properties
		public int CommissionEarningID {
			set { commissionEarningID = value; }
			get { return commissionEarningID; }
		}

		public int SalesID {
			set { salesID = value; }
			get { return salesID; }
		}

		public string ProductDescription {
			set { productDescription = value; }
			get { return productDescription; }
		}

		public float PaymentAmount {
			set { paymentAmount = value; }
			get { return paymentAmount; }
		}

		public DateTime PaymentEntryDate {
			set { paymentEntryDate = value; }
			get { return paymentEntryDate; }
		}

		public string CommissionAmount {
			set { commissionAmount = value; }
			get { return commissionAmount; }
		}

		public float CommissionRate {
			set { commissionRate = value; }
			get { return commissionRate; }
		}

		public int PaymentNo {
			set { paymentNo = value; }
			get { return paymentNo; }
		}

		public int ConsultantID {
			set { consultantID = value; }
			get { return consultantID; }
		}

		public DateTime RecordEntryDate {
			set { recordEntryDate = value; }
			get { return recordEntryDate; }
		}

		public int AssociateID {
			set { associateID = value; }
			get { return associateID; }
		}

		public float SalesAmount {
			set { salesAmount = value; }
			get { return salesAmount; }
		}

		public string CurrencyCode {
			set { currencyCode = value; }
			get { return currencyCode; }
		}

		public float ExchangeRate {
			set { exchangeRate = value; }
			get { return exchangeRate; }
		}

		public string CommissionAmountCa {
			set { commissionAmountCa = value; }
			get { return commissionAmountCa; }
		}

		public int LeadID {
			set { leadID = value; }
			get { return leadID; }
		}

		public DateTime SaleDate {
			set { saleDate = value; }
			get { return saleDate; }
		}

		#endregion
	}
}
