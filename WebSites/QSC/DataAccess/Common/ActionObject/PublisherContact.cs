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
	public class PublisherContact
	{
		private int iPublisherContactInstance = 0;
		private int iPublisherNumber = 0;
		private string sPublisherName = "";
		private string sFirstName = "";
		private string sLastName = "";
		private string sEmail = "";
		private string sPositionTitle = "";
		private int iPhoneListID = 0;

		public PublisherContact()
		{
		}
		public PublisherContact(int PublisherContactInstance, int PublisherNumber, string PublisherName, string FirstName, string LastName, string Email, string PositionTitle, int PhoneListID)
		{
			iPublisherContactInstance = PublisherContactInstance;
			iPublisherNumber = PublisherNumber;
			sPublisherName = PublisherName;
			sFirstName = FirstName;
			sLastName = LastName;
			sEmail = Email;
			sPositionTitle = PositionTitle;
			iPhoneListID = PhoneListID;
		}
		
		public int PublisherContactInstance
		{
			get
			{
				return iPublisherContactInstance;
			}
			set 
			{
				iPublisherContactInstance = value;
			}
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

		public string PublisherName 
		{
			get 
			{
				return sPublisherName;
			}
			set 
			{
				sPublisherName = value;
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
		public int PhoneListID
		{
			get
			{
				return iPhoneListID;
			}
			set 
			{
				iPhoneListID = value;
			}
		}
	}
}
