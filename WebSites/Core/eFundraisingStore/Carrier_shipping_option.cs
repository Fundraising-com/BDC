using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class CarrierShippingOption: eFundraisingStoreDataObject {

		private short shippingOptionId;
		private string description;


		public CarrierShippingOption() : this(short.MinValue) { }
		public CarrierShippingOption(short shippingOptionId) : this(shippingOptionId, null) { }
		public CarrierShippingOption(short shippingOptionId, string description) {
			this.shippingOptionId = shippingOptionId;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<CarrierShippingOption>\r\n" +
			"	<ShippingOptionId>" + shippingOptionId + "</ShippingOptionId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</CarrierShippingOption>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "shippingOptionId") {
					SetXmlValue(ref shippingOptionId, node.InnerText);
				}
				if(node.Name.ToLower() == "description") {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static CarrierShippingOption[] GetCarrierShippingOptions() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetCarrierShippingOptions();
		}

		public static CarrierShippingOption GetCarrierShippingOptionByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetCarrierShippingOptionByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertCarrierShippingOption(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateCarrierShippingOption(this);
		}
		#endregion

		#region Properties
		public short ShippingOptionId {
			set { shippingOptionId = value; }
			get { return shippingOptionId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
