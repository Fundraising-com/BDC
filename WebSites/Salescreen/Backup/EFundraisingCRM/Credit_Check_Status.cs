using System;

namespace efundraising.EFundraisingCRM
{
	/// <summary>
	/// Summary description for Credit_Check_Status.
	/// </summary>

	public enum CreditStatus
	{
		Accepted =1,
		Denied,
		Denied_Partially,
		Pending,
		No_Match
	}

	public class CreditCheckStatus : EFundraisingCRMDataObject
	{

        private int creditCheckStatusID;
		private string description;

		public CreditCheckStatus() : this(int.MinValue) { }
		public CreditCheckStatus(int creditCheckStatusID) : this(creditCheckStatusID, null) { }
		public CreditCheckStatus(int creditCheckStatusID, string description) 
		{
			this.creditCheckStatusID = CreditCheckStatusID;
			this.description = description;
		}


		#region Data Source Methods
		public static CreditCheckStatus GetCreditCheckStatusByID(int creditCheckStatusID) 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCreditCheckStatusByID(creditCheckStatusID);
		}


		public static CreditCheckStatus[] GetCreditCheckStatus() 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCreditCheckStatus();
		}

		#endregion

		#region Properties
		public int CreditCheckStatusID
		{
			set { creditCheckStatusID = value; }
			get { return creditCheckStatusID; }
		}

		public string Description
		{
			set { description = value; }
			get { return description; }
		}
		#endregion
	}
}
