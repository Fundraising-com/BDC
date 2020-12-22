using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class SponsorConsultant: EFundraisingCRMDataObject {

		private int sponsorConsultantID;
		private string firstName;
		private string middleInitial;
		private string lastName;
		private string title;
		private string dayPhone;
		private string dayTimeCall;
		private string eveningPhone;
		private string evenigTimeCall;
		private string alternatePhone;
		private string fax;
		private string email;
		private string comment;
		private int isActive;
		private string ntLogin;
		private float commissionRate;


		public SponsorConsultant() : this(int.MinValue) { }
		public SponsorConsultant(int sponsorConsultantID) : this(sponsorConsultantID, null) { }
		public SponsorConsultant(int sponsorConsultantID, string firstName) : this(sponsorConsultantID, firstName, null) { }
		public SponsorConsultant(int sponsorConsultantID, string firstName, string middleInitial) : this(sponsorConsultantID, firstName, middleInitial, null) { }
		public SponsorConsultant(int sponsorConsultantID, string firstName, string middleInitial, string lastName) : this(sponsorConsultantID, firstName, middleInitial, lastName, null) { }
		public SponsorConsultant(int sponsorConsultantID, string firstName, string middleInitial, string lastName, string title) : this(sponsorConsultantID, firstName, middleInitial, lastName, title, null) { }
		public SponsorConsultant(int sponsorConsultantID, string firstName, string middleInitial, string lastName, string title, string dayPhone) : this(sponsorConsultantID, firstName, middleInitial, lastName, title, dayPhone, null) { }
		public SponsorConsultant(int sponsorConsultantID, string firstName, string middleInitial, string lastName, string title, string dayPhone, string dayTimeCall) : this(sponsorConsultantID, firstName, middleInitial, lastName, title, dayPhone, dayTimeCall, null) { }
		public SponsorConsultant(int sponsorConsultantID, string firstName, string middleInitial, string lastName, string title, string dayPhone, string dayTimeCall, string eveningPhone) : this(sponsorConsultantID, firstName, middleInitial, lastName, title, dayPhone, dayTimeCall, eveningPhone, null) { }
		public SponsorConsultant(int sponsorConsultantID, string firstName, string middleInitial, string lastName, string title, string dayPhone, string dayTimeCall, string eveningPhone, string evenigTimeCall) : this(sponsorConsultantID, firstName, middleInitial, lastName, title, dayPhone, dayTimeCall, eveningPhone, evenigTimeCall, null) { }
		public SponsorConsultant(int sponsorConsultantID, string firstName, string middleInitial, string lastName, string title, string dayPhone, string dayTimeCall, string eveningPhone, string evenigTimeCall, string alternatePhone) : this(sponsorConsultantID, firstName, middleInitial, lastName, title, dayPhone, dayTimeCall, eveningPhone, evenigTimeCall, alternatePhone, null) { }
		public SponsorConsultant(int sponsorConsultantID, string firstName, string middleInitial, string lastName, string title, string dayPhone, string dayTimeCall, string eveningPhone, string evenigTimeCall, string alternatePhone, string fax) : this(sponsorConsultantID, firstName, middleInitial, lastName, title, dayPhone, dayTimeCall, eveningPhone, evenigTimeCall, alternatePhone, fax, null) { }
		public SponsorConsultant(int sponsorConsultantID, string firstName, string middleInitial, string lastName, string title, string dayPhone, string dayTimeCall, string eveningPhone, string evenigTimeCall, string alternatePhone, string fax, string email) : this(sponsorConsultantID, firstName, middleInitial, lastName, title, dayPhone, dayTimeCall, eveningPhone, evenigTimeCall, alternatePhone, fax, email, null) { }
		public SponsorConsultant(int sponsorConsultantID, string firstName, string middleInitial, string lastName, string title, string dayPhone, string dayTimeCall, string eveningPhone, string evenigTimeCall, string alternatePhone, string fax, string email, string comment) : this(sponsorConsultantID, firstName, middleInitial, lastName, title, dayPhone, dayTimeCall, eveningPhone, evenigTimeCall, alternatePhone, fax, email, comment, int.MinValue) { }
		public SponsorConsultant(int sponsorConsultantID, string firstName, string middleInitial, string lastName, string title, string dayPhone, string dayTimeCall, string eveningPhone, string evenigTimeCall, string alternatePhone, string fax, string email, string comment, int isActive) : this(sponsorConsultantID, firstName, middleInitial, lastName, title, dayPhone, dayTimeCall, eveningPhone, evenigTimeCall, alternatePhone, fax, email, comment, isActive, null) { }
		public SponsorConsultant(int sponsorConsultantID, string firstName, string middleInitial, string lastName, string title, string dayPhone, string dayTimeCall, string eveningPhone, string evenigTimeCall, string alternatePhone, string fax, string email, string comment, int isActive, string ntLogin) : this(sponsorConsultantID, firstName, middleInitial, lastName, title, dayPhone, dayTimeCall, eveningPhone, evenigTimeCall, alternatePhone, fax, email, comment, isActive, ntLogin, float.MinValue) { }
		public SponsorConsultant(int sponsorConsultantID, string firstName, string middleInitial, string lastName, string title, string dayPhone, string dayTimeCall, string eveningPhone, string evenigTimeCall, string alternatePhone, string fax, string email, string comment, int isActive, string ntLogin, float commissionRate) {
			this.sponsorConsultantID = sponsorConsultantID;
			this.firstName = firstName;
			this.middleInitial = middleInitial;
			this.lastName = lastName;
			this.title = title;
			this.dayPhone = dayPhone;
			this.dayTimeCall = dayTimeCall;
			this.eveningPhone = eveningPhone;
			this.evenigTimeCall = evenigTimeCall;
			this.alternatePhone = alternatePhone;
			this.fax = fax;
			this.email = email;
			this.comment = comment;
			this.isActive = isActive;
			this.ntLogin = ntLogin;
			this.commissionRate = commissionRate;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<SponsorConsultant>\r\n" +
			"	<SponsorConsultantID>" + sponsorConsultantID + "</SponsorConsultantID>\r\n" +
			"	<FirstName>" + System.Web.HttpUtility.HtmlEncode(firstName) + "</FirstName>\r\n" +
			"	<middleInitial>" + System.Web.HttpUtility.HtmlEncode(middleInitial) + "</middleInitial>\r\n" +
			"	<LastName>" + System.Web.HttpUtility.HtmlEncode(lastName) + "</LastName>\r\n" +
			"	<Title>" + System.Web.HttpUtility.HtmlEncode(title) + "</Title>\r\n" +
			"	<DayPhone>" + System.Web.HttpUtility.HtmlEncode(dayPhone) + "</DayPhone>\r\n" +
			"	<DayTimeCall>" + System.Web.HttpUtility.HtmlEncode(dayTimeCall) + "</DayTimeCall>\r\n" +
			"	<EveningPhone>" + System.Web.HttpUtility.HtmlEncode(eveningPhone) + "</EveningPhone>\r\n" +
			"	<EvenigTimeCall>" + System.Web.HttpUtility.HtmlEncode(evenigTimeCall) + "</EvenigTimeCall>\r\n" +
			"	<AlternatePhone>" + System.Web.HttpUtility.HtmlEncode(alternatePhone) + "</AlternatePhone>\r\n" +
			"	<Fax>" + System.Web.HttpUtility.HtmlEncode(fax) + "</Fax>\r\n" +
			"	<Email>" + System.Web.HttpUtility.HtmlEncode(email) + "</Email>\r\n" +
			"	<Comment>" + System.Web.HttpUtility.HtmlEncode(comment) + "</Comment>\r\n" +
			"	<IsActive>" + isActive + "</IsActive>\r\n" +
			"	<NtLogin>" + System.Web.HttpUtility.HtmlEncode(ntLogin) + "</NtLogin>\r\n" +
			"	<CommissionRate>" + commissionRate + "</CommissionRate>\r\n" +
			"</SponsorConsultant>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("sponsorConsultantId")) {
					SetXmlValue(ref sponsorConsultantID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("firstName")) {
					SetXmlValue(ref firstName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("middleInitial")) {
					SetXmlValue(ref middleInitial, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("lastName")) {
					SetXmlValue(ref lastName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("title")) {
					SetXmlValue(ref title, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("dayPhone")) {
					SetXmlValue(ref dayPhone, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("dayTimeCall")) {
					SetXmlValue(ref dayTimeCall, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("eveningPhone")) {
					SetXmlValue(ref eveningPhone, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("evenigTimeCall")) {
					SetXmlValue(ref evenigTimeCall, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("alternatePhone")) {
					SetXmlValue(ref alternatePhone, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("fax")) {
					SetXmlValue(ref fax, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("email")) {
					SetXmlValue(ref email, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("comment")) {
					SetXmlValue(ref comment, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isActive")) {
					SetXmlValue(ref isActive, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("ntLogin")) {
					SetXmlValue(ref ntLogin, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("commissionRate")) {
					SetXmlValue(ref commissionRate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static SponsorConsultant[] GetSponsorConsultants() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSponsorConsultants();
		}

		public static SponsorConsultant GetSponsorConsultantByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSponsorConsultantByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertSponsorConsultant(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateSponsorConsultant(this);
		}
		#endregion

		#region Properties
		public int SponsorConsultantID {
			set { sponsorConsultantID = value; }
			get { return sponsorConsultantID; }
		}

		public string FirstName {
			set { firstName = value; }
			get { return firstName; }
		}

		public string MiddleInitial {
			set { middleInitial = value; }
			get { return middleInitial; }
		}

		public string LastName {
			set { lastName = value; }
			get { return lastName; }
		}

		public string Title {
			set { title = value; }
			get { return title; }
		}

		public string DayPhone {
			set { dayPhone = value; }
			get { return dayPhone; }
		}

		public string DayTimeCall {
			set { dayTimeCall = value; }
			get { return dayTimeCall; }
		}

		public string EveningPhone {
			set { eveningPhone = value; }
			get { return eveningPhone; }
		}

		public string EvenigTimeCall {
			set { evenigTimeCall = value; }
			get { return evenigTimeCall; }
		}

		public string AlternatePhone {
			set { alternatePhone = value; }
			get { return alternatePhone; }
		}

		public string Fax {
			set { fax = value; }
			get { return fax; }
		}

		public string Email {
			set { email = value; }
			get { return email; }
		}

		public string Comment {
			set { comment = value; }
			get { return comment; }
		}

		public int IsActive {
			set { isActive = value; }
			get { return isActive; }
		}

		public string NtLogin {
			set { ntLogin = value; }
			get { return ntLogin; }
		}

		public float CommissionRate {
			set { commissionRate = value; }
			get { return commissionRate; }
		}

		#endregion
	}
}
