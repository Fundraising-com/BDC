namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class custcare_comments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int event_id { get; set; }

        [Required]
        [StringLength(8000)]
        public string comments { get; set; }

        public DateTime create_date { get; set; }
    }
}
