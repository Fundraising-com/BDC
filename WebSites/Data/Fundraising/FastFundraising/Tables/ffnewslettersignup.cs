namespace GA.BDC.Data.Fundraising.FastFundraising.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ffnewslettersignup")]
    public partial class ffnewslettersignup
    {
        [StringLength(100)]
        public string email { get; set; }

        public DateTime? entrydate { get; set; }

        [Key]
        public int rowid { get; set; }
    }
}
