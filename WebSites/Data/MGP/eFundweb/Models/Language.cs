namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Language")]
    public partial class Language
    {
        public Language()
        {
            Category_Desc = new HashSet<Category_Desc>();
            Package_Desc = new HashSet<Package_Desc>();
            Product_Desc = new HashSet<Product_Desc>();
            Questions_Desc = new HashSet<Questions_Desc>();
            Templates = new HashSet<Template>();
            Title_Desc = new HashSet<Title_Desc>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Language_ID { get; set; }

        [Column("Language")]
        [Required]
        [StringLength(60)]
        public string Language1 { get; set; }

        [StringLength(3)]
        public string Language_Path { get; set; }

        public virtual ICollection<Category_Desc> Category_Desc { get; set; }

        public virtual ICollection<Package_Desc> Package_Desc { get; set; }

        public virtual ICollection<Product_Desc> Product_Desc { get; set; }

        public virtual ICollection<Questions_Desc> Questions_Desc { get; set; }

        public virtual ICollection<Template> Templates { get; set; }

        public virtual ICollection<Title_Desc> Title_Desc { get; set; }
    }
}
