namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Advertiser")]
    public partial class Advertiser
    {
        public Advertiser()
        {
            C_tbd_promotion = new HashSet<C_tbd_promotion>();
            Advertiser_Partner = new HashSet<Advertiser_Partner>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Advertiser_ID { get; set; }

        public int? Advertisment_Type_ID { get; set; }

        public int? Contact_ID { get; set; }

        [StringLength(200)]
        public string Advertiser_Name { get; set; }

        public virtual ICollection<C_tbd_promotion> C_tbd_promotion { get; set; }

        public virtual Advertisment_Type Advertisment_Type { get; set; }

        public virtual Contact Contact { get; set; }

        public virtual ICollection<Advertiser_Partner> Advertiser_Partner { get; set; }
    }
}
