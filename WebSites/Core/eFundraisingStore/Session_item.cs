using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class SessionItem: eFundraisingStoreDataObject {

		private int sessionItemId;
		private int sessionId;
		private string name;
		private string value;


		public SessionItem() : this(int.MinValue) { }
		public SessionItem(int sessionItemId) : this(sessionItemId, int.MinValue) { }
		public SessionItem(int sessionItemId, int sessionId) : this(sessionItemId, sessionId, null) { }
		public SessionItem(int sessionItemId, int sessionId, string name) : this(sessionItemId, sessionId, name, null) { }
		public SessionItem(int sessionItemId, int sessionId, string name, string value) {
			this.sessionItemId = sessionItemId;
			this.sessionId = sessionId;
			this.name = name;
			this.value = value;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<SessionItem>\r\n" +
			"	<SessionItemId>" + sessionItemId + "</SessionItemId>\r\n" +
			"	<SessionId>" + sessionId + "</SessionId>\r\n" +
			"	<Name>" + System.Web.HttpUtility.HtmlEncode(name) + "</Name>\r\n" +
			"	<Value>" + System.Web.HttpUtility.HtmlEncode(value) + "</Value>\r\n" +
			"</SessionItem>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "sessionItemId") {
					SetXmlValue(ref sessionItemId, node.InnerText);
				}
				if(node.Name.ToLower() == "sessionId") {
					SetXmlValue(ref sessionId, node.InnerText);
				}
				if(node.Name.ToLower() == "name") {
					SetXmlValue(ref name, node.InnerText);
				}
				if(node.Name.ToLower() == "value") {
					SetXmlValue(ref value, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static SessionItem[] GetSessionItems() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetSessionItems();
		}

		public static SessionItem GetSessionItemByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetSessionItemByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertSessionItem(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateSessionItem(this);
		}
		#endregion

		#region Properties
		public int SessionItemId {
			set { sessionItemId = value; }
			get { return sessionItemId; }
		}

		public int SessionId {
			set { sessionId = value; }
			get { return sessionId; }
		}

		public string Name {
			set { name = value; }
			get { return name; }
		}

		public string Value {
			set { value = value; }
			get { return value; }
		}

		#endregion
	}
}
