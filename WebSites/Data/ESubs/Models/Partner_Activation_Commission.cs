namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Partner_Activation_Commission
    {
        [Key]
        public int Partner_Activation_Commission_ID { get; set; }

        public int Partner_ID { get; set; }

        public int Partner_Commission_Range_ID { get; set; }

        public decimal Fixed_Amount { get; set; }

        public DateTime Effective_Date { get; set; }

        public bool Active { get; set; }

        public virtual Partner_Commission_Range Partner_Commission_Range { get; set; }
    }
}
