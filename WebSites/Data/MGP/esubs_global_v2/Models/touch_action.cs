namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class touch_action
    {
        [Key]
        public int touch_action_id { get; set; }

        public int touch_id { get; set; }

        public DateTime action_date { get; set; }

        public int action_id { get; set; }

        [Required]
        [StringLength(255)]
        public string action_desc { get; set; }

        public DateTime create_date { get; set; }

        public virtual action action { get; set; }
    }
}
