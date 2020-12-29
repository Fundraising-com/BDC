namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class temp_member_bak
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int member_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string email { get; set; }

        [StringLength(100)]
        public string name { get; set; }
    }
}
