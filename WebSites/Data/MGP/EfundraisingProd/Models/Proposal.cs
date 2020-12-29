namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Proposal")]
    public partial class Proposal
    {
        public Proposal()
        {
            Tags = new HashSet<Tag>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Proposal_ID { get; set; }

        [StringLength(200)]
        public string Fax_Name { get; set; }

        [StringLength(200)]
        public string Email_Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
