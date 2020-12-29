using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class NewsletterSubscription: eFundraisingStoreDataObject {

		private int subscriptionId;
		private int partnerId;
		private string cultureCode;
		private string referrer;
		private string email;
		private string fullname;
		private short unsubscribed;
		private DateTime subscribeDate;
		private DateTime unsubscribeDate;


		public NewsletterSubscription() : this(int.MinValue) { }
		public NewsletterSubscription(int subscriptionId) : this(subscriptionId, int.MinValue) { }
		public NewsletterSubscription(int subscriptionId, int partnerId) : this(subscriptionId, partnerId, null) { }
		public NewsletterSubscription(int subscriptionId, int partnerId, string cultureCode) : this(subscriptionId, partnerId, cultureCode, null) { }
		public NewsletterSubscription(int subscriptionId, int partnerId, string cultureCode, string referrer) : this(subscriptionId, partnerId, cultureCode, referrer, null) { }
		public NewsletterSubscription(int subscriptionId, int partnerId, string cultureCode, string referrer, string email) : this(subscriptionId, partnerId, cultureCode, referrer, email, null) { }
		public NewsletterSubscription(int subscriptionId, int partnerId, string cultureCode, string referrer, string email, string fullname) : this(subscriptionId, partnerId, cultureCode, referrer, email, fullname, short.MinValue) { }
		public NewsletterSubscription(int subscriptionId, int partnerId, string cultureCode, string referrer, string email, string fullname, short unsubscribed) : this(subscriptionId, partnerId, cultureCode, referrer, email, fullname, unsubscribed, DateTime.MinValue) { }
		public NewsletterSubscription(int subscriptionId, int partnerId, string cultureCode, string referrer, string email, string fullname, short unsubscribed, DateTime subscribeDate) : this(subscriptionId, partnerId, cultureCode, referrer, email, fullname, unsubscribed, subscribeDate, DateTime.MinValue) { }
		public NewsletterSubscription(int subscriptionId, int partnerId, string cultureCode, string referrer, string email, string fullname, short unsubscribed, DateTime subscribeDate, DateTime unsubscribeDate) {
			this.subscriptionId = subscriptionId;
			this.partnerId = partnerId;
			this.cultureCode = cultureCode;
			this.referrer = referrer;
			this.email = email;
			this.fullname = fullname;
			this.unsubscribed = unsubscribed;
			this.subscribeDate = subscribeDate;
			this.unsubscribeDate = unsubscribeDate;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<NewsletterSubscription>\r\n" +
			"	<SubscriptionId>" + subscriptionId + "</SubscriptionId>\r\n" +
			"	<PartnerId>" + partnerId + "</PartnerId>\r\n" +
			"	<CultureCode>" + System.Web.HttpUtility.HtmlEncode(cultureCode) + "</CultureCode>\r\n" +
			"	<Referrer>" + System.Web.HttpUtility.HtmlEncode(referrer) + "</Referrer>\r\n" +
			"	<Email>" + System.Web.HttpUtility.HtmlEncode(email) + "</Email>\r\n" +
			"	<Fullname>" + System.Web.HttpUtility.HtmlEncode(fullname) + "</Fullname>\r\n" +
			"	<Unsubscribed>" + unsubscribed + "</Unsubscribed>\r\n" +
			"	<SubscribeDate>" + subscribeDate + "</SubscribeDate>\r\n" +
			"	<UnsubscribeDate>" + unsubscribeDate + "</UnsubscribeDate>\r\n" +
			"</NewsletterSubscription>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "subscriptionId") {
					SetXmlValue(ref subscriptionId, node.InnerText);
				}
				if(node.Name.ToLower() == "partnerId") {
					SetXmlValue(ref partnerId, node.InnerText);
				}
				if(node.Name.ToLower() == "cultureCode") {
					SetXmlValue(ref cultureCode, node.InnerText);
				}
				if(node.Name.ToLower() == "referrer") {
					SetXmlValue(ref referrer, node.InnerText);
				}
				if(node.Name.ToLower() == "email") {
					SetXmlValue(ref email, node.InnerText);
				}
				if(node.Name.ToLower() == "fullname") {
					SetXmlValue(ref fullname, node.InnerText);
				}
				if(node.Name.ToLower() == "unsubscribed") {
					SetXmlValue(ref unsubscribed, node.InnerText);
				}
				if(node.Name.ToLower() == "subscribeDate") {
					SetXmlValue(ref subscribeDate, node.InnerText);
				}
				if(node.Name.ToLower() == "unsubscribeDate") {
					SetXmlValue(ref unsubscribeDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static NewsletterSubscription[] GetNewsletterSubscriptions() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetNewsletterSubscriptions();
		}

		public static NewsletterSubscription GetNewsletterSubscriptionByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetNewsletterSubscriptionByID(id);
		}

		public static NewsletterSubscriptionCollection GetNewsletterSubscriptionsByEmailAndParnterId(string email, int partnerId) 
		{
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetNewsletterSubscriptionByEmailAndParnterId(email, partnerId);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertNewsletterSubscription(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateNewsletterSubscription(this);
		}
		#endregion

		#region Properties
		public int SubscriptionId {
			set { subscriptionId = value; }
			get { return subscriptionId; }
		}

		public int PartnerId {
			set { partnerId = value; }
			get { return partnerId; }
		}

		public string CultureCode {
			set { cultureCode = value; }
			get { return cultureCode; }
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

		public short Unsubscribed {
			set { unsubscribed = value; }
			get { return unsubscribed; }
		}

		public DateTime SubscribeDate {
			set { subscribeDate = value; }
			get { return subscribeDate; }
		}

		public DateTime UnsubscribeDate {
			set { unsubscribeDate = value; }
			get { return unsubscribeDate; }
		}

		#endregion
	}
}
