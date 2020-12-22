using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class FieldSalesManager: EFundraisingCRMDataObject {

		private int fSMID;
		private string qSPID;
		private int areaManagerID;
		private string firstName;
		private string password;
		private string lastName;
		private string email;
		private string homePhone;
		private string workPhone;
		private string faxNumber;
		private string tollFreePhone;
		private string mobilePhone;
		private string pagerPhone;
		private string region;


		public FieldSalesManager() : this(int.MinValue) { }
		public FieldSalesManager(int fSMID) : this(fSMID, null) { }
		public FieldSalesManager(int fSMID, string qSPID) : this(fSMID, qSPID, int.MinValue) { }
		public FieldSalesManager(int fSMID, string qSPID, int areaManagerID) : this(fSMID, qSPID, areaManagerID, null) { }
		public FieldSalesManager(int fSMID, string qSPID, int areaManagerID, string firstName) : this(fSMID, qSPID, areaManagerID, firstName, null) { }
		public FieldSalesManager(int fSMID, string qSPID, int areaManagerID, string firstName, string password) : this(fSMID, qSPID, areaManagerID, firstName, password, null) { }
		public FieldSalesManager(int fSMID, string qSPID, int areaManagerID, string firstName, string password, string lastName) : this(fSMID, qSPID, areaManagerID, firstName, password, lastName, null) { }
		public FieldSalesManager(int fSMID, string qSPID, int areaManagerID, string firstName, string password, string lastName, string email) : this(fSMID, qSPID, areaManagerID, firstName, password, lastName, email, null) { }
		public FieldSalesManager(int fSMID, string qSPID, int areaManagerID, string firstName, string password, string lastName, string email, string homePhone) : this(fSMID, qSPID, areaManagerID, firstName, password, lastName, email, homePhone, null) { }
		public FieldSalesManager(int fSMID, string qSPID, int areaManagerID, string firstName, string password, string lastName, string email, string homePhone, string workPhone) : this(fSMID, qSPID, areaManagerID, firstName, password, lastName, email, homePhone, workPhone, null) { }
		public FieldSalesManager(int fSMID, string qSPID, int areaManagerID, string firstName, string password, string lastName, string email, string homePhone, string workPhone, string faxNumber) : this(fSMID, qSPID, areaManagerID, firstName, password, lastName, email, homePhone, workPhone, faxNumber, null) { }
		public FieldSalesManager(int fSMID, string qSPID, int areaManagerID, string firstName, string password, string lastName, string email, string homePhone, string workPhone, string faxNumber, string tollFreePhone) : this(fSMID, qSPID, areaManagerID, firstName, password, lastName, email, homePhone, workPhone, faxNumber, tollFreePhone, null) { }
		public FieldSalesManager(int fSMID, string qSPID, int areaManagerID, string firstName, string password, string lastName, string email, string homePhone, string workPhone, string faxNumber, string tollFreePhone, string mobilePhone) : this(fSMID, qSPID, areaManagerID, firstName, password, lastName, email, homePhone, workPhone, faxNumber, tollFreePhone, mobilePhone, null) { }
		public FieldSalesManager(int fSMID, string qSPID, int areaManagerID, string firstName, string password, string lastName, string email, string homePhone, string workPhone, string faxNumber, string tollFreePhone, string mobilePhone, string pagerPhone) : this(fSMID, qSPID, areaManagerID, firstName, password, lastName, email, homePhone, workPhone, faxNumber, tollFreePhone, mobilePhone, pagerPhone, null) { }
		public FieldSalesManager(int fSMID, string qSPID, int areaManagerID, string firstName, string password, string lastName, string email, string homePhone, string workPhone, string faxNumber, string tollFreePhone, string mobilePhone, string pagerPhone, string region) {
			this.fSMID = fSMID;
			this.qSPID = qSPID;
			this.areaManagerID = areaManagerID;
			this.firstName = firstName;
			this.password = password;
			this.lastName = lastName;
			this.email = email;
			this.homePhone = homePhone;
			this.workPhone = workPhone;
			this.faxNumber = faxNumber;
			this.tollFreePhone = tollFreePhone;
			this.mobilePhone = mobilePhone;
			this.pagerPhone = pagerPhone;
			this.region = region;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<FieldSalesManager>\r\n" +
			"	<FSMID>" + fSMID + "</FSMID>\r\n" +
			"	<QSPID>" + System.Web.HttpUtility.HtmlEncode(qSPID) + "</QSPID>\r\n" +
			"	<AreaManagerID>" + areaManagerID + "</AreaManagerID>\r\n" +
			"	<FirstName>" + System.Web.HttpUtility.HtmlEncode(firstName) + "</FirstName>\r\n" +
			"	<Password>" + System.Web.HttpUtility.HtmlEncode(password) + "</Password>\r\n" +
			"	<LastName>" + System.Web.HttpUtility.HtmlEncode(lastName) + "</LastName>\r\n" +
			"	<Email>" + System.Web.HttpUtility.HtmlEncode(email) + "</Email>\r\n" +
			"	<HomePhone>" + System.Web.HttpUtility.HtmlEncode(homePhone) + "</HomePhone>\r\n" +
			"	<WorkPhone>" + System.Web.HttpUtility.HtmlEncode(workPhone) + "</WorkPhone>\r\n" +
			"	<FaxNumber>" + System.Web.HttpUtility.HtmlEncode(faxNumber) + "</FaxNumber>\r\n" +
			"	<TollFreePhone>" + System.Web.HttpUtility.HtmlEncode(tollFreePhone) + "</TollFreePhone>\r\n" +
			"	<MobilePhone>" + System.Web.HttpUtility.HtmlEncode(mobilePhone) + "</MobilePhone>\r\n" +
			"	<PagerPhone>" + System.Web.HttpUtility.HtmlEncode(pagerPhone) + "</PagerPhone>\r\n" +
			"	<Region>" + System.Web.HttpUtility.HtmlEncode(region) + "</Region>\r\n" +
			"</FieldSalesManager>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("fsmId")) {
					SetXmlValue(ref fSMID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("qspId")) {
					SetXmlValue(ref qSPID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("areaManagerId")) {
					SetXmlValue(ref areaManagerID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("firstName")) {
					SetXmlValue(ref firstName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("password")) {
					SetXmlValue(ref password, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("lastName")) {
					SetXmlValue(ref lastName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("email")) {
					SetXmlValue(ref email, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("homePhone")) {
					SetXmlValue(ref homePhone, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("workPhone")) {
					SetXmlValue(ref workPhone, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("faxNumber")) {
					SetXmlValue(ref faxNumber, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("tollFreePhone")) {
					SetXmlValue(ref tollFreePhone, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("mobilePhone")) {
					SetXmlValue(ref mobilePhone, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("pagerPhone")) {
					SetXmlValue(ref pagerPhone, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("region")) {
					SetXmlValue(ref region, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static FieldSalesManager[] GetFieldSalesManagers() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetFieldSalesManagers();
		}

		public static FieldSalesManager GetFieldSalesManagerByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetFieldSalesManagerByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertFieldSalesManager(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateFieldSalesManager(this);
		}
		#endregion

		#region Properties
		public int FSMID {
			set { fSMID = value; }
			get { return fSMID; }
		}

		public string QSPID {
			set { qSPID = value; }
			get { return qSPID; }
		}

		public int AreaManagerID {
			set { areaManagerID = value; }
			get { return areaManagerID; }
		}

		public string FirstName {
			set { firstName = value; }
			get { return firstName; }
		}

		public string Password {
			set { password = value; }
			get { return password; }
		}

		public string LastName {
			set { lastName = value; }
			get { return lastName; }
		}

		public string Email {
			set { email = value; }
			get { return email; }
		}

		public string HomePhone {
			set { homePhone = value; }
			get { return homePhone; }
		}

		public string WorkPhone {
			set { workPhone = value; }
			get { return workPhone; }
		}

		public string FaxNumber {
			set { faxNumber = value; }
			get { return faxNumber; }
		}

		public string TollFreePhone {
			set { tollFreePhone = value; }
			get { return tollFreePhone; }
		}

		public string MobilePhone {
			set { mobilePhone = value; }
			get { return mobilePhone; }
		}

		public string PagerPhone {
			set { pagerPhone = value; }
			get { return pagerPhone; }
		}

		public string Region {
			set { region = value; }
			get { return region; }
		}

		#endregion
	}
}
