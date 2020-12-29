using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.BDC.Shared.Entities
{
    public class EzFundSale
    {
        public EzFundSale()
        {
            Items = new List<EzFundSaleItem>();
            Vendors = new List<EzFundSaleVendor>();
            SubProducts = new List<SubProduct>();
            MatchingOrganizations = new Dictionary<int,int>();
        }
        /// <summary>
        /// Order Id
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// Workflow Process Id 
        /// </summary>
        public int ProcessId { get; set; }
        /// <summary>
        /// Order Type Code
        /// </summary>
        public string OrderTypeCode { get; set; }
        /// <summary>
        /// Organization Id - All should be related to an Organization
        /// </summary>
        public int OrganizationId { get; set; }
        /// <summary>
        /// Sale Tax Amount
        /// </summary>
        public decimal SaleTaxAmount { get; set; }
        /// <summary>
        /// Shipping Charge Calculated Amount Tax Amount
        /// </summary>
        public decimal ShippingCalculatedAmount { get; set; }
        /// <summary>
        /// Shipping Charge Over Amount Tax Amount
        /// </summary>
        public decimal ShippingOverAmount { get; set; }
        /// <summary>
        /// Total Charged Amount
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// Payment Method 
        /// </summary>
        public PaymentMethod PaymentMethod { get; set; }
        /// <summary>
        /// Internal Payment Method (Types of Credit Cards, Purchase Order, etc)
        /// </summary>
        public EzFundInternalPaymentMethod InternalPaymentMethod { get; set; }
        /// <summary>
        /// Credit Card Information
        /// </summary>
        public CreditCard CreditCard { get; set; }
        /// <summary>
        /// Sale Authorization Number
        /// </summary>
        public string SaleAuthorizationNumber { get; set; }
        /// <summary>
        /// Client
        /// </summary>
        public Client Client { get; set; }
        /// <summary>
        /// Referral
        /// </summary>
        public string ReferralCode { get; set; }
        /// <summary>
        /// Consultant
        /// </summary>
        public string DeliveryComments { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public EzFundSaleStatus Status { get; set; }
        /// <summary>
        /// Items
        /// </summary>
        public IList<EzFundSaleItem> Items { get; set; }
        /// <summary>
        /// Vendors
        /// </summary>
        public IList<EzFundSaleVendor> Vendors { get; set; }
        /// <summary>
        /// Sale Subproducts
        /// </summary>
        public IList<SubProduct> SubProducts { get; set; }
        /// <summary>
        /// Matching Organizations Ids
        /// </summary>
        public IDictionary<int,int> MatchingOrganizations { get; set; }
    }
}
