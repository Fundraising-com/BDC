namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Temp_Sale_Zip_Code
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(15)]
        public string Zip_Code { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Sale_To_Add_ID { get; set; }
    }
}
