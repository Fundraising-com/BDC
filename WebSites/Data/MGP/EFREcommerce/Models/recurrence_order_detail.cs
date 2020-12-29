namespace GA.BDC.Data.MGP.EFREcommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class recurrence_order_detail
    {
        [Key]
        public int recurrence_id { get; set; }

        public int recurrence_order_detail_id { get; set; }

        public int order_detail_id { get; set; }

        public bool deleted { get; set; }

        public DateTime create_date { get; set; }

        public virtual order_detail order_detail { get; set; }

        public virtual order_detail order_detail1 { get; set; }
    }
}
