namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class participation_channel
    {
        public participation_channel()
        {
            event_participation = new HashSet<event_participation>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int participation_channel_id { get; set; }

        [Required]
        [StringLength(50)]
        public string participation_channel_name { get; set; }

        public DateTime create_date { get; set; }

        public virtual ICollection<event_participation> event_participation { get; set; }
    }
}
