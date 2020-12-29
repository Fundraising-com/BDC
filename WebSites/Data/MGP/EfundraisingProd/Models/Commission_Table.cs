namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Commission_Table
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(4)]
        public string Promotion_Type_Code { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(4)]
        public string Channel_Code { get; set; }

        public decimal Commission_Rate { get; set; }
    }
}
