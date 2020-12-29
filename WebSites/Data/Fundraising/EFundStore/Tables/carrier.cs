namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("carrier")]
    public partial class carrier
    {
        [Key]
        public byte carrier_id { get; set; }

        [Required]
        [StringLength(50)]
        public string description { get; set; }
    }
}
