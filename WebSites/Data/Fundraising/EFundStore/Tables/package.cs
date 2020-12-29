namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("package")]
    public partial class package
    {
        [Key]
        public int package_id { get; set; }

        public int? parent_package_id { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        public byte? profit_percentage { get; set; }

        public bool enabled { get; set; }

        public DateTime? create_date { get; set; }

        [StringLength(100)]
        public string url { get; set; }

        public int order { get; set; }

        public int? shipping_fee_id { get; set; }

        public string meta_description { get; set; }

        public string meta_keywords { get; set; }

        public string meta_title { get; set; }

        public string description { get; set; }

        public string description2 { get; set; }
        
        public string link_group_key { get; set; }
    }
}
