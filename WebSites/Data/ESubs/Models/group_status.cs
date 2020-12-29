namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class group_status
    {
        public group_status()
        {
            group_group_status = new HashSet<group_group_status>();
        }

        [Key]
        public int group_status_id { get; set; }

        [Required]
        [StringLength(50)]
        public string description { get; set; }

        public virtual ICollection<group_group_status> group_group_status { get; set; }
    }
}
