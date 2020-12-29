namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class pap_scratchbook_campaign
    {
        public int id { get; set; }

        public int scratch_book_id { get; set; }

        public int pap_product_category_id { get; set; }

        public virtual pap_product_category pap_product_category { get; set; }

        public virtual scratch_book scratch_book { get; set; }
    }
}
