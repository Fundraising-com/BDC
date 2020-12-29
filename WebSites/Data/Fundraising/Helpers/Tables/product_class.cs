namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class product_class
    {
        public product_class()
        {
            Packages = new HashSet<Package>();
            Partner_Sales_Commission = new HashSet<Partner_Sales_Commission>();
            product_business_rule = new HashSet<product_business_rule>();
            sales_item = new HashSet<sales_item>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int product_class_id { get; set; }

        public byte division_id { get; set; }

        public byte accounting_class_id { get; set; }

        [Required]
        [StringLength(50)]
        public string description { get; set; }

        [StringLength(10)]
        public string product_code { get; set; }

        [StringLength(100)]
        public string display_name { get; set; }

        public bool is_displayable { get; set; }

        public byte minimum_order_qty { get; set; }

        public bool? tax_exempt { get; set; }

        public virtual ICollection<Package> Packages { get; set; }

        public virtual ICollection<Partner_Sales_Commission> Partner_Sales_Commission { get; set; }

        public virtual ICollection<product_business_rule> product_business_rule { get; set; }

        public virtual ICollection<sales_item> sales_item { get; set; }
    }
}
