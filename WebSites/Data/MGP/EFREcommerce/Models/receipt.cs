namespace GA.BDC.Data.MGP.EFREcommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("receipt")]
    public partial class receipt
    {
        [Key]
        public int receipt_id { get; set; }

        [StringLength(1024)]
        public string url { get; set; }

        public int order_id { get; set; }

        public virtual order order { get; set; }
    }
}
