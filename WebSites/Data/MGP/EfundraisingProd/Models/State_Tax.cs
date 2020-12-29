namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class State_Tax
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string State_Code { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(4)]
        public string Tax_Code { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime Effective_Date { get; set; }

        public decimal? Tax_Rate { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Tax_order { get; set; }

        public virtual State State { get; set; }

        public virtual Tax_Table Tax_Table { get; set; }
    }
}
