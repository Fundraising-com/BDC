using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    [Table("product_image")]
    public partial class product_image
    {
        [Required, Key]
        public int id { get; set; }

        [Required]
        public int product_id { get; set; }

        [Required, MaxLength]
        public string url { get; set; }
        
        [StringLength(200)]
        public string alternative_text { get; set; }

        [Required]
        public DateTime created { get; set; }

        [Required]
        public bool enabled { get; set; }

        [Required]
        public int sort { get; set; }

        [Required]
        public bool is_cover { get; set; }

    }
}
