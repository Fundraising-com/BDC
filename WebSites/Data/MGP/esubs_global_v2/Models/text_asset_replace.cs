namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class text_asset_replace
    {
        [Key]
        public int text_id { get; set; }

        public int text_group_id { get; set; }

        [StringLength(5)]
        public string culture_code { get; set; }

        [StringLength(200)]
        public string old_text { get; set; }

        [StringLength(200)]
        public string new_text { get; set; }

        public bool is_priority { get; set; }

        [StringLength(300)]
        public string description { get; set; }

        public DateTime create_date { get; set; }
    }
}
