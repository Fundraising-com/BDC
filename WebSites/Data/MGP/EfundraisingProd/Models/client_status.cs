namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class client_status
    {
        [Key]
        public int client_status_id { get; set; }

        [Required]
        [StringLength(50)]
        public string description { get; set; }
    }
}
