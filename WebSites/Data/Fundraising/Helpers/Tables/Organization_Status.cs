namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Organization_Status
    {
        public Organization_Status()
        {
            Organizations = new HashSet<Organization>();
        }

        [Key]
        public int Organization_Status_ID { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public virtual ICollection<Organization> Organizations { get; set; }
    }
}
