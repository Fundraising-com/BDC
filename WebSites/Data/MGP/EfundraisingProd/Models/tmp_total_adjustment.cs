namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tmp_total_adjustment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Sales_ID { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Adjustment_Amount { get; set; }
    }
}
