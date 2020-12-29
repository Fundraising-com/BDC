namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class hear_about_us_desc
    {
        [Key]
        [Column(Order = 0)]
        public byte hear_id { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte language_id { get; set; }

        [Required]
        [StringLength(100)]
        public string description { get; set; }

        public virtual hear_about_us hear_about_us { get; set; }

        public virtual language language { get; set; }
    }
}
