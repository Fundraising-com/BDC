namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int consultant_id { get; set; }

        [Required]
        [StringLength(50)]
        public string first_name { get; set; }

        [Required]
        [StringLength(50)]
        public string last_name { get; set; }

        [Required]
        [StringLength(10)]
        public string rda_tag { get; set; }

        public DateTime create_date { get; set; }
    }
}
