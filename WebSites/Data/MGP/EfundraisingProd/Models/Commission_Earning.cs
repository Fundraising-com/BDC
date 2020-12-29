namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Commission_Earning
    {
        [Key]
        public int Commission_Earning_ID { get; set; }

        public int? Sales_ID { get; set; }

        [StringLength(255)]
        public string Product_Description { get; set; }

        public decimal? Payment_Amount { get; set; }

        public DateTime? Payment_Entry_Date { get; set; }

        [StringLength(125)]
        public string Commission_Amount { get; set; }

        public decimal? Commission_Rate { get; set; }

        public int? Payment_No { get; set; }

        public int? Consultant_ID { get; set; }

        public DateTime Record_Entry_Date { get; set; }

        public int? Associate_ID { get; set; }

        public decimal? Sales_Amount { get; set; }

        [StringLength(10)]
        public string Currency_Code { get; set; }

        public decimal? Exchange_Rate { get; set; }

        [StringLength(125)]
        public string Commission_Amount_Ca { get; set; }

        public int? Lead_ID { get; set; }

        public DateTime? Sale_Date { get; set; }
    }
}
