namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class user_vote
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string session_id { get; set; }

        public int choice_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int survey_id { get; set; }

        public virtual choice choice { get; set; }

        public virtual survey survey { get; set; }
    }
}
