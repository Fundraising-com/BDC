namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class partner_payment_info
    {
        [Key]
        public int partner_payment_info_id { get; set; }

        public int partner_id { get; set; }

        public int payment_info_id { get; set; }

        public bool active { get; set; }

        public DateTime create_date { get; set; }
    }
}
