namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Questions_Desc
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Language_ID { get; set; }

        [Required]
        [StringLength(2400)]
        public string Questions_Display { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Questions_ID { get; set; }

        [StringLength(600)]
        public string Error_Message { get; set; }

        public virtual Language Language { get; set; }

        public virtual Question Question { get; set; }
    }
}
