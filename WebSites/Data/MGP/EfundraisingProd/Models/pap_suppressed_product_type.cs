namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pap_suppressed_product_type
    {
        [Key]
        public int pap_suppressed_product_type_id { get; set; }

        public int? application_id { get; set; }

        [StringLength(50)]
        public string ext_product_type_id { get; set; }

        public DateTime? create_date { get; set; }

        public virtual application application { get; set; }
    }
}
