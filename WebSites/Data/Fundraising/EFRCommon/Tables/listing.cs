namespace GA.BDC.Data.Fundraising.EFRCommon.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("listing")]
    public partial class listing
    {
        [Key]
        public int listing_id { get; set; }

        [StringLength(50)]
        public string description { get; set; }

        public double? listing_amount { get; set; }

        public int? listing_period { get; set; }

        public bool? is_visible { get; set; }

        public DateTime create_date { get; set; }
    }
}
