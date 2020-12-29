namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class country_names
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(2)]
        public string country_code { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte language_id { get; set; }

        [Required]
        [StringLength(50)]
        public string country_name { get; set; }

        public virtual country country { get; set; }

        public virtual language language { get; set; }
    }
}
