namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Advertisement")]
    public partial class Advertisement
    {
        public Advertisement()
        {
            C_tbd_promotion = new HashSet<C_tbd_promotion>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Advertisement_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Size { get; set; }

        public int? Nb_Colors { get; set; }

        [StringLength(255)]
        public string Comments { get; set; }

        public virtual ICollection<C_tbd_promotion> C_tbd_promotion { get; set; }
    }
}
