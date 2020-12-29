namespace GA.BDC.Data.MGP.EFREcommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("subdivision")]
    public partial class subdivision
    {
        public subdivision()
        {
            postal_address = new HashSet<postal_address>();
        }

        [Key]
        [StringLength(7)]
        public string subdivision_code { get; set; }

        [Required]
        [StringLength(2)]
        public string country_code { get; set; }

        [Required]
        [StringLength(100)]
        public string subdivision_name { get; set; }

        [StringLength(50)]
        public string subdivision_category { get; set; }

        public virtual country country { get; set; }

        public virtual ICollection<postal_address> postal_address { get; set; }
    }
}
