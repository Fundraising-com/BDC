namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class payment_status
    {
        public payment_status()
        {
            payment_payment_status = new HashSet<payment_payment_status>();
        }

        [Key]
        public int payment_status_id { get; set; }

        [StringLength(50)]
        public string description { get; set; }

        public virtual ICollection<payment_payment_status> payment_payment_status { get; set; }
    }
}
