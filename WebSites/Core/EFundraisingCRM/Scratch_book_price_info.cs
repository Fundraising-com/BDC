using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class ScratchBookPriceInfo: EFundraisingCRMDataObject {

		private string countryCode;
		private int scratchBookId;
		private short productClassId;
		private DateTime effectiveDate;
		private short unitPrice;


		public ScratchBookPriceInfo() : this(null) { }
		public ScratchBookPriceInfo(string countryCode) : this(countryCode, int.MinValue) { }
		public ScratchBookPriceInfo(string countryCode, int scratchBookId) : this(countryCode, scratchBookId, short.MinValue) { }
		public ScratchBookPriceInfo(string countryCode, int scratchBookId, short productClassId) : this(countryCode, scratchBookId, productClassId, DateTime.MinValue) { }
		public ScratchBookPriceInfo(string countryCode, int scratchBookId, short productClassId, DateTime effectiveDate) : this(countryCode, scratchBookId, productClassId, effectiveDate, short.MinValue) { }
		public ScratchBookPriceInfo(string countryCode, int scratchBookId, short productClassId, DateTime effectiveDate, short unitPrice) {
			this.countryCode = countryCode;
			this.scratchBookId = scratchBookId;
			this.productClassId = productClassId;
			this.effectiveDate = effectiveDate;
			this.unitPrice = unitPrice;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ScratchBookPriceInfo>\r\n" +
			"	<CountryCode>" + System.Web.HttpUtility.HtmlEncode(countryCode) + "</CountryCode>\r\n" +
			"	<ScratchBookId>" + scratchBookId + "</ScratchBookId>\r\n" +
			"	<ProductClassId>" + productClassId + "</ProductClassId>\r\n" +
			"	<EffectiveDate>" + effectiveDate + "</EffectiveDate>\r\n" +
			"	<UnitPrice>" + unitPrice + "</UnitPrice>\r\n" +
			"</ScratchBookPriceInfo>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("countryCode")) {
					SetXmlValue(ref countryCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("scratchBookId")) {
					SetXmlValue(ref scratchBookId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("productClassId")) {
					SetXmlValue(ref productClassId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("effectiveDate")) {
					SetXmlValue(ref effectiveDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("unitPrice")) {
					SetXmlValue(ref unitPrice, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ScratchBookPriceInfo[] GetScratchBookPriceInfos() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetScratchBookPriceInfos();
		}

		
		public static decimal GetScratchBookCurrentPriceByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetScratchBookCurrentPriceByID(id);
		}
/*
		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertScratchBookPriceInfo(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateScratchBookPriceInfo(this);
		}*/
		#endregion

		#region Properties
		public string CountryCode {
			set { countryCode = value; }
			get { return countryCode; }
		}

		public int ScratchBookId {
			set { scratchBookId = value; }
			get { return scratchBookId; }
		}

		public short ProductClassId {
			set { productClassId = value; }
			get { return productClassId; }
		}

		public DateTime EffectiveDate {
			set { effectiveDate = value; }
			get { return effectiveDate; }
		}

		public short UnitPrice {
			set { unitPrice = value; }
			get { return unitPrice; }
		}

		#endregion
	}
}
