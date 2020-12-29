namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class external_account
    {
        [Key]
        public int external_account_id { get; set; }

        public int food_account_id { get; set; }

        [StringLength(4)]
        public string fsm_id { get; set; }

        public int? online_account_id { get; set; }

        public int? event_participation_id { get; set; }

        public int? touch_id { get; set; }

        public DateTime create_date { get; set; }
    }
}
