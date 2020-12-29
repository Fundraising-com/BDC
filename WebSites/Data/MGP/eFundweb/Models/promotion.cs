namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("promotion")]
    public partial class promotion
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int promotion_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string promotion_type_code { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(255)]
        public string description { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(1)]
        public string visibility { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(1)]
        public string contact_name { get; set; }

        public int? tracking_serial { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int nb_impression_bought { get; set; }

        [Key]
        [Column(Order = 6)]
        public bool is_active { get; set; }

        public int? targeted_market_id { get; set; }

        public int? advertising_support_id { get; set; }

        public int? advertisement_id { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int partner_id { get; set; }

        [StringLength(255)]
        public string cookie_content { get; set; }

        public int? grabber_id { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int is_predictive { get; set; }

        public int? advertiser_id { get; set; }

        [StringLength(255)]
        public string keyword { get; set; }

        [StringLength(255)]
        public string script_name { get; set; }

        public int? advertisment_type_id { get; set; }

        public int? destination_id { get; set; }

        public int? advertiser_partner_id { get; set; }

        public bool? is_displayable { get; set; }
    }
}
