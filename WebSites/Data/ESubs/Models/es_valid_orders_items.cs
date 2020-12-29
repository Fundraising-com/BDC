namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class es_valid_orders_items
    {
        [Key]
        public int rownum { get; set; }

        public int? act { get; set; }

        public int? order_id { get; set; }

        public int? order_item_id { get; set; }

        public int? quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal? price { get; set; }

        [Column(TypeName = "money")]
        public decimal? total_amount { get; set; }

        [Column(TypeName = "money")]
        public decimal? sub_total { get; set; }

        [Column(TypeName = "money")]
        public decimal? tax { get; set; }

        [Column(TypeName = "money")]
        public decimal? freight { get; set; }

        [Column(TypeName = "money")]
        public decimal? redeemed_amount { get; set; }

        public int? supp_id { get; set; }

        [StringLength(255)]
        public string supp_name { get; set; }

        [StringLength(255)]
        public string first_name { get; set; }

        [StringLength(255)]
        public string last_name { get; set; }

        public int? event_id { get; set; }

        public int? product_id { get; set; }

        [StringLength(255)]
        public string product_desc { get; set; }

        public int? product_type_id { get; set; }

        [StringLength(255)]
        public string product_type_desc { get; set; }

        public DateTime? create_date { get; set; }

        public int? store_id { get; set; }
    }
}
