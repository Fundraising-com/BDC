using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class EFOSale: EFundraisingCRMDataObject {

		private int saleID;
		private int supporterID;
		private DateTime saleDate;
		private float amountToGroup;
		private float amountToSupplier;
		private float amount;
		private string deliveryAddress;
		private string stateCode;
		private string countryCode;
		private string deliveryCity;
		private string deliveryZipCode;
		private string cardName;
		private string cardAddress;
		private string transactionID;


		public EFOSale() : this(int.MinValue) { }
		public EFOSale(int saleID) : this(saleID, int.MinValue) { }
		public EFOSale(int saleID, int supporterID) : this(saleID, supporterID, DateTime.MinValue) { }
		public EFOSale(int saleID, int supporterID, DateTime saleDate) : this(saleID, supporterID, saleDate, float.MinValue) { }
		public EFOSale(int saleID, int supporterID, DateTime saleDate, float amountToGroup) : this(saleID, supporterID, saleDate, amountToGroup, float.MinValue) { }
		public EFOSale(int saleID, int supporterID, DateTime saleDate, float amountToGroup, float amountToSupplier) : this(saleID, supporterID, saleDate, amountToGroup, amountToSupplier, float.MinValue) { }
		public EFOSale(int saleID, int supporterID, DateTime saleDate, float amountToGroup, float amountToSupplier, float amount) : this(saleID, supporterID, saleDate, amountToGroup, amountToSupplier, amount, null) { }
		public EFOSale(int saleID, int supporterID, DateTime saleDate, float amountToGroup, float amountToSupplier, float amount, string deliveryAddress) : this(saleID, supporterID, saleDate, amountToGroup, amountToSupplier, amount, deliveryAddress, null) { }
		public EFOSale(int saleID, int supporterID, DateTime saleDate, float amountToGroup, float amountToSupplier, float amount, string deliveryAddress, string stateCode) : this(saleID, supporterID, saleDate, amountToGroup, amountToSupplier, amount, deliveryAddress, stateCode, null) { }
		public EFOSale(int saleID, int supporterID, DateTime saleDate, float amountToGroup, float amountToSupplier, float amount, string deliveryAddress, string stateCode, string countryCode) : this(saleID, supporterID, saleDate, amountToGroup, amountToSupplier, amount, deliveryAddress, stateCode, countryCode, null) { }
		public EFOSale(int saleID, int supporterID, DateTime saleDate, float amountToGroup, float amountToSupplier, float amount, string deliveryAddress, string stateCode, string countryCode, string deliveryCity) : this(saleID, supporterID, saleDate, amountToGroup, amountToSupplier, amount, deliveryAddress, stateCode, countryCode, deliveryCity, null) { }
		public EFOSale(int saleID, int supporterID, DateTime saleDate, float amountToGroup, float amountToSupplier, float amount, string deliveryAddress, string stateCode, string countryCode, string deliveryCity, string deliveryZipCode) : this(saleID, supporterID, saleDate, amountToGroup, amountToSupplier, amount, deliveryAddress, stateCode, countryCode, deliveryCity, deliveryZipCode, null) { }
		public EFOSale(int saleID, int supporterID, DateTime saleDate, float amountToGroup, float amountToSupplier, float amount, string deliveryAddress, string stateCode, string countryCode, string deliveryCity, string deliveryZipCode, string cardName) : this(saleID, supporterID, saleDate, amountToGroup, amountToSupplier, amount, deliveryAddress, stateCode, countryCode, deliveryCity, deliveryZipCode, cardName, null) { }
		public EFOSale(int saleID, int supporterID, DateTime saleDate, float amountToGroup, float amountToSupplier, float amount, string deliveryAddress, string stateCode, string countryCode, string deliveryCity, string deliveryZipCode, string cardName, string cardAddress) : this(saleID, supporterID, saleDate, amountToGroup, amountToSupplier, amount, deliveryAddress, stateCode, countryCode, deliveryCity, deliveryZipCode, cardName, cardAddress, null) { }
		public EFOSale(int saleID, int supporterID, DateTime saleDate, float amountToGroup, float amountToSupplier, float amount, string deliveryAddress, string stateCode, string countryCode, string deliveryCity, string deliveryZipCode, string cardName, string cardAddress, string transactionID) {
			this.saleID = saleID;
			this.supporterID = supporterID;
			this.saleDate = saleDate;
			this.amountToGroup = amountToGroup;
			this.amountToSupplier = amountToSupplier;
			this.amount = amount;
			this.deliveryAddress = deliveryAddress;
			this.stateCode = stateCode;
			this.countryCode = countryCode;
			this.deliveryCity = deliveryCity;
			this.deliveryZipCode = deliveryZipCode;
			this.cardName = cardName;
			this.cardAddress = cardAddress;
			this.transactionID = transactionID;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<EFOSale>\r\n" +
			"	<SaleID>" + saleID + "</SaleID>\r\n" +
			"	<SupporterID>" + supporterID + "</SupporterID>\r\n" +
			"	<SaleDate>" + saleDate + "</SaleDate>\r\n" +
			"	<AmountToGroup>" + amountToGroup + "</AmountToGroup>\r\n" +
			"	<AmountToSupplier>" + amountToSupplier + "</AmountToSupplier>\r\n" +
			"	<Amount>" + amount + "</Amount>\r\n" +
			"	<DeliveryAddress>" + System.Web.HttpUtility.HtmlEncode(deliveryAddress) + "</DeliveryAddress>\r\n" +
			"	<StateCode>" + System.Web.HttpUtility.HtmlEncode(stateCode) + "</StateCode>\r\n" +
			"	<CountryCode>" + System.Web.HttpUtility.HtmlEncode(countryCode) + "</CountryCode>\r\n" +
			"	<DeliveryCity>" + System.Web.HttpUtility.HtmlEncode(deliveryCity) + "</DeliveryCity>\r\n" +
			"	<DeliveryZipCode>" + System.Web.HttpUtility.HtmlEncode(deliveryZipCode) + "</DeliveryZipCode>\r\n" +
			"	<CardName>" + System.Web.HttpUtility.HtmlEncode(cardName) + "</CardName>\r\n" +
			"	<CardAddress>" + System.Web.HttpUtility.HtmlEncode(cardAddress) + "</CardAddress>\r\n" +
			"	<TransactionID>" + System.Web.HttpUtility.HtmlEncode(transactionID) + "</TransactionID>\r\n" +
			"</EFOSale>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("saleId")) {
					SetXmlValue(ref saleID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("supporterId")) {
					SetXmlValue(ref supporterID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("saleDate")) {
					SetXmlValue(ref saleDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("amountToGroup")) {
					SetXmlValue(ref amountToGroup, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("amountToSupplier")) {
					SetXmlValue(ref amountToSupplier, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("amount")) {
					SetXmlValue(ref amount, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("deliveryAddress")) {
					SetXmlValue(ref deliveryAddress, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("stateCode")) {
					SetXmlValue(ref stateCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("countryCode")) {
					SetXmlValue(ref countryCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("deliveryCity")) {
					SetXmlValue(ref deliveryCity, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("deliveryZipCode")) {
					SetXmlValue(ref deliveryZipCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("cardName")) {
					SetXmlValue(ref cardName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("cardAddress")) {
					SetXmlValue(ref cardAddress, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("transactionId")) {
					SetXmlValue(ref transactionID, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static EFOSale[] GetEFOSales() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEFOSales();
		}

		public static EFOSale GetEFOSaleByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEFOSaleByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertEFOSale(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateEFOSale(this);
		}
		#endregion

		#region Properties
		public int SaleID {
			set { saleID = value; }
			get { return saleID; }
		}

		public int SupporterID {
			set { supporterID = value; }
			get { return supporterID; }
		}

		public DateTime SaleDate {
			set { saleDate = value; }
			get { return saleDate; }
		}

		public float AmountToGroup {
			set { amountToGroup = value; }
			get { return amountToGroup; }
		}

		public float AmountToSupplier {
			set { amountToSupplier = value; }
			get { return amountToSupplier; }
		}

		public float Amount {
			set { amount = value; }
			get { return amount; }
		}

		public string DeliveryAddress {
			set { deliveryAddress = value; }
			get { return deliveryAddress; }
		}

		public string StateCode {
			set { stateCode = value; }
			get { return stateCode; }
		}

		public string CountryCode {
			set { countryCode = value; }
			get { return countryCode; }
		}

		public string DeliveryCity {
			set { deliveryCity = value; }
			get { return deliveryCity; }
		}

		public string DeliveryZipCode {
			set { deliveryZipCode = value; }
			get { return deliveryZipCode; }
		}

		public string CardName {
			set { cardName = value; }
			get { return cardName; }
		}

		public string CardAddress {
			set { cardAddress = value; }
			get { return cardAddress; }
		}

		public string TransactionID {
			set { transactionID = value; }
			get { return transactionID; }
		}

		#endregion
	}
}
