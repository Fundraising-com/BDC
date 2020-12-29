namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class audit_member_hierarchy
    {
        [Key]
        public int audit_id { get; set; }

        public DateTime audit_date { get; set; }

        [StringLength(200)]
        public string audit_user_name { get; set; }

        [Required]
        [StringLength(1)]
        public string audit_opcode { get; set; }

        [Required]
        [StringLength(200)]
        public string audit_modifier { get; set; }

        public int member_hierarchy_id { get; set; }

        public int? parent_member_hierarchy_id { get; set; }

        public int member_id { get; set; }

        public int? creation_channel_id { get; set; }

        public DateTime create_date { get; set; }

        public bool active { get; set; }

        public bool unsubscribe { get; set; }

        public DateTime? unsubscribe_date { get; set; }
    }
}
