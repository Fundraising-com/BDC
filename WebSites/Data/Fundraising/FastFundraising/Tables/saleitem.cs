namespace GA.BDC.Data.Fundraising.FastFundraising.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class saleitem
    {
        [Key]
        public int saleid { get; set; }

        public int? itemid { get; set; }

        [StringLength(100)]
        public string imagepath { get; set; }

        [StringLength(100)]
        public string descriptionpath { get; set; }

        public double? baseprice { get; set; }

        public int? status { get; set; }

        public DateTime? dateadded { get; set; }

        public DateTime? salestartdate { get; set; }

        public DateTime? saleenddate { get; set; }
    }
}
