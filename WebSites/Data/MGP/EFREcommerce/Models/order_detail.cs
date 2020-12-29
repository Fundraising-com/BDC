namespace GA.BDC.Data.MGP.EFREcommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class order_detail
    {
        public order_detail()
        {
            recurrence_order_detail = new HashSet<recurrence_order_detail>();
            recurrence_order_detail1 = new HashSet<recurrence_order_detail>();
        }

        [Key]
        public int order_detail_id { get; set; }

        public int order_id { get; set; }

        public int product_id { get; set; }

        public int status_id { get; set; }

        public int? shipment_group_id { get; set; }

        public int? reoccurance_plan_id { get; set; }

        public int? reoccurance_status_id { get; set; }

        public int quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal price { get; set; }

        public bool deleted { get; set; }

        public DateTime create_date { get; set; }

        public virtual order order { get; set; }

        public virtual product product { get; set; }

        public virtual recurrence_plan recurrence_plan { get; set; }

        public virtual status status { get; set; }

        public virtual shipment_group shipment_group { get; set; }

        public virtual status status1 { get; set; }

        public virtual ICollection<recurrence_order_detail> recurrence_order_detail { get; set; }

        public virtual ICollection<recurrence_order_detail> recurrence_order_detail1 { get; set; }
    }
}
