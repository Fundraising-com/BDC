namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class language_desc
    {
        [Key]
        [Column(Order = 0)]
        public byte language_id { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte display_language_id { get; set; }

        [Required]
        [StringLength(50)]
        public string language_name { get; set; }

        public virtual language language { get; set; }

        public virtual language language1 { get; set; }
    }
}
