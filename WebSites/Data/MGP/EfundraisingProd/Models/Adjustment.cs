namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Adjustment")]
    public partial class Adjustment
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Sales_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Adjustment_No { get; set; }

        public int? Reason_ID { get; set; }

        public DateTime? Adjustment_Date { get; set; }

        public decimal Adjustment_Amount { get; set; }

        [Column(TypeName = "text")]
        public string Comment { get; set; }

        public decimal? Adjustment_On_Shipping { get; set; }

        public decimal? Adjustment_On_Taxes { get; set; }

        public decimal? Adjustment_On_Sale_Amount { get; set; }

        public DateTime? Create_Date { get; set; }

        public int? Create_User_ID { get; set; }

        public int? Ext_Adjustment_Id { get; set; }

        public int? charge_id { get; set; }

        public virtual Reason Reason { get; set; }
    }
}
