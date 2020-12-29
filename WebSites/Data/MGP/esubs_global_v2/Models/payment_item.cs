namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class payment_item
    {
        [Key]
        public int payment_item_id { get; set; }

        public int payment_id { get; set; }

        public int qsp_order_detail_id { get; set; }

        [Column(TypeName = "money")]
        public decimal order_detail_amount { get; set; }

        public double profit_percentage { get; set; }

        [Column(TypeName = "money")]
        public decimal profit_amount { get; set; }

        public DateTime create_date { get; set; }

        public int? profit_id { get; set; }

        public int? profit_range_id { get; set; }

        public virtual payment payment { get; set; }
    }
}
