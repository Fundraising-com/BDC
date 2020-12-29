namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MD08
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [StringLength(255)]
        public string Chapter { get; set; }

        [Column("Last Name")]
        [StringLength(255)]
        public string Last_Name { get; set; }

        [Column("First Name")]
        [StringLength(255)]
        public string First_Name { get; set; }

        [StringLength(255)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(255)]
        public string City { get; set; }

        [StringLength(255)]
        public string State { get; set; }

        [Column("Zip code")]
        [StringLength(255)]
        public string Zip_code { get; set; }

        [Column("Email address")]
        [StringLength(255)]
        public string Email_address { get; set; }

        public int? group_id { get; set; }

        [Key]
        [Column(Order = 1)]
        public bool done { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int error { get; set; }
    }
}
