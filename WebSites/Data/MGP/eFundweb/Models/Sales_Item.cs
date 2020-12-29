namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sales_Item
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Sales_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Sales_Item_No { get; set; }

        public int Scratch_Book_ID { get; set; }

        [StringLength(2000)]
        public string Group_Name { get; set; }

        public short Quantity_Sold { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Unit_Price_Sold { get; set; }

        public short? Quantity_Free { get; set; }

        [StringLength(2000)]
        public string Suggested_Coupons { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Sales_Amount { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Paid_Amount { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Adjusted_Amount { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Discount_Amount { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Sales_Commission_Amount { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Sponsor_Commission_Amount { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Nb_units_sold { get; set; }

        [StringLength(255)]
        public string manual_product_description { get; set; }

        public int? Service_Type_ID { get; set; }

        public int? Product_Class_ID { get; set; }
    }
}
