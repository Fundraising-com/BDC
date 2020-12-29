namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class country
    {
        public country()
        {
            country_names = new HashSet<country_names>();
            cultures = new HashSet<culture1>();
        }

        [Key]
        [StringLength(2)]
        public string country_code { get; set; }

        [Required]
        [StringLength(50)]
        public string country_name { get; set; }

        [Required]
        [StringLength(3)]
        public string long_country_code { get; set; }

        [Required]
        [StringLength(3)]
        public string numeric_code { get; set; }

        [Required]
        [StringLength(4)]
        public string currency_code { get; set; }

        public virtual ICollection<country_names> country_names { get; set; }

        public virtual ICollection<culture1> cultures { get; set; }
    }
}
