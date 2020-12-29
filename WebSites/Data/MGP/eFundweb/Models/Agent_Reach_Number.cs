namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Agent_Reach_Number
    {
        [Key]
        public int Reach_Number_ID { get; set; }

        public int? Reach_Type_ID { get; set; }

        public int? Agent_ID { get; set; }

        [StringLength(35)]
        public string Reach_Number { get; set; }
    }
}
