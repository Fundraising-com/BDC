namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Conversion_Rate_Table
    {
        [Required]
        [StringLength(10)]
        public string Currency_Code { get; set; }

        public decimal? Conversion_Rate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Conversion_Date { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Conversion_Rate_Id { get; set; }
    }
}
