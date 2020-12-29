namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class scratch_book
    {
        public scratch_book()
        {
            Commission_Rate = new HashSet<Commission_Rate>();
            Inventory_Adjustment = new HashSet<Inventory_Adjustment>();
            pap_scratchbook_campaign = new HashSet<pap_scratchbook_campaign>();
            product_business_rule = new HashSet<product_business_rule>();
            product_desc = new HashSet<product_desc>();
            Product_Quantity = new HashSet<Product_Quantity>();
            products_packages = new HashSet<products_packages>();
            sales_item = new HashSet<sales_item>();
            sales_item_to_add = new HashSet<sales_item_to_add>();
            scratch_book_price_info = new HashSet<scratch_book_price_info>();
            cultures = new HashSet<culture1>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int scratch_book_id { get; set; }

        public byte? product_class_id { get; set; }

        public byte? supplier_id { get; set; }

        public int package_id { get; set; }

        [Required]
        [StringLength(100)]
        public string description { get; set; }

        public decimal? raising_potential { get; set; }

        [Required]
        [StringLength(20)]
        public string product_code { get; set; }

        [StringLength(100)]
        public string current_description { get; set; }

        public bool is_active { get; set; }

        public bool is_displayable { get; set; }

        public int? total_qty { get; set; }

        public decimal? fixed_profit { get; set; }

        public bool? replicated { get; set; }

        public int? SAPMaterialNo { get; set; }

        public bool? InHouse { get; set; }

        public virtual ICollection<Commission_Rate> Commission_Rate { get; set; }

        public virtual ICollection<Inventory_Adjustment> Inventory_Adjustment { get; set; }

        public virtual Package Package { get; set; }

        public virtual ICollection<pap_scratchbook_campaign> pap_scratchbook_campaign { get; set; }

        public virtual ICollection<product_business_rule> product_business_rule { get; set; }

        public virtual ICollection<product_desc> product_desc { get; set; }

        public virtual ICollection<Product_Quantity> Product_Quantity { get; set; }

        public virtual ICollection<products_packages> products_packages { get; set; }

        public virtual ICollection<sales_item> sales_item { get; set; }

        public virtual ICollection<sales_item_to_add> sales_item_to_add { get; set; }

        public virtual ICollection<scratch_book_price_info> scratch_book_price_info { get; set; }

        public virtual ICollection<culture1> cultures { get; set; }
    }
}
