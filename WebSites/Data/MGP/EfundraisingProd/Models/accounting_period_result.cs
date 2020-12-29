namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class accounting_period_result
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short accounting_period_result_id { get; set; }

        public byte? accounting_class_id { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? period { get; set; }

        public decimal? amount { get; set; }

        public decimal? budgeted_amount { get; set; }

        public virtual accounting_class accounting_class { get; set; }
    }
}
