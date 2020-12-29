namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class stats_personalization
    {
        [Key]
        public int stats_personalization_id { get; set; }

        public int event_participation_id { get; set; }

        public int stats_personalization_item_id { get; set; }

        public int? stats_personalization_section_id { get; set; }

        public DateTime create_date { get; set; }

        public virtual event_participation event_participation { get; set; }

        public virtual stats_personalization_item stats_personalization_item { get; set; }

        public virtual stats_personalization_section stats_personalization_section { get; set; }
    }
}
