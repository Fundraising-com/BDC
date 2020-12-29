namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class xfactor_member
    {
        [Key]
        [Column(Order = 0)]
        public string external_member_id { get; set; }

        [Key]
        [Column(Order = 1)]
        public string external_group_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int partner_id { get; set; }

        [StringLength(100)]
        public string first_name { get; set; }

        [StringLength(100)]
        public string middle_name { get; set; }

        [StringLength(100)]
        public string last_name { get; set; }

        [StringLength(100)]
        public string email_address { get; set; }

        [StringLength(5)]
        public string culture_code { get; set; }

        public int? opt_status_id { get; set; }

        public int? creation_channel_id { get; set; }

        [StringLength(100)]
        public string password { get; set; }

        [StringLength(1024)]
        public string comments { get; set; }

        public DateTime? created_date { get; set; }

        public bool? deleted { get; set; }
    }
}
