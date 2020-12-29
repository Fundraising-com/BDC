namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class event_change
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int event_id { get; set; }

        [Key]
        [Column(Order = 1)]
        public bool new_displayable_value { get; set; }

        [Key]
        [Column(Order = 2)]
        public bool old_displayable_value { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string user { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime create_date { get; set; }
    }
}
