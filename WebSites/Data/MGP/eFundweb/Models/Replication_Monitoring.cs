namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Replication_Monitoring
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Replication_ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Msg { get; set; }
    }
}
