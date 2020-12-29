namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("conciliation")]
    public partial class conciliation
    {
        [Key]
        public int conciliation_id { get; set; }

        public int sales_id { get; set; }

        public short sales_item_no { get; set; }

        public byte supplier_id { get; set; }

        public DateTime conciliate_date { get; set; }

        public bool is_conciliated { get; set; }

        public bool is_ordered { get; set; }

        [StringLength(25)]
        public string invoice_number { get; set; }
    }
}
