namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class business_calendar
    {
        [Key]
        public DateTime business_date { get; set; }

        public int weekend { get; set; }

        public int holiday { get; set; }
    }
}
