namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class touch_archive
    {
        [Key]
        [Column(Order = 0)]
        public int touch_id { get; set; }

        public int? event_participation_id { get; set; }

        public int? member_hierarchy_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int touch_info_id { get; set; }

        [Key]
        [Column(Order = 2)]
        public byte processed { get; set; }

        public DateTime? create_date { get; set; }

        [Key]
        [Column(Order = 3)]
        public Guid msrepl_tran_version { get; set; }
    }
}
