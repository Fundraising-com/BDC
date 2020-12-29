using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("blog.post")]
    public partial class blog_post
    {

        [Key]
        public int id { get; set; }

        [Required]
        public string title { get; set; }

        [Required]
        public string url { get; set; }

        [Required]
        public string author { get; set; }

        [Required]
        public string text { get; set; }

        [Required]
        public string summary { get; set; }

        public DateTime created { get; set; }

        public DateTime published { get; set; }

        [Required]
        public bool is_draft { get; set; }

        [Required]
        public int category_id { get; set; }

        [Required]
        public string image_url { get; set; }

        [Required]
        public string thumbnail_url { get; set; }

        public string meta_description { get; set; }

        public string meta_title { get; set; }

    }
}
