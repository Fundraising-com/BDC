using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class OnlineUser: eFundraisingStoreDataObject {

		private int onlineUserId;
		private string clientSequenceCode;
		private int clientId;
		private string email;
		private string onlineUserPwd;
		private DateTime dateCreated;


		public OnlineUser() : this(int.MinValue) { }
		public OnlineUser(int onlineUserId) : this(onlineUserId, null) { }
		public OnlineUser(int onlineUserId, string clientSequenceCode) : this(onlineUserId, clientSequenceCode, int.MinValue) { }
		public OnlineUser(int onlineUserId, string clientSequenceCode, int clientId) : this(onlineUserId, clientSequenceCode, clientId, null) { }
		public OnlineUser(int onlineUserId, string clientSequenceCode, int clientId, string email) : this(onlineUserId, clientSequenceCode, clientId, email, null) { }
		public OnlineUser(int onlineUserId, string clientSequenceCode, int clientId, string email, string onlineUserPwd) : this(onlineUserId, clientSequenceCode, clientId, email, onlineUserPwd, DateTime.MinValue) { }
		public OnlineUser(int onlineUserId, string clientSequenceCode, int clientId, string email, string onlineUserPwd, DateTime dateCreated) {
			this.onlineUserId = onlineUserId;
			this.clientSequenceCode = clientSequenceCode;
			this.clientId = clientId;
			this.email = email;
			this.onlineUserPwd = onlineUserPwd;
			this.dateCreated = dateCreated;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<OnlineUser>\r\n" +
			"	<OnlineUserId>" + onlineUserId + "</OnlineUserId>\r\n" +
			"	<ClientSequenceCode>" + System.Web.HttpUtility.HtmlEncode(clientSequenceCode) + "</ClientSequenceCode>\r\n" +
			"	<ClientId>" + clientId + "</ClientId>\r\n" +
			"	<Email>" + System.Web.HttpUtility.HtmlEncode(email) + "</Email>\r\n" +
			"	<OnlineUserPwd>" + onlineUserPwd + "</OnlineUserPwd>\r\n" +
			"	<DateCreated>" + dateCreated + "</DateCreated>\r\n" +
			"</OnlineUser>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "onlineUserId") {
					SetXmlValue(ref onlineUserId, node.InnerText);
				}
				if(node.Name.ToLower() == "clientSequenceCode") {
					SetXmlValue(ref clientSequenceCode, node.InnerText);
				}
				if(node.Name.ToLower() == "clientId") {
					SetXmlValue(ref clientId, node.InnerText);
				}
				if(node.Name.ToLower() == "email") {
					SetXmlValue(ref email, node.InnerText);
				}
				if(node.Name.ToLower() == "onlineUserPwd") {
					SetXmlValue(ref onlineUserPwd, node.InnerText);
				}
				if(node.Name.ToLower() == "dateCreated") {
					SetXmlValue(ref dateCreated, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static OnlineUser[] GetOnlineUsers() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetOnlineUsers();
		}

		public static OnlineUser GetOnlineUserByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetOnlineUserByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertOnlineUser(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateOnlineUser(this);
		}
		#endregion

		#region Properties
		public int OnlineUserId {
			set { onlineUserId = value; }
			get { return onlineUserId; }
		}

		public string ClientSequenceCode {
			set { clientSequenceCode = value; }
			get { return clientSequenceCode; }
		}

		public int ClientId {
			set { clientId = value; }
			get { return clientId; }
		}

		public string Email {
			set { email = value; }
			get { return email; }
		}

		public string OnlineUserPwd {
			set { onlineUserPwd = value; }
			get { return onlineUserPwd; }
		}

		public DateTime DateCreated {
			set { dateCreated = value; }
			get { return dateCreated; }
		}

		#endregion
	}
}
