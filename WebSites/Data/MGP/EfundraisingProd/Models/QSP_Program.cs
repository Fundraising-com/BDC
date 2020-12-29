namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class QSP_Program
    {
        public QSP_Program()
        {
            Template_Set = new HashSet<Template_Set>();
        }

        [Key]
        public int QSP_Program_ID { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        [StringLength(100)]
        public string Base_Directory { get; set; }

        public virtual ICollection<Template_Set> Template_Set { get; set; }
    }
}
