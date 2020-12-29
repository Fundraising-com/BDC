namespace GA.BDC.Data.MGP.EFREcommerce.Models
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
        public string description { get; set; }

        public virtual ICollection<subdivision> subdivisions { get; set; }
    }
}
