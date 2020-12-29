namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class prize_to_be_removed
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int prize_item_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int prize_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(40)]
        public string prize_item_code { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime expiration_date { get; set; }

        public DateTime? create_date { get; set; }
    }
}
