using System;
using System.Runtime.Serialization;
namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	/// <summary>
	/// Summary description for PostalAddress.
	/// </summary>
	/// 
	[Serializable]
	public class Address
	{
		private string _street1;
		private string _street2;
		private string _city;
		private string _stateProvince;
		private string _postalCode;
		private string _country;
		private int _type;
		private string sStateProvinceCode;
		private string sCountryCode;

		public Address(string Street1, string Street2, string City, string StateProvinceCode, string PostalCode,string CountryCode)
		{
			_street1			= Street1;
			_street2			= Street2;
			_city				= City;
			sStateProvinceCode		= StateProvinceCode;
			_postalCode			= PostalCode;
			sCountryCode			= CountryCode;
		}
		public int AddressType
		{
			get { return this._type; }
			set {_type = value; }
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

		public string Country
		{
			get{ return this._country; }
			set{ this._country = value; }
		}
		public string StateProvinceCode
		{
			get{ return this.sStateProvinceCode; }
			set{ this.sStateProvinceCode = value; }
		}
		public string CountryCode
		{
			get{ return this.sCountryCode; }
			set{ this.sCountryCode = value; }
		}

		


	}
}
