namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class partner_text_group
    {
        [Key]
        public int partner_text_group_id { get; set; }

        public int partner_id { get; set; }

        public int text_group_id { get; set; }

        public DateTime create_date { get; set; }
    }
}
