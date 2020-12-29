namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class client_sequence
    {
        [Key]
        [StringLength(2)]
        public string client_sequence_code { get; set; }

        [StringLength(50)]
        public string description { get; set; }

        public bool is_active { get; set; }
    }
}
