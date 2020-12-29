namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class salutation_desc
    {
        [Key]
        [Column(Order = 0)]
        public byte salutation_id { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte language_id { get; set; }

        [Required]
        [StringLength(15)]
        public string description { get; set; }

        public virtual language language { get; set; }

        public virtual salutation salutation { get; set; }
    }
}
