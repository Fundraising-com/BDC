namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("member")]
    public partial class member
    {
        public member()
        {
            member_hierarchy = new HashSet<member_hierarchy>();
            member_phone_number = new HashSet<member_phone_number>();
            member_postal_address = new HashSet<member_postal_address>();
        }

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

        public int? user_id { get; set; }

        public bool deleted { get; set; }

        [StringLength(100)]
        public string greeting { get; set; }

        public bool? email_validated { get; set; }

        public int? email_validation_response_code { get; set; }

        [StringLength(200)]
        public string email_validation_response_message { get; set; }

        public virtual ICollection<member_hierarchy> member_hierarchy { get; set; }

        public virtual ICollection<member_phone_number> member_phone_number { get; set; }

        public virtual ICollection<member_postal_address> member_postal_address { get; set; }
    }
}
