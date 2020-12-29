namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("touch")]
    public partial class touch
    {
        [Key]
        public int touch_id { get; set; }

        public int? event_participation_id { get; set; }

        public int? member_hierarchy_id { get; set; }

        public int touch_info_id { get; set; }

        public Byte processed { get; set; }

        public DateTime? create_date { get; set; }

        public Guid msrepl_tran_version { get; set; }

        public bool? delivery_confirmation { get; set; }

        public virtual event_participation event_participation { get; set; }

        public virtual member_hierarchy member_hierarchy { get; set; }

        public virtual touch_info touch_info { get; set; }

        public DateTime? delivery_date { get; set; }

        public int? external_status { get; set; }
    }
}
