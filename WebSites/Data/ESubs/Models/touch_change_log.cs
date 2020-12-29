namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class touch_change_log
    {
        public touch_change_log()
        {
            touch_change_log_details = new HashSet<touch_change_log_details>();
        }

        [Key]
        public int touch_change_log_id { get; set; }

        public int email_template_id { get; set; }

        [StringLength(5)]
        public string culture_code { get; set; }

        public DateTime create_date { get; set; }

        [Required]
        [StringLength(100)]
        public string created_by { get; set; }

        public virtual culture culture { get; set; }

        public virtual email_template email_template { get; set; }

        public virtual ICollection<touch_change_log_details> touch_change_log_details { get; set; }
    }
}
