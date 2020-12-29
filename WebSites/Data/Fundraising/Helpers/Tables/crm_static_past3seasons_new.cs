namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class crm_static_past3seasons_new
    {
        [Key]
        public int crm_static_past3seasons_new_id { get; set; }

        public int? account_no { get; set; }

        [StringLength(50)]
        public string account_name { get; set; }

        public decimal? total_sold { get; set; }

        [StringLength(9)]
        public string qsp_cust_billing_matching_code { get; set; }

        [StringLength(9)]
        public string qsp_cust_shipping_matching_code { get; set; }

        [StringLength(9)]
        public string qsp_account_matching_code { get; set; }

        [StringLength(4)]
        public string fm_id { get; set; }

        public int? status { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        [StringLength(20)]
        public string first_name { get; set; }

        [StringLength(30)]
        public string last_name { get; set; }

        [StringLength(20)]
        public string home_phone { get; set; }

        [StringLength(20)]
        public string work_phone { get; set; }

        [StringLength(20)]
        public string mobile_phone { get; set; }

        public DateTime? create_date { get; set; }
    }
}
