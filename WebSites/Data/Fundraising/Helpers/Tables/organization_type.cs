namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class organization_type
    {
        public organization_type()
        {
            organization_type_desc1 = new HashSet<organization_type_desc>();
        }

        [Key]
        public byte organization_type_id { get; set; }

        public byte party_type_id { get; set; }

        [Required]
        [StringLength(50)]
        public string organization_type_desc { get; set; }

        public bool is_school { get; set; }

        public virtual ICollection<organization_type_desc> organization_type_desc1 { get; set; }

        public virtual party_type party_type { get; set; }
    }
}
