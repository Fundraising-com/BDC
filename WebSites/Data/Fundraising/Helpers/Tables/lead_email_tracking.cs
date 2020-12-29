namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class lead_email_tracking
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int lead_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int sale_id { get; set; }

        public bool order_confirmation { get; set; }

        public bool order_shipped { get; set; }

        public bool follow_up { get; set; }

        public bool back_order_notification { get; set; }

        public bool issue_reported { get; set; }
    }
}
