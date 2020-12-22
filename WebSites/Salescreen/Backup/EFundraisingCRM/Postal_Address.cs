using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public enum PostalAddressStatus
	{
		Ok,
		Error
	}

	public class PostalAddress: EFundraisingCRMDataObject {

		private int postalAddressId;
		private string address;
		private string city;
		private string zipCode;
		private string countryCode;
		private string subdivisionCode;
		private DateTime createDate;


		public PostalAddress() : this(int.MinValue) { }
		public PostalAddress(int postalAddressId) : this(postalAddressId, null) { }
		public PostalAddress(int postalAddressId, string address) : this(postalAddressId, address, null) { }
		public PostalAddress(int postalAddressId, string address, string city) : this(postalAddressId, address, city, null) { }
		public PostalAddress(int postalAddressId, string address,  string city, string zipCode) : this(postalAddressId, address, city, zipCode, null) { }
		public PostalAddress(int postalAddressId, string address,  string city, string zipCode, string countryCode) : this(postalAddressId, address, city, zipCode, countryCode, null) { }
		public PostalAddress(int postalAddressId, string address,  string city, string zipCode, string countryCode, string subdivisionCode) : this(postalAddressId, address, city, zipCode, countryCode, subdivisionCode, DateTime.MinValue) { }
		public PostalAddress(int postalAddressId, string address,  string city, string zipCode, string countryCode, string subdivisionCode, DateTime createDate) {
			this.postalAddressId = postalAddressId;
			this.address = address;
			this.city = city;
			this.zipCode = zipCode;
			#region Hack
			// to get rid of 1's that are appearing in the postal addres
			if(countryCode!= null)
			{
				if(countryCode.StartsWith("1".ToString()))
				{
					countryCode.Replace("1".ToString(),"US".ToString());
				}
			}
			#endregion 
			this.countryCode = countryCode;
			
			if (subdivisionCode != null)
			{
				if(subdivisionCode.StartsWith("CA".ToString()))
				{
					subdivisionCode= this.fixProvince(subdivisionCode);
				}
				else
				{
					if(subdivisionCode.StartsWith("1".ToString()))
					{
						subdivisionCode.Replace("1".ToString(),"US".ToString());
					}
					this.subdivisionCode = subdivisionCode;
				}
			}
			this.createDate = createDate;
		}


		public bool IsDifferent(PostalAddress theAddress)
		{
			if (theAddress == null)
				return true;

			if (
				string.Compare(theAddress.address, this.address, true) != 0 ||
				string.Compare(theAddress.city, this.city, true) != 0 ||
				string.Compare(theAddress.zipCode, this.zipCode, true) != 0 ||
				string.Compare(theAddress.CountryCode, this.CountryCode, true) != 0 ||
				string.Compare(theAddress.subdivisionCode, this.subdivisionCode, true) != 0)
				return true;
			else
				return false;
		}

		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PostalAddress>\r\n" +
				"	<PostalAddressId>" + postalAddressId + "</PostalAddressId>\r\n" +
				"	<Address>" + System.Web.HttpUtility.HtmlEncode(address) + "</Address>\r\n" +
				"	<City>" + System.Web.HttpUtility.HtmlEncode(city) + "</City>\r\n" +
				"	<ZipCode>" + System.Web.HttpUtility.HtmlEncode(zipCode) + "</ZipCode>\r\n" +
				"	<CountryCode>" + countryCode + "</CountryCode>\r\n" +
				"	<SubdivisionCode>" + subdivisionCode + "</SubdivisionCode>\r\n" +
				"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
				"</PostalAddress>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "postalAddressId") {
					SetXmlValue(ref postalAddressId, node.InnerText);
				}
				if(node.Name.ToLower() == "address") {
					SetXmlValue(ref address, node.InnerText);
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
				if(node.Name.ToLower() == "subdivisionCode") {
					SetXmlValue(ref subdivisionCode, node.InnerText);
				}
				if(node.Name.ToLower() == "createDate") {
					SetXmlValue(ref createDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static PostalAddress[] GetPostalAddresss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPostalAddresss();
		}

		public static PostalAddress GetPostalAddressByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPostalAddressByID(id);
		}

		public PostalAddressStatus Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			int returnValue = dbo.InsertPostalAddress(this);
			switch(returnValue) 
			{
				case 1:
					return PostalAddressStatus.Ok;
			}
			return PostalAddressStatus.Error;
		}

		public PostalAddressStatus Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			int returnValue = dbo.UpdatePostalAddress(this);
			switch(returnValue) 
			{
				case 1:
					return PostalAddressStatus.Ok;
			}
			return PostalAddressStatus.Error;
		}
		#endregion

		#region Properties

		public int PostalAddressId {
			set { postalAddressId = value; }
			get { return postalAddressId; }
		}

		public string Address {
			set { address = value; }
			get { return address; }
		}

		public string City {
			set { city = value; }
			get { return city; }
		}

		public string ZipCode {
			set { zipCode = value; }
			get { return zipCode; }
		}

		public string CountryCode
		{
			#region Hack
			// hack to remove "1" and "2" for country in the postal address
			set
			{ 
				if(value.Trim().StartsWith("1".ToString()))
				{
					value = value.Trim().Replace("1".ToString(),"US".ToString());
				}
				else if(value.Trim().StartsWith("2".ToString()))
				{
					value = value.Trim().Replace("2".ToString(),"CA".ToString());
				}

					  countryCode = value; 
			}
			#endregion
			get { return countryCode; }
		}

		public string SubdivisionCode
		{
			
			set 
			{ 
				#region Hack to get rid of 1
				//to get rid of "1"s in the postalal address table
				if(value != null) 
				{
					if(value.StartsWith("CA".ToString())) 
					{
						subdivisionCode = this.fixProvince(value);
					}
					else if(value.Trim().StartsWith("1".ToString()))
					{
						subdivisionCode = value.Replace("1".ToString(),"US".ToString());
					}
					else if(value.Trim().StartsWith("2".ToString()))
					{
						subdivisionCode = value.Replace("2".ToString(),"CA".ToString());
					}
					else 
					{
						subdivisionCode = value;
					}
				} 
				else 
				{
					subdivisionCode = value;
				}
				#endregion
			}
			get { return subdivisionCode; }
		}

		public DateTime CreateDate {
			set { createDate = value; }
			get { return createDate; }
		}

		#endregion

		#region Patching Function
		private string fixProvince(string province)
		{
			// Patch to avoid inconsistency for canadian provinces abbreviation
			string tempSubDiv = province.Substring(province.IndexOf("-".ToString()) + 1);
			switch(tempSubDiv)
			{
				case "OT":
					return "CA-ON".ToString();
				case "SA":
					return "CA-SK".ToString();
				case "ALB":
					return "CA-AB".ToString();
				case "AL":
					return "CA-AB".ToString();
				case "QU":
					return "CA-QC".ToString();
				default:
					return province.ToString();

			}
		}
		#endregion
	}
}
