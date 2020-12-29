namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ewnchapter")]
    public partial class ewnchapter
    {
        [StringLength(8000)]
        public string Chapter { get; set; }

        [Column("Last Name")]
        [StringLength(8000)]
        public string Last_Name { get; set; }

        [Column("First Name")]
        [StringLength(8000)]
        public string First_Name { get; set; }

        [Column("Email address")]
        [StringLength(8000)]
        public string Email_address { get; set; }

        [StringLength(8000)]
        public string city { get; set; }

        [StringLength(50)]
        public string state { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
    }
}
