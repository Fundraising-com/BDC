namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Java_Errors
    {
        [Key]
        public int Error_ID { get; set; }

        [StringLength(255)]
        public string Class_Name { get; set; }

        [Column(TypeName = "text")]
        public string Error_Message { get; set; }

        public DateTime Error_Date { get; set; }
    }
}
