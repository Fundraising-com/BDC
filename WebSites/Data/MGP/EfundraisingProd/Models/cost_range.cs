namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class cost_range
    {
        [Key]
        public int cost_range_id { get; set; }

        public int scratch_book_id { get; set; }

        public byte? service_type_id { get; set; }

        public int? minimum { get; set; }

        public int? maximum { get; set; }

        public double? cost { get; set; }

        public decimal? margin_plan { get; set; }
    }
}
