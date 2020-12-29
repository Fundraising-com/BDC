namespace GA.BDC.Data.Fundraising.EFRCommon.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class profit_range
    {
        [Key]
        public int profit_range_id { get; set; }

        public int profit_id { get; set; }

        public double profit_range_percentage { get; set; }

        public int? min_sub { get; set; }

        public int? min_amount { get; set; }

        [Column("operator")]
        [StringLength(3)]
        public string _operator { get; set; }

        [StringLength(500)]
        public string disclaimer { get; set; }
    }
}
