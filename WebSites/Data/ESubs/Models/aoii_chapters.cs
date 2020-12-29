namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class aoii_chapters
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Column("Group Name")]
        [StringLength(255)]
        public string Group_Name { get; set; }

        [StringLength(255)]
        public string F2 { get; set; }

        [Column("Email/ID")]
        [StringLength(255)]
        public string Email_ID { get; set; }

        [StringLength(255)]
        public string Password { get; set; }

        [Key]
        [Column(Order = 1)]
        public bool done { get; set; }
    }
}
