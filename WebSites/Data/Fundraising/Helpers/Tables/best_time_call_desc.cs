namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class best_time_call_desc
    {
        [Key]
        [Column(Order = 0)]
        public byte best_time_call_id { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte language_id { get; set; }

        [Required]
        [StringLength(25)]
        public string description { get; set; }

        public virtual best_time_call best_time_call { get; set; }

        public virtual language language { get; set; }
    }
}
