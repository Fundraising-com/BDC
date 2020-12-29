namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ext_sales_status
    {
        public Ext_sales_status()
        {
            sales = new HashSet<sale>();
        }

        [Key]
        public int Ext_sales_status_id { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public virtual ICollection<sale> sales { get; set; }
    }
}
