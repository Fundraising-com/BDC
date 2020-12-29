namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("action")]
    public partial class action
    {
        public action()
        {
            touch_action = new HashSet<touch_action>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int action_id { get; set; }

        [Required]
        [StringLength(255)]
        public string action_desc { get; set; }

        public DateTime create_date { get; set; }

        public virtual ICollection<touch_action> touch_action { get; set; }
    }
}
