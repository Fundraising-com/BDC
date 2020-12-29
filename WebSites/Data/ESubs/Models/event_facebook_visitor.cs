namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class event_facebook_visitor
    {
        [Key]
        [Column("event_facebook_visitor")]
        public int event_facebook_visitor1 { get; set; }

        public int personalization_id { get; set; }

        [Required]
        [StringLength(50)]
        public string facebook_id { get; set; }

        [Required]
        [StringLength(500)]
        public string facebook_image_url { get; set; }

        [Required]
        [StringLength(50)]
        public string facebook_firstname { get; set; }

        [Required]
        [StringLength(50)]
        public string facebook_lastname { get; set; }

        public DateTime update_date { get; set; }

        public DateTime create_date { get; set; }

        public bool deleted { get; set; }
    }
}
