namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class web_form
    {
        [Key]
        public int web_form_id { get; set; }

        [Required]
        [StringLength(600)]
        public string web_form_desc { get; set; }

        public int web_form_type_id { get; set; }

        public int lead_status_id { get; set; }

        [StringLength(200)]
        public string stored_proc_to_call { get; set; }

        public DateTime? datestamp { get; set; }
    }
}
