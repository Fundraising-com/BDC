namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Lead_Promotion
    {
        [Key]
        public int Lead_Promotion_Id { get; set; }

        public int Lead_Id { get; set; }

        public int Promotion_Id { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime Entry_Date { get; set; }

        public virtual lead lead { get; set; }
    }
}
