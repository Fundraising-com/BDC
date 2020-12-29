namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("_tbd_partner_promotion")]
    public partial class C_tbd_partner_promotion
    {
        [Key]
        public int partner_promotion_id { get; set; }

        public int partner_id { get; set; }

        public int promotion_id { get; set; }

        public DateTime create_date { get; set; }
    }
}
