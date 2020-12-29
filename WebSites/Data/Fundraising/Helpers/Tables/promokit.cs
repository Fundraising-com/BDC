namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("promokit")]
    public partial class promokit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int lead_id { get; set; }

        public int? lead_visit_id { get; set; }

        public int kit_type_id { get; set; }

        public int carrier_id { get; set; }

        public int? carrier_tracking_id { get; set; }

        [StringLength(100)]
        public string street_address { get; set; }

        [StringLength(50)]
        public string city { get; set; }

        [StringLength(10)]
        public string zip_code { get; set; }

        [Required]
        [StringLength(10)]
        public string country_code { get; set; }

        [Required]
        [StringLength(10)]
        public string state_code { get; set; }

        public int validated { get; set; }

        public DateTime? create_date { get; set; }

        public DateTime? sent_date { get; set; }

        public int done { get; set; }
    }
}
