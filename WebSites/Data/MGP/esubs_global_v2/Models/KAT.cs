namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KAT")]
    public partial class KAT
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        [StringLength(255)]
        public string email { get; set; }

        [StringLength(255)]
        public string short_name { get; set; }

        [StringLength(255)]
        public string univ_name { get; set; }

        [StringLength(255)]
        public string redirect { get; set; }

        [Key]
        [Column(Order = 1)]
        public bool done { get; set; }
    }
}
