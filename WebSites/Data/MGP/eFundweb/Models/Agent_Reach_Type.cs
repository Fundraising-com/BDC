namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Agent_Reach_Type
    {
        [Key]
        public int Reach_Type_ID { get; set; }

        [StringLength(50)]
        public string Description { get; set; }
    }
}
