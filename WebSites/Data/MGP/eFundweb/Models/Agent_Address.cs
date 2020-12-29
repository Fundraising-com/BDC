namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Agent_Address
    {
        [Key]
        public int Address_ID { get; set; }

        public int? Agent_ID { get; set; }

        [StringLength(40)]
        public string Address { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(30)]
        public string State { get; set; }

        [StringLength(40)]
        public string Country { get; set; }

        [StringLength(10)]
        public string Zip { get; set; }
    }
}
