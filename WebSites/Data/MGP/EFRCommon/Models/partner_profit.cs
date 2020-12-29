namespace GA.BDC.Data.MGP.EFRCommon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class partner_profit
    {
        [Key]
        public int partner_profit_id { get; set; }

        public int partner_id { get; set; }

        public DateTime start_date { get; set; }

        public DateTime? end_date { get; set; }

        public int? profit_group_id { get; set; }

        public virtual profit_group profit_group { get; set; }
    }
}
