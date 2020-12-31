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
	public class FulfillmentHouseContact
	{
		private int iInstance = 0;
		private int iFulfillmentHouseID = 0;
		private string sFulfillmentHouseName = "";
		private string sFirstName = "";
		private string sLastName = "";
		private string sEmail = "";
		private string sPositionTitle = "";
		private string sWorkPhone = "";
		private string sFax = "";
		private string sCustomerServiceContactFirstName = "";
		private string sCustomerServiceContactLastName = "";
		private string sCustomerServiceContactEmail = "";
		private string sCustomerServiceContactPhone = "";

		public FulfillmentHouseContact()
		{
		}
		public FulfillmentHouseContact(int Instance, int FulfillmentHouseID, string FulfillmentHouseName, string FirstName, string LastName, string Email, string PositionTitle, string WorkPhone, string Fax, string CustomerServiceContactFirstName, string CustomerServiceContactLastName, string CustomerServiceContactEmail, string CustomerServiceContactPhone)
		{
			iInstance = Instance;
			iFulfillmentHouseID = FulfillmentHouseID;
			sFulfillmentHouseName = FulfillmentHouseName;
			sFirstName = FirstName;
			sLastName = LastName;
			sEmail = Email;
			sPositionTitle = PositionTitle;
			sWorkPhone = WorkPhone;
			sFax = Fax;
			sCustomerServiceContactFirstName = CustomerServiceContactFirstName;
			sCustomerServiceContactLastName = CustomerServiceContactLastName;
			sCustomerServiceContactEmail = CustomerServiceContactEmail;
			sCustomerServiceContactPhone = CustomerServiceContactPhone;
		}

		public int Instance 
		{
			get 
			{
				return iInstance;
			}
			set 
			{
				iInstance = value;
			}
		}

		public int FulfillmentHouseID 
		{
			get 
			{
				return iFulfillmentHouseID;
			}
			set 
			{
				iFulfillmentHouseID = value;
			}
		}

		public string FulfillmentHouseName 
		{
			get 
			{
				return sFulfillmentHouseName;
			}
			set 
			{
				sFulfillmentHouseName = value;
			}
		}

		public string FirstName 
		{
			get 
			{
				return sFirstName;
			}
			set 
			{
				sFirstName = value;
			}
		}

		public string LastName 
		{
			get 
			{
				return sLastName;
			}
			set 
			{
				sLastName = value;
			}
		}

		public string Email 
		{
			get 
			{
				return sEmail;
			}
			set 
			{
				sEmail = value;
			}
		}

		public string PositionTitle 
		{
			get 
			{
				return sPositionTitle;
			}
			set 
			{
				sPositionTitle = value;
			}
		}

		public string WorkPhone 
		{
			get 
			{
				return sWorkPhone;
			}
			set 
			{
				sWorkPhone = value;
			}
		}

		public string Fax 
		{
			get 
			{
				return sFax;
			}
			set 
			{
				sFax = value;
			}
		}

		public string CustomerServiceContactFirstName 
		{
			get 
			{
				return sCustomerServiceContactFirstName;
			}
			set 
			{
				sCustomerServiceContactFirstName = value;
			}
		}

		public string CustomerServiceContactLastName 
		{
			get 
			{
				return sCustomerServiceContactLastName;
			}
			set 
			{
				sCustomerServiceContactLastName = value;
			}
		}

		public string CustomerServiceContactEmail 
		{
			get 
			{
				return sCustomerServiceContactEmail;
			}
			set 
			{
				sCustomerServiceContactEmail = value;
			}
		}

		public string CustomerServiceContactPhone 
		{
			get 
			{
				return sCustomerServiceContactPhone;
			}
			set 
			{
				sCustomerServiceContactPhone = value;
			}
		}
	}
}
