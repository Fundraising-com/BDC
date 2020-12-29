namespace GA.BDC.Data.Fundraising.EFundStore.Tables
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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int package_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string culture_code { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        [Required]
        [StringLength(1000)]
        public string short_desc { get; set; }

        [Required]
        [StringLength(4000)]
        public string long_desc { get; set; }

        [StringLength(4000)]
        public string extra_desc { get; set; }

        [StringLength(100)]
        public string page_name { get; set; }

        [StringLength(100)]
        public string image_name { get; set; }

        public int? template_id { get; set; }

        [StringLength(200)]
        public string page_title { get; set; }

        [StringLength(100)]
        public string image_alt_text { get; set; }

        public int? display_order { get; set; }

        public bool? enabled { get; set; }

        [StringLength(4000)]
        public string configuration { get; set; }

        public DateTime? create_date { get; set; }
    }
}
