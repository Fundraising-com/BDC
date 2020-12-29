namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Commission_Outstanding
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Sales_ID { get; set; }

        public int? Consultant_ID { get; set; }

        public DateTime? Sales_Date { get; set; }

        public DateTime? Shipped_Date { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(50)]
        public string Payment_Term { get; set; }

        [StringLength(50)]
        public string First_Name { get; set; }

        [StringLength(50)]
        public string Last_Name { get; set; }

        [StringLength(100)]
        public string Organization { get; set; }

        [StringLength(20)]
        public string Day_Phone { get; set; }

        [StringLength(125)]
        public string Outstanding_Amount { get; set; }

        [StringLength(10)]
        public string Currency_Code { get; set; }

        [StringLength(125)]
        public string Outstanding_Commission { get; set; }
    }
}
