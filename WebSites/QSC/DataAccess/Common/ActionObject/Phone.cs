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
	public class Phone
	{
		private int iID = 0;
		private int iType = 0;
		private int iPhoneListID = 0;
		private string sPhoneNumber = "";
		private string sBestTimeToCall = "";

		public Phone()
		{
		}
		public Phone(int ID, int Type, int PhoneListID, string PhoneNumber, string BestTimeToCall)
		{
			iID = ID;
			iType = Type;
			iPhoneListID = PhoneListID;
			sPhoneNumber = PhoneNumber;
			sBestTimeToCall = BestTimeToCall;
		}
		
		public int ID
		{
			get
			{
				return iID;
			}
			set 
			{
				iID = value;
			}
		}
		public int Type
		{
			get
			{
				return iType;
			}
			set 
			{
				iType = value;
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
		public string PhoneNumber 
		{
			get 
			{
				return sPhoneNumber;
			}
			set 
			{
				sPhoneNumber = value;
			}
		}

		public string BestTimeToCall
		{
			get
			{
				return sBestTimeToCall;
			}
			set 
			{
				sBestTimeToCall = value;
			}
		}
	}
}
