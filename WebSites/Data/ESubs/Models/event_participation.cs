namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class event_participation
    {
        public event_participation()
        {
            earned_prize = new HashSet<earned_prize>();
            personalizations = new HashSet<personalization>();
            stats_personalization = new HashSet<stats_personalization>();
            touches = new HashSet<touch>();
        }

        [Key]
        public int event_participation_id { get; set; }

        public int event_id { get; set; }

        public int member_hierarchy_id { get; set; }

        public int? participation_channel_id { get; set; }

        public DateTime create_date { get; set; }

        [StringLength(200)]
        public string salutation { get; set; }

        public int? coppa_month { get; set; }

        public int? coppa_year { get; set; }

        public bool agree_term_services { get; set; }

        public bool holiday_reminders { get; set; }

        public virtual ICollection<earned_prize> earned_prize { get; set; }

        public virtual _event _event { get; set; }

        public virtual member_hierarchy member_hierarchy { get; set; }

        public virtual participation_channel participation_channel { get; set; }

        public virtual ICollection<personalization> personalizations { get; set; }

        public virtual ICollection<stats_personalization> stats_personalization { get; set; }

        public virtual ICollection<touch> touches { get; set; }
    }
}
