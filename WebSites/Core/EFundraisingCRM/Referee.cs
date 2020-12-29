using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class Referee: EFundraisingCRMDataObject {

		private int refereeId;
		private int leadId;
		private DateTime entryDate;
		private string firstName;
		private string lastName;
		private string email;
		private string phoneNumber;
		private int isEntered;


		public Referee() : this(int.MinValue) { }
		public Referee(int refereeId) : this(refereeId, int.MinValue) { }
		public Referee(int refereeId, int leadId) : this(refereeId, leadId, DateTime.MinValue) { }
		public Referee(int refereeId, int leadId, DateTime entryDate) : this(refereeId, leadId, entryDate, null) { }
		public Referee(int refereeId, int leadId, DateTime entryDate, string firstName) : this(refereeId, leadId, entryDate, firstName, null) { }
		public Referee(int refereeId, int leadId, DateTime entryDate, string firstName, string lastName) : this(refereeId, leadId, entryDate, firstName, lastName, null) { }
		public Referee(int refereeId, int leadId, DateTime entryDate, string firstName, string lastName, string email) : this(refereeId, leadId, entryDate, firstName, lastName, email, null) { }
		public Referee(int refereeId, int leadId, DateTime entryDate, string firstName, string lastName, string email, string phoneNumber) : this(refereeId, leadId, entryDate, firstName, lastName, email, phoneNumber, int.MinValue) { }
		public Referee(int refereeId, int leadId, DateTime entryDate, string firstName, string lastName, string email, string phoneNumber, int isEntered) {
			this.refereeId = refereeId;
			this.leadId = leadId;
			this.entryDate = entryDate;
			this.firstName = firstName;
			this.lastName = lastName;
			this.email = email;
			this.phoneNumber = phoneNumber;
			this.isEntered = isEntered;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Referee>\r\n" +
			"	<RefereeId>" + refereeId + "</RefereeId>\r\n" +
			"	<LeadId>" + leadId + "</LeadId>\r\n" +
			"	<EntryDate>" + entryDate + "</EntryDate>\r\n" +
			"	<FirstName>" + System.Web.HttpUtility.HtmlEncode(firstName) + "</FirstName>\r\n" +
			"	<LastName>" + System.Web.HttpUtility.HtmlEncode(lastName) + "</LastName>\r\n" +
			"	<Email>" + System.Web.HttpUtility.HtmlEncode(email) + "</Email>\r\n" +
			"	<PhoneNumber>" + System.Web.HttpUtility.HtmlEncode(phoneNumber) + "</PhoneNumber>\r\n" +
			"	<IsEntered>" + isEntered + "</IsEntered>\r\n" +
			"</Referee>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("refereeId")) {
					SetXmlValue(ref refereeId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("leadId")) {
					SetXmlValue(ref leadId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("entryDate")) {
					SetXmlValue(ref entryDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("firstName")) {
					SetXmlValue(ref firstName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("lastName")) {
					SetXmlValue(ref lastName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("email")) {
					SetXmlValue(ref email, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("phoneNumber")) {
					SetXmlValue(ref phoneNumber, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isEntered")) {
					SetXmlValue(ref isEntered, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Referee[] GetReferees() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetReferees();
		}

		public static Referee GetRefereeByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetRefereeByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertReferee(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateReferee(this);
		}
		#endregion

		#region Properties
		public int RefereeId {
			set { refereeId = value; }
			get { return refereeId; }
		}

		public int LeadId {
			set { leadId = value; }
			get { return leadId; }
		}

		public DateTime EntryDate {
			set { entryDate = value; }
			get { return entryDate; }
		}

		public string FirstName {
			set { firstName = value; }
			get { return firstName; }
		}

		public string LastName {
			set { lastName = value; }
			get { return lastName; }
		}

		public string Email {
			set { email = value; }
			get { return email; }
		}

		public string PhoneNumber {
			set { phoneNumber = value; }
			get { return phoneNumber; }
		}

		public int IsEntered {
			set { isEntered = value; }
			get { return isEntered; }
		}

		#endregion
	}
}
