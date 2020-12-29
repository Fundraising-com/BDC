namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class group_type
    {
        public group_type()
        {
            group_type_desc = new HashSet<group_type_desc>();
            targeted_market_type = new HashSet<targeted_market_type>();
        }

        [Key]
        public byte group_type_id { get; set; }

        public byte? party_type_id { get; set; }

        [Required]
        [StringLength(50)]
        public string description { get; set; }

        public virtual ICollection<group_type_desc> group_type_desc { get; set; }

        public virtual party_type party_type { get; set; }

        public virtual ICollection<targeted_market_type> targeted_market_type { get; set; }
    }
}
