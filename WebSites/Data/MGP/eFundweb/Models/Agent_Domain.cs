namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Agent_Domain
    {
        [Key]
        public int Domain_ID { get; set; }

        [StringLength(80)]
        public string Domain_Name { get; set; }

        public int? Agent_ID { get; set; }
    }
}
