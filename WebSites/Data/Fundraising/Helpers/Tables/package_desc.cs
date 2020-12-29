namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class package_desc
    {
        [Key]
        [Column(Order = 0)]
        public byte package_id { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte language_id { get; set; }

        [Required]
        [StringLength(50)]
        public string package_name { get; set; }

        [Required]
        [StringLength(500)]
        public string package_short_desc { get; set; }

        [Required]
        [StringLength(1000)]
        public string package_long_desc { get; set; }

        [StringLength(500)]
        public string package_extra_desc { get; set; }

        [StringLength(25)]
        public string package_small_img { get; set; }

        [StringLength(25)]
        public string package_large_img { get; set; }

        [StringLength(50)]
        public string page_url { get; set; }

        public virtual language language { get; set; }

        public virtual package1 package { get; set; }
    }
}
