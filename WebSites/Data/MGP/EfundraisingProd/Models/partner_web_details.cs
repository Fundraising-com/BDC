namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class partner_web_details
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int partner_id { get; set; }

        [Required]
        [StringLength(30)]
        public string top_menu { get; set; }

        [Required]
        [StringLength(30)]
        public string left_menu { get; set; }

        [Required]
        [StringLength(30)]
        public string right_menu { get; set; }

        [Required]
        [StringLength(30)]
        public string images_path { get; set; }

        [Required]
        [StringLength(20)]
        public string default_color { get; set; }

        [Required]
        [StringLength(30)]
        public string short_cut_menu { get; set; }

        [Required]
        [StringLength(30)]
        public string product_image_map { get; set; }
    }
}
