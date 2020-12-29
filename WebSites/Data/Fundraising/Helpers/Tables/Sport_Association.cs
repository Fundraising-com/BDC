namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sport_Association
    {
        [Key]
        public int Sport_Association_Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Sport_Ass_Desc { get; set; }
    }
}
