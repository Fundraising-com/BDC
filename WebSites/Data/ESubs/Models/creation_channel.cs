namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class creation_channel
    {
        public creation_channel()
        {
            member_hierarchy = new HashSet<member_hierarchy>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int creation_channel_id { get; set; }

        [Required]
        [StringLength(150)]
        public string creation_channel_name { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        public DateTime create_date { get; set; }

        public bool active { get; set; }

        public int member_type_id { get; set; }

        public bool? is_contact { get; set; }

        public virtual ICollection<member_hierarchy> member_hierarchy { get; set; }
    }
}
