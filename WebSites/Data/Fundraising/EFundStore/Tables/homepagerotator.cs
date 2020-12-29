using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    [Table("home_page_rotator")]
    public partial class homepagerotator
    {
        [Key]
        public int id { get; set; }

        [Required]
        [MaxLength(255)]
        public string image { get; set; }

        [Required]
        [MaxLength(255)]
        public string url { get; set; }

        [MaxLength(255)]
        public string alternative_text { get; set; }

        public DateTime created_on { get; set; }

        [Required]
        public bool is_active { get; set; }

        public int partner_id { get; set; }

        public int sort_order { get; set; }
    }
}

