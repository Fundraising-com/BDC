namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Testimonial
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Testimonials_ID { get; set; }

        public int Language_ID { get; set; }

        [Required]
        [StringLength(2000)]
        public string Text { get; set; }

        [Required]
        [StringLength(200)]
        public string Organism { get; set; }

        [Required]
        [StringLength(200)]
        public string Responsible { get; set; }
    }
}
