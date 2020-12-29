namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AR_Activity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AR_Activity_ID { get; set; }

        public int AR_Activity_Type_ID { get; set; }

        public int Sales_ID { get; set; }

        public int AR_Consultant_ID { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime AR_Activity_Date { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Completed_Date { get; set; }

        [StringLength(255)]
        public string Comments { get; set; }

        public virtual AR_Activity_Type AR_Activity_Type { get; set; }

        public virtual consultant consultant { get; set; }

        public virtual sale sale { get; set; }
    }
}
