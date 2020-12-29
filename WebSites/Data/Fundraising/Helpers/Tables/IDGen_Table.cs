namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IDGen_Table
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string Context { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(30)]
        public string ID_Name { get; set; }

        public int? Last_Value { get; set; }

        [StringLength(50)]
        public string Comment { get; set; }
    }
}
