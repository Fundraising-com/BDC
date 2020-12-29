namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class scratch_book_price_info
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string country_code { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int scratch_book_id { get; set; }

        [Key]
        [Column(Order = 2)]
        public byte product_class_id { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime effective_date { get; set; }

        public decimal? unit_price { get; set; }

        public virtual Country1 Country { get; set; }

        public virtual scratch_book scratch_book { get; set; }
    }
}
