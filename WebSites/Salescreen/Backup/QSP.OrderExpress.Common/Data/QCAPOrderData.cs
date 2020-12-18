using System;

namespace QSP.OrderExpress.Common.Data
{
    [Serializable]
    public class QCAPOrderData
    {
        public int Id { get; set; }
        public int FormId { get; set; }
        public int CampaignId { get; set; }
        public DateTime OrderDate { get; set; }
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public string FormName { get; set; }
        public decimal OrderTotal { get; set; }
    }
}
