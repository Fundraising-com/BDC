using System;

namespace Common
{
	///<summary>Postal Address data representation</summary>
	public class PostalAddress
	{
		///<summary>default constructor</summary>
		public PostalAddress(){}

		///<summary></summary>
		///<param name="Street1"></param>
		///<param name="Street2"></param>
		///<param name="City"></param>
		///<param name="StateProvince"></param>
		///<param name="PostalCode"></param>
		public PostalAddress(string Street1, string Street2, string City, string StateProvince, string PostalCode)
		{
			ConstructorWorker(-1, Street1, Street2, City, StateProvince, PostalCode, null, null, UNDEF_TYPE);
		}

		///<summary></summary>
		///<param name="Street1"></param>
		///<param name="Street2"></param>
		///<param name="City"></param>
		///<param name="StateProvince"></param>
		///<param name="PostalCode"></param>
		///<param name="PostalPlus4Code"></param>
		public PostalAddress(string Street1, string Street2, string City, string StateProvince, string PostalCode, string PostalPlus4Code)
		{
			ConstructorWorker(-1, Street1, Street2, City, StateProvince, PostalCode, PostalPlus4Code, null, UNDEF_TYPE);
		}

		public PostalAddress(string Street1, string Street2, string City, string StateProvince, string PostalCode, string PostalPlus4Code, string Country)
		{
			ConstructorWorker(-1, Street1, Street2, City, StateProvince, PostalCode, PostalPlus4Code, Country, UNDEF_TYPE);
		}

		public PostalAddress(int Address_ID, string Street1, string Street2, string City, string StateProvince, string PostalCode, string PostalPlus4Code, string Country)
		{
			ConstructorWorker(Address_ID, Street1, Street2, City, StateProvince, PostalCode, PostalPlus4Code, Country, UNDEF_TYPE);
		}

		public PostalAddress(int Address_ID, string Street1, string Street2, string City, string StateProvince, string PostalCode, string PostalPlus4Code, string Country, int AddressListID, int pType)
		{
			ConstructorWorker(Address_ID, Street1, Street2, City, StateProvince, PostalCode, PostalPlus4Code, Country, pType);
			_AddressListIDM = AddressListID;
		}

		public PostalAddress(int Type)
		{
			_type = Type;
		}

		private void ConstructorWorker(int pAddress_ID, string pStreet1, string pStreet2, string pCity, string pStateProvince, string pPostalCode, string pPostalPlus4Code, string pCountry, int pType)
		{
			_street1			= pStreet1;
			_street2			= pStreet2;
			_city				= pCity;
			_stateProvince		= pStateProvince;
			_postalCode			= pPostalCode;
			_postalPlus4Code	= pPostalPlus4Code;
			_country			= pCountry;
			_IDM				= pAddress_ID;
			_type				= pType;
		}

		private string _street1;
		private string _street2;
		private string _city;
		private string _stateProvince;
		private string _postalCode;
		private string _postalPlus4Code;
		private string _country;
		private int _type;
		private int _AddressListIDM = -1;
		private int UNDEF_TYPE = 54000;
		
		private int _IDM = -1;
		public int Address_ID
		{
			get { return this._IDM; }
			set { this._IDM = value; }
		}

		public int AddressListID
		{
			get { return this._AddressListIDM; }
			set { this._AddressListIDM = value; }
		}

		public int Type
		{
			get { return this._type; }
			set { this._type = value; }
		}

		public string Street1
		{
			get{ return this._street1; }
			set{ this._street1 = value; }
		}

		public string Street2
		{
			get{ return this._street2; }
			set{ this._street2 = value; }
		}

		public string City
		{
			get{ return this._city; }
			set{ this._city = value; }
		}

		public string StateProvince
		{
			get{ return this._stateProvince; }
			set{ this._stateProvince = value; }
		}

		public string PostalCode
		{
			get{ return this._postalCode; }
			set{ this._postalCode = value; }
		}

		public string PostalPlus4Code
		{
			get
			{ 
				try { return this._postalPlus4Code; }
				catch(NullReferenceException) { return ""; }
			}
			set{ this._postalPlus4Code = value; }
		}

		public string Country
		{
			get{ return this._country; }
			set{ this._country = value; }
		}

		public bool Validate()
		{			
			if(this._street1 == "") { return false; }
			if(this._city == "") { return false; }
			if(this._stateProvince == ""){ return false; } 
			if(this._country == ""){ return false; }
			return true;
		}


	}
}
