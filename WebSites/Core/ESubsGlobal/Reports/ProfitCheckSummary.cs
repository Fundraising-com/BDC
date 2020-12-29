using System;
using System.Collections.Generic;

namespace GA.BDC.Core.ESubsGlobal.Reports
{
    /// <summary>
    /// Summary description for ProfitCheckSummary
    /// </summary>
    [Serializable]
    public class ProfitCheckSummary
    {
        #region Private Fields

        private string _checkNumber;
        private string _checkPeriod;
        private int _totalItemCount = int.MinValue;
        private decimal _totalAmountPurchased;
        private double _profitPercentage;
        private decimal _totalProfitAmount;
        private decimal _paidAmount;
        private int _paymentStatusID = int.MinValue;
        private string _checkSentDate;
        private int _profitID = int.MinValue;
        private int _profitRangeID = int.MinValue;

        #endregion

        #region Constructor

        public ProfitCheckSummary() { }
        public ProfitCheckSummary(string chkNum, string chkPer, int ttlItmCnt, decimal ttlAmtPur, double prfPerc, 
                                  decimal ttlPrfAmt, decimal paidAmt, int payStatID, string chkSent, int prfID, int prfRgID)
        {
            _checkNumber = chkNum;
            _checkPeriod = chkPer;
            _totalItemCount = ttlItmCnt;
            _totalAmountPurchased = ttlAmtPur;
            _profitPercentage = prfPerc;
            _totalProfitAmount = ttlPrfAmt;
            _paidAmount = paidAmt;
            _paymentStatusID = payStatID;
            _checkSentDate = chkSent;
            _profitID = prfID;
            _profitRangeID = prfRgID;
        }

        public ProfitCheckSummary(ProfitCheckSummary copy)
        {
            _checkNumber = copy.CheckNumber;
            _checkPeriod = copy.CheckPeriod;
            _totalItemCount = copy.TotalItemCount;
            _totalAmountPurchased = copy.TotalAmountPurchased;
            _profitPercentage = copy.ProfitPercentage;
            _totalProfitAmount = copy.TotalProfitAmount;
            _paidAmount = copy.PaidAmount;
            _paymentStatusID = copy.PaymantStatusID;
            _checkSentDate = copy.CheckSentDate;
            _profitID = copy.ProfitID;
            _profitRangeID = copy.ProfitRangeID;
        }

        #endregion

        #region Public/Private Properties

        public string CheckNumber
        {
            get { return _checkNumber; }
            set { _checkNumber = value; }
        }

        public string CheckPeriod
        {
            get { return _checkPeriod; }
            set { _checkPeriod = value; }
        }

        public int TotalItemCount
        {
            get { return _totalItemCount; }
            set { _totalItemCount = value; }
        }

        public decimal TotalAmountPurchased
        {
            get { return _totalAmountPurchased; }
            set { _totalAmountPurchased = value; }
        }

        public double ProfitPercentage
        {
            get { return _profitPercentage; }
            set { _profitPercentage = value; }
        }

        public decimal TotalProfitAmount
        {
            get { return _totalProfitAmount; }
            set { _totalProfitAmount = value; }
        }

        public decimal PaidAmount
        {
            get { return _paidAmount; }
            set { _paidAmount = value; }
        }

        public int PaymantStatusID
        {
            get { return _paymentStatusID; }
            set { _paymentStatusID = value; }
        }

        public string CheckSentDate
        {
            get { return _checkSentDate; }
            set { _checkSentDate = value; }
        }

        public int ProfitID
        {
            get { return _profitID; }
            set { _profitID = value; }
        }

        public int ProfitRangeID
        {
            get { return _profitRangeID; }
            set { _profitRangeID = value; }
        }

        public string GetProfitPercentage
        {
            get { return (_profitPercentage * 100).ToString() + "%"; }
        }

        #endregion

        #region Public/Private Functions

        public static List<ProfitCheckSummary> GetCheckSummaryByID(int event_id)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetSponsorCheckSummaryReport(event_id);
        }

        #endregion
    }
}
