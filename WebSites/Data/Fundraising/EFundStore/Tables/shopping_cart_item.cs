namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class shopping_cart_item
    {
        [Key]       
        public int id { get; set; }
        [Required]
        public int shopping_cart_id { get; set; }
        [Required]
        public int product_id { get; set; }
        [Required]
        public int quantity { get; set; }
        [StringLength(200)]
        public string comments { get; set; }
        [Required]
        public DateTime created { get; set; }
    }
}
