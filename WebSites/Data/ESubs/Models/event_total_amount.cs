namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class event_total_amount
    {
        public int id { get; set; }

        public int event_id { get; set; }

        public int items { get; set; }

        [Column(TypeName = "money")]
        public decimal total_amount { get; set; }

        public int total_supporters { get; set; }

        [Column(TypeName = "money")]
        public decimal total_donation_amount { get; set; }

        public int total_donars { get; set; }

        [Column(TypeName = "money")]
        public decimal total_profit { get; set; }

        public DateTime create_date { get; set; }
    }
}
