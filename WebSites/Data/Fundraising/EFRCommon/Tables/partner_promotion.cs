namespace GA.BDC.Data.Fundraising.EFRCommon.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class partner_promotion
    {
        [Key]
        public int partner_promotion_id { get; set; }

        public int partner_id { get; set; }

        public int promotion_id { get; set; }

        public DateTime create_date { get; set; }
    }
}
