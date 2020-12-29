namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class accounting_class_shipping_fees
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

        public byte shipping_fee { get; set; }

        public virtual accounting_class accounting_class { get; set; }
    }
}
