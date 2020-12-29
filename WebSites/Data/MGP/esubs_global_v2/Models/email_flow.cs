namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class email_flow
    {
        public email_flow()
        {
        }

        [Key]
        public int email_flow_id { get; set; }

        [Required]
        public int email_template_selector_id { get; set; }

        [Required]
        [StringLength(120)]
        public string description { get; set; }

        [Required]
        public int manual_creation_channel_id { get; set; }

        [Required]
        public int import_creation_channel_id { get; set; }

        [Required]
        [StringLength(20)]
        public string user_type_from { get; set; }

        [Required]
        [StringLength(20)]
        public string user_type_to { get; set; }

        public DateTime create_date { get; set; }
    }
}
