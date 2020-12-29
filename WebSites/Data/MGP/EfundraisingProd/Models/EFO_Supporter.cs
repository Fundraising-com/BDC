namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EFO_Supporter
    {
        public EFO_Supporter()
        {
            EFO_Sale = new HashSet<EFO_Sale>();
        }

        [Key]
        public int Supporter_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int Participant_ID { get; set; }

        [Required]
        [StringLength(75)]
        public string Email { get; set; }

        public bool Is_Email_Good { get; set; }

        public bool Is_Active { get; set; }

        [StringLength(150)]
        public string Comments { get; set; }

        public bool Email_Sent { get; set; }

        public bool Is_Deletable { get; set; }

        [Required]
        [StringLength(25)]
        public string Relation { get; set; }

        public virtual EFO_Participant EFO_Participant { get; set; }

        public virtual ICollection<EFO_Sale> EFO_Sale { get; set; }
    }
}
