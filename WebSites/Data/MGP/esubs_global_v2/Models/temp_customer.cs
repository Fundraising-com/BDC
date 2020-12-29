namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class temp_customer
    {
        [StringLength(50)]
        public string lead_email { get; set; }

        public int? partner_id { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int organization_id { get; set; }

        public int? lead_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int organizer_id { get; set; }

        public byte? org_creation_channel { get; set; }

        [StringLength(75)]
        public string org_email { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string org_name { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int campaign_id { get; set; }

        [StringLength(1000)]
        public string group_name { get; set; }

        public int? campaign_type_id { get; set; }

        public int? season_id { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int participant_id { get; set; }

        public byte? part_creation_channel { get; set; }

        [StringLength(50)]
        public string part_email { get; set; }

        public int? identification { get; set; }

        [StringLength(75)]
        public string part_name { get; set; }

        [Key]
        [Column(Order = 5)]
        public bool part_default { get; set; }

        public int? supporter_id { get; set; }

        public byte? supp_creation_channel { get; set; }

        [StringLength(75)]
        public string supp_email { get; set; }

        [StringLength(75)]
        public string supp_name { get; set; }

        public int? supp_identification { get; set; }

        public bool? supp_default { get; set; }

        public int? customerid { get; set; }
    }
}
