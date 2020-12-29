namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class party_type
    {
        public party_type()
        {
            campaign_reason = new HashSet<campaign_reason>();
            group_type = new HashSet<group_type>();
            hear_about_us = new HashSet<hear_about_us>();
            organization_type = new HashSet<organization_type>();
            titles = new HashSet<title>();
        }

        [Key]
        public byte party_type_id { get; set; }

        [Required]
        [StringLength(25)]
        public string party_type_desc { get; set; }

        public virtual ICollection<campaign_reason> campaign_reason { get; set; }

        public virtual ICollection<group_type> group_type { get; set; }

        public virtual ICollection<hear_about_us> hear_about_us { get; set; }

        public virtual ICollection<organization_type> organization_type { get; set; }

        public virtual ICollection<title> titles { get; set; }
    }
}
