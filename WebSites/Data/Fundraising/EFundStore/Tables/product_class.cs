namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class product_class
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int product_class_id { get; set; }

        public int division_id { get; set; }

        public byte? accounting_class_id { get; set; }

        [StringLength(50)]
        public string description { get; set; }

        public bool display { get; set; }

        public byte minimum_order_qty { get; set; }
    }
}
