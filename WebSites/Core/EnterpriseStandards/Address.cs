using System;

namespace GA.BDC.Core.EnterpriseStandards
{
	/// <summary>
	/// Summary description for Address.
	/// </summary>
	/// 
	using System.Xml;
	
	public class Address: XmlBaseDataObject
	{
		protected int addressId;
		protected string addressType;
		protected string streetAddress;
		protected string stateCode;
		protected string countryCode;
		protected string city;
		protected string zipCode;
		protected string county;

		public Address() : this(int.MinValue, string.Empty) { }
		public Address(int addrId, string addressType) : this(addrId, addressType, null) { }
		public Address(int addrId, string addressType, string streetAddress) : this(addrId, addressType, streetAddress, null) { }
		public Address(int addrId, string addressType, string streetAddress, string stateCode) : this(addrId, addressType, streetAddress, stateCode, null) { }
		public Address(int addrId, string addressType, string streetAddress, string stateCode, string countryCode) : this(addrId, addressType, streetAddress, stateCode, countryCode, null) { }
		public Address(int addrId, string addressType, string streetAddress, string stateCode, string countryCode, string city) : this(addrId, addressType, streetAddress, stateCode, countryCode, city, null) { }
		public Address(int addrId, string addressType, string streetAddress, string stateCode, string countryCode, string city, string zipCode)
		{
			addressId= addrId;
			this.addressType = addressType;
			this.streetAddress = streetAddress;
			this.stateCode = stateCode;
			this.countryCode = countryCode;
			this.city = city;
			this.zipCode = zipCode;
		}

		#region Methods
	

		static public string IsNullReturnEmpty(string sValue)
		{
			if (sValue == null)
				return string.Empty;
			return sValue.Trim();
		}

		public virtual string ToHumanReadableString ()
		{
			string clientAddressString = "";
			clientAddressString += "AddressHygiene is Valid\r\n";
			clientAddressString += "Street Address : \t\t" + this.StreetAddress + "\r\n";
			clientAddressString += "State Code : \t\t" + this.StateCode + "\r\n";
			clientAddressString += "County : \t\t" + this.County + "\r\n";
			clientAddressString += "Country Code : \t\t" + this.CountryCode + "\r\n";
			clientAddressString += "City : \t\t" + this.City + "\r\n";
			clientAddressString += "Zip Code : \t\t" + this.ZipCode + "\r\n";
			
			return clientAddressString;
		}


		#endregion

		
		#region Properties



		public string AddressType 
		{
			set { addressType = value; }
			get { return IsNullReturnEmpty(addressType); }
		}

		public string StreetAddress 
		{
			set { streetAddress = value; }
			get { return IsNullReturnEmpty(streetAddress); }
		}

		public string StateCode 
		{
			set { stateCode = value; }
			get { return IsNullReturnEmpty(stateCode); }
		}

		public string CountryCode 
		{
			set { countryCode = value; }
			get { return IsNullReturnEmpty(countryCode); }
		}

		public string City 
		{
			set { city = value; }
			get { return IsNullReturnEmpty(city); }
		}

		public string ZipCode 
		{
			set { zipCode = value; }
			get { return IsNullReturnEmpty(zipCode); }
		}

		public string County 
		{
			set {county = value;}
			get {return IsNullReturnEmpty(county) ;}
		}



		public int AddressId
		{
			set {addressId = value;}
			get { return addressId;}
		}

		#endregion

		
		#region XML Methods

		public override string GenerateXML() 
		{
			return "<Addresse>\r\n" +
				"	<AddressId>" + addressId + "</AddressId>\r\n" +
				"	<AddressType>" + addressType + "</AddressType>\r\n" +
				"	<StreetAddress>" + streetAddress + "</StreetAddress>\r\n" +
				"	<City>" + city + "</City>\r\n" +
				"	<StateCode>" + stateCode + "</StateCode>\r\n" +
				"	<County>" + county + "</County>\r\n" +
				"	<ZipCode>" + zipCode+ "</ZipCode>\r\n" +
				"	<CountryCode>" + countryCode + "</CountryCode>\r\n" +
				"</Address>\r\n";
		}

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) 
		{
			foreach(XmlNode node in childNodes) 
			{
				if(ToLowerCase(node.Name) == ToLowerCase("addressType")) 
				{
					SetXmlValue(ref addressType, node.InnerText);
					continue;
				}
				if(ToLowerCase(node.Name) == ToLowerCase("streetAddress")) 
				{
					SetXmlValue(ref streetAddress, node.InnerText);
					continue;
				}
				if(ToLowerCase(node.Name) == ToLowerCase("county")) 
				{
					SetXmlValue(ref county, node.InnerText);
					continue;
				}
				if(ToLowerCase(node.Name) == ToLowerCase("stateCode")) 
				{
					SetXmlValue(ref stateCode, node.InnerText);
					continue;
				}
				if(ToLowerCase(node.Name) == ToLowerCase("countryCode")) 
				{
					SetXmlValue(ref countryCode, node.InnerText);
					continue;
				}
				if(ToLowerCase(node.Name) == ToLowerCase("city")) 
				{
					SetXmlValue(ref city, node.InnerText);
					continue;
				}
				if(ToLowerCase(node.Name) == ToLowerCase("zipCode")) 
				{
					SetXmlValue(ref zipCode, node.InnerText);
					continue;
				}
				
				if(ToLowerCase(node.Name) == ToLowerCase("zip")) 
				{
					SetXmlValue(ref zipCode, node.InnerText);
					continue;
				}

			}
		}

		
		// load from an xml string 
		public override void LoadXml(string xml) 
		{
			XmlDocument doc = new XmlDocument();
			try
			{
				doc.LoadXml(xml);
			}
			catch (Exception)
			{
				return;
			}

			foreach(XmlNode node in doc.ChildNodes) 
			{
				Load(node);
//				if (node.InnerXml.Trim() != string.Empty)
//					LoadXml(node.InnerXml);
			}
		}

		#endregion

		#endregion
	}

}
