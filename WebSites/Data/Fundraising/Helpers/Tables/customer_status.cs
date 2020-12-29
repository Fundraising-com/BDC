namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class customer_status
    {
        [Key]
        public int customer_status_id { get; set; }

        [StringLength(100)]
        public string customer_status_desc { get; set; }

        public DateTime? create_date { get; set; }
    }
}
