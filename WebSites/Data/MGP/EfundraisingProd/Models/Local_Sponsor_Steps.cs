namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Local_Sponsor_Steps
    {
        public Local_Sponsor_Steps()
        {
            Local_Sponsor = new HashSet<Local_Sponsor>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Step_Id { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public virtual ICollection<Local_Sponsor> Local_Sponsor { get; set; }
    }
}
