using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class Newsletter: EFundraisingCRMDataObject {

		private int newsletterID;
		private string referrer;
		private string email;
		private string fullname;
		private int unsubscribed;


		public Newsletter() : this(int.MinValue) { }
		public Newsletter(int newsletterID) : this(newsletterID, null) { }
		public Newsletter(int newsletterID, string referrer) : this(newsletterID, referrer, null) { }
		public Newsletter(int newsletterID, string referrer, string email) : this(newsletterID, referrer, email, null) { }
		public Newsletter(int newsletterID, string referrer, string email, string fullname) : this(newsletterID, referrer, email, fullname, int.MinValue) { }
		public Newsletter(int newsletterID, string referrer, string email, string fullname, int unsubscribed) {
			this.newsletterID = newsletterID;
			this.referrer = referrer;
			this.email = email;
			this.fullname = fullname;
			this.unsubscribed = unsubscribed;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Newsletter>\r\n" +
			"	<NewsletterID>" + newsletterID + "</NewsletterID>\r\n" +
			"	<Referrer>" + System.Web.HttpUtility.HtmlEncode(referrer) + "</Referrer>\r\n" +
			"	<Email>" + System.Web.HttpUtility.HtmlEncode(email) + "</Email>\r\n" +
			"	<Fullname>" + System.Web.HttpUtility.HtmlEncode(fullname) + "</Fullname>\r\n" +
			"	<Unsubscribed>" + unsubscribed + "</Unsubscribed>\r\n" +
			"</Newsletter>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("newsletterId")) {
					SetXmlValue(ref newsletterID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("referrer")) {
					SetXmlValue(ref referrer, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("email")) {
					SetXmlValue(ref email, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("fullname")) {
					SetXmlValue(ref fullname, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("unsubscribed")) {
					SetXmlValue(ref unsubscribed, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Newsletter[] GetNewsletters() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetNewsletters();
		}

		public static Newsletter GetNewsletterByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetNewsletterByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertNewsletter(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateNewsletter(this);
		}
		#endregion

		#region Properties
		public int NewsletterID {
			set { newsletterID = value; }
			get { return newsletterID; }
		}

		public string Referrer {
			set { referrer = value; }
			get { return referrer; }
		}

		public string Email {
			set { email = value; }
			get { return email; }
		}

		public string Fullname {
			set { fullname = value; }
			get { return fullname; }
		}

		public int Unsubscribed {
			set { unsubscribed = value; }
			get { return unsubscribed; }
		}

		#endregion
	}
}
