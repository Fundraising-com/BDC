namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class campaign_reason_desc
    {
        [Key]
        [Column(Order = 0)]
        public byte campaign_reason_id { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte language_id { get; set; }

        [Required]
        [StringLength(100)]
        public string description { get; set; }

        public virtual campaign_reason campaign_reason { get; set; }

        public virtual language language { get; set; }
    }
}
