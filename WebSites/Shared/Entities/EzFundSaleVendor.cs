using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.BDC.Shared.Entities
{
    public class EzFundSaleVendor
    {
        public EzFundSaleVendor()
        {
            Items = new List<SubProduct>();
        }
        /// <summary>
        /// Sub Order Id
        /// </summary>
        public int SubOrderId { get; set; }
        /// <summary>
        /// Product Code
        /// </summary>
        public string ProductCode { get; set; }
        /// <summary>
        /// OFRM Code
        /// </summary>
        public string OFRMCode { get; set; }
        /// <summary>
        /// Vendor Code
        /// </summary>
        public string VendorCode { get; set; }
        /// <summary>
        /// Warehouse Code
        /// </summary>
        public string WarehouseCode { get; set; }
        /// <summary>
        /// Purchase Order Number
        /// </summary>
        public string PONumber { get; set; }
        /// <summary>
        /// Purchase Order Date
        /// </summary>
        public DateTime PODate { get; set; }
        /// <summary>
        /// Purchase Order Confirmation Number
        /// </summary>
        public string POConfirmationNumber { get; set; }
        /// <summary>
        /// Purchase Order Confirmation Date
        /// </summary>
        public DateTime POConfirmationDate { get; set; }
        /// <summary>
        /// Bill Received Date
        /// </summary>
        public DateTime BillReceivedDate { get; set; }
        /// <summary>
        /// Bill Received Date
        /// </summary>
        public bool SentToVendorFlag { get; set; }
        /// <summary>
        /// Bill Received Date
        /// </summary>
        public bool ShowRebtFlag { get; set; }
        /// <summary>
        /// Purchase Order Confirmation Number
        /// </summary>
        public string DeliveryComment { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public EzFundSaleStatus Status { get; set; }
        /// <summary>
        /// Items
        /// </summary>
        public IList<SubProduct> Items { get; set; }
    }
}
