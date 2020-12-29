namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tag")]
    public partial class tag
    {
        public tag()
        {
            email_template_tag = new HashSet<email_template_tag>();
        }

        [Key]
        public int tag_id { get; set; }

        [Required]
        [StringLength(100)]
        public string start_tag_name { get; set; }

        [Required]
        [StringLength(100)]
        public string end_tag_name { get; set; }

        [Required]
        [StringLength(4000)]
        public string description { get; set; }

        public DateTime create_date { get; set; }

        public virtual ICollection<email_template_tag> email_template_tag { get; set; }
    }
}
