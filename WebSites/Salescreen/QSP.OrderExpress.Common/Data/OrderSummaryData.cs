using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSP.OrderExpress.Common.Data
{
    [Serializable]
    public class OrderSummaryData
    {
        public decimal SubTotal { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal ShippingCharges { get; set; }
        public decimal Surcharges { get; set; }
        public decimal Credits { get; set; }
        public decimal GrandTotal { get; set; }
    }
}
