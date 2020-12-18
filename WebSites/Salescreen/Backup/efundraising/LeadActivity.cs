
//
// 2006-09-14 :  Maxime Normand - New Class
//

using System;
using efundraising.efundraisingCore.DataAccess;

namespace efundraising.efundraisingCore
{
	/// <summary>
	/// Summary description for LeadActivity.
	/// </summary>
	public class LeadActivity
	{
		private int leadActivityID;
		private int leadID;
		private int leadActivityTypeID;
		private DateTime leadActivityDate;
		private DateTime completedDate;
		private string comments;
		
		public LeadActivity()
		{}
		
		public LeadActivity(int leadID, int leadActivityTypeID, string comments)
		{
			this.leadID = leadID;
			this.leadActivityTypeID = leadActivityTypeID;
			this.comments = comments;
		}

		public static LeadActivity GetLeadActivity(int leadActivityID)
		{
            //??
			EFundDatabase dbo = new EFundDatabase();
			return null;
		}

        //public void InsertLeadActivity()
        //{
        //    EFundDatabase dbo = new EFundDatabase();
        //    dbo.InsertLeadActivity(this);
        //}

        public void InsertLeadActivity()
        {
            efundraising.EFundraisingCRM.DataAccess.EFundraisingCRMDatabase dbo = new efundraising.EFundraisingCRM.DataAccess.EFundraisingCRMDatabase();
            dbo.InsertLeadActivity(this.LeadID, this.LeadActivityTypeID, this.Comments);
        }
		
		public void UpdateLeadActivity()
		{
            //EFundDatabase dbo = new EFundDatabase();
            //dbo.UpdatetLeadActivity(this);

            efundraising.EFundraisingCRM.DataAccess.EFundraisingCRMDatabase dbo = new efundraising.EFundraisingCRM.DataAccess.EFundraisingCRMDatabase();
            dbo.UpdateLeadActivity(this.LeadActivityID, this.LeadID, this.LeadActivityTypeID, this.LeadActivityDate, this.CompletedDate, this.Comments);
		}
				
		public System.DateTime CompletedDate
		{
			get { return this.completedDate; }
			set { this.completedDate = value; }
		}

		public int LeadActivityID
		{
			get { return this.leadActivityID; }
			set { this.leadActivityID = value; }
		}

		public System.DateTime LeadActivityDate
		{
			get { return this.leadActivityDate; }
			set { this.leadActivityDate = value; }
		}

		public int LeadActivityTypeID
		{
			get { return this.leadActivityTypeID; }
			set { this.leadActivityTypeID = value; }
		}

		public int LeadID
		{
			get { return this.leadID; }
			set { this.leadID = value; }
		}

		public string Comments
		{
			get { return this.comments; }
			set { this.comments = value; }
		}
	}
}
