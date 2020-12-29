namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class email_template_type
    {
        public email_template_type()
        {
            email_template = new HashSet<email_template>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int email_template_type_id { get; set; }

        [Required]
        [StringLength(50)]
        public string email_template_name { get; set; }

        public DateTime create_date { get; set; }

        public virtual ICollection<email_template> email_template { get; set; }
    }
}
