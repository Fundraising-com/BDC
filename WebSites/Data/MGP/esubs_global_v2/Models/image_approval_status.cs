namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class image_approval_status
    {
        public image_approval_status()
        {
            personalization_image = new HashSet<personalization_image>();
        }

        [Key]
        public int image_approval_status_id { get; set; }

        [StringLength(255)]
        public string image_approval_status_description { get; set; }

        public DateTime create_date { get; set; }

        public virtual ICollection<personalization_image> personalization_image { get; set; }
    }
}
