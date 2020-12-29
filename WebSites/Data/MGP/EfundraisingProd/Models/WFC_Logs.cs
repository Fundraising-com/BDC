namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WFC_Logs
    {
        public int id { get; set; }

        [StringLength(10)]
        public string customer_number { get; set; }

        [StringLength(10)]
        public string order_number { get; set; }

        public int? lead_id { get; set; }

        [StringLength(10)]
        public string sale_id { get; set; }

        public DateTime date { get; set; }

        [StringLength(255)]
        public string product_code { get; set; }

        [StringLength(10)]
        public string freight { get; set; }

        [StringLength(500)]
        public string result_message { get; set; }

        [StringLength(255)]
        public string address { get; set; }
    }
}
