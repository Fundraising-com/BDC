namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EFO_Group_Type
    {
        [Key]
        public int Group_Type_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }
    }
}
