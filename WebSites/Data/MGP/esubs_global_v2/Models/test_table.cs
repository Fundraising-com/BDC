namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class test_table
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int test_int { get; set; }

        [StringLength(50)]
        public string test_varchar { get; set; }
    }
}
