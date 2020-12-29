using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.BDC.Shared.Entities
{
    public class EzFundSaleItem
    {
        /// <summary>
        /// Order Id
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        public Product Product { get; set; }
        /// <summary>
        /// Total Invoiced Amount
        /// </summary>
        public decimal InvoicedAmount { get; set; }
        /// <summary>
        /// Total Quantity
        /// </summary>
        public int Quantity { get; set; }
    }
}
