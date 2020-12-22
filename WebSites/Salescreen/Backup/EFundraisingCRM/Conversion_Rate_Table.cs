using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class ConversionRateTable: EFundraisingCRMDataObject {

		private string currencyCode;
		private float conversionRate;
		private DateTime conversionDate;
		private int conversionRateId;


		public ConversionRateTable() : this(null) { }
		public ConversionRateTable(string currencyCode) : this(currencyCode, float.MinValue) { }
		public ConversionRateTable(string currencyCode, float conversionRate) : this(currencyCode, conversionRate, DateTime.MinValue) { }
		public ConversionRateTable(string currencyCode, float conversionRate, DateTime conversionDate) : this(currencyCode, conversionRate, conversionDate, int.MinValue) { }
		public ConversionRateTable(string currencyCode, float conversionRate, DateTime conversionDate, int conversionRateId) {
			this.currencyCode = currencyCode;
			this.conversionRate = conversionRate;
			this.conversionDate = conversionDate;
			this.conversionRateId = conversionRateId;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ConversionRateTable>\r\n" +
			"	<CurrencyCode>" + System.Web.HttpUtility.HtmlEncode(currencyCode) + "</CurrencyCode>\r\n" +
			"	<ConversionRate>" + conversionRate + "</ConversionRate>\r\n" +
			"	<ConversionDate>" + conversionDate + "</ConversionDate>\r\n" +
			"	<ConversionRateId>" + conversionRateId + "</ConversionRateId>\r\n" +
			"</ConversionRateTable>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("currencyCode")) {
					SetXmlValue(ref currencyCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("conversionRate")) {
					SetXmlValue(ref conversionRate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("conversionDate")) {
					SetXmlValue(ref conversionDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("conversionRateId")) {
					SetXmlValue(ref conversionRateId, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ConversionRateTable[] GetConversionRateTables() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetConversionRateTables();
		}

		/*
		public static ConversionRateTable GetConversionRateTableByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetConversionRateTableByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertConversionRateTable(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateConversionRateTable(this);
		}*/
		#endregion

		#region Properties
		public string CurrencyCode {
			set { currencyCode = value; }
			get { return currencyCode; }
		}

		public float ConversionRate {
			set { conversionRate = value; }
			get { return conversionRate; }
		}

		public DateTime ConversionDate {
			set { conversionDate = value; }
			get { return conversionDate; }
		}

		public int ConversionRateId {
			set { conversionRateId = value; }
			get { return conversionRateId; }
		}

		#endregion
	}
}
