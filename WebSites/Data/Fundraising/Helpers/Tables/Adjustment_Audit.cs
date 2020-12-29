namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Adjustment_Audit
    {
        [Key]
        public int AUDIT_ID { get; set; }

        [Required]
        [StringLength(2)]
        public string AUDIT_OPERATION { get; set; }

        [StringLength(50)]
        public string HOST { get; set; }

        [Required]
        [StringLength(50)]
        public string AUDIT_USERID { get; set; }

        public DateTime AUDIT_DATETIME { get; set; }

        public int? Sales_ID { get; set; }

        public int? Adjustment_No { get; set; }

        public int? Reason_ID { get; set; }

        public DateTime? Adjustment_Date { get; set; }

        public decimal? Adjustment_Amount { get; set; }

        [Column(TypeName = "text")]
        public string Comment { get; set; }

        public decimal? Adjustment_On_Shipping { get; set; }

        public decimal? Adjustment_On_Taxes { get; set; }

        public decimal? Adjustment_On_Sale_Amount { get; set; }

        public DateTime? Create_Date { get; set; }

        public int? Create_User_ID { get; set; }

        public int? Ext_Adjustment_Id { get; set; }

        public int? charge_id { get; set; }
    }
}
