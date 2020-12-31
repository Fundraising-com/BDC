using System;

namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	/// <summary>
	/// Summary description for Campaign.
	/// </summary>
	[Serializable]
	public class Campaign
	{
		int campaignID = 0;
		int accountID = 0;
		int isFmAccount =0;
		private string fmID;
		private double estimatedGrossSales;
        private int incentivesBillToID;

		public int CampaignID 
		{
			get 
			{
				return campaignID;
			}
			set 
			{
				campaignID = value;
			}
		}
		public int IsFMAccount 
		{
			get 
			{
				return isFmAccount;
			}
			set 
			{
				isFmAccount = value;
			}
		}

		public int AccountID 
		{
			get 
			{
				return accountID;
			}
			set 
			{
				accountID = value;
			}
		}

		public string FMID 
		{
			get 
			{
				return fmID;
			}
			set 
			{
				fmID = value;
			}
		}

		public double EstimatedGrossSales
		{
			get 
			{
				return estimatedGrossSales;
			}
			set 
			{
				estimatedGrossSales = value;
			}
		}
        public int IncentivesBillToID
		{
			get 
			{
                return incentivesBillToID;
			}
			set 
			{
                incentivesBillToID = value;
			}
		}
	}
}
