using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("product")]
    public partial class product
    {
        [Key]
        public int product_id { get; set; }

        [StringLength(100)]
        public string name { get; set; }

        public bool enabled { get; set; }

        [StringLength(8000)]
        public string description { get; set; }

        [StringLength(8000)]
        public string extra_information { get; set; }

        [StringLength(8000)]
        public string order_information { get; set; }

        [StringLength(200)]
        public string image_url { get; set; }

        [StringLength(200)]
        public string image_alternative_text { get; set; }

        [StringLength(200)]
        public string image_banner { get; set; }

        public bool is_store_purchasable { get; set; }

        public int? minimum_quantity { get; set; }

        public float? suggested_price { get; set; }

        public int? minimum_divisor { get; set; }

        public bool is_stacked_product { get; set; }

        [StringLength(200)]
        public string url { get; set; }

        [StringLength(200)]
        public string meta_description { get; set; }

        [StringLength(200)]
        public string meta_keywords { get; set; }

        [StringLength(200)]
        public string meta_title { get; set; }

        [StringLength(200)]
        public string canonical_url { get; set; }

        public DateTime create_date { get; set; }
        public int? shipping_fee_id { get; set; }
    }
}
