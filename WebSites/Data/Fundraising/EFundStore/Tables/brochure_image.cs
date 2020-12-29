namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class brochure_image
    {
        [Key]
        public byte brochure_image_id { get; set; }

        public int product_id { get; set; }

        [Required]
        [StringLength(100)]
        public string base_filename { get; set; }

        [Required]
        [StringLength(5)]
        public string file_ext { get; set; }

        public byte number_page { get; set; }
    }
}
