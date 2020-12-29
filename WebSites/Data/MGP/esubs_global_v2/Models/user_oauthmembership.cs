namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class user_oauthmembership
    {
        [Key, Column(Order = 1)]
        [StringLength(30)]
        public string provider { get; set; }

        [Key, Column(Order = 2)]
        [StringLength(100)]
        public string providerUserId { get; set; }

        [Required]
        public int userId { get; set; }
    }
}
