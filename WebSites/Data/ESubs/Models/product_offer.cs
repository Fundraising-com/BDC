namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class product_offer
    {
        public product_offer()
        {
            email_template_tag = new HashSet<email_template_tag>();
        }

        [Key]
        public int product_offer_id { get; set; }

        [Required]
        [StringLength(100)]
        public string description { get; set; }

        public DateTime create_date { get; set; }

        public virtual ICollection<email_template_tag> email_template_tag { get; set; }
    }
}
