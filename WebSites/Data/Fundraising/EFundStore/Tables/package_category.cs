namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class package_category
    {
        [Key]
        public int package_category_id { get; set; }

        [StringLength(100)]
        public string package_category_title { get; set; }

        [StringLength(500)]
        public string package_category_desc { get; set; }

        [StringLength(500)]
        public string image_url { get; set; }

        public DateTime? create_date { get; set; }

        [StringLength(200)]
        public string product_url { get; set; }
    }
}
