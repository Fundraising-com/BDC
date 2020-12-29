namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tellafriend_recipient
    {
        [Key]
        public int tellafriend_recipient_id { get; set; }

        public int tellafriend_id { get; set; }

        [Required]
        [StringLength(100)]
        public string email { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        public byte processed { get; set; }

        public DateTime create_date { get; set; }

        public virtual tellafriend tellafriend { get; set; }
    }
}
