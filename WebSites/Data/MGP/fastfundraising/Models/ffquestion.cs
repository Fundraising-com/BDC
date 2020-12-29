namespace GA.BDC.Data.MGP.fastfundraising.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ffquestion
    {
        [StringLength(50)]
        public string fname { get; set; }

        [StringLength(255)]
        public string email { get; set; }

        [StringLength(1000)]
        public string question { get; set; }

        public DateTime? entrydate { get; set; }

        [Key]
        public int rowid { get; set; }
    }
}
