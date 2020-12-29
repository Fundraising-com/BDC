namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("supplier")]
    public partial class supplier
    {
        [Key]
        public int supplier_id { get; set; }

        [Required]
        [StringLength(50)]
        public string supplier_name { get; set; }

        [StringLength(255)]
        public string street_adress { get; set; }

        [StringLength(30)]
        public string city { get; set; }

        [StringLength(20)]
        public string zip { get; set; }

        [StringLength(50)]
        public string contact_name { get; set; }

        [StringLength(20)]
        public string phone { get; set; }

        [StringLength(20)]
        public string fax { get; set; }

        [StringLength(20)]
        public string account_no { get; set; }

        public decimal? credit_margin { get; set; }

        [StringLength(10)]
        public string state_code { get; set; }

        [StringLength(10)]
        public string country_code { get; set; }

        [Column(TypeName = "text")]
        public string comments { get; set; }
    }
}
