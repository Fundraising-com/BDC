using System;
using System.Collections.Generic;
using System.Text;

namespace GA.BDC.Core.ESubsGlobal.Reports
{
    [Serializable]
    public class Donation
    {
        private Decimal _donationTotalAmount;
        private Decimal _donationProfit;
        private int _donationTotalNumberOfItemSold;

        public Donation()
        {
            _donationTotalAmount = 0;
            _donationProfit = 0;
            _donationTotalNumberOfItemSold = 0;
        }

        public static Donation GetDonationAmount(int donation_product_value_id, int event_participation_id, int event_id)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetDonationAmount(donation_product_value_id, event_participation_id, event_id);
        }

        #region Attributes
        public Decimal TotalAmount
        {
            set { _donationTotalAmount = value; }
            get { return _donationTotalAmount; }
        }

        public Decimal TotalProfit
        {
            set { _donationProfit = value; }
            get { return _donationProfit; }
        }

        public int TotalNumberOfItemSold
        {
            set { _donationTotalNumberOfItemSold = value; }
            get { return _donationTotalNumberOfItemSold; }
        }
        #endregion

    }
}
