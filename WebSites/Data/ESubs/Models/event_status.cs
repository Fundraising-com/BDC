namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class event_status
    {
        public event_status()
        {
            events = new HashSet<_event>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int event_status_id { get; set; }

        [Required]
        [StringLength(50)]
        public string event_status_name { get; set; }

        public DateTime create_date { get; set; }

        public virtual ICollection<_event> events { get; set; }
    }
}
