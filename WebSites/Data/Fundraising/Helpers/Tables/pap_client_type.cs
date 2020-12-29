namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pap_client_type
    {
        public pap_client_type()
        {
            pap_partner_client_type_overide = new HashSet<pap_partner_client_type_overide>();
        }

        [Key]
        public int pap_client_type_id { get; set; }

        public int? application_id { get; set; }

        public int? pap_product_category_id { get; set; }

        [StringLength(50)]
        public string ext_client_type_id { get; set; }

        public DateTime? create_date { get; set; }

        public virtual application application { get; set; }

        public virtual ICollection<pap_partner_client_type_overide> pap_partner_client_type_overide { get; set; }

        public virtual pap_product_category pap_product_category { get; set; }
    }
}
