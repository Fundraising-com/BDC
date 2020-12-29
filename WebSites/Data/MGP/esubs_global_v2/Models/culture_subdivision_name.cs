namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class culture_subdivision_name
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(5)]
        public string culture_code { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(7)]
        public string subdivision_code { get; set; }

        [Required]
        [StringLength(100)]
        public string subdivision_name { get; set; }

        public virtual culture culture { get; set; }

        public virtual subdivision subdivision { get; set; }
    }
}
