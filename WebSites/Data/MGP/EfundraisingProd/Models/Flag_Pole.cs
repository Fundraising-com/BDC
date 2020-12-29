namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Flag_Pole
    {
        public Flag_Pole()
        {
            Organizations = new HashSet<Organization>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Flag_Pole_ID { get; set; }

        [StringLength(15)]
        public string MDR_ID { get; set; }

        public virtual ICollection<Organization> Organizations { get; set; }
    }
}
