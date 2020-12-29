namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Lead_Priority
    {
        public Lead_Priority()
        {
            leads = new HashSet<lead>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Lead_Priority_Id { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public virtual ICollection<lead> leads { get; set; }
    }
}
