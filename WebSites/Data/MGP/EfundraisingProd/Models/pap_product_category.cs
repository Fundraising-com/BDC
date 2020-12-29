namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pap_product_category
    {
        public pap_product_category()
        {
            pap_client_type = new HashSet<pap_client_type>();
            pap_partner_product_type_overide = new HashSet<pap_partner_product_type_overide>();
            pap_product_type = new HashSet<pap_product_type>();
            pap_transaction = new HashSet<pap_transaction>();
        }

        [Key]
        public int pap_product_category_id { get; set; }

        [StringLength(50)]
        public string product_category_code { get; set; }

        public bool? is_default { get; set; }

        public DateTime? create_date { get; set; }

        [StringLength(255)]
        public string product_category_desc { get; set; }

        public bool? is_active { get; set; }

        public virtual ICollection<pap_client_type> pap_client_type { get; set; }

        public virtual ICollection<pap_partner_product_type_overide> pap_partner_product_type_overide { get; set; }

        public virtual ICollection<pap_product_type> pap_product_type { get; set; }

        public virtual ICollection<pap_transaction> pap_transaction { get; set; }
    }
}
