namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EFO_Admin
    {
        [Key]
        public int Admin_ID { get; set; }

        [Required]
        [StringLength(20)]
        public string UID { get; set; }

        [Required]
        [StringLength(32)]
        public string Password { get; set; }
    }
}
