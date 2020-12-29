namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class phone_number_tracking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int phone_number_tracking_id { get; set; }

        [StringLength(50)]
        public string phone_number_tracking_desc { get; set; }
    }
}
