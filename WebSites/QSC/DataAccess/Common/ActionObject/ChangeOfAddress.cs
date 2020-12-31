using System;

namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	/// <summary>
	/// Summary description for ChangeOfAddress.
	/// </summary>
	public class ChangeOfAddress:ActionObjectCommon
	{
		/*private string sFirstName;
		private string sLastName;
		private string sStreet1;
		private string sStreet2;
		private string sPostalCode;
		private string sState;
		private string sCountry;
		private string sCity;*/
		private Customer cCustomerOld;
		private Customer cCustomer;
		private int iProblemCode;

		public ChangeOfAddress( Customer CustomerOldInfo, Customer CustomerInfo,
			string UserID,int COHInstance,int TransID, int ProblemCode)
		{
			this.cCustomerOld = CustomerOldInfo;
			this.cCustomer = CustomerInfo;
			this.sUserID = UserID;
			this.iCOHInstance = COHInstance;
			this.iTransID = TransID;
			this.iProblemCode = ProblemCode;
		
		}

		public Customer CustomerOldInfo
		{
			get {return cCustomerOld;}
		}

			public Customer CustomerInfo
		{
			get {return cCustomer;}
		}

		public int ProblemCode 
		{
			get {return iProblemCode;}
			set {iProblemCode = value;}
		}
	}
}
