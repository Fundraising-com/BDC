namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Confirmation_Method
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Confirmation_Method_ID { get; set; }

        [StringLength(50)]
        public string Description { get; set; }
    }
}
