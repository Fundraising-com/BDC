namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    public partial class product_desc
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int product_id { get; set; }

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

        [StringLength(100)]
        public string page_name { get; set; }

        [StringLength(100)]
        public string image_name { get; set; }

        public int? template_id { get; set; }

        [StringLength(5000)]
        public string extra_desc { get; set; }

        [StringLength(200)]
        public string page_title { get; set; }

        [StringLength(100)]
        public string image_alt_text { get; set; }

        public int? display_order { get; set; }

        public int? display_order_featured { get; set; }

        public bool? enabled { get; set; }

        [StringLength(4000)]
        public string configuration { get; set; }

        public DateTime? create_date { get; set; }
        [StringLength(200)]
        public string url { get; set; }
        [MaxLength]
        public string description { get; set; }
        [MaxLength]
        public string flavors { get; set; }
        [MaxLength]
        public string packaging { get; set; }
        public double retail_price { get; set; }
        public bool is_store_purchasable { get; set; }

        public int minimum_quantity { get; set; }

        public string extra_information { get; set; }

        public string meta_description { get; set; }

        public string meta_title { get; set; }

        public string meta_keywords { get; set; }

        public string canonical_url { get; set; }
    }
}
