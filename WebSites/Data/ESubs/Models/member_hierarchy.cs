namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class member_hierarchy
    {
        public member_hierarchy()
        {
            event_participation = new HashSet<event_participation>();
            groups = new HashSet<group>();
            member_hierarchy1 = new HashSet<member_hierarchy>();
            touches = new HashSet<touch>();
        }

        [Key]
        public int member_hierarchy_id { get; set; }

        public int? parent_member_hierarchy_id { get; set; }

        public int member_id { get; set; }

        public int? creation_channel_id { get; set; }

        public DateTime create_date { get; set; }

        public bool active { get; set; }

        public bool unsubscribe { get; set; }

        public DateTime? unsubscribe_date { get; set; }

        public virtual creation_channel creation_channel { get; set; }

        public virtual ICollection<event_participation> event_participation { get; set; }

        public virtual ICollection<group> groups { get; set; }

        public virtual member member { get; set; }

        public virtual ICollection<member_hierarchy> member_hierarchy1 { get; set; }

        public virtual member_hierarchy member_hierarchy2 { get; set; }

        public virtual ICollection<touch> touches { get; set; }
    }
}
