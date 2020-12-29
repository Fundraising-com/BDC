namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("salutation")]
    public partial class salutation
    {
        [Key]
        public byte salutation_id { get; set; }

        [StringLength(10)]
        public string description { get; set; }
    }
}
