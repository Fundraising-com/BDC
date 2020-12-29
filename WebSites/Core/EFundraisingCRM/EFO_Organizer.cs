using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class EFOOrganizer: EFundraisingCRMDataObject {

		private int organizerID;
		private string name;
		private string userName;
		private string password;
		private string title;
		private string email;
		private string bestTimeToCall;
		private string eveningPhone;
		private string dayPhone;
		private string faxNumber;
		private DateTime entryDate;
		private string comments;
		private int organizationID;
		private int schoolID;


		public EFOOrganizer() : this(int.MinValue) { }
		public EFOOrganizer(int organizerID) : this(organizerID, null) { }
		public EFOOrganizer(int organizerID, string name) : this(organizerID, name, null) { }
		public EFOOrganizer(int organizerID, string name, string userName) : this(organizerID, name, userName, null) { }
		public EFOOrganizer(int organizerID, string name, string userName, string password) : this(organizerID, name, userName, password, null) { }
		public EFOOrganizer(int organizerID, string name, string userName, string password, string title) : this(organizerID, name, userName, password, title, null) { }
		public EFOOrganizer(int organizerID, string name, string userName, string password, string title, string email) : this(organizerID, name, userName, password, title, email, null) { }
		public EFOOrganizer(int organizerID, string name, string userName, string password, string title, string email, string bestTimeToCall) : this(organizerID, name, userName, password, title, email, bestTimeToCall, null) { }
		public EFOOrganizer(int organizerID, string name, string userName, string password, string title, string email, string bestTimeToCall, string eveningPhone) : this(organizerID, name, userName, password, title, email, bestTimeToCall, eveningPhone, null) { }
		public EFOOrganizer(int organizerID, string name, string userName, string password, string title, string email, string bestTimeToCall, string eveningPhone, string dayPhone) : this(organizerID, name, userName, password, title, email, bestTimeToCall, eveningPhone, dayPhone, null) { }
		public EFOOrganizer(int organizerID, string name, string userName, string password, string title, string email, string bestTimeToCall, string eveningPhone, string dayPhone, string faxNumber) : this(organizerID, name, userName, password, title, email, bestTimeToCall, eveningPhone, dayPhone, faxNumber, DateTime.MinValue) { }
		public EFOOrganizer(int organizerID, string name, string userName, string password, string title, string email, string bestTimeToCall, string eveningPhone, string dayPhone, string faxNumber, DateTime entryDate) : this(organizerID, name, userName, password, title, email, bestTimeToCall, eveningPhone, dayPhone, faxNumber, entryDate, null) { }
		public EFOOrganizer(int organizerID, string name, string userName, string password, string title, string email, string bestTimeToCall, string eveningPhone, string dayPhone, string faxNumber, DateTime entryDate, string comments) : this(organizerID, name, userName, password, title, email, bestTimeToCall, eveningPhone, dayPhone, faxNumber, entryDate, comments, int.MinValue) { }
		public EFOOrganizer(int organizerID, string name, string userName, string password, string title, string email, string bestTimeToCall, string eveningPhone, string dayPhone, string faxNumber, DateTime entryDate, string comments, int organizationID) : this(organizerID, name, userName, password, title, email, bestTimeToCall, eveningPhone, dayPhone, faxNumber, entryDate, comments, organizationID, int.MinValue) { }
		public EFOOrganizer(int organizerID, string name, string userName, string password, string title, string email, string bestTimeToCall, string eveningPhone, string dayPhone, string faxNumber, DateTime entryDate, string comments, int organizationID, int schoolID) {
			this.organizerID = organizerID;
			this.name = name;
			this.userName = userName;
			this.password = password;
			this.title = title;
			this.email = email;
			this.bestTimeToCall = bestTimeToCall;
			this.eveningPhone = eveningPhone;
			this.dayPhone = dayPhone;
			this.faxNumber = faxNumber;
			this.entryDate = entryDate;
			this.comments = comments;
			this.organizationID = organizationID;
			this.schoolID = schoolID;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<EFOOrganizer>\r\n" +
			"	<OrganizerID>" + organizerID + "</OrganizerID>\r\n" +
			"	<Name>" + System.Web.HttpUtility.HtmlEncode(name) + "</Name>\r\n" +
			"	<UserName>" + System.Web.HttpUtility.HtmlEncode(userName) + "</UserName>\r\n" +
			"	<Password>" + System.Web.HttpUtility.HtmlEncode(password) + "</Password>\r\n" +
			"	<Title>" + System.Web.HttpUtility.HtmlEncode(title) + "</Title>\r\n" +
			"	<Email>" + System.Web.HttpUtility.HtmlEncode(email) + "</Email>\r\n" +
			"	<BestTimeToCall>" + System.Web.HttpUtility.HtmlEncode(bestTimeToCall) + "</BestTimeToCall>\r\n" +
			"	<EveningPhone>" + System.Web.HttpUtility.HtmlEncode(eveningPhone) + "</EveningPhone>\r\n" +
			"	<DayPhone>" + System.Web.HttpUtility.HtmlEncode(dayPhone) + "</DayPhone>\r\n" +
			"	<FaxNumber>" + System.Web.HttpUtility.HtmlEncode(faxNumber) + "</FaxNumber>\r\n" +
			"	<EntryDate>" + entryDate + "</EntryDate>\r\n" +
			"	<Comments>" + System.Web.HttpUtility.HtmlEncode(comments) + "</Comments>\r\n" +
			"	<OrganizationID>" + organizationID + "</OrganizationID>\r\n" +
			"	<SchoolID>" + schoolID + "</SchoolID>\r\n" +
			"</EFOOrganizer>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("organizerId")) {
					SetXmlValue(ref organizerID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("name")) {
					SetXmlValue(ref name, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("userName")) {
					SetXmlValue(ref userName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("password")) {
					SetXmlValue(ref password, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("title")) {
					SetXmlValue(ref title, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("email")) {
					SetXmlValue(ref email, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("bestTimeToCall")) {
					SetXmlValue(ref bestTimeToCall, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("eveningPhone")) {
					SetXmlValue(ref eveningPhone, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("dayPhone")) {
					SetXmlValue(ref dayPhone, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("faxNumber")) {
					SetXmlValue(ref faxNumber, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("entryDate")) {
					SetXmlValue(ref entryDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("comments")) {
					SetXmlValue(ref comments, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("organizationId")) {
					SetXmlValue(ref organizationID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("schoolId")) {
					SetXmlValue(ref schoolID, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static EFOOrganizer[] GetEFOOrganizers() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEFOOrganizers();
		}

		public static EFOOrganizer GetEFOOrganizerByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEFOOrganizerByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertEFOOrganizer(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateEFOOrganizer(this);
		}
		#endregion

		#region Properties
		public int OrganizerID {
			set { organizerID = value; }
			get { return organizerID; }
		}

		public string Name {
			set { name = value; }
			get { return name; }
		}

		public string UserName {
			set { userName = value; }
			get { return userName; }
		}

		public string Password {
			set { password = value; }
			get { return password; }
		}

		public string Title {
			set { title = value; }
			get { return title; }
		}

		public string Email {
			set { email = value; }
			get { return email; }
		}

		public string BestTimeToCall {
			set { bestTimeToCall = value; }
			get { return bestTimeToCall; }
		}

		public string EveningPhone {
			set { eveningPhone = value; }
			get { return eveningPhone; }
		}

		public string DayPhone {
			set { dayPhone = value; }
			get { return dayPhone; }
		}

		public string FaxNumber {
			set { faxNumber = value; }
			get { return faxNumber; }
		}

		public DateTime EntryDate {
			set { entryDate = value; }
			get { return entryDate; }
		}

		public string Comments {
			set { comments = value; }
			get { return comments; }
		}

		public int OrganizationID {
			set { organizationID = value; }
			get { return organizationID; }
		}

		public int SchoolID {
			set { schoolID = value; }
			get { return schoolID; }
		}

		#endregion
	}
}
