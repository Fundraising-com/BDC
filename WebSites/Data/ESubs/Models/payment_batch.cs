namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class payment_batch
    {
        public payment_batch()
        {
            payments = new HashSet<payment>();
        }

        [Key]
        public int payment_batch_id { get; set; }

        [StringLength(1024)]
        public string filename { get; set; }

        public DateTime createdate { get; set; }

        public DateTime? confirmation_date { get; set; }

        public DateTime? cancelled_date { get; set; }

        public virtual ICollection<payment> payments { get; set; }
    }
}
