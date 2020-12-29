namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class event_type
    {
        public event_type()
        {
            events = new HashSet<_event>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int event_type_id { get; set; }

        [Required]
        [StringLength(50)]
        public string event_type_name { get; set; }

        public DateTime create_date { get; set; }

        public virtual ICollection<_event> events { get; set; }
    }
}
