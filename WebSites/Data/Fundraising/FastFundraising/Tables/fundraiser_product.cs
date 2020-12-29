using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.Fundraising.FastFundraising.Tables
{
    [Table("fundraiser_product")]
    public partial class fundraiser_product
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        [Required]
        [StringLength(100)]
        public string image { get; set; }

        [StringLength(100)]
        public string sell_sheet_path { get; set; }

        [StringLength(100)]
        public string store_url { get; set; }

        [StringLength(100)]
        public string nutrition_information_sheet_path { get; set; }

        [Required]
        public bool has_nutrition_information_sheet { get; set; }
        [Required]
        public bool has_sell_sheet { get; set; }
        [Required]
        public bool is_purchasable { get; set; }
        [Required]
        public int fundraiser_category_id { get; set; }
    }
}
