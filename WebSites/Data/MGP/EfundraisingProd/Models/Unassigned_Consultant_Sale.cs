namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Unassigned_Consultant_Sale
    {
        [Key]
        public int Unassignation_ID { get; set; }

        public int Sale_ID { get; set; }

        public int? Old_Consultant_ID { get; set; }

        public int? New_Consultant_ID { get; set; }

        public DateTime? Unassigned_Date { get; set; }
    }
}
