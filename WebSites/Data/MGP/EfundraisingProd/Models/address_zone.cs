namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class address_zone
    {
        [Key]
        public int address_zone_id { get; set; }

        [Required]
        [StringLength(255)]
        public string description { get; set; }
    }
}
