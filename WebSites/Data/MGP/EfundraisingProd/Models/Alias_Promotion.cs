namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Alias_Promotion
    {
        [Key]
        [StringLength(255)]
        public string Cookie_Content { get; set; }

        public int Promotion_ID { get; set; }
    }
}
