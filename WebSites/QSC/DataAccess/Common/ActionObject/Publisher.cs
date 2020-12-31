using System;
using System.Runtime.Serialization;
using QSPFulfillment.DataAccess.Business;

namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	/// <summary>
	/// Summary description for Magazine.
	/// </summary>
	/// 
	[Serializable]
	public class Publisher
	{
		private int iPublisherNumber = 0;
		private string sName = "";
		private string sStatus = "";
		private string sAddress1 = "";
		private string sAddress2 = "";
		private string sCity = "";
		private string sStateProvince = "";
		private string sZip = "";
		private string sCountryCode = "";

		public Publisher()
		{
		}
		public Publisher(int PublisherNumber, string Name, string Status, string Address1, string Address2, string City, string StateProvince, string Zip, string CountryCode)
		{
			iPublisherNumber = PublisherNumber;
			sName = Name;
			sStatus = Status;
			sAddress1 = Address1;
			sAddress2 = Address2;
			sCity = City;
			sStateProvince = StateProvince;
			sZip = Zip;
			sCountryCode = CountryCode;
		}

		public int PublisherNumber
		{
			get
			{
				return iPublisherNumber;
			}
			set 
			{
				iPublisherNumber = value;
			}
		}
		public string Name
		{
			get
			{
				return sName;
			}
			set 
			{
				sName = value;
			}
		}
		public string Status
		{
			get
			{
				return sStatus;
			}
			set 
			{
				sStatus = value;
			}
		}
		public string Address1
		{
			get
			{
				return sAddress1;
			}
			set 
			{
				sAddress1 = value;
			}
		}
		public string Address2
		{
			get
			{
				return sAddress2;
			}
			set 
			{
				sAddress2 = value;
			}
		}
		public string City
		{
			get
			{
				return sCity;
			}
			set 
			{
				sCity = value;
			}
		}
		public string StateProvince
		{
			get
			{
				return sStateProvince;
			}
			set 
			{
				sStateProvince = value;
			}
		}
		public string Zip
		{
			get
			{
				return sZip;
			}
			set 
			{
				sZip = value;
			}
		}
		public string CountryCode
		{
			get
			{
				return sCountryCode;
			}
			set 
			{
				sCountryCode = value;
			}
		}
	}
}
