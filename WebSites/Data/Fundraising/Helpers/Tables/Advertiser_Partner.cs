namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Advertiser_Partner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Advertiser_Partner_ID { get; set; }

        public int? Advertiser_ID { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public virtual Advertiser Advertiser { get; set; }
    }
}
