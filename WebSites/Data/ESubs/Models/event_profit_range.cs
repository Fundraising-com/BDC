namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class event_profit_range
    {
        [Key]
        public int event_profit_range_id { get; set; }

        public int? event_id { get; set; }

        public int? profit_id { get; set; }

        public int? profit_range_id { get; set; }

        public DateTime? create_date { get; set; }

        public DateTime? cancelled_date { get; set; }

        public bool? is_cancelled { get; set; }

        public virtual _event _event { get; set; }
    }
}
