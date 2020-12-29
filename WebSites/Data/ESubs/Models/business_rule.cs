namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class business_rule
    {
        public business_rule()
        {
            touch_info = new HashSet<touch_info>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int business_rule_id { get; set; }

        public int email_template_id { get; set; }

        [Required]
        [StringLength(200)]
        public string business_rule_name { get; set; }

        [StringLength(100)]
        public string stored_procedure_call { get; set; }

        public short priority_level { get; set; }

        public short member_type_id { get; set; }

        public short email_priority { get; set; }

        public bool active { get; set; }

        public DateTime create_date { get; set; }

        public virtual ICollection<touch_info> touch_info { get; set; }
    }
}
