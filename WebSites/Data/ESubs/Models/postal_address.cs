namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class postal_address
    {
        public postal_address()
        {
            member_postal_address = new HashSet<member_postal_address>();
            payment_info = new HashSet<payment_info>();
        }

        [Key]
        public int postal_address_id { get; set; }

        [StringLength(100)]
        public string address_1 { get; set; }

        [StringLength(100)]
        public string address_2 { get; set; }

        [StringLength(100)]
        public string city { get; set; }

        [StringLength(10)]
        public string zip_code { get; set; }

        [StringLength(2)]
        public string country_code { get; set; }

        [StringLength(7)]
        public string subdivision_code { get; set; }

        public DateTime create_date { get; set; }

        [StringLength(10)]
        public string matching_code { get; set; }

        public int? is_validated { get; set; }

        public virtual ICollection<member_postal_address> member_postal_address { get; set; }

        public virtual ICollection<payment_info> payment_info { get; set; }

        public virtual subdivision subdivision { get; set; }
    }
}
