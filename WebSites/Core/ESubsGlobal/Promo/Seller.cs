using System;

namespace GA.BDC.Core.ESubsGlobal.Promo
{
	/// <summary>
	/// Summary description for Seller.
	/// </summary>
   public class Seller : BusinessBase.BusinessBase
	{
		protected string sellerName;
        protected string emailAddress;
        protected int eventID = int.MinValue;
        protected int quantity = int.MinValue;
		protected float amount;
		protected DateTime createDate;
		public Seller()
		{
			
		}

		public string SellerName
		{
			get { return sellerName;}
			set { sellerName = value;}
		}

        public string EmailAddress
		{
            get { return emailAddress; }
            set { emailAddress = value; }
		}

		public int EventID
		{
			get { return eventID;}
			set { eventID = value;}
		}

		public int Quantity
		{
			get { return quantity;}
			set { quantity = value;}
		}

		public float Amount
		{
			get { return amount;}
			set { amount = value;}
		}

		public DateTime CreateDate
		{
			get { return createDate;}
			set { createDate = value;}
		}

		#region Data Source Methods
		
		public static Seller[] GetTop3Sellers(int prizeID)
		{
			DataAccess.ESubsGlobalDatabase dbo = new GA.BDC.Core.ESubsGlobal.DataAccess.ESubsGlobalDatabase();
			return dbo.GetTop3Sellers(prizeID);
		}

        public static Seller GetCurrentParticipantMonthlySalesByPrizeID(int prizeID, int eventParticipationId)
        {
            DataAccess.ESubsGlobalDatabase dbo = new GA.BDC.Core.ESubsGlobal.DataAccess.ESubsGlobalDatabase();
            return dbo.GetCurrentParticipantMonthlySalesByPrizeID(prizeID, eventParticipationId);
        }

		#endregion
	}
}
