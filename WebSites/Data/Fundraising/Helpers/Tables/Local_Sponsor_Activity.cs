namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Local_Sponsor_Activity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Local_Sponsor_Activity_ID { get; set; }

        public int Local_Sponsor_Activity_Type_ID { get; set; }

        public int Sales_ID { get; set; }

        public int Sponsor_Consultant_ID { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime Local_Sponsor_Activity_Date { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Completed_Date { get; set; }

        [StringLength(255)]
        public string Comments { get; set; }

        public int? Brand_ID { get; set; }

        public int? Local_Sponsor_ID { get; set; }

        public virtual Local_Sponsor_Activity_Type Local_Sponsor_Activity_Type { get; set; }

        public virtual Sponsor_Consultant Sponsor_Consultant { get; set; }
    }
}
