namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class log_harmony_transfer_leads
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string list_name { get; set; }

        [StringLength(100)]
        public string list_desc { get; set; }

        public int? old_consultant_id { get; set; }

        public int? new_consultant_id { get; set; }

        public int? transferer_id { get; set; }

        public DateTime? transfer_date { get; set; }

        public int? lead_id { get; set; }
    }
}
