namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("country")]
    public partial class country
    {
        public country()
        {
            cultures = new HashSet<culture>();
            culture_country_name = new HashSet<culture_country_name>();
            payments = new HashSet<payment>();
            subdivisions = new HashSet<subdivision>();
        }

        [Key]
        [StringLength(2)]
        public string country_code { get; set; }

        [Required]
        [StringLength(100)]
        public string country_name { get; set; }

        [Required]
        [StringLength(4000)]
        public string descriptive_information { get; set; }

        public virtual ICollection<culture> cultures { get; set; }

        public virtual ICollection<culture_country_name> culture_country_name { get; set; }

        public virtual ICollection<payment> payments { get; set; }

        public virtual ICollection<subdivision> subdivisions { get; set; }
    }
}
