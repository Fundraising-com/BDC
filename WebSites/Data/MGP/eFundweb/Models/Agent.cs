namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Agent")]
    public partial class Agent
    {
        [Key]
        public int Agent_ID { get; set; }

        [StringLength(40)]
        public string URL { get; set; }

        [StringLength(50)]
        public string Company { get; set; }

        [StringLength(40)]
        public string Agent_Name { get; set; }

        [StringLength(70)]
        public string Logo { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] Add_Timestamp { get; set; }

        [StringLength(10)]
        public string Add_By_User { get; set; }
    }
}
