namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Agent_Email
    {
        [Key]
        public int Email_ID { get; set; }

        [StringLength(50)]
        public string Email_Address { get; set; }

        public int? Agent_ID { get; set; }
    }
}
