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
	public class FulfillmentHouse
	{
		private int fulfillmentHouseNumber = 0;
		private string name = String.Empty;
		private string status = String.Empty;
		private string address1 = String.Empty;
		private string address2 = String.Empty;
		private string city = String.Empty;
		private string stateProvince = String.Empty;
		private string zip = String.Empty;
		private string country = String.Empty;
		private InterfaceMedia interfaceMedia = 0;
		private InterfaceLayout interfaceLayout = 0;
		private TransmissionMethod transmissionMethod = 0;
		private bool hardCopy = false;
		private string qspAgencyCode = String.Empty;
		private string isEffortKeyRequired = String.Empty;
		private string payGroupLookUpCode = String.Empty;

		public FulfillmentHouse() { }

		public FulfillmentHouse(int fulfillmentHouseNumber, string name, string status, string address1, string address2, string city, string stateProvince, string zip, string country, InterfaceMedia interfaceMedia, InterfaceLayout interfaceLayout, TransmissionMethod transmissionMethod, bool hardCopy, string qspAgencyCode, string isEffortKeyRequired, string payGroupLookUpCode)
		{
			this.fulfillmentHouseNumber = fulfillmentHouseNumber;
			this.name = name;
			this.status = status;
			this.address1 = address1;
			this.address2 = address2;
			this.city = city;
			this.stateProvince = stateProvince;
			this.zip = zip;
			this.country = country;
			this.interfaceMedia = interfaceMedia;
			this.interfaceLayout = interfaceLayout;
			this.transmissionMethod = transmissionMethod;
			this.hardCopy = hardCopy;
			this.qspAgencyCode = qspAgencyCode;
			this.isEffortKeyRequired = isEffortKeyRequired;
			this.payGroupLookUpCode = payGroupLookUpCode;
		}

		public int FulfillmentHouseNumber
		{
			get
			{
				return fulfillmentHouseNumber;
			}
			set 
			{
				fulfillmentHouseNumber = value;
			}
		}

		public string Name
		{
			get
			{
				return name;
			}
			set 
			{
				name = value;
			}
		}

		public string Status
		{
			get
			{
				return status;
			}
			set 
			{
				status = value;
			}
		}

		public string Address1
		{
			get
			{
				return address1;
			}
			set 
			{
				address1 = value;
			}
		}

		public string Address2
		{
			get
			{
				return address2;
			}
			set 
			{
				address2 = value;
			}
		}

		public string City
		{
			get
			{
				return city;
			}
			set 
			{
				city = value;
			}
		}

		public string StateProvince
		{
			get
			{
				return stateProvince;
			}
			set 
			{
				stateProvince = value;
			}
		}

		public string Zip
		{
			get
			{
				return zip;
			}
			set 
			{
				zip = value;
			}
		}

		public string Country 
		{
			get 
			{
				return country;
			}
			set 
			{
				country = value;
			}
		}

		public InterfaceMedia InterfaceMedia
		{
			get
			{
				return interfaceMedia;
			}
			set 
			{
				interfaceMedia = value;
			}
		}

		public InterfaceLayout InterfaceLayout
		{
			get
			{
				return interfaceLayout;
			}
			set 
			{
				interfaceLayout = value;
			}
		}

		public TransmissionMethod TransmissionMethod
		{
			get
			{
				return transmissionMethod;
			}
			set 
			{
				transmissionMethod = value;
			}
		}

		public bool HardCopy
		{
			get
			{
				return hardCopy;
			}
			set 
			{
				hardCopy = value;
			}
		}

		public string QSPAgencyCode 
		{
			get 
			{
				return qspAgencyCode;
			}
			set 
			{
				qspAgencyCode = value;
			}
		}

		public string IsEffortKeyRequired 
		{
			get 
			{
				return isEffortKeyRequired;
			}
			set 
			{
				isEffortKeyRequired = value;
			}
		}

		public string PayGroupLookUpCode 
		{
			get 
			{
				return payGroupLookUpCode;
			}
			set 
			{
				payGroupLookUpCode = value;
			}
		}
	}
}
