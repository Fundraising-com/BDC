namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Credit_Approval_Method
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Credit_Approval_Method_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }
    }
}
