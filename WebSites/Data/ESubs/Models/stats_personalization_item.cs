namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class stats_personalization_item
    {
        public stats_personalization_item()
        {
            stats_personalization = new HashSet<stats_personalization>();
        }

        [Key]
        public int stats_personalization_item_id { get; set; }

        [Required]
        [StringLength(100)]
        public string description { get; set; }

        public DateTime create_date { get; set; }

        public virtual ICollection<stats_personalization> stats_personalization { get; set; }
    }
}
