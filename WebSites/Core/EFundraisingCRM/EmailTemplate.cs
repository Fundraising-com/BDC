using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public enum EmailTemplateStatus {
		Ok,
		Error
	}

	public enum EmailTemplateSort {
		EmailTemplateID,
		Description,
		Subject,
		Active
	}

	public class EmailTemplate: EFundraisingCRMDataObject{

		private int emailTemplateId;
		private string description;
		private string subject;
		private string message;
		private int channel;
		private string configuration;
		private int active;

		private EmailTemplateSort sort = EmailTemplateSort.EmailTemplateID;
		private bool sortAssending = true;

		public EmailTemplate() : this(int.MinValue) { }
		public EmailTemplate(int emailTemplateId) : this(emailTemplateId, null) { }
		public EmailTemplate(int emailTemplateId, string description) : this(emailTemplateId, description, null) { }
		public EmailTemplate(int emailTemplateId, string description, string subject) : this(emailTemplateId, description, subject, null) { }
		public EmailTemplate(int emailTemplateId, string description, string subject, string message) : this(emailTemplateId, description, subject, message, int.MinValue) { }
		public EmailTemplate(int emailTemplateId, string description, string subject, string message, int channel) : this(emailTemplateId, description, subject, message, channel, null) { }
		public EmailTemplate(int emailTemplateId, string description, string subject, string message, int channel, string configuration) : this(emailTemplateId, description, subject, message, channel, configuration, int.MinValue) { }
		public EmailTemplate(int emailTemplateId, string description, string subject, string message, int channel, string configuration, int active) {
			this.emailTemplateId = emailTemplateId;
			this.description = description;
			this.subject = subject;
			this.message = message;
			this.channel = channel;
			this.configuration = configuration;
			this.active = active;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<EmailTemplate>\r\n" +
				"	<EmailTemplateId>" + emailTemplateId + "</EmailTemplateId>\r\n" +
				"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
				"	<Subject>" + System.Web.HttpUtility.HtmlEncode(subject) + "</Subject>\r\n" +
				"	<Message>" + System.Web.HttpUtility.HtmlEncode(message) + "</Message>\r\n" +
				"	<Channel>" + channel + "</Channel>\r\n" +
				"	<Configuration>" + System.Web.HttpUtility.HtmlEncode(configuration) + "</Configuration>\r\n" +
				"	<Active>" + active + "</Active>\r\n" +
				"</EmailTemplate>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "emailTemplateId") {
					SetXmlValue(ref emailTemplateId, node.InnerText);
				}
				if(node.Name.ToLower() == "description") {
					SetXmlValue(ref description, node.InnerText);
				}
				if(node.Name.ToLower() == "subject") {
					SetXmlValue(ref subject, node.InnerText);
				}
				if(node.Name.ToLower() == "message") {
					SetXmlValue(ref message, node.InnerText);
				}
				if(node.Name.ToLower() == "channel") {
					SetXmlValue(ref channel, node.InnerText);
				}
				if(node.Name.ToLower() == "configuration") {
					SetXmlValue(ref configuration, node.InnerText);
				}
				if(node.Name.ToLower() == "active") {
					SetXmlValue(ref active, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static EmailTemplate[] GetEmailTemplates() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEmailTemplates();
		}

		public static EmailTemplate GetEmailTemplateByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEmailTemplateByID(id);
		}

		public EmailTemplateStatus Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			int status = dbo.InsertEmailTemplate(this);
			switch(status) {
				case 1:
					return EmailTemplateStatus.Ok;
				default:
					return EmailTemplateStatus.Error;
			}
		}

		public EmailTemplateStatus Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			int status = dbo.UpdateEmailTemplate(this);
			switch(status) {
				case 1:
					return EmailTemplateStatus.Ok;
				default:
					return EmailTemplateStatus.Error;
			}
		}

		#endregion

		#region Properties
		public int EmailTemplateId {
			set { emailTemplateId = value; }
			get { return emailTemplateId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		public string Subject {
			set { subject = value; }
			get { return subject; }
		}

		public string Message {
			set { message = value; }
			get { return message; }
		}

		public int Channel {
			set { channel = value; }
			get { return channel; }
		}

		public string Configuration {
			set { configuration = value; }
			get { return configuration; }
		}

		public int Active {
			set { active = value; }
			get { return active; }
		}

		public EmailTemplateSort Sort {
			get { return sort; }
			set { sort = value; }
		}

		public bool SortAssending {
			get { return sortAssending; }
			set { sortAssending = value; }
		}

		#endregion

		#region IComparable Members

		public override int CompareTo(object obj) {
			EmailTemplate emailTemplate1 = this;
			EmailTemplate emailTemplate2 = (EmailTemplate)obj;
			if(!sortAssending) {
				EmailTemplate tmp = emailTemplate2;
				emailTemplate2 = this;
				emailTemplate1 = tmp;
			}
			switch(sort) {
				case EmailTemplateSort.EmailTemplateID:
					return emailTemplate1.emailTemplateId.CompareTo(emailTemplate2.emailTemplateId);
				case EmailTemplateSort.Description:
					return emailTemplate1.Description.CompareTo(emailTemplate2.Description);
				case EmailTemplateSort.Active:
					return emailTemplate1.Active.CompareTo(emailTemplate2.Active);
				case EmailTemplateSort.Subject:
					return emailTemplate1.Subject.CompareTo(emailTemplate2.Subject);
			}
			return 0;
		}

		#endregion
	}
}
