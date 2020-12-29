namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BeFree")]
    public partial class BeFree
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string Merchant_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(1)]
        public string Record_Type { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime Date_Insert { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(20)]
        public string Source_ID { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(20)]
        public string Transaction_ID { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(20)]
        public string Product_Key { get; set; }

        [Key]
        [Column(Order = 6)]
        public decimal Qty_Product { get; set; }

        [Key]
        [Column(Order = 7)]
        public decimal Unit_Price { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(3)]
        public string Currency_Type { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(20)]
        public string Merchandise_Type { get; set; }
    }
}
