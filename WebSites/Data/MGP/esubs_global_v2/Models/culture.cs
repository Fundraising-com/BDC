namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("culture")]
    public partial class culture
    {
        public culture()
        {
            culture_country_name = new HashSet<culture_country_name>();
            culture_subdivision_name = new HashSet<culture_subdivision_name>();
            email_template_culture = new HashSet<email_template_culture>();
            events = new HashSet<_event>();
            store_template = new HashSet<store_template>();
            touch_change_log = new HashSet<touch_change_log>();
        }

        [Key]
        [StringLength(5)]
        public string culture_code { get; set; }

        [Required]
        [StringLength(2)]
        public string country_code { get; set; }

        [Required]
        [StringLength(2)]
        public string language_code { get; set; }

        [Required]
        [StringLength(50)]
        public string culture_name { get; set; }

        public virtual country country { get; set; }

        public virtual ICollection<culture_country_name> culture_country_name { get; set; }

        public virtual language language { get; set; }

        public virtual ICollection<culture_subdivision_name> culture_subdivision_name { get; set; }

        public virtual ICollection<email_template_culture> email_template_culture { get; set; }

        public virtual ICollection<_event> events { get; set; }

        public virtual ICollection<store_template> store_template { get; set; }

        public virtual ICollection<touch_change_log> touch_change_log { get; set; }
    }
}
