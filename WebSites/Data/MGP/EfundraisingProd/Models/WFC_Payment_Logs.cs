namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WFC_Payment_Logs
    {
        public int id { get; set; }

        public int? sale_id { get; set; }

        [StringLength(50)]
        public string invoice_number { get; set; }

        [StringLength(100)]
        public string message { get; set; }
    }
}
