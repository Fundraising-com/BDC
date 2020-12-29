namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class unused_account_number
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int account_number { get; set; }
    }
}
