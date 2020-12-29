namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class opt_status
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int opt_status_id { get; set; }

        [Required]
        [StringLength(50)]
        public string opt_status_name { get; set; }

        public DateTime create_date { get; set; }
    }
}
