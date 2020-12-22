using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class EFOSupporter: EFundraisingCRMDataObject {

		private int supporterID;
		private string name;
		private int participantID;
		private string email;
		private int isEmailGood;
		private int isActive;
		private string comments;
		private int emailSent;
		private int isDeletable;
		private string relation;


		public EFOSupporter() : this(int.MinValue) { }
		public EFOSupporter(int supporterID) : this(supporterID, null) { }
		public EFOSupporter(int supporterID, string name) : this(supporterID, name, int.MinValue) { }
		public EFOSupporter(int supporterID, string name, int participantID) : this(supporterID, name, participantID, null) { }
		public EFOSupporter(int supporterID, string name, int participantID, string email) : this(supporterID, name, participantID, email, int.MinValue) { }
		public EFOSupporter(int supporterID, string name, int participantID, string email, int isEmailGood) : this(supporterID, name, participantID, email, isEmailGood, int.MinValue) { }
		public EFOSupporter(int supporterID, string name, int participantID, string email, int isEmailGood, int isActive) : this(supporterID, name, participantID, email, isEmailGood, isActive, null) { }
		public EFOSupporter(int supporterID, string name, int participantID, string email, int isEmailGood, int isActive, string comments) : this(supporterID, name, participantID, email, isEmailGood, isActive, comments, int.MinValue) { }
		public EFOSupporter(int supporterID, string name, int participantID, string email, int isEmailGood, int isActive, string comments, int emailSent) : this(supporterID, name, participantID, email, isEmailGood, isActive, comments, emailSent, int.MinValue) { }
		public EFOSupporter(int supporterID, string name, int participantID, string email, int isEmailGood, int isActive, string comments, int emailSent, int isDeletable) : this(supporterID, name, participantID, email, isEmailGood, isActive, comments, emailSent, isDeletable, null) { }
		public EFOSupporter(int supporterID, string name, int participantID, string email, int isEmailGood, int isActive, string comments, int emailSent, int isDeletable, string relation) {
			this.supporterID = supporterID;
			this.name = name;
			this.participantID = participantID;
			this.email = email;
			this.isEmailGood = isEmailGood;
			this.isActive = isActive;
			this.comments = comments;
			this.emailSent = emailSent;
			this.isDeletable = isDeletable;
			this.relation = relation;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<EFOSupporter>\r\n" +
			"	<SupporterID>" + supporterID + "</SupporterID>\r\n" +
			"	<Name>" + System.Web.HttpUtility.HtmlEncode(name) + "</Name>\r\n" +
			"	<ParticipantID>" + participantID + "</ParticipantID>\r\n" +
			"	<Email>" + System.Web.HttpUtility.HtmlEncode(email) + "</Email>\r\n" +
			"	<IsEmailGood>" + isEmailGood + "</IsEmailGood>\r\n" +
			"	<IsActive>" + isActive + "</IsActive>\r\n" +
			"	<Comments>" + System.Web.HttpUtility.HtmlEncode(comments) + "</Comments>\r\n" +
			"	<EmailSent>" + emailSent + "</EmailSent>\r\n" +
			"	<IsDeletable>" + isDeletable + "</IsDeletable>\r\n" +
			"	<Relation>" + System.Web.HttpUtility.HtmlEncode(relation) + "</Relation>\r\n" +
			"</EFOSupporter>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("supporterId")) {
					SetXmlValue(ref supporterID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("name")) {
					SetXmlValue(ref name, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("participantId")) {
					SetXmlValue(ref participantID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("email")) {
					SetXmlValue(ref email, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isEmailGood")) {
					SetXmlValue(ref isEmailGood, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isActive")) {
					SetXmlValue(ref isActive, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("comments")) {
					SetXmlValue(ref comments, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("emailSent")) {
					SetXmlValue(ref emailSent, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isDeletable")) {
					SetXmlValue(ref isDeletable, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("relation")) {
					SetXmlValue(ref relation, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static EFOSupporter[] GetEFOSupporters() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEFOSupporters();
		}

		public static EFOSupporter GetEFOSupporterByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEFOSupporterByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertEFOSupporter(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateEFOSupporter(this);
		}
		#endregion

		#region Properties
		public int SupporterID {
			set { supporterID = value; }
			get { return supporterID; }
		}

		public string Name {
			set { name = value; }
			get { return name; }
		}

		public int ParticipantID {
			set { participantID = value; }
			get { return participantID; }
		}

		public string Email {
			set { email = value; }
			get { return email; }
		}

		public int IsEmailGood {
			set { isEmailGood = value; }
			get { return isEmailGood; }
		}

		public int IsActive {
			set { isActive = value; }
			get { return isActive; }
		}

		public string Comments {
			set { comments = value; }
			get { return comments; }
		}

		public int EmailSent {
			set { emailSent = value; }
			get { return emailSent; }
		}

		public int IsDeletable {
			set { isDeletable = value; }
			get { return isDeletable; }
		}

		public string Relation {
			set { relation = value; }
			get { return relation; }
		}

		#endregion
	}
}
