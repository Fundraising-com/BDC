namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class temp_touch_info
    {
        [Key]
        public int touch_info_id { get; set; }

        public int? touch_id { get; set; }

        public int? event_participation_id { get; set; }

        public bool? processed { get; set; }

        public short? rule_id { get; set; }

        public DateTime? create_date { get; set; }
    }
}
