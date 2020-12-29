namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Agent_Comment
    {
        [Key]
        public int Comment_ID { get; set; }

        public int Agent_ID { get; set; }

        [StringLength(250)]
        public string Comment { get; set; }

        public DateTime Timestamp { get; set; }

        [StringLength(15)]
        public string Added_By { get; set; }
    }
}
