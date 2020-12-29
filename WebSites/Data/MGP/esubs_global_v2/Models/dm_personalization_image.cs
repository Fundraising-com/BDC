namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class dm_personalization_image
    {
        [Key]
        public int image_id { get; set; }

        public int direct_mail_info_id { get; set; }

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
