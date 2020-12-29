namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class audit_personalization_image
    {
        [Key]
        public int audit_id { get; set; }

        public DateTime audit_date { get; set; }

        [StringLength(200)]
        public string audit_user_name { get; set; }

        [Required]
        [StringLength(1)]
        public string audit_opcode { get; set; }

        [Required]
        [StringLength(200)]
        public string audit_modifier { get; set; }

        public int image_id { get; set; }

        public int personalization_id { get; set; }

        [StringLength(255)]
        public string image_url { get; set; }

        public bool deleted { get; set; }

        public DateTime create_date { get; set; }

        public bool? isCoverAlbum { get; set; }

        public int image_approval_status_id { get; set; }

        [StringLength(255)]
        public string approver_name { get; set; }

        public DateTime? approved_date { get; set; }
    }
}
