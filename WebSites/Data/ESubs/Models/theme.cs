namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("theme")]
    public partial class theme
    {
        public theme()
        {
            default_email_template = new HashSet<default_email_template>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int theme_id { get; set; }

        [Required]
        [StringLength(50)]
        public string theme_name { get; set; }

        public DateTime create_date { get; set; }

        public virtual ICollection<default_email_template> default_email_template { get; set; }
    }
}
