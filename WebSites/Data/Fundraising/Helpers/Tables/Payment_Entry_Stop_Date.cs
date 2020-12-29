namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Payment_Entry_Stop_Date
    {
        [Key]
        [Column("Payment_Entry_Stop_Date")]
        public DateTime Payment_Entry_Stop_Date1 { get; set; }
    }
}
