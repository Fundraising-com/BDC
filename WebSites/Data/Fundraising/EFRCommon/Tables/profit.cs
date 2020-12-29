namespace GA.BDC.Data.Fundraising.EFRCommon.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("profit")]
    public partial class profit
    {
        [Key]
        public int profit_id { get; set; }

        public double profit_percentage { get; set; }

        [StringLength(50)]
        public string description { get; set; }

        [StringLength(500)]
        public string disclaimer { get; set; }

        [StringLength(500)]
        public string alt_disclaimer { get; set; }

        public int? profit_group_id { get; set; }

        public int? qsp_catalog_type_id { get; set; }
    }
}
