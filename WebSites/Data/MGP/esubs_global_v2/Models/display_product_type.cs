namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class display_product_type
    {
        [Key]
        public int display_product_type_id { get; set; }

        public int display_id { get; set; }

        public int external_product_type_id { get; set; }

        public int store_id { get; set; }

        [Required]
        [StringLength(255)]
        public string description { get; set; }

        public DateTime create_date { get; set; }
    }
}
