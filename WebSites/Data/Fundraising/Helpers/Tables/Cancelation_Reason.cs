namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cancelation_Reason
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Cancelation_Reason_Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }
    }
}
