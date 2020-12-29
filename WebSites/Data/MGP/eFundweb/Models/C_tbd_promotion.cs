namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("_tbd_promotion")]
    public partial class C_tbd_promotion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Promotion_ID { get; set; }

        [StringLength(4)]
        public string Promotion_Type_Code { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Visibility { get; set; }

        [StringLength(100)]
        public string Contact_Name { get; set; }

        [StringLength(35)]
        public string Tracking_Serial { get; set; }

        public int? Nb_Impression_Bought { get; set; }

        public bool Is_Active { get; set; }

        public int? Targeted_Market_ID { get; set; }

        public int? Advertising_Support_ID { get; set; }

        public int? Advertisement_ID { get; set; }

        public int Partner_ID { get; set; }

        [StringLength(255)]
        public string Cookie_Content { get; set; }

        public int? Grabber_ID { get; set; }

        public bool Is_Predictive { get; set; }

        public int? Advertiser_ID { get; set; }

        [StringLength(255)]
        public string Keyword { get; set; }

        [StringLength(100)]
        public string Script_Name { get; set; }

        public int? Advertisment_Type_ID { get; set; }

        public int? Destination_ID { get; set; }

        public int? Advertiser_Partner_ID { get; set; }

        public bool Is_Displayable { get; set; }

        public virtual C_tbd_partner C_tbd_partner { get; set; }

        public virtual Advertisement Advertisement { get; set; }

        public virtual Advertiser Advertiser { get; set; }

        public virtual Advertiser_Partner Advertiser_Partner { get; set; }

        public virtual Advertising_Support Advertising_Support { get; set; }

        public virtual Advertisment_Type Advertisment_Type { get; set; }

        public virtual Destination Destination { get; set; }

        public virtual Grabber Grabber { get; set; }

        public virtual C_tbd_promotion_type C_tbd_promotion_type { get; set; }

        public virtual Targeted_Market Targeted_Market { get; set; }
    }
}
