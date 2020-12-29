namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Kiwani
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [StringLength(255)]
        public string MbrLast { get; set; }

        [StringLength(255)]
        public string MbrFirst { get; set; }

        [StringLength(255)]
        public string Division { get; set; }

        [Column("Member ID")]
        [StringLength(255)]
        public string Member_ID { get; set; }

        [Column("Club Name")]
        [StringLength(255)]
        public string Club_Name { get; set; }

        [StringLength(255)]
        public string OffcHeld { get; set; }

        [StringLength(255)]
        public string Password { get; set; }

        [Column("Re-enter Password")]
        [StringLength(255)]
        public string Re_enter_Password { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(255)]
        public string City { get; set; }

        [StringLength(255)]
        public string State { get; set; }

        [Column("Zip Code")]
        [StringLength(255)]
        public string Zip_Code { get; set; }

        [StringLength(255)]
        public string Country { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [Key]
        [Column(Order = 1)]
        public bool done { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int error { get; set; }
    }
}
