namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Group_Type
    {
        public Group_Type()
        {
            Targeted_Market_Type = new HashSet<Targeted_Market_Type>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Group_Type_ID { get; set; }

        public int? Party_Type_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Description_Fr { get; set; }

        public virtual ICollection<Targeted_Market_Type> Targeted_Market_Type { get; set; }
    }
}
