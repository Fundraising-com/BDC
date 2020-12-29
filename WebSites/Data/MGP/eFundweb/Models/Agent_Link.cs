namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Agent_Link
    {
        [Key]
        public int Link_ID { get; set; }

        public int Agent_ID { get; set; }

        [StringLength(120)]
        public string URL { get; set; }

        [StringLength(120)]
        public string Description { get; set; }
    }
}
