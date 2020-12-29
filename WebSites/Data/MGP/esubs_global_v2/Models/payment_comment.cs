namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class payment_comment
    {
        [Key]
        public int payment_comment_id { get; set; }

        public int? payment_id { get; set; }

        [StringLength(4000)]
        public string comment { get; set; }

        [StringLength(256)]
        public string nt_login { get; set; }

        public DateTime? create_date { get; set; }

        public virtual payment payment { get; set; }
    }
}
