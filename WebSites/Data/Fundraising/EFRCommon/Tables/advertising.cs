namespace GA.BDC.Data.Fundraising.EFRCommon.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("advertising")]
    public partial class advertising
    {
        [Key]
        public int advertising_id { get; set; }

        public int? lead_id { get; set; }

        public int? org_promotion_id { get; set; }

        public int? advertising_type_id { get; set; }

        [StringLength(20)]
        public string first_name { get; set; }

        [StringLength(20)]
        public string last_name { get; set; }

        [StringLength(20)]
        public string phone { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        [StringLength(45)]
        public string compagnie_name { get; set; }

        [StringLength(150)]
        public string compagnie_url { get; set; }

        [StringLength(100)]
        public string display_url { get; set; }

        [StringLength(355)]
        public string listing_text { get; set; }

        [Column(TypeName = "image")]
        public byte[] picture_url { get; set; }

        [StringLength(100)]
        public string image_type { get; set; }

        [StringLength(20)]
        public string is_visible { get; set; }

        public DateTime? start_date { get; set; }

        public DateTime? end_date { get; set; }

        public DateTime? create_date { get; set; }
    }
}
