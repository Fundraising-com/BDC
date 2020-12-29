namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class lead_history
    {
        [Key]
        public int transaction_id { get; set; }

        public int lead_id { get; set; }

        public int promotion_id { get; set; }

        [Required]
        [StringLength(4)]
        public string channel_code { get; set; }

        public int consultant_id { get; set; }

        [StringLength(50)]
        public string first_name { get; set; }

        [StringLength(50)]
        public string last_name { get; set; }

        [StringLength(200)]
        public string organization { get; set; }

        [StringLength(100)]
        public string street_address { get; set; }

        [StringLength(50)]
        public string city { get; set; }

        [Required]
        [StringLength(10)]
        public string state_code { get; set; }

        [Required]
        [StringLength(10)]
        public string country_code { get; set; }

        [StringLength(10)]
        public string zip_code { get; set; }

        [StringLength(20)]
        public string day_phone { get; set; }

        [StringLength(20)]
        public string evening_phone { get; set; }

        [StringLength(20)]
        public string fax { get; set; }

        [StringLength(100)]
        public string email { get; set; }

        public DateTime lead_entry_date { get; set; }

        public int? participant_count { get; set; }

        public DateTime transaction_date { get; set; }
    }
}
