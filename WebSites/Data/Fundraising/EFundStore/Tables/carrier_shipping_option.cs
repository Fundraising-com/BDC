namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class carrier_shipping_option
    {
        [Key]
        public byte shipping_option_id { get; set; }

        [Required]
        [StringLength(50)]
        public string description { get; set; }
    }
}
