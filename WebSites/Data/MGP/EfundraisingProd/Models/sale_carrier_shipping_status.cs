namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sale_carrier_shipping_status
    {
        [Key]
        [Column(Order = 0)]
        public byte carrier_shipping_status_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int sales_id { get; set; }

        public DateTime? status_entry_date { get; set; }

        public virtual carrier_shipping_status carrier_shipping_status { get; set; }

        public virtual sale sale { get; set; }
    }
}
