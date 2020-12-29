namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("kiwanisgroup")]
    public partial class kiwanisgroup
    {
        public int group_id { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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
