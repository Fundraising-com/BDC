namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class consultant_address
    {
        [Key]
        public short consultant_address_id { get; set; }

        public int consultant_id { get; set; }

        [Required]
        [StringLength(10)]
        public string country_code { get; set; }

        [Required]
        [StringLength(10)]
        public string state_code { get; set; }

        [Required]
        [StringLength(100)]
        public string street_address { get; set; }

        [Required]
        [StringLength(25)]
        public string city { get; set; }

        [Required]
        [StringLength(15)]
        public string zip_code { get; set; }

        public DateTime date_inserted { get; set; }

        public virtual consultant consultant { get; set; }
    }
}
