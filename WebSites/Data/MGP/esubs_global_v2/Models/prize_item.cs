namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class prize_item
    {
        [Key]
        public int prize_item_id { get; set; }

        public int prize_id { get; set; }

        [StringLength(255)]
        public string prize_item_code { get; set; }

        public DateTime expiration_date { get; set; }

        public DateTime? create_date { get; set; }

        public virtual earned_prize earned_prize { get; set; }

        public virtual prize prize { get; set; }
    }
}
