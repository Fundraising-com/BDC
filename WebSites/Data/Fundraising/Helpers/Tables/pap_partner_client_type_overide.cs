namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pap_partner_client_type_overide
    {
        [Key]
        [Column(Order = 0)]
        public int id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int pap_product_category_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int pap_client_type_id { get; set; }

        public virtual pap_client_type pap_client_type { get; set; }

        public virtual pap_product_category pap_product_category { get; set; }
    }
}
