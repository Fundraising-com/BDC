using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class Carrier: eFundraisingStoreDataObject {

		private short carrierId;
		private string description;


		public Carrier() : this(short.MinValue) { }
		public Carrier(short carrierId) : this(carrierId, null) { }
		public Carrier(short carrierId, string description) {
			this.carrierId = carrierId;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Carrier>\r\n" +
			"	<CarrierId>" + carrierId + "</CarrierId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</Carrier>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "carrierId") {
					SetXmlValue(ref carrierId, node.InnerText);
				}
				if(node.Name.ToLower() == "description") {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Carrier[] GetCarriers() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetCarriers();
		}

		public static Carrier GetCarrierByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetCarrierByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertCarrier(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateCarrier(this);
		}
		#endregion

		#region Properties
		public short CarrierId {
			set { carrierId = value; }
			get { return carrierId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
