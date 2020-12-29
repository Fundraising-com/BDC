using System;
using System.Collections.Generic;

namespace GA.BDC.Core.ESubsGlobal.Reports
{
    [Serializable]
    public class TopSellersStats : StatsBase
    {
        #region Private Fields
        private int _counter;
        private string _name;
        private int _subsSold;
        private decimal _totalAmount;
        private decimal _totalDonationAmount;
        private DateTime _createDate;
        #endregion

        #region Constructor
        public TopSellersStats() : this(1, string.Empty, 0, 0M, 0M, System.DateTime.MinValue) { }
        public TopSellersStats(int counter, string name, int subsSold, decimal totalAmount, decimal totalDonationAmount, DateTime createDate) 
        {
            _counter = counter;
            _name = name;
            _subsSold = subsSold;
            _totalAmount = totalAmount;
            _totalDonationAmount = totalDonationAmount;
            _createDate = createDate;
        }
        #endregion

        #region Public/Private Properties
        public int Counter
        {
            get { return _counter; }
            set { _counter = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int SubscriptionsSold
        {
            get { return _subsSold; }
            set { _subsSold = value; }
        }

        public decimal TotalAmount
        {
            get { return _totalAmount; }
            set { _totalAmount = value; }
        }

        public decimal TotalDonationAmount
        {
            get { return _totalDonationAmount; }
            set { _totalDonationAmount = value; }
        }

        public DateTime CreateDate
        {
            get { return _createDate; }
            set { _createDate = value; }
        }

        public string DisplayText
        {
            get { return _counter.ToString()+". " + _name.Trim() + " - <b>" + _subsSold.ToString() + " item(s) sold</b>"; }
        }

        public string DisplayDonationText
        {
            get { return _counter.ToString() + ". " + _name.Trim() + " - <b>" + TotalDonationAmount.ToString("$###,###,##0.00") + "</b>"; }
        }
        #endregion

        #region Public/Private Functions
        public static List<StatsBase> Load(eSubsGlobalEnvironment env)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetCampaignManagerStatsByReportType(CampaignManagerReport.ReportName.TOP_SELLERS, env.Event.EventID);
        }
        #endregion
    }
}
