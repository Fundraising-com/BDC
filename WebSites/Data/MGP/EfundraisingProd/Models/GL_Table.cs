namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GL_Table
    {
        [Key]
        [StringLength(20)]
        public string GL_Code { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [StringLength(20)]
        public string GL_Account_No { get; set; }

        [StringLength(1)]
        public string Debit_Credit { get; set; }
    }
}
