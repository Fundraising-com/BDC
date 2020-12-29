using System;
using System.Collections.Generic;
using System.Text;

namespace GA.BDC.Shared.Entities
{
    public class FundPassCoupon
    {
        /// <summary>
        /// CodeId
        /// </summary>
        public int CodeId { get; set; }
        /// <summary>
        /// FundraisingCodeName
        /// </summary>
        public string FundraisingCodeName { get; set; }
        /// <summary>
        /// FundraisingCode
        /// </summary>
        public string FundraisingCode { get; set; }
        /// <summary>
        /// NumberOfCoupons
        /// </summary>
        public int NumberOfCoupons { get; set; }
        /// <summary>
        /// CouponPrice
        /// </summary>
        public decimal CouponPrice { get; set; }
        /// <summary>
        /// CouponStartdate
        /// </summary>
        public DateTime CouponStartdate { get; set; }
        /// <summary>
        /// CouponEnddate
        /// </summary>
        public DateTime CouponEnddate { get; set; }
        /// <summary>
        /// LeadIdUsed
        /// </summary>
        public int LeadIdUsed { get; set; }
        /// <summary>
        /// CouponUsed
        /// </summary>
        public bool CouponUsed { get; set; }
        /// <summary>
        /// CouponUsed
        /// </summary>
        public DateTime? CouponUsedDate { get; set; }
        /// <summary>
        /// CreateDate
        /// </summary>
        public DateTime CreateDate { get; set; }
        
    }
}
