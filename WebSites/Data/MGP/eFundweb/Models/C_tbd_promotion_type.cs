namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("_tbd_promotion_type")]
    public partial class C_tbd_promotion_type
    {
        public C_tbd_promotion_type()
        {
            C_tbd_promotion = new HashSet<C_tbd_promotion>();
        }

        [Key]
        [StringLength(4)]
        public string Promotion_Type_Code { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Default_Commission_Rate { get; set; }

        public virtual ICollection<C_tbd_promotion> C_tbd_promotion { get; set; }
    }
}
