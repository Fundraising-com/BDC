namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Advertisment_Type
    {
        public Advertisment_Type()
        {
            Advertisers = new HashSet<Advertiser>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Advertisment_Type_ID { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public virtual ICollection<Advertiser> Advertisers { get; set; }
    }
}
