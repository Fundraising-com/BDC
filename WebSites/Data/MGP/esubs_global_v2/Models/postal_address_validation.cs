namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class postal_address_validation
    {
        

        [Key]
        public int id { get; set; }

        [StringLength(100)]
        public string address_1 { get; set; }

        [StringLength(100)]
        public string city { get; set; }

        [StringLength(10)]
        public string zip_code { get; set; }

        [StringLength(2)]
        public string state_code { get; set; }

        [StringLength(2)]
        public string country_code { get; set; }

       
        public bool enabled { get; set; }

        public DateTime create_date { get; set; }

       
    }
}
