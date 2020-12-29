namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EFO_Email_Type
    {
        public EFO_Email_Type()
        {
            EFO_Campaign_Status = new HashSet<EFO_Campaign_Status>();
            EFO_Tag = new HashSet<EFO_Tag>();
            EFO_Status = new HashSet<EFO_Status>();
        }

        [Key]
        public int Email_Type_ID { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Body { get; set; }

        [Required]
        [StringLength(150)]
        public string Description { get; set; }

        public virtual ICollection<EFO_Campaign_Status> EFO_Campaign_Status { get; set; }

        public virtual ICollection<EFO_Tag> EFO_Tag { get; set; }

        public virtual ICollection<EFO_Status> EFO_Status { get; set; }
    }
}
