namespace GA.BDC.Data.Fundraising.EFundStore.Tables
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
        [StringLength(50)]
        public string city { get; set; }

        [StringLength(10)]
        public string zip_code { get; set; }

        [Required]
        [StringLength(10)]
        public string country_code { get; set; }

        [StringLength(100)]
        public string attention_of { get; set; }
    }
}
