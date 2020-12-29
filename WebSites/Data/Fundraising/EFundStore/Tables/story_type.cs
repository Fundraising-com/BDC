namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class story_type
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int story_type_id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }
    }
}
