namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class partner_payment_config
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int partner_id { get; set; }

        public int? profit_percentage { get; set; }

        public int? payment_to { get; set; }

        public int? email_template_id { get; set; }

        public int? first_email_template_id { get; set; }

        public bool is_default { get; set; }

        public int? partner_payment_info_id { get; set; }

        public bool? excluded { get; set; }

        public virtual email_template email_template { get; set; }

        public virtual email_template email_template1 { get; set; }
    }
}
