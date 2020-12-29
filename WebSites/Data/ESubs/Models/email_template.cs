namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class email_template
    {
        public email_template()
        {
            email_template_culture = new HashSet<email_template_culture>();
            email_template_tag = new HashSet<email_template_tag>();
            partner_payment_config = new HashSet<partner_payment_config>();
            partner_payment_config1 = new HashSet<partner_payment_config>();
            touch_change_log = new HashSet<touch_change_log>();
        }

        [Key]
        public int email_template_id { get; set; }

        public int email_template_type_id { get; set; }

        [Required]
        [StringLength(50)]
        public string email_template_name { get; set; }

        [Required]
        [StringLength(255)]
        public string description { get; set; }

        [StringLength(100)]
        public string param_procedure_call { get; set; }

        [Required]
        [StringLength(50)]
        public string from_name { get; set; }

        [Required]
        [StringLength(100)]
        public string from_email_address { get; set; }

        [Required]
        [StringLength(50)]
        public string reply_to_name { get; set; }

        [Required]
        [StringLength(100)]
        public string reply_to_email_address { get; set; }

        [Required]
        [StringLength(50)]
        public string bounce_name { get; set; }

        [Required]
        [StringLength(100)]
        public string bounce_email_address { get; set; }

        public DateTime create_date { get; set; }

        public virtual ICollection<email_template_culture> email_template_culture { get; set; }

        public virtual email_template_type email_template_type { get; set; }

        public virtual ICollection<email_template_tag> email_template_tag { get; set; }

        public virtual ICollection<partner_payment_config> partner_payment_config { get; set; }

        public virtual ICollection<partner_payment_config> partner_payment_config1 { get; set; }

        public virtual ICollection<touch_change_log> touch_change_log { get; set; }
    }
}
