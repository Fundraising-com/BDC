using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.BDC.Shared.Entities
{
    public class ItemVendor
    {
        /// <summary>
        /// Item Code
        /// </summary>
        public string ItemCode { get; set; }
        /// <summary>
        /// Vendor Code
        /// </summary>
        public string VendorCode { get; set; }
        /// <summary>
        /// Vendor Item Code
        /// </summary>
        public string VendorItemCode { get; set; }
        /// <summary>
        /// Vendor Item Sequential Number
        /// </summary>
        public int ItemSeqNumber { get; set; }
        /// <summary>
        /// Item Cost Amount
        /// </summary>
        public decimal ItemCostAmount { get; set; }
    }
}
