namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("banner_view_port")]
    public partial class banner_view_port
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int banner_id { get; set; }
        [Required]
        public int view_port_id { get; set; }
    }
}
