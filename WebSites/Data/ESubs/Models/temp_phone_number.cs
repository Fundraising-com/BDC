namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class temp_phone_number
    {
        [Key]
        [Column(Order = 0)]
        public int phone_number_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int member_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int phone_number_type_id { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string phone_number { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime create_date { get; set; }
    }
}
