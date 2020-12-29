namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sales_item
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int sales_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short sales_item_no { get; set; }

        public int scratch_book_id { get; set; }

        public byte? service_type_id { get; set; }

        public int? product_class_id { get; set; }

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

        public decimal? profit_margin { get; set; }

        public int? participant_id { get; set; }

        public virtual participant participant { get; set; }

        public virtual product_class product_class { get; set; }

        public virtual sale sale { get; set; }

        public virtual scratch_book scratch_book { get; set; }
    }
}
