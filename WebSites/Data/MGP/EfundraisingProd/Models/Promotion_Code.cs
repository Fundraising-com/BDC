namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Promotion_Code
    {
        [Key]
        public int Promotion_Code_ID { get; set; }

        [Required]
        [StringLength(25)]
        public string Promotion_Code_Desc { get; set; }
    }
}
