using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class ClientAddressType: eFundraisingStoreDataObject {

		private string clientAddressTypeId;
		private string description;


		public ClientAddressType() : this(null) { }
		public ClientAddressType(string clientAddressTypeId) : this(clientAddressTypeId, null) { }
		public ClientAddressType(string clientAddressTypeId, string description) {
			this.clientAddressTypeId = clientAddressTypeId;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ClientAddressType>\r\n" +
			"	<ClientAddressTypeId>" + System.Web.HttpUtility.HtmlEncode(clientAddressTypeId) + "</ClientAddressTypeId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</ClientAddressType>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "clientAddressTypeId") {
					SetXmlValue(ref clientAddressTypeId, node.InnerText);
				}
				if(node.Name.ToLower() == "description") {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ClientAddressType[] GetClientAddressTypes() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetClientAddressTypes();
		}
		/*
		public static ClientAddressType GetClientAddressTypeByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetClientAddressTypeByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertClientAddressType(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateClientAddressType(this);
		}*/
		#endregion

		#region Properties
		public string ClientAddressTypeId {
			set { clientAddressTypeId = value; }
			get { return clientAddressTypeId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
