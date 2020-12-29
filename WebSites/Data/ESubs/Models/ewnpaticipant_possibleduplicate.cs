namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ewnpaticipant_possibleduplicate
    {
        [Column("Name Last")]
        [StringLength(800)]
        public string Name_Last { get; set; }

        [Column("Name First")]
        [StringLength(800)]
        public string Name_First { get; set; }

        [StringLength(800)]
        public string State { get; set; }

        [Column("Current Chapter City")]
        [StringLength(800)]
        public string Current_Chapter_City { get; set; }

        [Column("eMail Address")]
        [StringLength(800)]
        public string eMail_Address { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Key]
        [Column(Order = 1)]
        public bool done { get; set; }
    }
}
