using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSP.OrderExpress.Common.Data
{
    [Serializable]
    public class ChargeData
    {
        public int OrderChargeId { get; set; }
        public int OrderId { get; set; }
        public int ChargeId { get; set; }
        public string ChargeName { get; set; }
        public int ChargeToId { get; set; }
        public string ChargeToName { get; set; }
        public int? AccountId { get; set; }
        public decimal? EstimatedAmount { get; set; }
        public decimal? Amount { get; set; }
        public string Comment { get; set; }

        public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public string CreateUserFirstName { get; set; }
        public string CreateUserLastName { get; set; }
    }
}
