namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Lead_Conditions
    {
        public Lead_Conditions()
        {
            Lead_Combinaisons = new HashSet<Lead_Combinaisons>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Condition_ID { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        public virtual ICollection<Lead_Combinaisons> Lead_Combinaisons { get; set; }
    }
}
