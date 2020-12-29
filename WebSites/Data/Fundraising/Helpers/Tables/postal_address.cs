namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class postal_address
    {
        [Key]
        public int postal_address_id { get; set; }

        [StringLength(100)]
        public string address { get; set; }

        [StringLength(100)]
        public string city { get; set; }

        [StringLength(10)]
        public string zip_code { get; set; }

        [StringLength(10)]
        public string country_code { get; set; }

        [StringLength(7)]
        public string subdivision_code { get; set; }

        public DateTime create_date { get; set; }
    }
}
