namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Priority")]
    public partial class Priority
    {
        public Priority()
        {
            Comments = new HashSet<Comment>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Priority_ID { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public int? Color_Code { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
