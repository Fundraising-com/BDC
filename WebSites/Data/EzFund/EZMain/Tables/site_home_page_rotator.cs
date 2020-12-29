using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
   
    [Table("home_page_rotator")]
    public partial class home_page_rotator
    {
        [Key]
        public int id { get; set; }

        [Required]
        [MaxLength(255)]
        public string image { get; set; }

        [MaxLength(255)]
        public string title { get; set; }

        [MaxLength(255)]
        public string subtitle { get; set; }

        [MaxLength(255)]
        public string url { get; set; }

        [MaxLength(255)]
        public string category_url { get; set; }

        [MaxLength(255)]
        public string alternative_text { get; set; }

        public DateTime created_on { get; set; }

        [Required]
        public bool is_active { get; set; }

        [Required]
        public bool is_product { get; set; }

        public int partner_id { get; set; }

        public int sort_order { get; set; }
    }
    
}
