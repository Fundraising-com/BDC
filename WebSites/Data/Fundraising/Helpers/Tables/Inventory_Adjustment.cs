namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Inventory_Adjustment
    {
        [Key]
        public int Inventory_Adjustment_ID { get; set; }

        public int Inventory_Adjustment_Type_ID { get; set; }

        public int Scratch_Book_Id { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Adjustment_Date { get; set; }

        public int? Quantity { get; set; }

        public virtual Inventory_Adjustment_Type Inventory_Adjustment_Type { get; set; }

        public virtual scratch_book scratch_book { get; set; }
    }
}
