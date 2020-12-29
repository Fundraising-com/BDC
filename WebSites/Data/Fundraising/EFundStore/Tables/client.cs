namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("client")]
    public partial class client
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(2)]
        public string client_sequence_code { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int client_id { get; set; }

        [Required]
        [StringLength(4)]
        public string organization_class_code { get; set; }

        public int? group_type_id { get; set; }

        [Required]
        [StringLength(4)]
        public string channel_code { get; set; }

        public int promotion_id { get; set; }

        public int? lead_id { get; set; }

        public int division_id { get; set; }

        public int? csr_consultant_id { get; set; }

        public int? title_id { get; set; }

        [StringLength(10)]
        public string salutation { get; set; }

        [Required]
        [StringLength(50)]
        public string first_name { get; set; }

        [Required]
        [StringLength(50)]
        public string last_name { get; set; }

        [StringLength(50)]
        public string title { get; set; }

        [StringLength(100)]
        public string organization { get; set; }

        [StringLength(20)]
        public string day_phone { get; set; }

        [StringLength(45)]
        public string day_time_call { get; set; }

        [StringLength(20)]
        public string evening_phone { get; set; }

        [StringLength(20)]
        public string evening_time_call { get; set; }

        [StringLength(20)]
        public string fax { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        [Column(TypeName = "text")]
        public string extra_comment { get; set; }

        public bool interested_in_agent { get; set; }

        public bool interested_in_online { get; set; }

        [StringLength(10)]
        public string day_phone_ext { get; set; }

        [StringLength(10)]
        public string evening_phone_ext { get; set; }

        [StringLength(20)]
        public string other_phone { get; set; }

        [StringLength(10)]
        public string other_phone_ext { get; set; }
    }
}
