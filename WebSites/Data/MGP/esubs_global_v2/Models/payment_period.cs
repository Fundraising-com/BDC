namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class payment_period
    {
        public payment_period()
        {
            payments = new HashSet<payment>();
        }

        [Key]
        public int payment_period_id { get; set; }

        public DateTime start_date { get; set; }

        public DateTime end_date { get; set; }

        public DateTime create_date { get; set; }

        public virtual ICollection<payment> payments { get; set; }
    }
}
