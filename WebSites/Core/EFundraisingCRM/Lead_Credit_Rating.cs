using System;

namespace GA.BDC.Core.EFundraisingCRM
{
	/// <summary>
	/// Summary description for Lead_Credit_Rating.
	/// </summary>
	/// 

	public enum CreditRating
	{
		Standard =1,
		Good,
		Bad
	}


	public class LeadCreditRating
	{

        private int leadCreditRatingID;
		private string description;

		public LeadCreditRating()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		
		#region Data Source Methods
		public static LeadCreditRating[] GetLeadCreditRating() 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLeadCreditRating();
		}

		
		public static LeadCreditRating GetLeadCreditRatingByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLeadCreditRatingByID(id);
		}

		
		#endregion

		#region Properties
		public int LeadCreditRatingID 
		{
			set { leadCreditRatingID = value; }
			get { return leadCreditRatingID; }
		}

		public string Description
		{
			set { description  = value; }
			get { return description; }
		}

		#endregion
	}
}
