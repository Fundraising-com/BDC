namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("subdivision")]
    public partial class subdivision
    {
        [Key]
        [StringLength(7)]
        public string subdivision_code { get; set; }

        [Required]
        [StringLength(2)]
        public string country_code { get; set; }

        [Required]
        [StringLength(100)]
        public string subdivision_name_1 { get; set; }

        [StringLength(100)]
        public string subdivision_name_2 { get; set; }

        [StringLength(100)]
        public string subdivision_name_3 { get; set; }

        [StringLength(10)]
        public string regional_division { get; set; }

        [StringLength(50)]
        public string subdivision_category { get; set; }
    }
}
