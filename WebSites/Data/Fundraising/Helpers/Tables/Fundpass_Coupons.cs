using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    [Table("Fundpass_Coupons")]
    public partial class Fundpass_Coupons
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        /// <summary>
        /// CodeId
        /// </summary>
        public int Code_ID { get; set; }
        /// <summary>
        /// FundraisingCodeName
        /// </summary>
        public string Fundraising_Code_Name { get; set; }
        /// <summary>
        /// FundraisingCode
        /// </summary>
        public string Fundraising_Code { get; set; }
        /// <summary>
        /// NumberOfCoupons
        /// </summary>
        public int Number_Of_Coupons { get; set; }
        /// <summary>
        /// CouponPrice
        /// </summary>
        public decimal Coupon_Price { get; set; }
        /// <summary>
        /// CouponStartdate
        /// </summary>
        public DateTime Coupon_Startdate { get; set; }
        /// <summary>
        /// CouponEnddate
        /// </summary>
        public DateTime Coupon_Enddate { get; set; }
        /// <summary>
        /// LeadIdUsed
        /// </summary>
        public int Lead_Id_Used { get; set; }
        /// <summary>
        /// CouponUsed
        /// </summary>
        public bool Coupon_Used { get; set; }
        /// <summary>
        /// CouponUsed
        /// </summary>
        public DateTime? Coupon_Used_Date { get; set; }
        /// <summary>
        /// CreateDate
        /// </summary>
        public DateTime Create_Date { get; set; }

    }
}
