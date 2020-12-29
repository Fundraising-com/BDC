namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Price_Range
    {
        [Key]
        public int Price_Range_ID { get; set; }

        public int Package_ID { get; set; }

        public int Minimum_Qty { get; set; }

        public int Maximum_Qty { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal Unit_Price_Sold { get; set; }

        public virtual Package Package { get; set; }
    }
}
