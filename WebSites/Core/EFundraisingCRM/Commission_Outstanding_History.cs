using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class CommissionOutstandingHistory: EFundraisingCRMDataObject {

		private int salesID;
		private int month;
		private int year;
		private int consultantID;
		private DateTime salesDate;
		private DateTime shippedDate;
		private string status;
		private string paymentTerm;
		private string firstName;
		private string lastName;
		private string organization;
		private string dayPhone;
		private string outstandingAmount;
		private string currencyCode;
		private string outstandingCommission;


		public CommissionOutstandingHistory() : this(int.MinValue) { }
		public CommissionOutstandingHistory(int salesID) : this(salesID, int.MinValue) { }
		public CommissionOutstandingHistory(int salesID, int month) : this(salesID, month, int.MinValue) { }
		public CommissionOutstandingHistory(int salesID, int month, int year) : this(salesID, month, year, int.MinValue) { }
		public CommissionOutstandingHistory(int salesID, int month, int year, int consultantID) : this(salesID, month, year, consultantID, DateTime.MinValue) { }
		public CommissionOutstandingHistory(int salesID, int month, int year, int consultantID, DateTime salesDate) : this(salesID, month, year, consultantID, salesDate, DateTime.MinValue) { }
		public CommissionOutstandingHistory(int salesID, int month, int year, int consultantID, DateTime salesDate, DateTime shippedDate) : this(salesID, month, year, consultantID, salesDate, shippedDate, null) { }
		public CommissionOutstandingHistory(int salesID, int month, int year, int consultantID, DateTime salesDate, DateTime shippedDate, string status) : this(salesID, month, year, consultantID, salesDate, shippedDate, status, null) { }
		public CommissionOutstandingHistory(int salesID, int month, int year, int consultantID, DateTime salesDate, DateTime shippedDate, string status, string paymentTerm) : this(salesID, month, year, consultantID, salesDate, shippedDate, status, paymentTerm, null) { }
		public CommissionOutstandingHistory(int salesID, int month, int year, int consultantID, DateTime salesDate, DateTime shippedDate, string status, string paymentTerm, string firstName) : this(salesID, month, year, consultantID, salesDate, shippedDate, status, paymentTerm, firstName, null) { }
		public CommissionOutstandingHistory(int salesID, int month, int year, int consultantID, DateTime salesDate, DateTime shippedDate, string status, string paymentTerm, string firstName, string lastName) : this(salesID, month, year, consultantID, salesDate, shippedDate, status, paymentTerm, firstName, lastName, null) { }
		public CommissionOutstandingHistory(int salesID, int month, int year, int consultantID, DateTime salesDate, DateTime shippedDate, string status, string paymentTerm, string firstName, string lastName, string organization) : this(salesID, month, year, consultantID, salesDate, shippedDate, status, paymentTerm, firstName, lastName, organization, null) { }
		public CommissionOutstandingHistory(int salesID, int month, int year, int consultantID, DateTime salesDate, DateTime shippedDate, string status, string paymentTerm, string firstName, string lastName, string organization, string dayPhone) : this(salesID, month, year, consultantID, salesDate, shippedDate, status, paymentTerm, firstName, lastName, organization, dayPhone, null) { }
		public CommissionOutstandingHistory(int salesID, int month, int year, int consultantID, DateTime salesDate, DateTime shippedDate, string status, string paymentTerm, string firstName, string lastName, string organization, string dayPhone, string outstandingAmount) : this(salesID, month, year, consultantID, salesDate, shippedDate, status, paymentTerm, firstName, lastName, organization, dayPhone, outstandingAmount, null) { }
		public CommissionOutstandingHistory(int salesID, int month, int year, int consultantID, DateTime salesDate, DateTime shippedDate, string status, string paymentTerm, string firstName, string lastName, string organization, string dayPhone, string outstandingAmount, string currencyCode) : this(salesID, month, year, consultantID, salesDate, shippedDate, status, paymentTerm, firstName, lastName, organization, dayPhone, outstandingAmount, currencyCode, null) { }
		public CommissionOutstandingHistory(int salesID, int month, int year, int consultantID, DateTime salesDate, DateTime shippedDate, string status, string paymentTerm, string firstName, string lastName, string organization, string dayPhone, string outstandingAmount, string currencyCode, string outstandingCommission) {
			this.salesID = salesID;
			this.month = month;
			this.year = year;
			this.consultantID = consultantID;
			this.salesDate = salesDate;
			this.shippedDate = shippedDate;
			this.status = status;
			this.paymentTerm = paymentTerm;
			this.firstName = firstName;
			this.lastName = lastName;
			this.organization = organization;
			this.dayPhone = dayPhone;
			this.outstandingAmount = outstandingAmount;
			this.currencyCode = currencyCode;
			this.outstandingCommission = outstandingCommission;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<CommissionOutstandingHistory>\r\n" +
			"	<SalesID>" + salesID + "</SalesID>\r\n" +
			"	<Month>" + month + "</Month>\r\n" +
			"	<Year>" + year + "</Year>\r\n" +
			"	<ConsultantID>" + consultantID + "</ConsultantID>\r\n" +
			"	<SalesDate>" + salesDate + "</SalesDate>\r\n" +
			"	<ShippedDate>" + shippedDate + "</ShippedDate>\r\n" +
			"	<Status>" + System.Web.HttpUtility.HtmlEncode(status) + "</Status>\r\n" +
			"	<PaymentTerm>" + System.Web.HttpUtility.HtmlEncode(paymentTerm) + "</PaymentTerm>\r\n" +
			"	<FirstName>" + System.Web.HttpUtility.HtmlEncode(firstName) + "</FirstName>\r\n" +
			"	<LastName>" + System.Web.HttpUtility.HtmlEncode(lastName) + "</LastName>\r\n" +
			"	<Organization>" + System.Web.HttpUtility.HtmlEncode(organization) + "</Organization>\r\n" +
			"	<DayPhone>" + System.Web.HttpUtility.HtmlEncode(dayPhone) + "</DayPhone>\r\n" +
			"	<OutstandingAmount>" + System.Web.HttpUtility.HtmlEncode(outstandingAmount) + "</OutstandingAmount>\r\n" +
			"	<CurrencyCode>" + System.Web.HttpUtility.HtmlEncode(currencyCode) + "</CurrencyCode>\r\n" +
			"	<OutstandingCommission>" + System.Web.HttpUtility.HtmlEncode(outstandingCommission) + "</OutstandingCommission>\r\n" +
			"</CommissionOutstandingHistory>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("salesId")) {
					SetXmlValue(ref salesID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("month")) {
					SetXmlValue(ref month, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("year")) {
					SetXmlValue(ref year, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("consultantId")) {
					SetXmlValue(ref consultantID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("salesDate")) {
					SetXmlValue(ref salesDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("shippedDate")) {
					SetXmlValue(ref shippedDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("status")) {
					SetXmlValue(ref status, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("paymentTerm")) {
					SetXmlValue(ref paymentTerm, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("firstName")) {
					SetXmlValue(ref firstName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("lastName")) {
					SetXmlValue(ref lastName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("organization")) {
					SetXmlValue(ref organization, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("dayPhone")) {
					SetXmlValue(ref dayPhone, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("outstandingAmount")) {
					SetXmlValue(ref outstandingAmount, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("currencyCode")) {
					SetXmlValue(ref currencyCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("outstandingCommission")) {
					SetXmlValue(ref outstandingCommission, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static CommissionOutstandingHistory[] GetCommissionOutstandingHistorys() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCommissionOutstandingHistorys();
		}

		public static CommissionOutstandingHistory GetCommissionOutstandingHistoryByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCommissionOutstandingHistoryByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertCommissionOutstandingHistory(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateCommissionOutstandingHistory(this);
		}
		#endregion

		#region Properties
		public int SalesID {
			set { salesID = value; }
			get { return salesID; }
		}

		public int Month {
			set { month = value; }
			get { return month; }
		}

		public int Year {
			set { year = value; }
			get { return year; }
		}

		public int ConsultantID {
			set { consultantID = value; }
			get { return consultantID; }
		}

		public DateTime SalesDate {
			set { salesDate = value; }
			get { return salesDate; }
		}

		public DateTime ShippedDate {
			set { shippedDate = value; }
			get { return shippedDate; }
		}

		public string Status {
			set { status = value; }
			get { return status; }
		}

		public string PaymentTerm {
			set { paymentTerm = value; }
			get { return paymentTerm; }
		}

		public string FirstName {
			set { firstName = value; }
			get { return firstName; }
		}

		public string LastName {
			set { lastName = value; }
			get { return lastName; }
		}

		public string Organization {
			set { organization = value; }
			get { return organization; }
		}

		public string DayPhone {
			set { dayPhone = value; }
			get { return dayPhone; }
		}

		public string OutstandingAmount {
			set { outstandingAmount = value; }
			get { return outstandingAmount; }
		}

		public string CurrencyCode {
			set { currencyCode = value; }
			get { return currencyCode; }
		}

		public string OutstandingCommission {
			set { outstandingCommission = value; }
			get { return outstandingCommission; }
		}

		#endregion
	}
}
