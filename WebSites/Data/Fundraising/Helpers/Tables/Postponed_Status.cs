namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Postponed_Status
    {
        public Postponed_Status()
        {
            Postponed_Sale = new HashSet<Postponed_Sale>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Postponed_Status_ID { get; set; }

        [Required]
        [StringLength(30)]
        public string Description { get; set; }

        public virtual ICollection<Postponed_Sale> Postponed_Sale { get; set; }
    }
}
