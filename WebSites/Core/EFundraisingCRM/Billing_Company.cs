using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class BillingCompany: EFundraisingCRMDataObject {

		private int billingCompanyID;
		private string billingCompanyCode;
		private string billingCompanyName;
		private string streetAddress;
		private string cityName;
		private string stateCode;
		private string zipCode;
		private string countryCode;
		private string telephoneNumber;
		private string email;
		private string web;
		private string logo;
		private string invoiceTitle;
		private string invoiceFooter;
		private int cultureID;


		public BillingCompany() : this(int.MinValue) { }
		public BillingCompany(int billingCompanyID) : this(billingCompanyID, null) { }
		public BillingCompany(int billingCompanyID, string billingCompanyCode) : this(billingCompanyID, billingCompanyCode, null) { }
		public BillingCompany(int billingCompanyID, string billingCompanyCode, string billingCompanyName) : this(billingCompanyID, billingCompanyCode, billingCompanyName, null) { }
		public BillingCompany(int billingCompanyID, string billingCompanyCode, string billingCompanyName, string streetAddress) : this(billingCompanyID, billingCompanyCode, billingCompanyName, streetAddress, null) { }
		public BillingCompany(int billingCompanyID, string billingCompanyCode, string billingCompanyName, string streetAddress, string cityName) : this(billingCompanyID, billingCompanyCode, billingCompanyName, streetAddress, cityName, null) { }
		public BillingCompany(int billingCompanyID, string billingCompanyCode, string billingCompanyName, string streetAddress, string cityName, string stateCode) : this(billingCompanyID, billingCompanyCode, billingCompanyName, streetAddress, cityName, stateCode, null) { }
		public BillingCompany(int billingCompanyID, string billingCompanyCode, string billingCompanyName, string streetAddress, string cityName, string stateCode, string zipCode) : this(billingCompanyID, billingCompanyCode, billingCompanyName, streetAddress, cityName, stateCode, zipCode, null) { }
		public BillingCompany(int billingCompanyID, string billingCompanyCode, string billingCompanyName, string streetAddress, string cityName, string stateCode, string zipCode, string countryCode) : this(billingCompanyID, billingCompanyCode, billingCompanyName, streetAddress, cityName, stateCode, zipCode, countryCode, null) { }
		public BillingCompany(int billingCompanyID, string billingCompanyCode, string billingCompanyName, string streetAddress, string cityName, string stateCode, string zipCode, string countryCode, string telephoneNumber) : this(billingCompanyID, billingCompanyCode, billingCompanyName, streetAddress, cityName, stateCode, zipCode, countryCode, telephoneNumber, null) { }
		public BillingCompany(int billingCompanyID, string billingCompanyCode, string billingCompanyName, string streetAddress, string cityName, string stateCode, string zipCode, string countryCode, string telephoneNumber, string email) : this(billingCompanyID, billingCompanyCode, billingCompanyName, streetAddress, cityName, stateCode, zipCode, countryCode, telephoneNumber, email, null) { }
		public BillingCompany(int billingCompanyID, string billingCompanyCode, string billingCompanyName, string streetAddress, string cityName, string stateCode, string zipCode, string countryCode, string telephoneNumber, string email, string web) : this(billingCompanyID, billingCompanyCode, billingCompanyName, streetAddress, cityName, stateCode, zipCode, countryCode, telephoneNumber, email, web, null) { }
		public BillingCompany(int billingCompanyID, string billingCompanyCode, string billingCompanyName, string streetAddress, string cityName, string stateCode, string zipCode, string countryCode, string telephoneNumber, string email, string web, string logo) : this(billingCompanyID, billingCompanyCode, billingCompanyName, streetAddress, cityName, stateCode, zipCode, countryCode, telephoneNumber, email, web, logo, null) { }
		public BillingCompany(int billingCompanyID, string billingCompanyCode, string billingCompanyName, string streetAddress, string cityName, string stateCode, string zipCode, string countryCode, string telephoneNumber, string email, string web, string logo, string invoiceTitle) : this(billingCompanyID, billingCompanyCode, billingCompanyName, streetAddress, cityName, stateCode, zipCode, countryCode, telephoneNumber, email, web, logo, invoiceTitle, null) { }
		public BillingCompany(int billingCompanyID, string billingCompanyCode, string billingCompanyName, string streetAddress, string cityName, string stateCode, string zipCode, string countryCode, string telephoneNumber, string email, string web, string logo, string invoiceTitle, string invoiceFooter) {
			this.billingCompanyID = billingCompanyID;
			this.billingCompanyCode = billingCompanyCode;
			this.billingCompanyName = billingCompanyName;
			this.streetAddress = streetAddress;
			this.cityName = cityName;
			this.stateCode = stateCode;
			this.zipCode = zipCode;
			this.countryCode = countryCode;
			this.telephoneNumber = telephoneNumber;
			this.email = email;
			this.web = web;
			this.logo = logo;
			this.invoiceTitle = invoiceTitle;
			this.invoiceFooter = invoiceFooter;
		}

		#region Static Data
		public static BillingCompany eFundraising_USA {
			get { return new BillingCompany(1, "eFundraising - USA", "eFundraising.com Corp.", "205 West Service Road", "CHAMPLAIN", "NY", "12919", "US", "1-800-561-8388", null, "www.efundraising.com", @"P:\logos\logo_B&W.gif", "INVOICE", "12% interest will be charged for late payments. Please indicate the Invoice Reference number on your check.    Please make your check payable to eFundraising.com Corp."); }
		}
		public static BillingCompany eFundraising_CAN {
			get { return new BillingCompany(2, "eFundraising - CAN", "eFundraising.com Corp. 2967162 CANADA INC.", "33 PRINCE STREET SUITE 200", "MONTREAL", "QU", "H3C 2M7", "CA", "1-888-875-1245", null, "www.efundraising.com", @"P:\logos\logo_B&W.gif", "INVOICE", "12% interest will be charged for late payments. Please indicate the Invoice Reference number on your check.    Please make your check payable to eFundraising.com Corp."); }
		}
		public static BillingCompany eFundraising_QC {
			get { return new BillingCompany(3, "eFundraising - QC", "Corp. eFundraising.com", "33, RUE PRINCE SUITE 200", "MONTRÉAL", "QU", "H3C 2M7", "CA", "1-877-875-8701", null, "www.efundraising.com", @"P:\logos\logo_B&W.gif", "FACTURE", "Des intérêts au taux annuel de 12% seront facturés pour tout retard de paiement.Veuillez S.V.P. inscrire le numéro de référence de la facture à l'endos de votre chèque.    Veuillez libeller votre chèque au nom de Corp. eFundraising.com."); }
		}
		public static BillingCompany MyTeam {
			get { return new BillingCompany(4, "MyTeam", "MyTeamfundraising.com", "205 West Service Road", "CHAMPLAIN", "NY", "12919", "US", "1-877-MYTEAM-7", null, "www.myteamfundraising.com", @"P:\logos\ptitmyteamblack.gif", "INVOICE", "12% interest will be charged for late payments. Please indicate the Invoice Reference number on your check."); }
		}
		public static BillingCompany QSP {
			get { return new BillingCompany(5, "QSP", "QSP / Reader's Digest", "205 West Service Road", "CHAMPLAIN", "NY", "12919", "US", ".", null, "www.qsp.com", @"P:\logos\QSP-readers50pct.jpg", "INVOICE", "12% interest will be charged for late payments. Please indicate the Invoice Reference number on your check."); }
		}
		public static BillingCompany Active {
			get { return new BillingCompany(6, "Active", "Active Team Fundraising", "205 West Service Road", "CHAMPLAIN", "NY", "12919", "US", "1-800-561-8388", null, "www.activeteamfundraising.com", @"P:\logos\ActiveTeam.gif", "INVOICE", "12% interest will be charged for late payments. Please indicate the Invoice Reference number on your check."); }
		}
		public static BillingCompany FR {
			get { return new BillingCompany(7, "FR", "Fundraising.com", "205 West Service Road", "CHAMPLAIN", "NY", "12919", "US", "1-800-561-8388", null, "www.fundraising.com", @"P:\logos\fr.jpg", "INVOICE", "12% interest will be charged for late payments. Please indicate the Invoice Reference number on your check."); }
		}
		#endregion

		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<BillingCompany>\r\n" +
			"	<BillingCompanyID>" + billingCompanyID + "</BillingCompanyID>\r\n" +
			"	<BillingCompanyCode>" + System.Web.HttpUtility.HtmlEncode(billingCompanyCode) + "</BillingCompanyCode>\r\n" +
			"	<BillingCompanyName>" + System.Web.HttpUtility.HtmlEncode(billingCompanyName) + "</BillingCompanyName>\r\n" +
			"	<StreetAddress>" + System.Web.HttpUtility.HtmlEncode(streetAddress) + "</StreetAddress>\r\n" +
			"	<CityName>" + System.Web.HttpUtility.HtmlEncode(cityName) + "</CityName>\r\n" +
			"	<StateCode>" + System.Web.HttpUtility.HtmlEncode(stateCode) + "</StateCode>\r\n" +
			"	<ZipCode>" + System.Web.HttpUtility.HtmlEncode(zipCode) + "</ZipCode>\r\n" +
			"	<CountryCode>" + System.Web.HttpUtility.HtmlEncode(countryCode) + "</CountryCode>\r\n" +
			"	<TelephoneNumber>" + System.Web.HttpUtility.HtmlEncode(telephoneNumber) + "</TelephoneNumber>\r\n" +
			"	<Email>" + System.Web.HttpUtility.HtmlEncode(email) + "</Email>\r\n" +
			"	<Web>" + System.Web.HttpUtility.HtmlEncode(web) + "</Web>\r\n" +
			"	<Logo>" + System.Web.HttpUtility.HtmlEncode(logo) + "</Logo>\r\n" +
			"	<InvoiceTitle>" + System.Web.HttpUtility.HtmlEncode(invoiceTitle) + "</InvoiceTitle>\r\n" +
			"	<InvoiceFooter>" + System.Web.HttpUtility.HtmlEncode(invoiceFooter) + "</InvoiceFooter>\r\n" +
			"</BillingCompany>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("billingCompanyId")) {
					SetXmlValue(ref billingCompanyID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("billingCompanyCode")) {
					SetXmlValue(ref billingCompanyCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("billingCompanyName")) {
					SetXmlValue(ref billingCompanyName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("streetAddress")) {
					SetXmlValue(ref streetAddress, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("cityName")) {
					SetXmlValue(ref cityName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("stateCode")) {
					SetXmlValue(ref stateCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("zipCode")) {
					SetXmlValue(ref zipCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("countryCode")) {
					SetXmlValue(ref countryCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("telephoneNumber")) {
					SetXmlValue(ref telephoneNumber, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("email")) {
					SetXmlValue(ref email, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("web")) {
					SetXmlValue(ref web, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("logo")) {
					SetXmlValue(ref logo, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("invoiceTitle")) {
					SetXmlValue(ref invoiceTitle, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("invoiceFooter")) {
					SetXmlValue(ref invoiceFooter, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static BillingCompany[] GetBillingCompanys() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetBillingCompanys();
		}

		public static BillingCompany GetBillingCompanyByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetBillingCompanyByID(id);
		}

		public static System.Data.DataTable RSBillingCompanyBySaleID(int id) 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.RSBillingCompanyBySaleID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertBillingCompany(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateBillingCompany(this);
		}
		#endregion

		#region Properties
		public int BillingCompanyID {
			set { billingCompanyID = value; }
			get { return billingCompanyID; }
		}

		public string BillingCompanyCode {
			set { billingCompanyCode = value; }
			get { return billingCompanyCode; }
		}

		public string BillingCompanyName {
			set { billingCompanyName = value; }
			get { return billingCompanyName; }
		}

		public string StreetAddress {
			set { streetAddress = value; }
			get { return streetAddress; }
		}

		public string CityName {
			set { cityName = value; }
			get { return cityName; }
		}

		public string StateCode {
			set { stateCode = value; }
			get { return stateCode; }
		}

		public string ZipCode {
			set { zipCode = value; }
			get { return zipCode; }
		}

		public string CountryCode {
			set { countryCode = value; }
			get { return countryCode; }
		}

		public string TelephoneNumber {
			set { telephoneNumber = value; }
			get { return telephoneNumber; }
		}

		public string Email {
			set { email = value; }
			get { return email; }
		}

		public string Web {
			set { web = value; }
			get { return web; }
		}

		public string Logo {
			set { logo = value; }
			get { return logo; }
		}

		public string InvoiceTitle {
			set { invoiceTitle = value; }
			get { return invoiceTitle; }
		}

		public string InvoiceFooter {
			set { invoiceFooter = value; }
			get { return invoiceFooter; }
		}

		public int CultureID
		{
			set { cultureID = value; }
			get { return cultureID; }
		}

		#endregion
	}
}
