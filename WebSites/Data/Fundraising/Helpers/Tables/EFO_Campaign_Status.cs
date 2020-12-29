namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EFO_Campaign_Status
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Campaign_ID { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime Date_To_Change { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Status_ID { get; set; }

        public int? Email_Type_ID { get; set; }

        public virtual EFO_Campaign EFO_Campaign { get; set; }

        public virtual EFO_Email_Type EFO_Email_Type { get; set; }

        public virtual EFO_Status EFO_Status { get; set; }
    }
}
