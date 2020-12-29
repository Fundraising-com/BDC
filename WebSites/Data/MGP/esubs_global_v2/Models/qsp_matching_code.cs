namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class qsp_matching_code
    {
        [Key]
        public int qsp_matching_code_id { get; set; }

        public int account_id { get; set; }

        [Required]
        [StringLength(10)]
        public string cust_billing_matching_code { get; set; }

        [Required]
        [StringLength(10)]
        public string cust_shipping_matching_code { get; set; }

        [Required]
        [StringLength(10)]
        public string account_matching_code { get; set; }
    }
}
