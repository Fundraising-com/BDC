namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
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
            Advertiser_Partner = new HashSet<Advertiser_Partner>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Advertiser_ID { get; set; }

        public int? Advertisment_Type_ID { get; set; }

        public int? Contact_ID { get; set; }

        [StringLength(200)]
        public string Advertiser_Name { get; set; }

        public virtual Advertisment_Type Advertisment_Type { get; set; }

        public virtual Contact Contact { get; set; }

        public virtual ICollection<Advertiser_Partner> Advertiser_Partner { get; set; }
    }
}
