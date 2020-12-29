using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("category")]
    public partial class category
    {
        [Key]
        public int category_id { get; set; }

        public int? parent_category_id { get; set; }

        [StringLength(100)]
        public string name { get; set; }

        public byte profit_percentage { get; set; }

        public bool enabled { get; set; }

        public DateTime create_date { get; set; }

        public int order { get; set; }

        [StringLength(100)]
        public string url { get; set; }

        [StringLength(8000)]
        public string description { get; set; }

        [StringLength(8000)]
        public string description2 { get; set; }

        [StringLength(1000)]
        public string short_desc { get; set; }

        [StringLength(1000)]
        public string long_desc { get; set; }

        [StringLength(4000)]
        public string extra_desc { get; set; }

        [StringLength(4000)]
        public string configuration { get; set; }

        [StringLength(100)]
        public string image_name { get; set; }

        [StringLength(100)]
        public string image_alt_text { get; set; }

        [StringLength(200)]
        public string meta_description { get; set; }

        [StringLength(200)]
        public string meta_keywords { get; set; }

        [StringLength(200)]
        public string meta_title { get; set; }
    }
}
