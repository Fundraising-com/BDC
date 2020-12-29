namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("personalization")]
    public partial class personalization
    {
        public personalization()
        {
            personalization_image = new HashSet<personalization_image>();
        }

        [Key]
        public int personalization_id { get; set; }

        public int event_participation_id { get; set; }

        [StringLength(100)]
        public string header_title1 { get; set; }

        [StringLength(100)]
        public string header_title2 { get; set; }

        [StringLength(2000)]
        public string body { get; set; }

        [Column(TypeName = "money")]
        public decimal? fundraising_goal { get; set; }

        [StringLength(7)]
        public string site_bgcolor { get; set; }

        [StringLength(7)]
        public string header_bgcolor { get; set; }

        [StringLength(7)]
        public string header_color { get; set; }

        [StringLength(255)]
        public string group_url { get; set; }

        [StringLength(255)]
        public string image_url { get; set; }

        public DateTime create_date { get; set; }

        public byte image_motivator { get; set; }

        public bool skip { get; set; }

        [StringLength(255)]
        public string redirect { get; set; }

        public bool? displayGroupMessage { get; set; }

        public bool? remind_later { get; set; }

        public virtual event_participation event_participation { get; set; }

        public virtual ICollection<personalization_image> personalization_image { get; set; }
        [MaxLength]
        public string twitter_widget_id { get; set; }

        [MaxLength]
        public string facebook_embeded_post { get; set; }
    }
}
