namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Promotion_Group
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Promo_Group_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }
    }
}
