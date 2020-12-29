namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class temp_payment_info
    {
        [Key]
        public int payment_info_id { get; set; }

        public int? check_id { get; set; }
    }
}
