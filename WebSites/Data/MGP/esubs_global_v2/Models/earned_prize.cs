namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class earned_prize
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int prize_item_id { get; set; }

        public int? event_participation_id { get; set; }

        public DateTime create_date { get; set; }

        public virtual event_participation event_participation { get; set; }

        public virtual prize_item prize_item { get; set; }
    }
}
