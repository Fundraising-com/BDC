using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSP.OrderExpress.Common.Data
{
    [Serializable]
    public class OrderDetailData
    {
        public int Number { get; set; }
        public string ItemNumber { get; set; }
        public string ItemName { get; set; }
        public int UnitsPerCase { get; set; }
        public int OrderedProCodeCases { get; set; }
        public int OrderedCases { get; set; }
        public decimal CasePrice { get; set; }
        public List<OrderDetailTaxData> OrderDetailTaxes { get; set; }
        public decimal Total
        {
            get
            {
                return (OrderedCases - OrderedProCodeCases) * CasePrice;
            }
        }
        public double TaxRate
        {
            get
            {
                double result = 0;

                if (this.OrderDetailTaxes != null)
                {
                    foreach (OrderDetailTaxData taxData in this.OrderDetailTaxes)
                    {
                        if (taxData.TaxRate.HasValue)
                        {
                            result += taxData.TaxRate.Value;
                        }
                    }
                }

                return result;
            }
        }
        public decimal TaxAmount
        {
            get
            {
                decimal result = 0;

                result = this.Total * Convert.ToDecimal(this.TaxRate);

                return result;
            }
        }
    }
}
