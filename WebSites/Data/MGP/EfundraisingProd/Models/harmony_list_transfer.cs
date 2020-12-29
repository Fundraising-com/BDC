namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class harmony_list_transfer
    {
        public int id { get; set; }

        [StringLength(100)]
        public string list_name { get; set; }

        [StringLength(100)]
        public string list_desc { get; set; }
    }
}
