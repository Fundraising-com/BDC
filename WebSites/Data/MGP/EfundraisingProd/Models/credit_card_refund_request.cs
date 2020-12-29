namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class credit_card_refund_request
    {
        [Key]
        public int credit_card_refund_request_id { get; set; }

        public int sale_id { get; set; }

        public int? bpps_id { get; set; }

        public DateTime? request_date { get; set; }

        [Required]
        [StringLength(3)]
        public string status_code { get; set; }

        [Column(TypeName = "money")]
        public decimal? refund_amount { get; set; }

        public bool? processed { get; set; }

        public byte credit_card_type_id { get; set; }

        public bool? cancelled { get; set; }
    }
}
