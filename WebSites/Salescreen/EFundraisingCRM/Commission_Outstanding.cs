using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class CommissionOutstanding: EFundraisingCRMDataObject {

		private int salesID;
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


		public CommissionOutstanding() : this(int.MinValue) { }
		public CommissionOutstanding(int salesID) : this(salesID, int.MinValue) { }
		public CommissionOutstanding(int salesID, int consultantID) : this(salesID, consultantID, DateTime.MinValue) { }
		public CommissionOutstanding(int salesID, int consultantID, DateTime salesDate) : this(salesID, consultantID, salesDate, DateTime.MinValue) { }
		public CommissionOutstanding(int salesID, int consultantID, DateTime salesDate, DateTime shippedDate) : this(salesID, consultantID, salesDate, shippedDate, null) { }
		public CommissionOutstanding(int salesID, int consultantID, DateTime salesDate, DateTime shippedDate, string status) : this(salesID, consultantID, salesDate, shippedDate, status, null) { }
		public CommissionOutstanding(int salesID, int consultantID, DateTime salesDate, DateTime shippedDate, string status, string paymentTerm) : this(salesID, consultantID, salesDate, shippedDate, status, paymentTerm, null) { }
		public CommissionOutstanding(int salesID, int consultantID, DateTime salesDate, DateTime shippedDate, string status, string paymentTerm, string firstName) : this(salesID, consultantID, salesDate, shippedDate, status, paymentTerm, firstName, null) { }
		public CommissionOutstanding(int salesID, int consultantID, DateTime salesDate, DateTime shippedDate, string status, string paymentTerm, string firstName, string lastName) : this(salesID, consultantID, salesDate, shippedDate, status, paymentTerm, firstName, lastName, null) { }
		public CommissionOutstanding(int salesID, int consultantID, DateTime salesDate, DateTime shippedDate, string status, string paymentTerm, string firstName, string lastName, string organization) : this(salesID, consultantID, salesDate, shippedDate, status, paymentTerm, firstName, lastName, organization, null) { }
		public CommissionOutstanding(int salesID, int consultantID, DateTime salesDate, DateTime shippedDate, string status, string paymentTerm, string firstName, string lastName, string organization, string dayPhone) : this(salesID, consultantID, salesDate, shippedDate, status, paymentTerm, firstName, lastName, organization, dayPhone, null) { }
		public CommissionOutstanding(int salesID, int consultantID, DateTime salesDate, DateTime shippedDate, string status, string paymentTerm, string firstName, string lastName, string organization, string dayPhone, string outstandingAmount) : this(salesID, consultantID, salesDate, shippedDate, status, paymentTerm, firstName, lastName, organization, dayPhone, outstandingAmount, null) { }
		public CommissionOutstanding(int salesID, int consultantID, DateTime salesDate, DateTime shippedDate, string status, string paymentTerm, string firstName, string lastName, string organization, string dayPhone, string outstandingAmount, string currencyCode) : this(salesID, consultantID, salesDate, shippedDate, status, paymentTerm, firstName, lastName, organization, dayPhone, outstandingAmount, currencyCode, null) { }
		public CommissionOutstanding(int salesID, int consultantID, DateTime salesDate, DateTime shippedDate, string status, string paymentTerm, string firstName, string lastName, string organization, string dayPhone, string outstandingAmount, string currencyCode, string outstandingCommission) {
			this.salesID = salesID;
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
			return "<CommissionOutstanding>\r\n" +
			"	<SalesID>" + salesID + "</SalesID>\r\n" +
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
			"</CommissionOutstanding>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("salesId")) {
					SetXmlValue(ref salesID, node.InnerText);
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
		public static CommissionOutstanding[] GetCommissionOutstandings() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCommissionOutstandings();
		}

		public static CommissionOutstanding GetCommissionOutstandingByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCommissionOutstandingByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertCommissionOutstanding(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateCommissionOutstanding(this);
		}
		#endregion

		#region Properties
		public int SalesID {
			set { salesID = value; }
			get { return salesID; }
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
