namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pap_transaction
    {
        [Key]
        public int pap_transaction_id { get; set; }

        public int? order_id { get; set; }

        public int? pap_product_category_id { get; set; }

        public decimal? total_cost { get; set; }

        [StringLength(50)]
        public string action_code { get; set; }

        [StringLength(50)]
        public string ext_transaction_id { get; set; }

        [StringLength(50)]
        public string ext_status_id { get; set; }

        public int? lead_id { get; set; }

        public int? lead_visit_id { get; set; }

        public int? campaign_id { get; set; }

        public int? cart_id { get; set; }

        public DateTime? create_date { get; set; }

        public int application_id { get; set; }

        public virtual application application { get; set; }
    }
}
