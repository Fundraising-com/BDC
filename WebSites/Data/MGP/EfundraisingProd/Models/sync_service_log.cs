namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sync_service_log
    {
        [Key]
        public DateTime ProcessDate { get; set; }

        public bool Success { get; set; }
    }
}
