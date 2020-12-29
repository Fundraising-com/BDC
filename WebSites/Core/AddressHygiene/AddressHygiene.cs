using System;

namespace GA.BDC.Core.AddressHygiene
{
	using EnterpriseStandards;
	using System.Xml;

	public enum AddressComparable 
	{
		StreetAddress,
		StateCode,
		County,
		State,
		CountryCode,
		City,
		ZipCode,
		AddressId,
		ToHumanReadableString
	}


	/// <summary>
	/// Summary description for AddressHygiene.
	/// </summary>
	
	public class AddressHygiene: Address, IComparable
	{
		protected AddressComparable sortBy = AddressComparable.ToHumanReadableString;
		private string addrfaultcode;
		static private System.Collections.Hashtable InvalidAddressMessage = GetInvalidAddressMessage();
		static private System.Collections.Hashtable HashtableCountryCode =  GetCountryCode();
		
		static private string[] specialCharactersInXml =  {"&amp;", "&lt;", "&gt;", "&quot;", "&apos;"};
		static private string[] specialCharacters      =  {"&", "<;", ">", "\"", "'"};

		#region Methods
		
		

		private string ConvertToSafeXml(string theStr)
		{
			if (theStr == null || theStr.Trim() == string.Empty)
				return string.Empty;

			string result = theStr;
			for (int i=0; i < specialCharactersInXml.Length; i++)
			{
				result = result.Replace(specialCharacters[i], specialCharactersInXml[i]);
			}
			return result;
		}

		public override string ToHumanReadableString()
		{
//			string clientAddressString = "";
//			clientAddressString += "Street Address : \t\t" + this.StreetAddress + "\r\n";
//			clientAddressString += "County : \t\t" + this.County + "\r\n";
//			clientAddressString += "State Code : \t\t" + this.StateCode + "\r\n";
//			clientAddressString += "Country Code : \t\t" + this.CountryCode + "\r\n";
//			clientAddressString += "City : \t\t" + this.City + "\r\n";
//			clientAddressString += "Zip Code : \t\t" + this.ZipCode + "\r\n";
//			
//			return clientAddressString;

			System.Text.StringBuilder strBuilder = new System.Text.StringBuilder();
			strBuilder.Append("Street Address : \t\t" + this.StreetAddress + "\r\n");
			strBuilder.Append("County : \t\t" + this.County + "\r\n");
			strBuilder.Append("State Code : \t\t" + this.StateCode + "\r\n");
			strBuilder.Append("Country Code : \t\t" + this.CountryCode + "\r\n");
			strBuilder.Append("City : \t\t" + this.City + "\r\n");
			strBuilder.Append("Zip Code : \t\t" + this.ZipCode + "\r\n");

			return strBuilder.ToString();
		}
		
		public string ToHumanReadableStringOneLine()
		{
//			string clientAddressString = "";
//			clientAddressString += "Street Address : " + this.StreetAddress + " , ";
//			clientAddressString += "County : " + this.County + " , ";
//			clientAddressString += "State Code : " + this.StateCode + " , ";
//			clientAddressString += "Country Code : " + this.CountryCode + " , ";
//			clientAddressString += "City : " + this.City + " , ";
//			clientAddressString += "Zip Code : " + this.ZipCode + "";
//			
//			return clientAddressString;

			System.Text.StringBuilder strBuilder = new System.Text.StringBuilder();
			strBuilder.Append("Address ID : " + this.AddressId + " , ");
			strBuilder.Append("Street Address : " + this.StreetAddress + " , ");
			strBuilder.Append("County : " + this.County + " , ");
			strBuilder.Append("State Code : " + this.StateCode + " , ");
			strBuilder.Append("Country Code : " + this.CountryCode + " , ");
			strBuilder.Append("City : " + this.City + " , ");
			strBuilder.Append("Zip Code : " + this.ZipCode);

			return strBuilder.ToString();
		}

		public string ToReadableStringValidMessage()
		{
			string clientAddressString = "";
			if (IsValid)
			{
				clientAddressString += "AddressHygiene is Valid\r\n";
			}
			else
			{
				if (InvalidAddressMessage == null)
					InvalidAddressMessage = GetInvalidAddressMessage();
				clientAddressString += "AddressHygiene Error : \t\t" + InvalidAddressMessage[this.addrfaultcode].ToString() + "\r\n";
			}
			clientAddressString += "Street Address : \t\t" + this.StreetAddress + "\r\n";
			clientAddressString += "County : \t\t" + this.County + "\r\n";
			clientAddressString += "State Code : \t\t" + this.StateCode + "\r\n";
			clientAddressString += "Country Code : \t\t" + this.CountryCode + "\r\n";
			clientAddressString += "City : \t\t" + this.City + "\r\n";
			clientAddressString += "Zip Code : \t\t" + this.ZipCode + "\r\n";
			
			return clientAddressString;

		}

		public string ToInvalidMessage()
		{
			string clientAddressString = string.Empty;
			if (!IsValid)
			{
				if (InvalidAddressMessage == null)
					InvalidAddressMessage = GetInvalidAddressMessage();
				clientAddressString += InvalidAddressMessage[this.addrfaultcode].ToString();
			}
			return clientAddressString;
		}

		public string ToXmlAddressValidation()
		{
//			string result = string.Empty;
//			result += string.Format("<address1>{0}</address1><address2/>", ConvertToSafeXml(this.StreetAddress));
//			result += string.Format("<city>{0}</city>", ConvertToSafeXml(this.City));
//			result += string.Format("<county>{0}</county>", ConvertToSafeXml(this.County));
//			result += string.Format("<state>{0}</state>", ConvertToSafeXml(this.StateCode));
//			result += string.Format("<zip>{0}</zip>", ConvertToSafeXml(this.ZipCode));
//			result += string.Format("<country>{0}</country>", ConvertToSafeXml(this.CountryCode));
//			return result;

			System.Text.StringBuilder strBuilder = new System.Text.StringBuilder();
			strBuilder.AppendFormat("<address1>{0}</address1><address2/>", ConvertToSafeXml(this.StreetAddress) );
			strBuilder.AppendFormat("<city>{0}</city>", ConvertToSafeXml(this.City));
			strBuilder.AppendFormat("<county>{0}</county>", ConvertToSafeXml(this.County));
			strBuilder.AppendFormat("<state>{0}</state>", ConvertToSafeXml(this.StateCode));
			strBuilder.AppendFormat("<zip>{0}</zip>", ConvertToSafeXml(this.ZipCode));
			strBuilder.AppendFormat("<country>{0}</country>", ConvertToSafeXml(this.CountryCode));
			return strBuilder.ToString();
		}

		public bool IsValid
		{
			get
			{
				if (addrfaultcode != null && addrfaultcode.Trim() != string.Empty)
					return false;
				return true;
			}
		}

		public string InvalidDescription()
		{
			if (!IsValid)
			{
				if (InvalidAddressMessage == null)
					InvalidAddressMessage = GetInvalidAddressMessage();
				return InvalidAddressMessage[this.addrfaultcode].ToString() ;
			}
			return string.Empty;
		}
		#endregion

		#region Properties

		
		public string Addrfaultcode
		{
			set {addrfaultcode = value;}
			get {return IsNullReturnEmpty(addrfaultcode);}
		}

		
		public AddressComparable SortBy
		{
			set {sortBy = value;}
			get {return sortBy;}
		}

		public string Country
		{
			get 
			{
				if (countryCode != null && countryCode.Trim() != string.Empty)
				{
					return GetCountryName(countryCode);
				}
				return string.Empty;
			}
		}

		

		#endregion

		#region XML Methods

		public override string GenerateXML() 
		{
			return "<AddressHygiene>\r\n" +
				"	<AddressId>" + addressId + "</AddressId>\r\n" +
				"	<AddressType>" + addressType + "</AddressType>\r\n" +
				"	<StreetAddress>" + streetAddress + "</StreetAddress>\r\n" +
				"	<City>" + city + "</City>\r\n" +
				"	<StateCode>" + stateCode + "</StateCode>\r\n" +
				"	<County>" + county + "</County>\r\n" +
				"	<ZipCode>" + zipCode+ "</ZipCode>\r\n" +
				"	<CountryCode>" + countryCode + "</CountryCode>\r\n" +
				"</AddressHygiene>\r\n";
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
				if(ToLowerCase(node.Name) == ToLowerCase("stateCode")) 
				{
					SetXmlValue(ref stateCode, node.InnerText);
					continue;
				}
				if(ToLowerCase(node.Name) == ToLowerCase("state")) 
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
				
				if(ToLowerCase(node.Name) == ToLowerCase("address1")) 
				{
					SetXmlValue(ref streetAddress, node.InnerText);
					continue;
				}
				if(ToLowerCase(node.Name) == ToLowerCase("address")) 
				{
					SetXmlValue(ref streetAddress, node.InnerText);
					continue;
				}
				if(ToLowerCase(node.Name) == ToLowerCase("country")) 
				{
					string sTmp = string.Empty;
					SetXmlValue(ref sTmp, node.InnerText);
					countryCode = GetCountryCode(sTmp);
					continue;
				}
				if(ToLowerCase(node.Name) == ToLowerCase("county")) 
				{
					SetXmlValue(ref county, node.InnerText);
					continue;
				}

				if(ToLowerCase(node.Name) == ToLowerCase("addrfaultcode")) 
				{
					SetXmlValue(ref addrfaultcode, node.InnerText);
					continue;
				}
			}
		}

		
		#endregion

		#endregion

		#region IComparable Members

		public int CompareTo(object obj)
		{
			// TODO:  Add Address.CompareTo implementation
			AddressHygiene addr = obj as AddressHygiene;
			if (addr != null)
			{
				switch (sortBy)
				{
					case AddressComparable.AddressId:
					{
						if (this.AddressId == addr.AddressId)
							return 0;
						else if (this.AddressId < addr.AddressId)
							return -1;
						else
							return 1;
							
					}
					case AddressComparable.City:
						return string.Compare(City, addr.City, true);
					case AddressComparable.CountryCode:
						return string.Compare(CountryCode, addr.CountryCode, true);
					case AddressComparable.StateCode:
						return string.Compare(StateCode, addr.StateCode, true);
					case AddressComparable.County:
						return string.Compare(County, addr.County, true);
					case AddressComparable.State:
						return string.Compare(StateCode, addr.StateCode, true);
					case AddressComparable.StreetAddress:
						return string.Compare(StreetAddress, addr.StreetAddress, true);
					case AddressComparable.ZipCode:
						return string.Compare(ZipCode, addr.ZipCode, true);
					case AddressComparable.ToHumanReadableString:
						return string.Compare(ToHumanReadableString(), addr.ToHumanReadableString(), true);
					default:
						return string.Compare(ToHumanReadableString(), addr.ToHumanReadableString(), true);
				}
			}
			return 0;
		}


		#endregion

		#region Static Methods

		


		static public bool IsDifferent(AddressHygiene ahOrg, AddressHygiene ahVal)
		{
			if (ahOrg == null || ahVal == null)
				return true;

			if (string.Compare(ahOrg.ZipCode, ahVal.ZipCode, true) != 0)
				return true;

			if (string.Compare(ahOrg.StreetAddress, ahVal.StreetAddress, true) != 0)
				return true;

			if (string.Compare(ahOrg.City, ahVal.City, true) != 0)
				return true;

			if (string.Compare(ahOrg.CountryCode, ahVal.CountryCode, true) != 0)
				return true;

			if (string.Compare(ahOrg.StateCode, ahVal.StateCode, true) != 0)
				return true;


			return false;
		}


		static public string GetCountryCode(string CountryName)
		{
			if (CountryName == null || CountryName.Trim() == string.Empty)
				return string.Empty;

			string sTmp = (string)HashtableCountryCode[CountryName.ToLower()];
			if (sTmp == null)
				return string.Empty;
			return sTmp.Trim().ToUpper();
		}

		static public string GetCountryName(string cCode)
		{
			if (cCode == null || cCode.Trim() == string.Empty)
				return string.Empty;

			string sTmp = string.Empty;

			foreach (object obj in HashtableCountryCode.Keys)
			{
				sTmp = HashtableCountryCode[obj] as string;
				if (sTmp != null && string.Compare(sTmp, cCode, true) ==0)
					return (obj as string);
			}

			return string.Empty;
		}

		static private System.Collections.Hashtable GetCountryCode()
		{
			System.Collections.Hashtable hashTB = new System.Collections.Hashtable();
			hashTB["united states"] = "us";
			hashTB["canada"] = "ca";
			return hashTB;
		}

		static private System.Collections.Hashtable GetInvalidAddressMessage()
		{
			string xmlString=@"<Invalid>
<ErrorCode>F101</ErrorCode><Description>Lastline is bad.</Description>
<ErrorCode>F212</ErrorCode><Description>No locality postal code is bad.</Description>
<ErrorCode>F213</ErrorCode><Description>Bad locality, no postal code.</Description>
<ErrorCode>F214</ErrorCode><Description>Bad locality, bad postal code.</Description>
<ErrorCode>F216</ErrorCode><Description>Bad postal code; cant determine which locality to select.</Description>
<ErrorCode>F302</ErrorCode><Description>No primary address line.</Description>
<ErrorCode>F412</ErrorCode><Description>Street name not found in postal directory.</Description>
<ErrorCode>F413</ErrorCode><Description>Possible street-name matches are too close to choose one.</Description>
<ErrorCode>F420</ErrorCode><Description>Primary range is missing.</Description>
<ErrorCode>F421</ErrorCode><Description>Primary range is not valid for the street, route, or building.</Description>
<ErrorCode>F422</ErrorCode><Description>Predirectional needed; input is wrong or missing.</Description>
<ErrorCode>F423</ErrorCode><Description>Suffix needed; input is wrong or missing.</Description>
<ErrorCode>F425</ErrorCode><Description>Suffix and directional needed, input is wrong or missing.</Description>
<ErrorCode>F427</ErrorCode><Description>Postdirectional needed; input is wrong or missing.</Description>
<ErrorCode>F428</ErrorCode><Description>Bad postal code; cant select an address match.</Description>
<ErrorCode>F429</ErrorCode><Description>Bad city; cant select an address match. International engine: This is valid for U.K. addresses only.</Description>
<ErrorCode>F430</ErrorCode><Description>Possible address-line matches are too close to choose one.</Description>
<ErrorCode>F431</ErrorCode><Description>Puerto Rican urbanization needed;input is wrong or missing.</Description>
<ErrorCode>F432</ErrorCode><Description>Address conflicts with postal code,and the same street has a different	postal code.</Description>
<ErrorCode>F433</ErrorCode><Description>The input postal code is in the directory,	but the input street data is not in the directory. International engine: This is valid for U.K.	addresses only. (Two-digit DPS)</Description>
<ErrorCode>F434</ErrorCode><Description>Street assignment was possible, there was a duplicate postal-code match.</Description>
<ErrorCode>F435</ErrorCode><Description>No street assignment, no postal code.</Description>
<ErrorCode>F436</ErrorCode><Description>No street assignment, postal code not matched.</Description>
<ErrorCode>F437</ErrorCode><Description>Multiple match, different directory areas.</Description>
<ErrorCode>F438</ErrorCode><Description>Secondary address information is missing.</Description>
<ErrorCode>F439</ErrorCode><Description>Exact match made in EWS directory</Description>
<ErrorCode>F440</ErrorCode><Description>Insufficient input data; cannot choose between multiple streetlevel matches.</Description>
<ErrorCode>F441</ErrorCode><Description>Insufficient input data; street level match occurred using partial-range matching.</Description>
<ErrorCode>F450</ErrorCode><Description>Unit missing or out of range.</Description>
<ErrorCode>F503</ErrorCode><Description>Postal code is not in area covered by partial postal-code directory.</Description>
<ErrorCode>F504</ErrorCode><Description>Overlapping ranges in postal directory.</Description>
<ErrorCode>F505</ErrorCode><Description>Matched to undeliverable default record. The generated undeliverable (F505) has no ZIP+4 listed, an invalid CART, and is flagged as undeliverable. (United States)</Description>
<ErrorCode>F600</ErrorCode><Description>Undeliverable.</Description>
<ErrorCode>F451</ErrorCode><Description>PO box missing or out of range.Data Quality assigns this code when	there is a post office match, but there are no P.O. box matches.</Description>
<ErrorCode>F452</ErrorCode><Description>PO box disagrees with postal code.	International engine: This is valid	for Finland addresses only.</Description>
<ErrorCode>F453</ErrorCode><Description>More than one match found for unit,could not choose one.</Description>
<ErrorCode>F500</ErrorCode><Description>Other error</Description>
<ErrorCode>F501</ErrorCode><Description>Foreign address.</Description>
<ErrorCode>F502</ErrorCode><Description>Input record entirely blank.</Description>
<ErrorCode>F503</ErrorCode><Description>Postal code is not in area covered by partial postal-code directory.</Description>
<ErrorCode>F504</ErrorCode><Description>Overlapping ranges in postal directory.</Description>
<ErrorCode>F505</ErrorCode><Description>Matched to undeliverable default record. The generated undeliverable (F505) has no ZIP+4 listed, an invalid CART, and is flagged as undeliverable. (United States)</Description>
<ErrorCode>F600</ErrorCode><Description>Undeliverable.</Description></Invalid>";
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xmlString);
			XmlNodeList nodeList = doc.SelectNodes("descendant::ErrorCode");
			int iCount= nodeList.Count;
			System.Collections.Hashtable result = new System.Collections.Hashtable();
			for (int i=0; i < iCount; i++)
			{
				string errorDescription = nodeList[i].NextSibling.FirstChild.Value;
				string errorKey = nodeList[i].FirstChild.Value;
				result[errorKey] = errorDescription;
			}
			return result;
		}


		#endregion
	}


}
