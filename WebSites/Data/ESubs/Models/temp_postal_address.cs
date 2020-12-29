namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class temp_postal_address
    {
        [Key]
        public int postal_address_id { get; set; }

        public int? check_id { get; set; }

        public int? organization_id { get; set; }

        [StringLength(100)]
        public string address { get; set; }

        [StringLength(30)]
        public string city { get; set; }

        [StringLength(10)]
        public string zip { get; set; }

        public DateTime? create_date { get; set; }
    }
}
