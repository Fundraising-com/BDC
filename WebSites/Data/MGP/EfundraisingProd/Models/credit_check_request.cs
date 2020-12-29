namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class credit_check_request
    {
        [Key]
        public int credit_check_id { get; set; }

        public int lead_id { get; set; }

        public int? consultant_id { get; set; }

        public DateTime? request_date { get; set; }

        public DateTime? order_date { get; set; }

        [Column(TypeName = "money")]
        public decimal? amount_requested { get; set; }

        public int? credit_status_id { get; set; }

        public int? credit_score { get; set; }

        [Column(TypeName = "money")]
        public decimal? amount_approved { get; set; }

        [StringLength(50)]
        public string last_name { get; set; }

        [StringLength(50)]
        public string first_name { get; set; }

        [StringLength(25)]
        public string mid_init { get; set; }

        [StringLength(75)]
        public string address { get; set; }

        [StringLength(50)]
        public string city { get; set; }

        [StringLength(50)]
        public string state { get; set; }

        [StringLength(10)]
        public string zip { get; set; }

        [StringLength(20)]
        public string ssn { get; set; }

        public DateTime? result_date { get; set; }

        public DateTime? result_confirmation_date { get; set; }

        [Column(TypeName = "text")]
        public string credit_report { get; set; }
    }
}
