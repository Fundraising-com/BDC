namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sales_item_to_add
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short sales_item_to_add_no { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int sale_to_add_id { get; set; }

        public int scratch_book_id { get; set; }

        public byte? service_type_id { get; set; }

        [Column(TypeName = "text")]
        public string group_name { get; set; }

        public short quantity_sold { get; set; }

        public decimal unit_price_sold { get; set; }

        public short quantity_free { get; set; }

        [Column(TypeName = "text")]
        public string suggested_coupons { get; set; }

        public decimal sales_amount { get; set; }

        public decimal paid_amount { get; set; }

        public decimal adjusted_amount { get; set; }

        public decimal? discount_amount { get; set; }

        public decimal sales_commission_amount { get; set; }

        public decimal sponsor_commission_amount { get; set; }

        public decimal? nb_units_sold { get; set; }

        [StringLength(255)]
        public string manual_product_description { get; set; }

        public virtual sale_to_add sale_to_add { get; set; }

        public virtual scratch_book scratch_book { get; set; }

        public virtual service_type service_type { get; set; }
    }
}
