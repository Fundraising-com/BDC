namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("advertisement")]
    public partial class advertisement
    {
        public advertisement()
        {
            C_tbd_promotion = new HashSet<C_tbd_promotion>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int advertisement_id { get; set; }

        public byte division_id { get; set; }

        [Required]
        [StringLength(50)]
        public string description { get; set; }

        public double? size { get; set; }

        public int? nb_colors { get; set; }

        [StringLength(255)]
        public string comments { get; set; }

        public virtual ICollection<C_tbd_promotion> C_tbd_promotion { get; set; }

        public virtual division division { get; set; }
    }
}
