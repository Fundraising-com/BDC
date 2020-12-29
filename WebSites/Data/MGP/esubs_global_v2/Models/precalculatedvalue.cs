namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("precalculatedvalue")]
    public partial class precalculatedvalue
    {
        [Column(TypeName = "money")]
        public decimal? sales_amount_grand_total { get; set; }

        [Key]
        public DateTime update_date { get; set; }
    }
}
