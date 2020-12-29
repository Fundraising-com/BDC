using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public enum ClientAddressStatus
	{
		Ok,
		Error
	}
	
	public class ClientAddress: EFundraisingCRMDataObject {

		private int addressId;
		private string clientSequenceCode;
		private int clientId;
		private string addressType;
		private string streetAddress;
		private string stateCode;
		private string countryCode;
		private string city;
		private string zipCode;
		private string attentionOf;
		private string matchingCode;
		private int addressZoneId;
		private string phone1;
		private string phone2;
		private string location;
        private bool pickUp;
        private int warehouseId;


		public ClientAddress() : this(int.MinValue) { }
		public ClientAddress(int addressId) : this(addressId, null) { }
		public ClientAddress(int addressId, string clientSequenceCode) : this(addressId, clientSequenceCode, int.MinValue) { }
		public ClientAddress(int addressId, string clientSequenceCode, int clientId) : this(addressId, clientSequenceCode, clientId, null) { }
		public ClientAddress(int addressId, string clientSequenceCode, int clientId, string addressType) : this(addressId, clientSequenceCode, clientId, addressType, null) { }
		public ClientAddress(int addressId, string clientSequenceCode, int clientId, string addressType, string streetAddress) : this(addressId, clientSequenceCode, clientId, addressType, streetAddress, null) { }
		public ClientAddress(int addressId, string clientSequenceCode, int clientId, string addressType, string streetAddress, string stateCode) : this(addressId, clientSequenceCode, clientId, addressType, streetAddress, stateCode, null) { }
		public ClientAddress(int addressId, string clientSequenceCode, int clientId, string addressType, string streetAddress, string stateCode, string countryCode) : this(addressId, clientSequenceCode, clientId, addressType, streetAddress, stateCode, countryCode, null) { }
		public ClientAddress(int addressId, string clientSequenceCode, int clientId, string addressType, string streetAddress, string stateCode, string countryCode, string city) : this(addressId, clientSequenceCode, clientId, addressType, streetAddress, stateCode, countryCode, city, null) { }
		public ClientAddress(int addressId, string clientSequenceCode, int clientId, string addressType, string streetAddress, string stateCode, string countryCode, string city, string zipCode) : this(addressId, clientSequenceCode, clientId, addressType, streetAddress, stateCode, countryCode, city, zipCode, null) { }
		public ClientAddress(int addressId, string clientSequenceCode, int clientId, string addressType, string streetAddress, string stateCode, string countryCode, string city, string zipCode, string attentionOf) : this(addressId, clientSequenceCode, clientId, addressType, streetAddress, stateCode, countryCode, city, zipCode, attentionOf, null) { }
		public ClientAddress(int addressId, string clientSequenceCode, int clientId, string addressType, string streetAddress, string stateCode, string countryCode, string city, string zipCode, string attentionOf, string matchingCode) : this(addressId, clientSequenceCode, clientId, addressType, streetAddress, stateCode, countryCode, city, zipCode, attentionOf, matchingCode, int.MinValue) { }
		public ClientAddress(int addressId, string clientSequenceCode, int clientId, string addressType, string streetAddress, string stateCode, string countryCode, string city, string zipCode, string attentionOf, string matchingCode, int addressZoneId) : this(addressId, clientSequenceCode, clientId, addressType, streetAddress, stateCode, countryCode, city, zipCode, attentionOf, matchingCode, addressZoneId, null) { }
		public ClientAddress(int addressId, string clientSequenceCode, int clientId, string addressType, string streetAddress, string stateCode, string countryCode, string city, string zipCode, string attentionOf, string matchingCode, int addressZoneId, string phone1) : this(addressId, clientSequenceCode, clientId, addressType, streetAddress, stateCode, countryCode, city, zipCode, attentionOf, matchingCode, addressId, phone1, null) { }
		public ClientAddress(int addressId, string clientSequenceCode, int clientId, string addressType, string streetAddress, string stateCode, string countryCode, string city, string zipCode, string attentionOf, string matchingCode, int addressZoneId, string phone1, string phone2) {
			this.addressId = addressId;
			this.clientSequenceCode = clientSequenceCode;
			this.clientId = clientId;
			this.addressType = addressType;
			this.streetAddress = streetAddress;
			this.stateCode = stateCode;
			this.countryCode = countryCode;
			this.city = city;
			this.zipCode = zipCode;
			this.attentionOf = attentionOf;
			this.matchingCode = matchingCode;
			this.addressZoneId = (addressZoneId == int.MinValue? 3: addressZoneId);
			this.phone1 = phone1;
			this.phone2 = phone2;
		}

		#region Methods
		
		public string ToHumanReadableString ()
		{
			string clientAddressString = "";
			
			clientAddressString += "Address Type : \t\t" + this.AddressType + "\r\n";
			clientAddressString += "Street Address : \t\t" + this.StreetAddress + "\r\n";
			clientAddressString += "State Code : \t\t" + this.StateCode + "\r\n";
			clientAddressString += "Country Code : \t\t" + this.CountryCode + "\r\n";
			clientAddressString += "City : \t\t" + this.City + "\r\n";
			clientAddressString += "Zip Code : \t\t" + this.ZipCode + "\r\n";
			if (this.AttentionOf != null)
				clientAddressString += "Attention Of : \t\t" + this.AttentionOf + "\r\n";
			if (this.MatchingCode != null)
				clientAddressString += "Time to Call : \t\t" + this.MatchingCode + "\r\n";
			
			return clientAddressString;
		}
		
		#endregion

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
			"	<CountryCode>" + System.Web.HttpUtility.HtmlEncode(countryCode) + "</CountryCode>\r\n" +
			"	<City>" + System.Web.HttpUtility.HtmlEncode(city) + "</City>\r\n" +
			"	<ZipCode>" + System.Web.HttpUtility.HtmlEncode(zipCode) + "</ZipCode>\r\n" +
			"	<AttentionOf>" + System.Web.HttpUtility.HtmlEncode(attentionOf) + "</AttentionOf>\r\n" +
			"	<MatchingCode>" + System.Web.HttpUtility.HtmlEncode(matchingCode) + "</MatchingCode>\r\n" +
			"	<AddressZoneId>" + addressZoneId + "</AddressZoneId>\r\n" +
			"	<Phone1>" + phone1 + "</Phone1>\r\n" +
			"	<Phone2>" + phone2 + "</Phone2>\r\n" +
			"</ClientAddress>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("addressId")) {
					SetXmlValue(ref addressId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("clientSequenceCode")) {
					SetXmlValue(ref clientSequenceCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("clientId")) {
					SetXmlValue(ref clientId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("addressType")) {
					SetXmlValue(ref addressType, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("streetAddress")) {
					SetXmlValue(ref streetAddress, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("stateCode")) {
					SetXmlValue(ref stateCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("countryCode")) {
					SetXmlValue(ref countryCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("city")) {
					SetXmlValue(ref city, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("zipCode")) {
					SetXmlValue(ref zipCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("attentionOf")) {
					SetXmlValue(ref attentionOf, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("matchingCode")) {
					SetXmlValue(ref matchingCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("addresszoneid")) {
					SetXmlValue(ref addressZoneId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("phone1")) {
					SetXmlValue(ref phone1, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("phone2")) {
					SetXmlValue(ref phone2, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ClientAddress[] GetClientAddresss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetClientAddresss();
		}

		public static ClientAddress GetClientAddressByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetClientAddressByID(id);
		}
			
		public static ClientAddress GetClientAddressByIdSequenceAddressType(int clientId, string clientSequenceCode, string addressType) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetClientAddressByIdSequenceAddressType(clientId, clientSequenceCode, addressType);
		}

		public ClientAddressStatus Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			int returnValue = dbo.InsertClientAddress(this);
			switch(returnValue) 
			{
				case 1:
				case 2:
					return ClientAddressStatus.Ok;
				default:
					return ClientAddressStatus.Error;
			}
		}

		public ClientAddressStatus Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			int returnValue = dbo.UpdateClientAddress(this);
			switch(returnValue) 
			{
				case 1:
				case 2:
					return ClientAddressStatus.Ok;
				default:
					return ClientAddressStatus.Error;
			}
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

		public string CountryCode {
			set { countryCode = value; }
			get { return countryCode; }
		}

		public string City {
			set { city = value; }
			get { return city; }
		}

		public string ZipCode {
			set { zipCode = value; }
			get { return zipCode; }
		}

		public string AttentionOf {
			set { attentionOf = value; }
			get { return attentionOf; }
		}

		public string MatchingCode {
			set { matchingCode = value; }
			get { return matchingCode; }
		}

		public int AddressZoneId {
			get { return addressZoneId; }
			set { addressZoneId = value; }
		}

		public string Phone1 {
			get { return phone1; }
			set { phone1 = value; }
		}
		
		public string Phone2 {
			get { return phone2; }
			set { phone2 = value; }
		}

		public string Location
		{
			get { return location; }
			set { location = value; }
		}


        public int WarehouseId
        {
            get { return warehouseId; }
            set { warehouseId = value; }
        }

        public bool PickUp
        {
            get { return pickUp; }
            set { pickUp = value; }
        }

		#endregion
	}
}
