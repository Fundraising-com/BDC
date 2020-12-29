namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Alias_Country_Code
    {
        [Key]
        [StringLength(255)]
        public string Input_Country_Code { get; set; }

        [Required]
        [StringLength(10)]
        public string Country_Code { get; set; }

        public virtual Country1 Country { get; set; }
    }
}
