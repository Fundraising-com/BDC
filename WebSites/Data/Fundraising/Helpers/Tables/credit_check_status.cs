namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class credit_check_status
    {
        [Key]
        public int credit_check_status_id { get; set; }

        [StringLength(20)]
        public string description { get; set; }
    }
}
