namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("charge")]
    public partial class charge
    {
        [Key]
        public int charge_id { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        public decimal? default_amount { get; set; }

        public bool is_credit { get; set; }

        public bool is_disabled { get; set; }

        public DateTime create_date { get; set; }

        public int create_user_id { get; set; }

        public DateTime? update_date { get; set; }

        public int? update_user_id { get; set; }

        [StringLength(100)]
        public string fulf_charge_id { get; set; }
    }
}
