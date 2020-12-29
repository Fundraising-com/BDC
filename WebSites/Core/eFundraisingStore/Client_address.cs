using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class ClientAddress: eFundraisingStoreDataObject {

		private int addressId;
		private string clientSequenceCode;
		private int clientId;
		private string addressType;
		private string streetAddress;
		private string stateCode;
		private string city;
		private string zipCode;
		private string countryCode;
		private string attentionOf;


		public ClientAddress() : this(int.MinValue) { }
		public ClientAddress(int addressId) : this(addressId, null) { }
		public ClientAddress(int addressId, string clientSequenceCode) : this(addressId, clientSequenceCode, int.MinValue) { }
		public ClientAddress(int addressId, string clientSequenceCode, int clientId) : this(addressId, clientSequenceCode, clientId, null) { }
		public ClientAddress(int addressId, string clientSequenceCode, int clientId, string addressType) : this(addressId, clientSequenceCode, clientId, addressType, null) { }
		public ClientAddress(int addressId, string clientSequenceCode, int clientId, string addressType, string streetAddress) : this(addressId, clientSequenceCode, clientId, addressType, streetAddress, null) { }
		public ClientAddress(int addressId, string clientSequenceCode, int clientId, string addressType, string streetAddress, string stateCode) : this(addressId, clientSequenceCode, clientId, addressType, streetAddress, stateCode, null) { }
		public ClientAddress(int addressId, string clientSequenceCode, int clientId, string addressType, string streetAddress, string stateCode, string city) : this(addressId, clientSequenceCode, clientId, addressType, streetAddress, stateCode, city, null) { }
		public ClientAddress(int addressId, string clientSequenceCode, int clientId, string addressType, string streetAddress, string stateCode, string city, string zipCode) : this(addressId, clientSequenceCode, clientId, addressType, streetAddress, stateCode, city, zipCode, null) { }
		public ClientAddress(int addressId, string clientSequenceCode, int clientId, string addressType, string streetAddress, string stateCode, string city, string zipCode, string countryCode) : this(addressId, clientSequenceCode, clientId, addressType, streetAddress, stateCode, city, zipCode, countryCode, null) { }
		public ClientAddress(int addressId, string clientSequenceCode, int clientId, string addressType, string streetAddress, string stateCode, string city, string zipCode, string countryCode, string attentionOf) {
			this.addressId = addressId;
			this.clientSequenceCode = clientSequenceCode;
			this.clientId = clientId;
			this.addressType = addressType;
			this.streetAddress = streetAddress;
			this.stateCode = stateCode;
			this.city = city;
			this.zipCode = zipCode;
			this.countryCode = countryCode;
			this.attentionOf = attentionOf;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ClientAddress>\r\n" +
			"	<AddressId>" + addressId + "</AddressId>\r\n" +
			"	<ClientSequenceCode>" + System.Web.HttpUtility.HtmlEncode(clientSequenceCode) + "</ClientSequenceCode>\r\n" +
			"	<ClientId>" + clientId + "</ClientId>\r\n" +
			"	<AddressType>" + System.Web.HttpUtility.HtmlEncode(addressType) + "</AddressType>\r\n" +
			"	<StreetAddress>" + System.Web.HttpUtility.HtmlEncode(streetAddress) + "</StreetAddress>\r\n" +
			"	<StateCode>" + System.Web.HttpUtility.HtmlEncode(stateCode) + "</StateCode>\r\n" +
			"	<City>" + System.Web.HttpUtility.HtmlEncode(city) + "</City>\r\n" +
			"	<ZipCode>" + System.Web.HttpUtility.HtmlEncode(zipCode) + "</ZipCode>\r\n" +
			"	<CountryCode>" + System.Web.HttpUtility.HtmlEncode(countryCode) + "</CountryCode>\r\n" +
			"	<AttentionOf>" + System.Web.HttpUtility.HtmlEncode(attentionOf) + "</AttentionOf>\r\n" +
			"</ClientAddress>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "addressId") {
					SetXmlValue(ref addressId, node.InnerText);
				}
				if(node.Name.ToLower() == "clientSequenceCode") {
					SetXmlValue(ref clientSequenceCode, node.InnerText);
				}
				if(node.Name.ToLower() == "clientId") {
					SetXmlValue(ref clientId, node.InnerText);
				}
				if(node.Name.ToLower() == "addressType") {
					SetXmlValue(ref addressType, node.InnerText);
				}
				if(node.Name.ToLower() == "streetAddress") {
					SetXmlValue(ref streetAddress, node.InnerText);
				}
				if(node.Name.ToLower() == "stateCode") {
					SetXmlValue(ref stateCode, node.InnerText);
				}
				if(node.Name.ToLower() == "city") {
					SetXmlValue(ref city, node.InnerText);
				}
				if(node.Name.ToLower() == "zipCode") {
					SetXmlValue(ref zipCode, node.InnerText);
				}
				if(node.Name.ToLower() == "countryCode") {
					SetXmlValue(ref countryCode, node.InnerText);
				}
				if(node.Name.ToLower() == "attentionOf") {
					SetXmlValue(ref attentionOf, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ClientAddress[] GetClientAddresss() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetClientAddresss();
		}

		public static ClientAddress GetClientAddressByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetClientAddressByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertClientAddress(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateClientAddress(this);
		}
		#endregion

		#region Properties
		public int AddressId {
			set { addressId = value; }
			get { return addressId; }
		}

		public string ClientSequenceCode {
			set { clientSequenceCode = value; }
			get { return clientSequenceCode; }
		}

		public int ClientId {
			set { clientId = value; }
			get { return clientId; }
		}

		public string AddressType {
			set { addressType = value; }
			get { return addressType; }
		}

		public string StreetAddress {
			set { streetAddress = value; }
			get { return streetAddress; }
		}

		public string StateCode {
			set { stateCode = value; }
			get { return stateCode; }
		}

		public string City {
			set { city = value; }
			get { return city; }
		}

		public string ZipCode {
			set { zipCode = value; }
			get { return zipCode; }
		}

		public string CountryCode {
			set { countryCode = value; }
			get { return countryCode; }
		}

		public string AttentionOf {
			set { attentionOf = value; }
			get { return attentionOf; }
		}

		#endregion
	}
}
