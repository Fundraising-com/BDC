namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("culture")]
    public partial class culture
    {
        [Key]
        [StringLength(5)]
        public string culture_code { get; set; }

        [Required]
        [StringLength(2)]
        public string country_code { get; set; }

        [Required]
        [StringLength(2)]
        public string language_code { get; set; }

        [Required]
        [StringLength(50)]
        public string culture_name { get; set; }
    }
}
