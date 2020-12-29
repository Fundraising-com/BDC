namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class temp_group_member
    {
        public int? partner_id { get; set; }

        [Key]
        [StringLength(100)]
        public string email { get; set; }

        public int? organization_id { get; set; }
    }
}
