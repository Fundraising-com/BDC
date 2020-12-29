namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Input_Select
    {
        [Required]
        [StringLength(100)]
        public string Table_Source { get; set; }

        [Required]
        [StringLength(100)]
        public string Column_Desc { get; set; }

        [Required]
        [StringLength(100)]
        public string Column_ID { get; set; }

        [StringLength(200)]
        public string Clauses { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Questions_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Language_ID { get; set; }
    }
}
