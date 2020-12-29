using System;
using System.Collections.Generic;

namespace GA.BDC.Core.ESubsGlobal.Reports
{
    /// <summary>
    /// Summary description for ProfitCheckDetail
    /// </summary>
    [Serializable]
    public class ProfitCheckDetail
    {
        #region Private Fields

        private string _memberName;
        private string _supporterName;
        private int _paymentItemID = int.MinValue;
        private string _productTypeName;
        private decimal _orderDetailAmount;
        private decimal _profitAmount;
        private double _profitPercentage;
        private string _purchaseDate;
        private decimal _linePrice;
        private int _lineQuantity = int.MinValue;
        private int _profitID = int.MinValue;
        private int _profitRangeID = int.MinValue;

        #endregion

        #region Constructor

        public ProfitCheckDetail() { }
        public ProfitCheckDetail(string memName, string supName, int payItmID, string prdTypName, decimal ordDetAmt, decimal prfAmt, double prfPct, string prcDate, decimal linePr, int lineQty, int prfID, int prfRangID)
        {
            _memberName = memName;
            _supporterName = supName;
            _paymentItemID = payItmID;
            _productTypeName = prdTypName;
            _orderDetailAmount = ordDetAmt;
            _profitAmount = prfAmt;
            _profitPercentage = prfPct;
            _purchaseDate = prcDate;
            _linePrice = linePr;
            _lineQuantity = lineQty;
            _profitID = prfID;
            _profitRangeID = prfRangID;
        }

        #endregion

        #region Public/Private Properties

        public string MemberName
        {
            get { return _memberName; }
            set { _memberName = value; }
        }

        public string SupporterName
        {
            get { return _supporterName; }
            set { _supporterName = value; }
        }

        public int PaymentItemID
        {
            get { return _paymentItemID; }
            set { _paymentItemID = value; }
        }

        public string ProductTypeName
        {
            get { return _productTypeName; }
            set { _productTypeName = value; }
        }

        public decimal OrderDetailAmount
        {
            get { return _orderDetailAmount; }
            set { _orderDetailAmount = value; }
        }

        public decimal ProfitAmount
        {
            get { return _profitAmount; }
            set { _profitAmount = value; }
        }

        public double ProfitPercentage
        {
            get { return _profitPercentage; }
            set { _profitPercentage = value; }
        }

        public string PurchaseDate
        {
            get { return _purchaseDate; }
            set { _purchaseDate = value; }
        }

        public decimal LinePrice
        {
            get { return _linePrice; }
            set { _linePrice = value; }
        }

        public int LineQuantity
        {
            get { return _lineQuantity; }
            set { _lineQuantity = value; }
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

        public static List<ProfitCheckDetail> GetCheckDetailByID(int event_id, int check_number)
        {
            DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
            return dbo.GetSponsorCheckDetailReport(event_id, check_number);
        }

        #endregion
    }
}
