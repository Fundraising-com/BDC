namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class mke_replication_test
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int test { get; set; }

        [Required]
        [StringLength(50)]
        public string test_value { get; set; }
    }
}
