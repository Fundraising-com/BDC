namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class gallery_image
    {
        [Key]
        public int gallery_image_id { get; set; }

        public int partner_id { get; set; }

        [StringLength(5)]
        public string culture_code { get; set; }

        [StringLength(200)]
        public string name { get; set; }

        [StringLength(200)]
        public string category_name { get; set; }

        [StringLength(100)]
        public string directory_name { get; set; }

        [StringLength(100)]
        public string file_name { get; set; }
    }
}
