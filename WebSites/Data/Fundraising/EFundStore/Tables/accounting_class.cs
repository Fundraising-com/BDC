namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class accounting_class
    {
        [Key]
        public byte accounting_class_id { get; set; }

        public byte carrier_id { get; set; }

        public byte shipping_option_id { get; set; }

        [Required]
        [StringLength(50)]
        public string description { get; set; }

        public int rank { get; set; }

        public byte? delivery_days { get; set; }

        public byte? shipping_fees { get; set; }

        public int? free_shipping_amount { get; set; }
    }
}
