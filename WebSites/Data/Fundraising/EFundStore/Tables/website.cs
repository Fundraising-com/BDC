namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("website")]
    public partial class website
    {
        [Key]
        public short website_id { get; set; }

        public int partner_id { get; set; }

        public byte webproject_id { get; set; }

        [Required]
        [StringLength(50)]
        public string website_dns { get; set; }
    }
}
