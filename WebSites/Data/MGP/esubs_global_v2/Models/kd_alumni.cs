namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class kd_alumni
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [StringLength(255)]
        public string Chapter { get; set; }

        [Column("Alumni Name")]
        [StringLength(255)]
        public string Alumni_Name { get; set; }

        [StringLength(255)]
        public string redirect { get; set; }

        [Column("EMAIL ID")]
        [StringLength(255)]
        public string EMAIL_ID { get; set; }

        [StringLength(255)]
        public string PW { get; set; }

        [StringLength(255)]
        public string City { get; set; }

        [StringLength(255)]
        public string State { get; set; }

        [StringLength(255)]
        public string Zip { get; set; }

        public bool done { get; set; }
    }
}
