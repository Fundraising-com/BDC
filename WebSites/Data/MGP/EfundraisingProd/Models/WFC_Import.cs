namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WFC_Import
    {
        public int id { get; set; }

        [StringLength(50)]
        public string Order_Number { get; set; }

        public int? Sales_ID { get; set; }

        public bool ToBeCorrected { get; set; }
    }
}
