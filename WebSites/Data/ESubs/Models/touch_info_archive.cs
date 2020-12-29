namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class touch_info_archive
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int touch_info_id { get; set; }

        public int? business_rule_id { get; set; }

        public int? visitor_log_id { get; set; }

        public DateTime launch_date { get; set; }

        public DateTime create_date { get; set; }

        public int? reminder_interval_day { get; set; }
    }
}
