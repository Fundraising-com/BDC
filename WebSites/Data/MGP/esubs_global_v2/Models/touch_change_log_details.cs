namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class touch_change_log_details
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int touch_change_log_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int email_template_field_id { get; set; }

        [Required]
        [StringLength(8000)]
        public string value { get; set; }

        public bool prod_refreshed { get; set; }

        [StringLength(100)]
        public string refreshed_by { get; set; }

        public DateTime? refreshed_date { get; set; }

        public DateTime create_date { get; set; }

        public virtual email_template_field email_template_field { get; set; }

        public virtual touch_change_log touch_change_log { get; set; }
    }
}
