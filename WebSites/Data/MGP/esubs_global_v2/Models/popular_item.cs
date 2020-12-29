namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class popular_item
    {
        [Key]
        public int popular_item_id { get; set; }

        public int partner_id { get; set; }

        [StringLength(5)]
        public string culture_code { get; set; }

        public int? type_id { get; set; }

        public int? entity_id { get; set; }

        public int? storefront_category_id { get; set; }

        [StringLength(100)]
        public string name { get; set; }

        [StringLength(200)]
        public string description { get; set; }

        [StringLength(150)]
        public string image_file_name { get; set; }
    }
}
