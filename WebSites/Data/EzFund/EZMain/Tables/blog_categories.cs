using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("blog.categories")]
    public partial class blog_categories
    {

        [Key]
        [Required]
        public int id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string url { get; set; }


    }
}
