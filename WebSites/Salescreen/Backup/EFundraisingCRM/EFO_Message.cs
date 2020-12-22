using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class EFOMessage: EFundraisingCRMDataObject {

		private int messageID;
		private int participantID;
		private int isRead;
		private DateTime dateSent;
		private DateTime dateReceived;
		private string fromName;
		private string fromEmail;
		private string toName;
		private string toEmail;
		private string subject;
		private string body;
		private string contentType;


		public EFOMessage() : this(int.MinValue) { }
		public EFOMessage(int messageID) : this(messageID, int.MinValue) { }
		public EFOMessage(int messageID, int participantID) : this(messageID, participantID, int.MinValue) { }
		public EFOMessage(int messageID, int participantID, int isRead) : this(messageID, participantID, isRead, DateTime.MinValue) { }
		public EFOMessage(int messageID, int participantID, int isRead, DateTime dateSent) : this(messageID, participantID, isRead, dateSent, DateTime.MinValue) { }
		public EFOMessage(int messageID, int participantID, int isRead, DateTime dateSent, DateTime dateReceived) : this(messageID, participantID, isRead, dateSent, dateReceived, null) { }
		public EFOMessage(int messageID, int participantID, int isRead, DateTime dateSent, DateTime dateReceived, string fromName) : this(messageID, participantID, isRead, dateSent, dateReceived, fromName, null) { }
		public EFOMessage(int messageID, int participantID, int isRead, DateTime dateSent, DateTime dateReceived, string fromName, string fromEmail) : this(messageID, participantID, isRead, dateSent, dateReceived, fromName, fromEmail, null) { }
		public EFOMessage(int messageID, int participantID, int isRead, DateTime dateSent, DateTime dateReceived, string fromName, string fromEmail, string toName) : this(messageID, participantID, isRead, dateSent, dateReceived, fromName, fromEmail, toName, null) { }
		public EFOMessage(int messageID, int participantID, int isRead, DateTime dateSent, DateTime dateReceived, string fromName, string fromEmail, string toName, string toEmail) : this(messageID, participantID, isRead, dateSent, dateReceived, fromName, fromEmail, toName, toEmail, null) { }
		public EFOMessage(int messageID, int participantID, int isRead, DateTime dateSent, DateTime dateReceived, string fromName, string fromEmail, string toName, string toEmail, string subject) : this(messageID, participantID, isRead, dateSent, dateReceived, fromName, fromEmail, toName, toEmail, subject, null) { }
		public EFOMessage(int messageID, int participantID, int isRead, DateTime dateSent, DateTime dateReceived, string fromName, string fromEmail, string toName, string toEmail, string subject, string body) : this(messageID, participantID, isRead, dateSent, dateReceived, fromName, fromEmail, toName, toEmail, subject, body, null) { }
		public EFOMessage(int messageID, int participantID, int isRead, DateTime dateSent, DateTime dateReceived, string fromName, string fromEmail, string toName, string toEmail, string subject, string body, string contentType) {
			this.messageID = messageID;
			this.participantID = participantID;
			this.isRead = isRead;
			this.dateSent = dateSent;
			this.dateReceived = dateReceived;
			this.fromName = fromName;
			this.fromEmail = fromEmail;
			this.toName = toName;
			this.toEmail = toEmail;
			this.subject = subject;
			this.body = body;
			this.contentType = contentType;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<EFOMessage>\r\n" +
			"	<MessageID>" + messageID + "</MessageID>\r\n" +
			"	<ParticipantID>" + participantID + "</ParticipantID>\r\n" +
			"	<IsRead>" + isRead + "</IsRead>\r\n" +
			"	<DateSent>" + dateSent + "</DateSent>\r\n" +
			"	<DateReceived>" + dateReceived + "</DateReceived>\r\n" +
			"	<FromName>" + System.Web.HttpUtility.HtmlEncode(fromName) + "</FromName>\r\n" +
			"	<FromEmail>" + System.Web.HttpUtility.HtmlEncode(fromEmail) + "</FromEmail>\r\n" +
			"	<ToName>" + System.Web.HttpUtility.HtmlEncode(toName) + "</ToName>\r\n" +
			"	<ToEmail>" + System.Web.HttpUtility.HtmlEncode(toEmail) + "</ToEmail>\r\n" +
			"	<Subject>" + System.Web.HttpUtility.HtmlEncode(subject) + "</Subject>\r\n" +
			"	<Body>" + System.Web.HttpUtility.HtmlEncode(body) + "</Body>\r\n" +
			"	<ContentType>" + System.Web.HttpUtility.HtmlEncode(contentType) + "</ContentType>\r\n" +
			"</EFOMessage>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("messageId")) {
					SetXmlValue(ref messageID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("participantId")) {
					SetXmlValue(ref participantID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isRead")) {
					SetXmlValue(ref isRead, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("dateSent")) {
					SetXmlValue(ref dateSent, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("dateReceived")) {
					SetXmlValue(ref dateReceived, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("fromName")) {
					SetXmlValue(ref fromName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("fromEmail")) {
					SetXmlValue(ref fromEmail, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("toName")) {
					SetXmlValue(ref toName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("toEmail")) {
					SetXmlValue(ref toEmail, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("subject")) {
					SetXmlValue(ref subject, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("body")) {
					SetXmlValue(ref body, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("contentType")) {
					SetXmlValue(ref contentType, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static EFOMessage[] GetEFOMessages() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEFOMessages();
		}

		public static EFOMessage GetEFOMessageByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEFOMessageByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertEFOMessage(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateEFOMessage(this);
		}
		#endregion

		#region Properties
		public int MessageID {
			set { messageID = value; }
			get { return messageID; }
		}

		public int ParticipantID {
			set { participantID = value; }
			get { return participantID; }
		}

		public int IsRead {
			set { isRead = value; }
			get { return isRead; }
		}

		public DateTime DateSent {
			set { dateSent = value; }
			get { return dateSent; }
		}

		public DateTime DateReceived {
			set { dateReceived = value; }
			get { return dateReceived; }
		}

		public string FromName {
			set { fromName = value; }
			get { return fromName; }
		}

		public string FromEmail {
			set { fromEmail = value; }
			get { return fromEmail; }
		}

		public string ToName {
			set { toName = value; }
			get { return toName; }
		}

		public string ToEmail {
			set { toEmail = value; }
			get { return toEmail; }
		}

		public string Subject {
			set { subject = value; }
			get { return subject; }
		}

		public string Body {
			set { body = value; }
			get { return body; }
		}

		public string ContentType {
			set { contentType = value; }
			get { return contentType; }
		}

		#endregion
	}
}
