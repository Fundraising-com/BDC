namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("story")]
    public partial class story
    {
        [Key]
        public int story_id { get; set; }

        public int story_type_id { get; set; }

        public int group_type_id { get; set; }

        [Column(TypeName = "text")]
        public string story_text { get; set; }
    }
}
