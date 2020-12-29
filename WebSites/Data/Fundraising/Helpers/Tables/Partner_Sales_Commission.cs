namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Partner_Sales_Commission
    {
        [Key]
        public int Partner_Sales_Commission_ID { get; set; }

        public int Partner_ID { get; set; }

        public int Product_Class_ID { get; set; }

        public decimal Variable_Rate { get; set; }

        public DateTime Effective_Date { get; set; }

        public bool Active { get; set; }

        public virtual product_class product_class { get; set; }
    }
}
