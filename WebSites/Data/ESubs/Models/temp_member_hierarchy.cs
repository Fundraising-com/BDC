namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class temp_member_hierarchy
    {
        [Key]
        public int member_hierarchy_id { get; set; }

        public int? parent_member_hierarchy_id { get; set; }

        public int? member_id { get; set; }

        public int? organization_id { get; set; }

        public int? participant_id { get; set; }

        public int? supporter_id { get; set; }
    }
}
