namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class custom_email_template
    {
        [Key]
        public int custom_email_template_id { get; set; }

        public int touch_info_id { get; set; }

        [StringLength(100)]
        public string subject { get; set; }

        public string body_txt { get; set; }

        public string body_html { get; set; }

        public DateTime create_date { get; set; }

        public virtual touch_info touch_info { get; set; }
    }
}
