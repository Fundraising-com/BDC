namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Applicable_Tax_To_Add
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(4)]
        public string Tax_Code { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Sale_To_Add_ID { get; set; }

        public decimal Tax_Amount { get; set; }

        public virtual sale_to_add sale_to_add { get; set; }

        public virtual Tax_Table Tax_Table { get; set; }
    }
}
