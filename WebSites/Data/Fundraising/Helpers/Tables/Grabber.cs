namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Grabber")]
    public partial class Grabber
    {
        [Key]
        public int Grabber_Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Grabber_Desc { get; set; }
    }
}
