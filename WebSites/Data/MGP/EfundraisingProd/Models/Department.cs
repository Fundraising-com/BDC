namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Department")]
    public partial class Department
    {
        public Department()
        {
            Comments = new HashSet<Comment>();
            consultants = new HashSet<consultant>();
            General_Comment = new HashSet<General_Comment>();
        }

        [Key]
        public int Department_Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Department_name { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<consultant> consultants { get; set; }

        public virtual ICollection<General_Comment> General_Comment { get; set; }
    }
}
