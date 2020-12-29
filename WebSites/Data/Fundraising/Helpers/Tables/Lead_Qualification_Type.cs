namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Lead_Qualification_Type
    {
        public Lead_Qualification_Type()
        {
            leads = new HashSet<lead>();
            Lead_Combinaisons = new HashSet<Lead_Combinaisons>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Lead_Qualification_Type_ID { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public int? Weight { get; set; }

        public virtual ICollection<lead> leads { get; set; }

        public virtual ICollection<Lead_Combinaisons> Lead_Combinaisons { get; set; }
    }
}
