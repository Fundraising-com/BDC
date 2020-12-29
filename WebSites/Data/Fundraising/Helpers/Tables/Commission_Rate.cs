namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Commission_Rate
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Consultant_ID { get; set; }

        public decimal Commission_Rate_Free { get; set; }

        public decimal Commission_Rate_No_Free { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Scratch_Book_ID { get; set; }

        public virtual consultant consultant { get; set; }

        public virtual scratch_book scratch_book { get; set; }
    }
}
