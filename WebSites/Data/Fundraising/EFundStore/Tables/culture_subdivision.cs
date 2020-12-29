namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class culture_subdivision
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(5)]
        public string culture_code { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(7)]
        public string subdivision_code { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }
    }
}
