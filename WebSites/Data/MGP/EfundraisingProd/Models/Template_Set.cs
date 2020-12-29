namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Template_Set
    {
        [Key]
        public int Template_Set_ID { get; set; }

        public int QSP_Program_ID { get; set; }

        [StringLength(100)]
        public string Supporter_Path { get; set; }

        [StringLength(100)]
        public string Generic_Path { get; set; }

        [StringLength(100)]
        public string Edit_Path { get; set; }

        public virtual QSP_Program QSP_Program { get; set; }
    }
}
