namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Title")]
    public partial class Title
    {
        public Title()
        {
            Title_Desc1 = new HashSet<Title_Desc>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Title_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Title_Desc { get; set; }

        public int Party_Type_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Title_Desc_Fr { get; set; }

        public virtual ICollection<Title_Desc> Title_Desc1 { get; set; }
    }
}
