namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class featured_event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int event_id { get; set; }

        public int? display_order { get; set; }
    }
}
