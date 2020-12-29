namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Production_Status
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Production_Status_ID { get; set; }

        [StringLength(50)]
        public string Description { get; set; }
    }
}
