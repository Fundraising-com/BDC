namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Images_ID { get; set; }

        public int Template_ID { get; set; }

        [Required]
        [StringLength(120)]
        public string Images_Path { get; set; }

        public virtual Template Template { get; set; }
    }
}
