namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Default_Consultant_Rate
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Consultant_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(4)]
        public string Promotion_Type_Code { get; set; }

        public decimal? Default_Commission_Rate { get; set; }

        public virtual consultant consultant { get; set; }
    }
}
