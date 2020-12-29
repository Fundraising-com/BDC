namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sales_Change_Log
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Sales_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string Table_Name { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string Column_Name { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime Change_Date_Time { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(50)]
        public string User_Name { get; set; }

        [StringLength(255)]
        public string From_Value { get; set; }

        [StringLength(255)]
        public string To_Value { get; set; }

        [StringLength(255)]
        public string Comment { get; set; }

        [Required]
        [StringLength(50)]
        public string Computer_Name { get; set; }

        public int? Cancelation_reason_Id { get; set; }

        [StringLength(255)]
        public string Other_Reason { get; set; }
    }
}
