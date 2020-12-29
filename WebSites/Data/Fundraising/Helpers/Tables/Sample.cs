namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sample")]
    public partial class Sample
    {
        public Sample()
        {
            promotional_kit = new HashSet<promotional_kit>();
        }

        public int SampleID { get; set; }

        [Required]
        [StringLength(50)]
        public string SampleName { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public bool Active { get; set; }

        public virtual ICollection<promotional_kit> promotional_kit { get; set; }
    }
}
