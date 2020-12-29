using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("blog.posts_tags")]
    public partial class post_tag
    {


        [Key]
        public int ID { get; set; }

        [Required]
        public int post_id { get; set; }

        [Required]
        public int tag_id { get; set; }


    }
}
