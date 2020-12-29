namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class email_template_field
    {
        public email_template_field()
        {
            touch_change_log_details = new HashSet<touch_change_log_details>();
        }

        [Key]
        public int email_template_field_id { get; set; }

        [Required]
        [StringLength(50)]
        public string field_name { get; set; }

        [Required]
        [StringLength(50)]
        public string table_name { get; set; }

        [Required]
        [StringLength(50)]
        public string type_name { get; set; }

        public DateTime create_date { get; set; }

        public virtual ICollection<touch_change_log_details> touch_change_log_details { get; set; }
    }
}
