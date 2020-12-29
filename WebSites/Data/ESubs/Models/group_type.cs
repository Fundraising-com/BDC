namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class group_type
    {
        public group_type()
        {
            events = new HashSet<_event>();
        }

        [Key]
        public int group_type_id { get; set; }

        [Required]
        [StringLength(100)]
        public string description { get; set; }

        public DateTime? create_date { get; set; }

        public virtual ICollection<_event> events { get; set; }
    }
}
