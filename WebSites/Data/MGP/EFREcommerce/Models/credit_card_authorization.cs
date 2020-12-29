namespace GA.BDC.Data.MGP.EFREcommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class credit_card_authorization
    {
        [Key]
        public int credit_authorization_id { get; set; }

        public int? credit_card_id { get; set; }

        public int? transmission_id { get; set; }

        public int? transmission_type { get; set; }

        public DateTime? transmission_date { get; set; }

        [StringLength(50)]
        public string transaction_type { get; set; }

        [Column(TypeName = "money")]
        public decimal? amount { get; set; }

        [StringLength(50)]
        public string response_code { get; set; }

        [StringLength(50)]
        public string response_auth_code { get; set; }

        public int? response_order_id { get; set; }

        public int? response_payment_id { get; set; }

        [StringLength(200)]
        public string comment { get; set; }

        public bool deleted { get; set; }

        public DateTime create_date { get; set; }

        [StringLength(200)]
        public string response_message { get; set; }

        public virtual credit_card credit_card { get; set; }
    }
}
