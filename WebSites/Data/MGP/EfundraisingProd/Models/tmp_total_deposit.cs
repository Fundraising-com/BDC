namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tmp_total_deposit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Sales_ID { get; set; }

        public decimal? Total_Deposit { get; set; }
    }
}
