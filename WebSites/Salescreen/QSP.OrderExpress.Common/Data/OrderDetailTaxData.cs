using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSP.OrderExpress.Common.Data
{

    [Serializable]
    public class OrderDetailTaxData
    {
        public int Id { get; set; }
        public double? TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
    }
}
