namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class credit_card_refund_request_status
    {
        [Key]
        [StringLength(3)]
        public string status_code { get; set; }

        [StringLength(100)]
        public string description { get; set; }
    }
}
