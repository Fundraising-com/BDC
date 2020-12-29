namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Advertiser_Partner
    {
        public Advertiser_Partner()
        {
            C_tbd_promotion = new HashSet<C_tbd_promotion>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Advertiser_Partner_ID { get; set; }

        public int? Advertiser_ID { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public virtual ICollection<C_tbd_promotion> C_tbd_promotion { get; set; }

        public virtual Advertiser Advertiser { get; set; }
    }
}
