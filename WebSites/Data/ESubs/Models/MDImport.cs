namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MDImport")]
    public partial class MDImport
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [StringLength(255)]
        public string Chapter_name { get; set; }

        [StringLength(255)]
        public string Chapter { get; set; }

        [StringLength(255)]
        public string Last_Name { get; set; }

        [StringLength(255)]
        public string First_Name { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [Key]
        [Column(Order = 1)]
        public bool done { get; set; }
    }
}
