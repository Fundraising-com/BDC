namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tag
    {
        public Tag()
        {
            Proposals = new HashSet<Proposal>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Tags_ID { get; set; }

        [StringLength(100)]
        public string Label { get; set; }

        [StringLength(100)]
        public string Control_Name { get; set; }

        public virtual ICollection<Proposal> Proposals { get; set; }
    }
}
