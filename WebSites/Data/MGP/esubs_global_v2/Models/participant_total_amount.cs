namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class participant_total_amount
    {
        public int id { get; set; }

        public int event_participation_id { get; set; }

        [Required]
        [StringLength(201)]
        public string participant_name { get; set; }

        public int items { get; set; }

        [Column(TypeName = "money")]
        public decimal total_amount { get; set; }

        [Column(TypeName = "money")]
        public decimal total_supporters { get; set; }

        [Column(TypeName = "money")]
        public decimal total_donation_amount { get; set; }

        [Column(TypeName = "money")]
        public decimal total_donors { get; set; }

        [Column(TypeName = "money")]
        public decimal total_profit { get; set; }

        public DateTime create_date { get; set; }
    }
}
