namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("application")]
    public partial class application
    {
        public application()
        {
            pap_client_type = new HashSet<pap_client_type>();
            pap_product_type = new HashSet<pap_product_type>();
            pap_suppressed_product_type = new HashSet<pap_suppressed_product_type>();
            pap_transaction = new HashSet<pap_transaction>();
        }

        [Key]
        public int application_id { get; set; }

        [StringLength(50)]
        public string description { get; set; }

        public DateTime? create_date { get; set; }

        public virtual ICollection<pap_client_type> pap_client_type { get; set; }

        public virtual ICollection<pap_product_type> pap_product_type { get; set; }

        public virtual ICollection<pap_suppressed_product_type> pap_suppressed_product_type { get; set; }

        public virtual ICollection<pap_transaction> pap_transaction { get; set; }
    }
}
