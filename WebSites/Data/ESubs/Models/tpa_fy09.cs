namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tpa_fy09
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Column("Chapter Name")]
        [StringLength(255)]
        public string Chapter_Name { get; set; }

        [StringLength(255)]
        public string State { get; set; }

        [Column("First Name")]
        [StringLength(255)]
        public string First_Name { get; set; }

        [Column("Last Name")]
        [StringLength(255)]
        public string Last_Name { get; set; }

        [Column("Phone Number")]
        [StringLength(255)]
        public string Phone_Number { get; set; }

        [Column("Email Address")]
        [StringLength(255)]
        public string Email_Address { get; set; }

        [StringLength(255)]
        public string Password { get; set; }

        [Key]
        [Column(Order = 1)]
        public bool done { get; set; }

        [Key]
        [Column(Order = 2)]
        public bool error { get; set; }
    }
}
