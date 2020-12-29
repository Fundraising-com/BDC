namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Lead_Personalized_050403
    {
        [Key]
        [StringLength(100)]
        public string GoodEmail { get; set; }
    }
}
