namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Alias_State
    {
        [Key]
        [StringLength(255)]
        public string Input_State_Code { get; set; }

        [Required]
        [StringLength(10)]
        public string State_Code { get; set; }

        public virtual State State { get; set; }
    }
}
