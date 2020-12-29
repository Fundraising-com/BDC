namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class transfer_status
    {
        public transfer_status()
        {
            C_tbd_promotion = new HashSet<C_tbd_promotion>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte transfer_status_id { get; set; }

        [Required]
        [StringLength(25)]
        public string transfer_status_desc { get; set; }

        public virtual ICollection<C_tbd_promotion> C_tbd_promotion { get; set; }
    }
}
