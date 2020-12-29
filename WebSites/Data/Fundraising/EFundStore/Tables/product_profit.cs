namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("product_profit")]
    public partial class product_profit
    {
        [Key]
        public int id { get; set; }
        [Required]
        public double price { get; set; }
        [Required]
        public int min_qty { get; set; }
        [Required]
        public int max_qty { get; set; }
        [Required]
        public int product_id { get; set; }
    }
}
