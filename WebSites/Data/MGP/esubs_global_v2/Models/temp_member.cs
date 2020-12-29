namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class temp_member
    {
        [Key]
        [Column(Order = 0)]
        public int member_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string email { get; set; }

        [StringLength(100)]
        public string name { get; set; }
    }
}
