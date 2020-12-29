using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class EFOParticipant: EFundraisingCRMDataObject {

		private int participantID;
		private string name;
		private int campaignID;
		private string email;
		private string comments;
		private int emailSent;
		private int isActive;
		private int isDefault;
		private int isDeletable;


		public EFOParticipant() : this(int.MinValue) { }
		public EFOParticipant(int participantID) : this(participantID, null) { }
		public EFOParticipant(int participantID, string name) : this(participantID, name, int.MinValue) { }
		public EFOParticipant(int participantID, string name, int campaignID) : this(participantID, name, campaignID, null) { }
		public EFOParticipant(int participantID, string name, int campaignID, string email) : this(participantID, name, campaignID, email, null) { }
		public EFOParticipant(int participantID, string name, int campaignID, string email, string comments) : this(participantID, name, campaignID, email, comments, int.MinValue) { }
		public EFOParticipant(int participantID, string name, int campaignID, string email, string comments, int emailSent) : this(participantID, name, campaignID, email, comments, emailSent, int.MinValue) { }
		public EFOParticipant(int participantID, string name, int campaignID, string email, string comments, int emailSent, int isActive) : this(participantID, name, campaignID, email, comments, emailSent, isActive, int.MinValue) { }
		public EFOParticipant(int participantID, string name, int campaignID, string email, string comments, int emailSent, int isActive, int isDefault) : this(participantID, name, campaignID, email, comments, emailSent, isActive, isDefault, int.MinValue) { }
		public EFOParticipant(int participantID, string name, int campaignID, string email, string comments, int emailSent, int isActive, int isDefault, int isDeletable) {
			this.participantID = participantID;
			this.name = name;
			this.campaignID = campaignID;
			this.email = email;
			this.comments = comments;
			this.emailSent = emailSent;
			this.isActive = isActive;
			this.isDefault = isDefault;
			this.isDeletable = isDeletable;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<EFOParticipant>\r\n" +
			"	<ParticipantID>" + participantID + "</ParticipantID>\r\n" +
			"	<Name>" + System.Web.HttpUtility.HtmlEncode(name) + "</Name>\r\n" +
			"	<CampaignID>" + campaignID + "</CampaignID>\r\n" +
			"	<Email>" + System.Web.HttpUtility.HtmlEncode(email) + "</Email>\r\n" +
			"	<Comments>" + System.Web.HttpUtility.HtmlEncode(comments) + "</Comments>\r\n" +
			"	<EmailSent>" + emailSent + "</EmailSent>\r\n" +
			"	<IsActive>" + isActive + "</IsActive>\r\n" +
			"	<IsDefault>" + isDefault + "</IsDefault>\r\n" +
			"	<IsDeletable>" + isDeletable + "</IsDeletable>\r\n" +
			"</EFOParticipant>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("participantId")) {
					SetXmlValue(ref participantID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("name")) {
					SetXmlValue(ref name, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("campaignId")) {
					SetXmlValue(ref campaignID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("email")) {
					SetXmlValue(ref email, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("comments")) {
					SetXmlValue(ref comments, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("emailSent")) {
					SetXmlValue(ref emailSent, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isActive")) {
					SetXmlValue(ref isActive, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isDefault")) {
					SetXmlValue(ref isDefault, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isDeletable")) {
					SetXmlValue(ref isDeletable, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static EFOParticipant[] GetEFOParticipants() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEFOParticipants();
		}

		public static EFOParticipant GetEFOParticipantByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEFOParticipantByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertEFOParticipant(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateEFOParticipant(this);
		}
		#endregion

		#region Properties
		public int ParticipantID {
			set { participantID = value; }
			get { return participantID; }
		}

		public string Name {
			set { name = value; }
			get { return name; }
		}

		public int CampaignID {
			set { campaignID = value; }
			get { return campaignID; }
		}

		public string Email {
			set { email = value; }
			get { return email; }
		}

		public string Comments {
			set { comments = value; }
			get { return comments; }
		}

		public int EmailSent {
			set { emailSent = value; }
			get { return emailSent; }
		}

		public int IsActive {
			set { isActive = value; }
			get { return isActive; }
		}

		public int IsDefault {
			set { isDefault = value; }
			get { return isDefault; }
		}

		public int IsDeletable {
			set { isDeletable = value; }
			get { return isDeletable; }
		}

		#endregion
	}
}
