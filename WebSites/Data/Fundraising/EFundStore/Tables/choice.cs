namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("choice")]
    public partial class choice
    {
        public choice()
        {
            user_vote = new HashSet<user_vote>();
            surveys = new HashSet<survey>();
        }

        [Key]
        public int choice_id { get; set; }

        [Required]
        [StringLength(100)]
        public string choice_desc { get; set; }

        [StringLength(50)]
        public string location { get; set; }

        [StringLength(100)]
        public string image { get; set; }

        public virtual ICollection<user_vote> user_vote { get; set; }

        public virtual ICollection<survey> surveys { get; set; }
    }
}
