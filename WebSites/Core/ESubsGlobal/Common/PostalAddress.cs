
/* Title:	PostalAddress
 * Author:	Jean-Francois Buist
 * Summary:	Postal address compliant with eSubs database.
 * 
 * Create Date:	August 1, 2005
 * 
 */

using System;
using System.Xml.Serialization;

namespace GA.BDC.Core.ESubsGlobal.Common {
	public enum PostalAddressType : int {
		HOME_ADDRESS = 1,
		BUSINESS_ADDRESS = 2
	}

	public enum PostalAddressStatus
	{
		Error,
		Ok
	}

	/// <summary>
	/// Summary description for PostalAddress.
	/// </summary>
    [Serializable]
	public class PostalAddress {
		private int id;	// database id
		private string address1;
		private string address2;
		private string city;
		private string zipCode;
		private CountryCode countryCode;
		private string subDivisionCode;
		private bool active;
		private PostalAddressType postalAddressTypeID;
		private string postalAddressTypeName;
		private int isValidated = 0;

		//new fields
		private DateTime createDate;
		private string matchingCode;

		public PostalAddress() : this(int.MinValue) {}
		public PostalAddress(int id) : this(id, null) {}
		public PostalAddress(int id, string address1) : this(id, address1, null) {}
		public PostalAddress(int id, string address1, string address2) : this(id, address1, address2, null) {}
		public PostalAddress(int id, string address1, string address2, string city) : this(id, address1, address2, city, null) {}
		public PostalAddress(int id, string address1, string address2, string city, string zipCode) : this(id, address1, address2, city, zipCode, null) {}
		public PostalAddress(int id, string address1, string address2, string city, string zipCode, CountryCode countryCode) : this(id, address1, address2, city, zipCode, countryCode, null) {}
		public PostalAddress(int id, string address1, string address2, string city, string zipCode, CountryCode countryCode, string subDivisionCode) : this(id, address1, address2, city, zipCode, countryCode, subDivisionCode, true) {}
		public PostalAddress(int id, string address1, string address2, string city, string zipCode, CountryCode countryCode, string subDivisionCode, bool active) : this(id, address1, address2, city, zipCode, countryCode, subDivisionCode, active, PostalAddressType.HOME_ADDRESS) {}
		public PostalAddress(int id, string address1, string address2, string city, string zipCode, CountryCode countryCode, string subDivisionCode, bool active, PostalAddressType postalAddressTypeID) : this(id, address1, address2, city, zipCode, countryCode, subDivisionCode, active, postalAddressTypeID, null) {}
		public PostalAddress(int id, string address1, string address2, string city, string zipCode, CountryCode countryCode, string subDivisionCode, bool active, PostalAddressType postalAddressTypeID, string postalAddressTypeName) {
			this.id = id;	// database id
			this.address1 = address1;
			this.address2 = address2;
			this.city = city;
			this.zipCode = zipCode;
			this.countryCode = countryCode;
			this.subDivisionCode = subDivisionCode;
			this.active = active;
			this.postalAddressTypeID = postalAddressTypeID;
			this.postalAddressTypeName = postalAddressTypeName;
        }

        #region Methods

        public override string ToString()
        {
            return this.address1 + " " 
                + this.address2 + " " 
                + this.city + " "
                + Common.SubDivisionCode.GetSubDivisionDescriptionFromCode(this.subDivisionCode) + " " 
                + this.countryCode + " " 
                + this.zipCode;
        }
        #endregion

        #region Attributes
        public int Id {
			set { id = value; }
			get { return id; }
		}

		public PostalAddressType PostalAddressTypeID {
			set { postalAddressTypeID = value; }
			get { return postalAddressTypeID; }
		}

		public string PostalAddressTypeName {
			set { postalAddressTypeName = value; }
			get { return postalAddressTypeName; }
		}

		public string Address1 {
			set { address1 = value; }
			get { return address1; }
		}

		public string Address2 {
			set { address2 = value; }
			get { return address2; }
		}

		public string City {
			set { city = value; }
			get { return city; }
		}

		public string ZipCode {
			set { zipCode = value; }
			get { return zipCode; }
		}

		public int IsValidated {
			get {return isValidated; }
			set { isValidated = value; }
		}

		[XmlIgnore]
		public CountryCode CountryCode {
			set { countryCode = value; }
			get { return countryCode; }
		}

		public string SubDivisionCode {
			set { subDivisionCode = value; }
			get { return subDivisionCode; }
		}

	
		public bool Active 
		{
			set { active = value; }
			get { return active; }
		}
		
		public DateTime CreateDate 
		{
			set { createDate = value; }
			get { return createDate; }
		}

		public string MatchingCode 
		{
			set { matchingCode = value; }
			get { return matchingCode; }
		}


		#endregion


		#region Data Source Methods
		public static PostalAddress GetPostalAddressByID(int ID) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPostalAddressByID(ID);
		}

		public int Insert() 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.InsertPostalAddress(this);
		}

        public int Update()
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.UpdatePostalAddress(this);
        }

		#endregion
	}
}
