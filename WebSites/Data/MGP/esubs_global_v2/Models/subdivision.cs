namespace GA.BDC.Data.MGP.esubs_global_v2.Models
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
            culture_subdivision_name = new HashSet<culture_subdivision_name>();
            payments = new HashSet<payment>();
            postal_address = new HashSet<postal_address>();
            store_template = new HashSet<store_template>();
        }

        [Key]
        [StringLength(7)]
        public string subdivision_code { get; set; }

        [Required]
        [StringLength(2)]
        public string country_code { get; set; }

        [Required]
        [StringLength(100)]
        public string subdivision_name_1 { get; set; }

        [StringLength(100)]
        public string subdivision_name_2 { get; set; }

        [StringLength(100)]
        public string subdivision_name_3 { get; set; }

        [StringLength(10)]
        public string regional_division { get; set; }

        [StringLength(50)]
        public string subdivision_category { get; set; }

        public virtual country country { get; set; }

        public virtual ICollection<culture_subdivision_name> culture_subdivision_name { get; set; }

        public virtual ICollection<payment> payments { get; set; }

        public virtual ICollection<postal_address> postal_address { get; set; }

        public virtual ICollection<store_template> store_template { get; set; }
    }
}
