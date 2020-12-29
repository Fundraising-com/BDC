namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class crm_users
    {
        [StringLength(10)]
        public string consultant_ID { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string user_name { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string password { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int access_level { get; set; }
    }
}
