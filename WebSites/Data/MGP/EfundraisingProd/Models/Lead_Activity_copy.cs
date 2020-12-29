namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Lead_Activity_copy
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Lead_Activity_Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Lead_Id { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Lead_Activity_Type_Id { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime Lead_Activity_Date { get; set; }

        public DateTime? Completed_Date { get; set; }

        [Column(TypeName = "text")]
        public string Comments { get; set; }
    }
}
