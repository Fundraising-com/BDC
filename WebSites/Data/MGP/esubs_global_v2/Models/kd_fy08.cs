namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class kd_fy08
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public double? EventID { get; set; }

        [StringLength(255)]
        public string Chapter { get; set; }

        [StringLength(255)]
        public string State { get; set; }

        [StringLength(255)]
        public string Sponsor { get; set; }

        [StringLength(255)]
        public string Action { get; set; }

        public double? done { get; set; }
    }
}
