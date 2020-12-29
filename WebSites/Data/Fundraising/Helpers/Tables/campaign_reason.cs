namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class campaign_reason
    {
        public campaign_reason()
        {
            campaign_reason_desc1 = new HashSet<campaign_reason_desc>();
        }

        [Key]
        public byte campaign_reason_id { get; set; }

        public byte party_type_id { get; set; }

        [Required]
        [StringLength(50)]
        public string campaign_reason_desc { get; set; }

        public virtual ICollection<campaign_reason_desc> campaign_reason_desc1 { get; set; }

        public virtual party_type party_type { get; set; }
    }
}
