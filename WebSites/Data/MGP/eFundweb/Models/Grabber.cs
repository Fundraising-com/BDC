namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Grabber")]
    public partial class Grabber
    {
        public Grabber()
        {
            C_tbd_promotion = new HashSet<C_tbd_promotion>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Grabber_ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Grabber_Desc { get; set; }

        public virtual ICollection<C_tbd_promotion> C_tbd_promotion { get; set; }
    }
}
