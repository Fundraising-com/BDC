namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class product_price_info
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int product_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string country_code { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime effective_date { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int product_class_id { get; set; }

        public decimal? unit_price { get; set; }
    }
}
