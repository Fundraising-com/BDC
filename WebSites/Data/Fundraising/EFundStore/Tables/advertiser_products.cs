using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAnnotationsExtensions;

namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    public partial class advertiser_products
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int product_id { get; set; }

        [Required]
        public int partner_id { get; set; }

        [StringLength(50)]
        public string phone_number { get; set; }

        [StringLength(1000)]
        public string kit_button_custom_url { get; set; }

        [StringLength(1000)]
        public string add_to_cart_custom_url { get; set; }

        public bool enabled { get; set; }

        public DateTime create_date { get; set; }

    }
}
