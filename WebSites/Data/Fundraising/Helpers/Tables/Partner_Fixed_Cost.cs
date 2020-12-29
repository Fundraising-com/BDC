namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Partner_Fixed_Cost
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Partner_ID { get; set; }

        public decimal Cost_By_Lead { get; set; }
    }
}
