namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("xavier")]
    public partial class xavier
    {
        public int? lead_id { get; set; }

        [StringLength(50)]
        public string type { get; set; }

        [Key]
        public bool done { get; set; }
    }
}
