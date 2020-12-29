namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class touch_info
    {
        public touch_info()
        {
            custom_email_template = new HashSet<custom_email_template>();
            touches = new HashSet<touch>();
        }

        [Key]
        public int touch_info_id { get; set; }

        public int? business_rule_id { get; set; }

        public int? visitor_log_id { get; set; }

        public DateTime launch_date { get; set; }

        public DateTime create_date { get; set; }

        public int? reminder_interval_day { get; set; }

        public virtual business_rule business_rule { get; set; }

        public virtual ICollection<custom_email_template> custom_email_template { get; set; }

        public virtual ICollection<touch> touches { get; set; }
    }
}
