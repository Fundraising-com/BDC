namespace GA.BDC.Data.MGP.EfundraisingProd.Models
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
        public int promotion_id { get; set; }

        [Required]
        [StringLength(4)]
        public string promotion_type_code { get; set; }

        public int? targeted_market_id { get; set; }

        public int? advertising_support_id { get; set; }

        public int? advertisement_id { get; set; }

        public int partner_id { get; set; }

        public int? advertiser_id { get; set; }

        public byte transfer_status_id { get; set; }

        public int? advertisment_type_id { get; set; }

        public int? destination_id { get; set; }

        public int? advertiser_partner_id { get; set; }

        public int? grabber_id { get; set; }

        [Required]
        [StringLength(100)]
        public string description { get; set; }

        [StringLength(100)]
        public string script_name { get; set; }

        [StringLength(100)]
        public string contact_name { get; set; }

        [StringLength(50)]
        public string visibility { get; set; }

        [StringLength(35)]
        public string tracking_serial { get; set; }

        public int? nb_impression_bought { get; set; }

        public bool is_active { get; set; }

        [StringLength(255)]
        public string cookie_content { get; set; }

        public bool? is_predictive { get; set; }

        [StringLength(255)]
        public string keyword { get; set; }

        public bool? is_displayable { get; set; }

        public virtual C_tbd_partner C_tbd_partner { get; set; }

        public virtual advertisement advertisement { get; set; }

        public virtual Advertiser Advertiser { get; set; }

        public virtual Advertiser_Partner Advertiser_Partner { get; set; }

        public virtual Advertising_Support Advertising_Support { get; set; }

        public virtual Advertisment_Type Advertisment_Type { get; set; }

        public virtual Destination Destination { get; set; }

        public virtual Grabber Grabber { get; set; }

        public virtual C_tbd_promotion_type C_tbd_promotion_type { get; set; }

        public virtual Targeted_Market Targeted_Market { get; set; }

        public virtual transfer_status transfer_status { get; set; }
    }
}
