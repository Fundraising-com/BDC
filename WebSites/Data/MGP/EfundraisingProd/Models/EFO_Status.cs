namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EFO_Status
    {
        public EFO_Status()
        {
            EFO_Campaign_Status = new HashSet<EFO_Campaign_Status>();
            EFO_Email_Type = new HashSet<EFO_Email_Type>();
        }

        [Key]
        public int Status_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        public virtual ICollection<EFO_Campaign_Status> EFO_Campaign_Status { get; set; }

        public virtual ICollection<EFO_Email_Type> EFO_Email_Type { get; set; }
    }
}
