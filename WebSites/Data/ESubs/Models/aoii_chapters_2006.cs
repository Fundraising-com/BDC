namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class aoii_chapters_2006
    {
        [StringLength(255)]
        public string chapter { get; set; }

        [Key]
        [Column(Order = 0)]
        public byte done { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
    }
}
