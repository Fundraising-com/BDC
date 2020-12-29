namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WFC_Import_Payments
    {
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string Invoice_number { get; set; }

        public bool ToBeCorrected { get; set; }
    }
}
