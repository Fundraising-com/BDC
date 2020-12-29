namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class group_type_desc
    {
        [Key]
        [Column(Order = 0)]
        public byte group_type_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string culture_code { get; set; }

        [StringLength(100)]
        public string description { get; set; }
    }
}
