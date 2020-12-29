namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EFO_Participant
    {
        public EFO_Participant()
        {
            EFO_Message = new HashSet<EFO_Message>();
            EFO_Supporter = new HashSet<EFO_Supporter>();
        }

        [Key]
        public int Participant_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int Campaign_ID { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(150)]
        public string Comments { get; set; }

        public bool Email_Sent { get; set; }

        public bool Is_Active { get; set; }

        public bool Is_Default { get; set; }

        public bool Is_Deletable { get; set; }

        public virtual EFO_Campaign EFO_Campaign { get; set; }

        public virtual ICollection<EFO_Message> EFO_Message { get; set; }

        public virtual ICollection<EFO_Supporter> EFO_Supporter { get; set; }
    }
}
