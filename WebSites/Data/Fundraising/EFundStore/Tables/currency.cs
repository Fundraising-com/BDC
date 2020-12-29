namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("currency")]
    public partial class currency
    {
        [Key]
        public int id { get; set; }

        [StringLength(2)]
        public string country_code { get; set; }

        [StringLength(3)]
        public string currency_code { get; set; }
    }
}
