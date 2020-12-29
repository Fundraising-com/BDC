namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("kiwanischange")]
    public partial class kiwanischange
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int group_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int member_Hierarchy_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int member_id { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(5)]
        public string culture_code { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int opt_status_id { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(100)]
        public string first_name { get; set; }

        [StringLength(100)]
        public string middle_name { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(100)]
        public string last_name { get; set; }

        [StringLength(1)]
        public string gender { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(100)]
        public string email_address { get; set; }

        [StringLength(100)]
        public string password { get; set; }

        [Key]
        [Column(Order = 8)]
        public bool bounced { get; set; }

        [StringLength(20)]
        public string external_member_id { get; set; }

        [StringLength(1024)]
        public string comments { get; set; }

        [Key]
        [Column(Order = 9)]
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
