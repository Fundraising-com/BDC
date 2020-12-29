namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("launch")]
    public partial class launch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int launch_ID { get; set; }

        [Required]
        [StringLength(255)]
        public string redirect_to { get; set; }
    }
}
