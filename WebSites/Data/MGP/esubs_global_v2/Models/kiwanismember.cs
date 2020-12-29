namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("kiwanismember")]
    public partial class kiwanismember
    {
        [Key]
        public int member_id { get; set; }

        [Required]
        [StringLength(5)]
        public string culture_code { get; set; }

        public int opt_status_id { get; set; }

        [Required]
        [StringLength(100)]
        public string first_name { get; set; }

        [StringLength(100)]
        public string middle_name { get; set; }

        [Required]
        [StringLength(100)]
        public string last_name { get; set; }

        [StringLength(1)]
        public string gender { get; set; }

        [Required]
        [StringLength(100)]
        public string email_address { get; set; }

        [StringLength(100)]
        public string password { get; set; }

        public bool bounced { get; set; }

        [StringLength(20)]
        public string external_member_id { get; set; }

        [StringLength(1024)]
        public string comments { get; set; }

        public DateTime create_date { get; set; }

        [StringLength(100)]
        public string parent_first_name { get; set; }

        [StringLength(100)]
        public string parent_last_name { get; set; }

        public int? partner_id { get; set; }

        public int? lead_id { get; set; }

        public bool? unsubscribe { get; set; }

        public DateTime? unsubscribe_date { get; set; }

        public int? facebook_id { get; set; }
    }
}
