namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class lead_publipage_reconcile
    {
        [StringLength(50)]
        public string city { get; set; }

        [StringLength(100)]
        public string organization { get; set; }

        [Key]
        [StringLength(50)]
        public string email { get; set; }

        [StringLength(50)]
        public string first_name { get; set; }

        [StringLength(50)]
        public string last_name { get; set; }

        [StringLength(100)]
        public string street_address { get; set; }

        [StringLength(50)]
        public string consulant { get; set; }

        [StringLength(100)]
        public string partner_name { get; set; }
    }
}
