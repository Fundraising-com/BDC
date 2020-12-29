namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class email_flow_template
    {
        public email_flow_template()
        {
        }

        [Key]
        public int email_flow_template_id { get; set; }

        [Required]
        public int email_flow_id { get; set; }

        [Required]
        public int event_type_id { get; set; }

        [Required]
        public int email_template_id { get; set; }

        [Required]
        public int business_rule_id { get; set; }

        public int? override_manual_creation_channel_id { get; set; }

        public int? override_import_creation_channel_id { get; set; }

        public DateTime create_date { get; set; }
    }
}
