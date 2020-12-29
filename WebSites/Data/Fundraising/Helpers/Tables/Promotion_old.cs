namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Promotion_old
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Promotion_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(4)]
        public string Promotion_Type_Code { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Visibility { get; set; }

        [StringLength(100)]
        public string Contact_Name { get; set; }

        [StringLength(35)]
        public string Tracking_Serial { get; set; }

        public int? Nb_Impression_Bought { get; set; }

        [Key]
        [Column(Order = 3)]
        public bool Is_Active { get; set; }

        public int? Targeted_Market_ID { get; set; }

        public int? Advertising_Support_ID { get; set; }

        public int? Advertisement_Id { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Partner_ID { get; set; }

        [StringLength(255)]
        public string Cookie_Content { get; set; }

        public int? Grabber_Id { get; set; }

        public bool? Is_Predictive { get; set; }

        public int? Advertiser_ID { get; set; }

        [StringLength(255)]
        public string Keyword { get; set; }

        [StringLength(100)]
        public string Script_Name { get; set; }

        public int? Advertisment_Type_ID { get; set; }

        public int? Destination_ID { get; set; }

        public int? Advertiser_Partner_ID { get; set; }
    }
}
