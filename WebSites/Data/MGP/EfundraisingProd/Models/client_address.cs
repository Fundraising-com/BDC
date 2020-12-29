namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class client_address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int address_id { get; set; }

        [Required]
        [StringLength(2)]
        public string client_sequence_code { get; set; }

        public int client_id { get; set; }

        [Required]
        [StringLength(2)]
        public string address_type { get; set; }

        [StringLength(100)]
        public string street_address { get; set; }

        [Required]
        [StringLength(10)]
        public string state_code { get; set; }

        [Required]
        [StringLength(10)]
        public string country_code { get; set; }

        [Required]
        [StringLength(50)]
        public string city { get; set; }

        [StringLength(10)]
        public string zip_code { get; set; }

        [StringLength(100)]
        public string attention_of { get; set; }

        [StringLength(50)]
        public string matching_code { get; set; }

        public int address_zone_id { get; set; }

        [StringLength(20)]
        public string phone_1 { get; set; }

        [StringLength(20)]
        public string phone_2 { get; set; }

        [StringLength(100)]
        public string Location { get; set; }

        public bool pick_up { get; set; }

        public int? warehouse_id { get; set; }

        public virtual client client { get; set; }

        public virtual Country1 Country { get; set; }

        public virtual State State { get; set; }

        public virtual client_address_type client_address_type { get; set; }
    }
}
