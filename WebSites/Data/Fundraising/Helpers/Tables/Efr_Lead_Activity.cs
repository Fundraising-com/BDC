namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Efr_Lead_Activity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Lead_Activity_Id { get; set; }

        public int Lead_Id { get; set; }

        public int Lead_Activity_Type_Id { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime Lead_Activity_Date { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Completed_Date { get; set; }

        [Column(TypeName = "text")]
        public string Comments { get; set; }

        public virtual lead lead { get; set; }

        public virtual Lead_Activity_Type Lead_Activity_Type { get; set; }
    }
}
