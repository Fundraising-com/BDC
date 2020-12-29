namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class scratch_book_commission
    {
        [Key]
        public int scratch_book_commission_id { get; set; }

        public int? scratch_book_id { get; set; }

        public decimal? commission_rate { get; set; }

        public decimal? commission_rate_ca { get; set; }

        public DateTime create_date { get; set; }
    }
}
