using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class ClientSequence: eFundraisingStoreDataObject {

		private string clientSequenceCode;
		private string description;
		private short isActive;


		public ClientSequence() : this(null) { }
		public ClientSequence(string clientSequenceCode) : this(clientSequenceCode, null) { }
		public ClientSequence(string clientSequenceCode, string description) : this(clientSequenceCode, description, short.MinValue) { }
		public ClientSequence(string clientSequenceCode, string description, short isActive) {
			this.clientSequenceCode = clientSequenceCode;
			this.description = description;
			this.isActive = isActive;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ClientSequence>\r\n" +
			"	<ClientSequenceCode>" + System.Web.HttpUtility.HtmlEncode(clientSequenceCode) + "</ClientSequenceCode>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"	<IsActive>" + isActive + "</IsActive>\r\n" +
			"</ClientSequence>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "clientSequenceCode") {
					SetXmlValue(ref clientSequenceCode, node.InnerText);
				}
				if(node.Name.ToLower() == "description") {
					SetXmlValue(ref description, node.InnerText);
				}
				if(node.Name.ToLower() == "isActive") {
					SetXmlValue(ref isActive, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ClientSequence[] GetClientSequences() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetClientSequences();
		}

		/*
		public static ClientSequence GetClientSequenceByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetClientSequenceByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertClientSequence(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateClientSequence(this);
		}*/
		#endregion

		#region Properties
		public string ClientSequenceCode {
			set { clientSequenceCode = value; }
			get { return clientSequenceCode; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		public short IsActive {
			set { isActive = value; }
			get { return isActive; }
		}

		#endregion
	}
}
