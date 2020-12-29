namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class control_type
    {
        [Key]
        public int control_type_id { get; set; }

        [Required]
        [StringLength(200)]
        public string assembly_name { get; set; }

        [Column("namespace")]
        [Required]
        [StringLength(200)]
        public string _namespace { get; set; }

        [Required]
        [StringLength(100)]
        public string class_name { get; set; }

        [Required]
        [StringLength(100)]
        public string display_attribute { get; set; }

        [StringLength(100)]
        public string binding_name { get; set; }

        [Required]
        [StringLength(100)]
        public string event_handler_name { get; set; }

        public bool auto_post_back { get; set; }

        public DateTime datestamp { get; set; }
    }
}
