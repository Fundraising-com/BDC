using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.Fundraising.FastFundraising.Tables
{
    [Table("fundraiser_category")]
    public partial class fundraiser_category
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        [Required]
        [StringLength(100)]
        public string image { get; set; }

        public int display_order { get; set; }
        public string url { get; set; }
    }
}
