using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class ClientAddressType: EFundraisingCRMDataObject {

		private string addressType;
		private string addressTypeDesc;


		public ClientAddressType() : this(null) { }
		public ClientAddressType(string addressType) : this(addressType, null) { }
		public ClientAddressType(string addressType, string addressTypeDesc) {
			this.addressType = addressType;
			this.addressTypeDesc = addressTypeDesc;
		}

		#region Static Data
		
		public static ClientAddressType BillingAddress
		{
			get { return new ClientAddressType("bt", "billing address"); }
		}
		
		public static ClientAddressType ShippingAddress
		{
			get { return new ClientAddressType("st", "shipping address"); }
		}
		
		#endregion

		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ClientAddressType>\r\n" +
			"	<AddressType>" + System.Web.HttpUtility.HtmlEncode(addressType) + "</AddressType>\r\n" +
			"	<AddressTypeDesc>" + System.Web.HttpUtility.HtmlEncode(addressTypeDesc) + "</AddressTypeDesc>\r\n" +
			"</ClientAddressType>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("addressType")) {
					SetXmlValue(ref addressType, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("addressTypeDesc")) {
					SetXmlValue(ref addressTypeDesc, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ClientAddressType[] GetClientAddressTypes() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetClientAddressTypes();
		}

		/*
		public static ClientAddressType GetClientAddressTypeByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetClientAddressTypeByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertClientAddressType(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateClientAddressType(this);
		}*/
		#endregion

		#region Properties
		public string AddressType {
			set { addressType = value; }
			get { return addressType; }
		}

		public string AddressTypeDesc {
			set { addressTypeDesc = value; }
			get { return addressTypeDesc; }
		}

		#endregion
	}
}
