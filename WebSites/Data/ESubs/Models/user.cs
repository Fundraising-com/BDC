namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class user
    {
        [Key]
        public int user_id { get; set; }

        [Required]
        [StringLength(5)]
        public string culture_code { get; set; }

        [Required]
        [StringLength(100)]
        public string first_name { get; set; }

        [Required]
        [StringLength(100)]
        public string last_name { get; set; }

        [Required]
        [StringLength(100)]
        public string email_address { get; set; }

        [Required]
        [StringLength(100)]
        public string username { get; set; }

        [Required]
        [StringLength(100)]
        public string password { get; set; }

        public int? partner_id { get; set; }

        public DateTime create_date { get; set; }

        public int? member_id { get; set; }

        public int? coppa_month { get; set; }

        public int? coppa_year { get; set; }

        public bool? agree_term_services { get; set; }

        public bool? unsubscribe { get; set; }

        public DateTime? unsubscribe_date { get; set; }

        public bool opt_status_id { get; set; }
    }
}
