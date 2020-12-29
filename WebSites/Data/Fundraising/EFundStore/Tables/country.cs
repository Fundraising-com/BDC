namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("country")]
    public partial class country
    {
        [Key]
        [StringLength(2)]
        public string country_code { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        [Required]
        [StringLength(4000)]
        public string descriptive_information { get; set; }

        public bool display { get; set; }
    }
}
