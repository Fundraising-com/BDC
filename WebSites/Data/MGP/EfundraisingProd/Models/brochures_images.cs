namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class brochures_images
    {
        [Key]
        public byte brochures_images_id { get; set; }

        public int product_id { get; set; }

        [Required]
        [StringLength(100)]
        public string base_filename { get; set; }

        [Required]
        [StringLength(5)]
        public string file_ext { get; set; }

        public byte number_pages { get; set; }
    }
}
