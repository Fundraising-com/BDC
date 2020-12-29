namespace GA.BDC.Data.MGP.fastfundraising.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class fc_testimonial
    {
        public int id { get; set; }

        public int? fc_id { get; set; }

        public DateTime created_date { get; set; }

        [Required]
        public string comments { get; set; }

        [StringLength(400)]
        public string commentor { get; set; }

        [StringLength(400)]
        public string account { get; set; }

        public virtual FC FC { get; set; }
    }
}
