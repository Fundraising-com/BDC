namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class accounting_class_shipping_fee
    {
        [Key]
        [Column(Order = 0)]
        public byte accounting_class_id { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "numeric")]
        public decimal min_amount { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "numeric")]
        public decimal max_amount { get; set; }

        [Key]
        [Column(Order = 3)]
        public byte shipping_fee { get; set; }
    }
}
