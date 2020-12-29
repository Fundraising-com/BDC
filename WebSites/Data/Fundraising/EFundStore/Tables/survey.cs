namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("survey")]
    public partial class survey
    {
        public survey()
        {
            user_vote = new HashSet<user_vote>();
            choices = new HashSet<choice>();
        }

        [Key]
        public int survey_id { get; set; }

        [Required]
        [StringLength(40)]
        public string page_name { get; set; }

        public short display { get; set; }

        public virtual ICollection<user_vote> user_vote { get; set; }

        public virtual ICollection<choice> choices { get; set; }
    }
}
