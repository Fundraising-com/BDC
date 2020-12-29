namespace GA.BDC.Data.MGP.EFRCommon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class product_package
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int product_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int package_id { get; set; }

        public byte? display_order { get; set; }

        public bool display { get; set; }

        public DateTime? create_date { get; set; }

        public virtual package package { get; set; }

        public virtual product product { get; set; }
    }
}
