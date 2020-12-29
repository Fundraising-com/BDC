using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class Unsubscribe: eFundraisingStoreDataObject {

		private int unsubscribeId;
		private string email;
		private short unsubscribed;
		private DateTime unsubscribedDate;


		public Unsubscribe() : this(int.MinValue) { }
		public Unsubscribe(int unsubscribeId) : this(unsubscribeId, null) { }
		public Unsubscribe(int unsubscribeId, string email) : this(unsubscribeId, email, short.MinValue) { }
		public Unsubscribe(int unsubscribeId, string email, short unsubscribed) : this(unsubscribeId, email, unsubscribed, DateTime.MinValue) { }
		public Unsubscribe(int unsubscribeId, string email, short unsubscribed, DateTime unsubscribedDate) {
			this.unsubscribeId = unsubscribeId;
			this.email = email;
			this.unsubscribed = unsubscribed;
			this.unsubscribedDate = unsubscribedDate;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Unsubscribe>\r\n" +
			"	<UnsubscribeId>" + unsubscribeId + "</UnsubscribeId>\r\n" +
			"	<Email>" + System.Web.HttpUtility.HtmlEncode(email) + "</Email>\r\n" +
			"	<Unsubscribed>" + unsubscribed + "</Unsubscribed>\r\n" +
			"	<UnsubscribedDate>" + unsubscribedDate + "</UnsubscribedDate>\r\n" +
			"</Unsubscribe>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "unsubscribeId") {
					SetXmlValue(ref unsubscribeId, node.InnerText);
				}
				if(node.Name.ToLower() == "email") {
					SetXmlValue(ref email, node.InnerText);
				}
				if(node.Name.ToLower() == "unsubscribed") {
					SetXmlValue(ref unsubscribed, node.InnerText);
				}
				if(node.Name.ToLower() == "unsubscribedDate") {
					SetXmlValue(ref unsubscribedDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Unsubscribe[] GetUnsubscribes() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetUnsubscribes();
		}

		public static Unsubscribe GetUnsubscribeByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetUnsubscribeByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertUnsubscribe(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateUnsubscribe(this);
		}
		#endregion

		#region Properties
		public int UnsubscribeId {
			set { unsubscribeId = value; }
			get { return unsubscribeId; }
		}

		public string Email {
			set { email = value; }
			get { return email; }
		}

		public short Unsubscribed {
			set { unsubscribed = value; }
			get { return unsubscribed; }
		}

		public DateTime UnsubscribedDate {
			set { unsubscribedDate = value; }
			get { return unsubscribedDate; }
		}

		#endregion
	}
}
