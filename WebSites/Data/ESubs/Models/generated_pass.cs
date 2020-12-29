namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class generated_pass
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int member_id { get; set; }

        [StringLength(6)]
        public string password { get; set; }

        [Key]
        [Column(Order = 1)]
        public int id { get; set; }
    }
}
